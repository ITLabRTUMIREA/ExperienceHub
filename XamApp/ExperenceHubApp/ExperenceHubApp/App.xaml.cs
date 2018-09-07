using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ExperenceHubApp
{
	public partial class App : Application
	{
        public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();

            Application.Current.Properties["URL"] = "http://5d7f0dcd.ngrok.io";
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
