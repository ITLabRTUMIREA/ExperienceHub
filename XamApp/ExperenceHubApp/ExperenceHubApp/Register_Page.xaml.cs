using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExperenceHubApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register_Page : ContentPage
	{
        private bool correct = false;
         
		public Register_Page ()
		{
			InitializeComponent ();
		}

        private async void Sing_Up_Clicked(object sender, EventArgs e)
        {

            if (First_name.Text != String.Empty &&
                Last_name.Text != String.Empty &&
                Email.Text != String.Empty &&
                Password.Text != String.Empty &&
                Conf_password.Text != String.Empty &&
                correct)
            {
                Person new_person = new Person();
                new_person.firstname = First_name.Text;
                new_person.lastname = Last_name.Text;
                new_person.email = Email.Text;
                new_person.password = Password.Text;

                string json = JsonConvert.SerializeObject(new_person);
                HttpContent content = new StringContent(json);

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Content = content;
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(Application.Current.Properties["URL"] + "/api/registration");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
            }
        }

        private void Conf_Changed(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform.Equals(Device.UWP)) {
                if (Password.Text != String.Empty)
                {
                    if (Password.Text == Conf_password.Text)
                    {
                        Conf_text.Text = "Passwords are same";
                        Conf_text.TextColor = Color.Green;
                        correct = true;
                    } else
                    {
                        Conf_text.Text = "Passwords are diffrent";
                        Conf_text.TextColor = Color.Red;
                        correct = false;
                    }
                }
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}