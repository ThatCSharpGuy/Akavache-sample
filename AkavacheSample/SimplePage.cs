using System;
using System.Reactive.Linq;
using Akavache;
using Xamarin.Forms;

namespace AkavacheSample
{
	public class SimplePage : ContentPage
	{
		const string SimpleUserKey = "user_key";
		public SimplePage()
		{
			var usernameEntry = new Entry { Placeholder = "Username" };
			var fullNameEntry = new Entry { Placeholder = "Full name" };
			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (s, a) =>
			{
				await BlobCache
					.LocalMachine
					.InsertObject(SimpleUserKey,
								  new Usuario
								  {
									  Username = usernameEntry.Text,
									  FullName = fullNameEntry.Text
								  });
			};


			var usernameLabel = new Label { Text = "Username: " };
			var fullNameLabel = new Label { Text = "Full name: " };
			var fetchButton = new Button { Text = "Get saved" };
			fetchButton.Clicked += async (sender, e) =>
			{
				var usuario = await BlobCache
								.LocalMachine
								.GetObject<Usuario>(SimpleUserKey);

				usernameLabel.Text = "Username: " + usuario.Username;
				fullNameLabel.Text = "Full name: " + usuario.FullName;
			};

			Content = new StackLayout
			{
				Padding = 20,
				Children = {
					usernameEntry,
					fullNameEntry,
					saveButton,
					new Label { Text  = "Saved username", Style = Device.Styles.TitleStyle },
					usernameLabel,
					fullNameLabel,
					fetchButton
				}
			};
		}
	}
}


