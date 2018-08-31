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

            Authority authority = new Authority();

            if(!authority.LogIn)
            {
                Detail = new NavigationPage(authority);
            }
		}
	}
}
