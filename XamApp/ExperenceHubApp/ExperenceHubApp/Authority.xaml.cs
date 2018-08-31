using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExperenceHubApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Authority : ContentPage
	{
        private bool login = false;
        public bool LogIn
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
		public Authority ()
		{
			InitializeComponent ();

            if (login)
            {

            }
		}
	}
}