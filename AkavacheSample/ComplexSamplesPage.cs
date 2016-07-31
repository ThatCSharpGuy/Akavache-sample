using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Xamarin.Forms;

namespace AkavacheSample
{
	public class ComplexSamplesPage : ContentPage
	{
		Label statusLabel;
		Label valueLabel;

		const string Key = "Date";

		public ComplexSamplesPage()
		{
			Title = "Complex sample";

			statusLabel = new Label { Text = "-", HorizontalTextAlignment = TextAlignment.Center };
			valueLabel = new Label {  
				HorizontalTextAlignment = TextAlignment.Center, 
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) 
			};
			SetValue(DateTime.Now);

			var getObjectButton = new Button { Text = "GetObject" };
			getObjectButton.Clicked += async (s, a) =>
			{
				try
				{
					statusLabel.Text = "Trabajando...";
					var dt = await BlobCache.LocalMachine
					                        .GetObject<DateTime>(Key);
					SetValue(dt);
				}
				catch 
				{
					SetValue(DateTime.MinValue);
				}
			};

			var getOrFetchObjectButton = new Button { Text = "GetOrFetchObject" };
			getOrFetchObjectButton.Clicked += async (s, a) =>
		   {
			   statusLabel.Text = "Trabajando...";
			   var dt = await BlobCache.LocalMachine
									   .GetOrFetchObject(Key,
														 () => GetDateTime());
			   SetValue(dt);
		   };

			var getAndFetchLatestButton = new Button { Text = "GetAndFetchLatest" };
			getAndFetchLatestButton.Clicked += (s, a) =>
		   {
			   statusLabel.Text = "Trabajando...";
				BlobCache.LocalMachine
				         .GetAndFetchLatest(Key,
				                            () => GetDateTime())
				         .Subscribe((obj) => 
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						SetValue(obj);
					});
				});
		   };

			var invalidateObjectButton = new Button { Text = "InvalidateObject" };
			invalidateObjectButton.Clicked += async (s, a) =>
			{
				statusLabel.Text = "Trabajando...";
				await BlobCache.LocalMachine
							   .InvalidateObject<DateTime>(Key);

				statusLabel.Text = "-";
			};
			
			Content = new StackLayout
			{
				Spacing = 20,
				Padding = 20,
				Children = {
					statusLabel,
					valueLabel,
					getObjectButton,
					getOrFetchObjectButton,
					getAndFetchLatestButton,
					invalidateObjectButton
				}
			};
		}

		async Task<DateTime> GetDateTime()
		{
			await Task.Delay(1000);
			return DateTime.Now;
		}

		void SetValue(DateTime dt)
		{
			if (dt != DateTime.MinValue)
			{
				valueLabel.Text = dt.ToString("HH:mm:ss");
			}
			else 
			{
				valueLabel.Text = "--:--:--";
			}
			statusLabel.Text = "-";
		}
	}
}


