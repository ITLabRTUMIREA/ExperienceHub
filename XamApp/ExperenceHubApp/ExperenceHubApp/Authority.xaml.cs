using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExperenceHubApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Authority : ContentPage
	{
        public Authority ()
		{
			InitializeComponent ();
		}

        private async void Sing_In_Button(object sender, EventArgs e)
        {
            Person person = new Person();

            person.login = Login.Text;
            person.password = Password.Text;
            string json = JsonConvert.SerializeObject(person);
            HttpContent content = new StringContent(json);

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.Content = content;
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(Application.Current.Properties ["URL"] + "/api/authorize");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            bool flag = true;
            while (flag)
            {
                try
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent responseContent = response.Content;
                        var json1 = await responseContent.ReadAsStringAsync();
                        Application.Current.Properties["Person"] = JsonConvert.DeserializeObject<Person>(json1);
                        await Navigation.PushAsync(new Cabinet());
                    }
                    flag = false;
                } catch
                {
                    flag = true;
                }
            }
        }

        private void Sing_Up_Button(object sender, EventArgs e)
        {
              Navigation.PushAsync(new Register_Page());
        }
    }
}