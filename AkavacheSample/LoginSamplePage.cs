using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using Xamarin.Forms;

namespace AkavacheSample
{
	public class LoginSamplePage : ContentPage
	{
		Entry userEntry;
		Entry passEntry;

		public LoginSamplePage()
		{
			Title = "Login sample";

			userEntry = new Entry { Placeholder = "Username" };
			passEntry = new Entry { Placeholder = "Password", IsPassword = true };
			var loginButton = new Button { Text = "Login" };
			var eraseLoginButton = new Button { Text = "Erase login" };

			loginButton.Clicked += async (s, a) =>
			{
				await BlobCache.Secure.SaveLogin(userEntry.Text,
				                                 passEntry.Text,
				                                 "thatcsharpguy.com",
				                                 DateTimeOffset.Now.AddDays(7));
			};


			eraseLoginButton.Clicked += async (s, a) =>
			{
				userEntry.Text = "";
				passEntry.Text = "";
				await BlobCache.Secure.EraseLogin();
			};

			Content = new StackLayout
			{
				Padding = 20,
				Spacing= 10,
				Children = {
					userEntry,
					passEntry,
					loginButton,
					eraseLoginButton
				}
			};
		}

		protected override async void OnAppearing()
		{
			try
			{
				var loginInfo = await BlobCache.Secure.GetLoginAsync("thatcsharpguy.com");
				userEntry.Text = loginInfo.UserName;
				passEntry.Text = loginInfo.Password;
			}
			catch(KeyNotFoundException ex)
			{
				await DisplayAlert("Login", "Login not found", "OK");
			}
			base.OnAppearing();
		}
	}
}


