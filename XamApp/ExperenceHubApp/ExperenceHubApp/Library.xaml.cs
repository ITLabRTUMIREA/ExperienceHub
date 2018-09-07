using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace ExperenceHubApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Library : ContentPage
	{
		public Library ()
		{
			InitializeComponent ();
            
            try
            {
                Person person = (Person)Application.Current.Properties["Person"];
                List<Lesson> lessons = person.Lessons;

                int k = 0;
                for (int i = 0; i < lessons.Count / 6; i++)
                {
                    int k1 = 0;
                    lesson_grid.RowDefinitions.Add(new RowDefinition());
                    while (k1 < 6 || k < lessons.Count)
                    {
                        lesson_grid.Children.Add(PasteCard(lessons[k]), i, k1);
                        k += 1; k1 += 1;
                    }
                }
            }
            catch
            {
                lesson_grid.RowDefinitions.Add(new RowDefinition());
                Label error = new Label() { Text = "Unfortunatly you don't have lessons yet.", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                error.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                lesson_grid.Children.Add(error, 0, 0);
                Grid.SetColumnSpan(error, 6);
            }

            if (Device.Idiom != TargetIdiom.Desktop)
            {
                Add.IsVisible = false;
            }
        }

        ImageSource GetImage(Lesson lesson)
        {
            using (var ms = new MemoryStream(lesson.picture))
            {
                return ImageSource.FromStream(() => ms);
            }
        }

        Frame PasteCard(Lesson lesson)
        {
            Frame frame = new Frame();
            StackLayout stack = new StackLayout()
            {
                Orientation = 0,
                Children = {
                    new Image() {
                        Source = GetImage(lesson)
                    },
                    new Label()
                    {
                        Text = lesson.name
                    }
                }
            };
            frame.Content = stack;
            return frame;
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VideoAdd());
        }

        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("Person")) {
                Person person = (Person)Application.Current.Properties["Person"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", person.token);
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(Application.Current.Properties ["URL"] + "/api/account/" + person.id.ToString() + "/lessons");
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Add("Accept", "application/json");


                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    var json = await responseContent.ReadAsStringAsync();
                    person.Lessons = JsonConvert.DeserializeObject<List<Lesson>>(json);
                    Application.Current.Properties["Person"] = person;
                }
            }
        }
    }
}