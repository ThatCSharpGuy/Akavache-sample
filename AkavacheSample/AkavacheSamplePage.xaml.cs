using Xamarin.Forms;

namespace AkavacheSample
{
	public partial class AkavacheSamplePage : ContentPage
	{
		public AkavacheSamplePage()
		{
			Title = "Akavache samples";
			NavigationPage.SetBackButtonTitle(this, "");
			InitializeComponent();

			SimpleSamplesButton.Clicked += async (sender, args) =>
			{
				await Navigation.PushAsync(new SimplePage());
			};

			LoginSampleButton.Clicked += async (sender, args) =>
			{
				await Navigation.PushAsync(new LoginSamplePage());
			};

			BytesSamplesButton.Clicked += async (sender, args) =>
			{
				await Navigation.PushAsync(new BytesSamplesPage());
			};

			ComplexSamplesButton.Clicked += async (sender, args) =>
			{
				await Navigation.PushAsync(new ComplexSamplesPage());
			};
		}
	}
}

