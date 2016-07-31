using System;
using System.IO;
using System.Reactive.Linq;
using Akavache;
using Splat;
using Xamarin.Forms;

namespace AkavacheSample
{
	public class BytesSamplesPage : ContentPage
	{
		Editor FileContentEntry;

		public BytesSamplesPage()
		{
			Title = "Byte samples";

			FileContentEntry = new Editor { HeightRequest = 150 };

			var downloadFile = new Button { Text = "Download file" };
			downloadFile.Clicked += async (sender, e) =>
			{
				var bytes = await BlobCache.LocalMachine
				         .DownloadUrl("https://gist.githubusercontent.com/fferegrino/8c0014cc5e4c348c28460679088fac40/raw/43ed14705c1acd8cb08c12757fd1802dbfd41838/hello.txt");

				var text  = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

				FileContentEntry.Text = text;
			};

			Content = new StackLayout
			{
				Padding = 20,
				Children = {
					FileContentEntry,
					downloadFile
				}
			};
		}
	}
}


