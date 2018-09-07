using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExperenceHubApp
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("Person"))
            {
                Detail = new NavigationPage(new Authority());
            } 
		}

        private void Lesson_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Library());
            this.IsPresented = false;
        }

        private void Messege_Clicked(object sender, EventArgs e)
        {

        }
        private void Shop_Clicked(object sender, EventArgs e)
        { 

        }
        private void Account_Clicked(object sender, EventArgs e)
        {
            if (!Application.Current.Properties.ContainsKey("Person"))
            {
                Detail = new NavigationPage(new Authority());
            } else
            {
                Detail = new NavigationPage(new Cabinet());
            }
            this.IsPresented = false;
        }
        private void Settings_Clicked(object sender, EventArgs e)
        {

        }
    }
}
