using System;
using System.Collections.Generic;
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
			Title = "Simple sample";

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
								  },
					              DateTimeOffset.Now.AddSeconds(15));
								  // Valido durante 15 segundos
			};


			var usernameLabel = new Label { Text = "Username: " };
			var fullNameLabel = new Label { Text = "Full name: " };
			var fetchButton = new Button { Text = "Get saved" };
			fetchButton.Clicked += async (sender, e) =>
			{
				await BlobCache.LocalMachine.Vacuum();

				try
				{
					var usuario = await BlobCache
									.LocalMachine
									.GetObject<Usuario>(SimpleUserKey);

					usernameLabel.Text = "Username: " + usuario.Username;
					fullNameLabel.Text = "Full name: " + usuario.FullName;
				}
				catch(KeyNotFoundException ex)
				{
					usernameLabel.Text = ex.Message;
				}
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


