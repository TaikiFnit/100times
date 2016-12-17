using System;

using Xamarin.Forms;

namespace pushit
{
	public class App : Application
	{
		public App()
		{
			int counter = 0;
			TimeSpan now = new TimeSpan(0, 0, 0, 0, 0);
			bool is_started = false;

			var button = new Button
			{
				TextColor = Color.White,
				Text = "Start",
				BorderColor = Color.White,
				BorderWidth = 1,
				FontAttributes = FontAttributes.Bold,
				Margin = new Thickness(20, 20, 20, 20)
			};

			var titleLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Push Button Quickly!!!",
				TextColor = Color.White,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Italic,
				Margin = new Thickness(20, 20, 20, 20)
			};

			var timeLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "00:00:00",
				TextColor = Color.White,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				Margin = new Thickness(20, 20, 20, 20)

			};

			var statusLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Push this button",
				TextColor = Color.White,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};

			double currentProgress = 0;

			var progress = new ProgressBar
			{
				Progress = currentProgress,
				Margin = new Thickness(20, 20, 20, 20),
			};

			// The root page of your application
			var content = new ContentPage
			{
				BackgroundColor = Color.Teal,
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.StartAndExpand,
					Children = {
						titleLabel,
						timeLabel,
						progress,
						statusLabel,
						button
					}
				}
			};

			// systemがevent handlerをsetしている可能性があるので += で handler を追加
			button.Clicked += (sender, e) =>
			{
				if (is_started == false)
				{
					button.Text = "Push!";
					currentProgress = 0;
					counter = 0;
					now = new TimeSpan(0, 0, 0, 0, 0);
					is_started = true;
					progress.ProgressTo(currentProgress, 1, Easing.Linear);
					statusLabel.Text = $"{counter}%";

					Device.StartTimer(TimeSpan.FromSeconds(0.1), () => {

						if (counter >= 100)
						{
							return false;
						}

						int day = now.Days;
						int hour = now.Hours;
						int minute = now.Minutes;
						int second = now.Seconds;
						int millisecond = now.Milliseconds + 1;

						if (millisecond >= 59)
						{
							second++;
							millisecond = 0;
						}
						if (second >= 59)
						{
							minute++;
							second = 0;
						}
						if (minute >= 59)
						{
							hour++;
							minute = 0;
						}
						if (hour >= 23)
						{
							day++;
							hour = 0;
						}

						now = new TimeSpan(day, hour, minute, second, millisecond);
						timeLabel.Text = now.Minutes.ToString().PadLeft(2, '0') + ":" + now.Seconds.ToString().PadLeft(2, '0') + ":" + now.Milliseconds.ToString().PadLeft(2, '0'); 

						return true;
					});

					return;
				}

				counter++;
				currentProgress += 0.01;
				statusLabel.Text = $"{counter}%";
				progress.ProgressTo(currentProgress, 250, Easing.Linear);

				if (counter >= 100)
				{
					statusLabel.Text = "Finish! Result time: " + timeLabel.Text;
					button.Text = "Retry";
					is_started = false;
					return;
				}
			};


			MainPage = new NavigationPage(content);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
