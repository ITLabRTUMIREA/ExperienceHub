using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExperenceHubApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoAdd : ContentPage
	{
		public VideoAdd ()
		{
			InitializeComponent ();
		}

        private void Add_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("Person")) {
                Person person = (Person)Application.Current.Properties["Person"];

                Lesson lessons = new Lesson();
                lessons.name = Name.Text;
                lessons.description = Description.Text;
                lessons.price = float.Parse(Price.Text);
                lessons.recordtime = DateTime.Today;
                lessons.creatorID = person.id;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Application.Current.Properties["URL"] + /*дальнейшая ссылка на Валин бэк*/"");
                request.Method = HttpMethod.Post;

                HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Post, new Uri(Application.Current.Properties["URL"] + ""));


            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Record_Clicked(object sender, EventArgs e)
        {
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                //После проверка на подключенные камеры и работа с алгаритмами записи видео
            }
        }

        private async void Browse_Clicked(object sender, EventArgs e)
        {
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                var file = await CrossFilePicker.Current.PickFile();
                if (file != null)
                {
                    ZipFile.CreateFromDirectory(file.FilePath, Name.Text);
                }
            }
        }
    }
}