using Xamarin.Forms;

namespace AkavacheSample
{
	public partial class AkavacheSamplePage : ContentPage
	{
		public AkavacheSamplePage()
		{
			InitializeComponent();

			SimpleSamplesButton.Clicked += async (sender, args) =>
			{
				await Navigation.PushAsync(new SimplePage());
			};
		}
	}
}

