using MyWeatherApp.Model;
using MyWeatherApp.ViewModel;
using Newtonsoft.Json.Linq;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyWeatherApp.Views
{

    // page used as popup to show a countries' list
	public class CountriesPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        ListView countriesList;
        ActivityIndicator indicator;

        public CountriesPopupPage ()
		{
            this.Title = "Countries";
            
            // create the listview
             countriesList = new ListView()
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.Default,
                BackgroundColor = Color.White,
                SeparatorColor = Color.Gold,                
                WidthRequest = 240,
                HeightRequest = 180,
                Margin=new Thickness(0,5,0,0)
               

            };
            countriesList.ItemSelected += SelectedItem;

            // load indicator
            indicator = new ActivityIndicator() { IsRunning = true, IsVisible = true, Color = Color.Blue, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

            // set content
            Content = new StackLayout
            {
                BackgroundColor=Color.White,
                Margin = new Thickness(40),
                Orientation = StackOrientation.Vertical,HorizontalOptions=LayoutOptions.Center,WidthRequest=300, HeightRequest=300,
                Children = {indicator, new Label { Text = "Please choose a country", HorizontalOptions = LayoutOptions.Center },countriesList }
            };


            // get countries' info from api
            RetrieveCountriesFromAPI();


        }


        // event country selection
        private void SelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedCountry = e.SelectedItem;
            var countryCode = "";
            foreach (var item in Countries)
            {
                if (selectedCountry.Equals(item.Value))
                {
                    countryCode = item.Key;
                    break;
                }
            }

            // update flag
            var mainViewModel = MainPage.Instance.BindingContext as MainViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.CountryCode = countryCode;
            }

            MainPage.Instance.Navigation.PopAsync();
        }


        // get countries info
        private async void RetrieveCountriesFromAPI()
        {

            dynamic results = await DataService.RetrieveDataFromService("https://restcountries.eu/rest/v2/all?fields=name;alpha2Code").ConfigureAwait(false);
            var countries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryModel>>(results);
            foreach (var item in countries)
            {
                Countries.Add(item.alpha2Code, item.name);
                
            }

            // update view
            Device.BeginInvokeOnMainThread(() =>
            {
                countriesList.ItemsSource = Countries.Values;

                indicator.IsRunning = false;
                indicator.IsVisible = false;
            });
            

        }



       

        // dictionary hosting countries' info (name and code)
        public Dictionary<string, string> Countries { get; set; } = new Dictionary<string, string>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

       

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}