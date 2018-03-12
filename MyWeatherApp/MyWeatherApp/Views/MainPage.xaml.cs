using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyWeatherApp
{
	public partial class MainPage : ContentPage
	{
        public static MainPage Instance;

		public MainPage()
		{
			InitializeComponent();
            Instance = this;
            
        }
	}
}
