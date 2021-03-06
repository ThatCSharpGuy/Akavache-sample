﻿using Akavache;
using Xamarin.Forms;

namespace AkavacheSample
{
	public partial class App : Application
	{
		public App()
		{
			BlobCache.ApplicationName = "AvakacheSample";
			InitializeComponent();

			MainPage = new NavigationPage(new AkavacheSamplePage());
		}



		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			BlobCache.Shutdown().Wait();
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

