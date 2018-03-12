
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyWeatherApp.Controls;
using MyWeatherApp.Model;
using MyWeatherApp.Views;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyWeatherApp.ViewModel
{

    //viewmodel class of the mainpage

    public class MainViewModel:ViewModelBase
    {

       private string _apiKeyy = "5f97a03e0275d11fb8fbaad375938067";

        //Country code used in the api call
        private string _countryCode = "de";
        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
            set
            {
                _countryCode = value;
                RaisePropertyChanged("CountryCode");
                RaisePropertyChanged("CountryImageSource");
                
            }
        }


        //Country flag
        private string _countryImageSource;
        public string CountryImageSource
        {
            get
            {
                _countryImageSource = "http://www.countryflags.io/" + CountryCode + "/flat/64.png";
                return _countryImageSource;
            }
            set
            {
                _countryImageSource = value;
                RaisePropertyChanged("CountryImageSource");
            }
        }



        private async Task<dynamic> SetCountryIconUrl(string queryString)
        {
            dynamic results = await DataService.RetrieveDataFromService(queryString).ConfigureAwait(false);
            dynamic data = JObject.Parse(results);
            _countryImageSource = data.flag;
            RaisePropertyChanged("CountryImageSource");
            return data.flag;
        }



        private ICommand _showCountriesListCommand;
        public ICommand ShowCountriesListCommand
        {
            get
            {
                if (_showCountriesListCommand == null)
                {
                    _showCountriesListCommand = new RelayCommand<object>(o => ShowCountriesList(o), o => CanExecuteShowCountriesList(o));
                }
                return _showCountriesListCommand;
            }
        }


        private bool CanExecuteShowCountriesList(object o)
        {
            return !IsBusy;
        }




        private void ShowCountriesList(object o)
        {
            MainPage.Instance.Navigation.PushAsync(new CountriesPopupPage());
        }



        private ICommand _retrieveForecastsByLocationCommand;
        public ICommand RetrieveForecastsByLocationCommand
        {
            get
            {
                if (_retrieveForecastsByLocationCommand == null)
                {
                    _retrieveForecastsByLocationCommand = new RelayCommand<object>(o => RetrieveForecastsByLocation(o), o => CanExecuteRetrieveForecastsByLocation(o));
                }
                return _retrieveForecastsByLocationCommand;
            }
        }

        private bool CanExecuteRetrieveForecastsByLocation(object o)
        {
            return !IsBusy;
        }


       

        private async void RetrieveForecastsByLocation(object o)
        {
            var forecastsListLayout = o as BindableStackLayout;
            if (forecastsListLayout != null)
            {
                forecastsListLayout.Children.Clear();
            }
            


            var isLocationPermissionGranted = await DependencyService.Get<ILocationPermission>().IsLocationPermissionGranted();

            if (!isLocationPermissionGranted)
            {
                var requested = DependencyService.Get<ILocationPermission>().RequestLocationPermission();
                return;
            }
            else
            {
                var isGPSOn = await DependencyService.Get<ILocationPermission>().IsGPSOn();
                if (!isGPSOn)
                {
                    var gpsStarted = await DependencyService.Get<ILocationPermission>().TurnGPSOn();
                    return;
                }
                else
                {
                    // show loading indicator
                    IsBusy = true;
                    RaisePropertyChanged("IsBusy");

                    try
                    {
                        // get current location
                        var locator = CrossGeolocator.Current;
                        locator.DesiredAccuracy = 50;
                        var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));


                        // get weather forecasts by given location
                        var forecastsList = await GetWeatherByLocation(position.Latitude, position.Longitude);
                        var hasCreated = await CreateForecastFrames(forecastsList);
                        if (!hasCreated)
                        {
                            MainPage.Instance?.DisplayAlert("Notification", "please make sure you are connected with the Internet and you have entered a valide ZIP Code!", "OK");
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        await MainPage.Instance.DisplayAlert("GPS Access denied", "Please turn your GPS Module on to get access the current Location!", "OK");
                        
                    }
                }
            }


            // hide loading indicator
            IsBusy = false;
            RaisePropertyChanged("IsBusy");
        }


        private ICommand _retrieveForecastsCommand;
        public ICommand RetrieveForecastsCommand
        {
            get
            {
                if (_retrieveForecastsCommand == null)
                {
                    _retrieveForecastsCommand = new RelayCommand<object>(o => RetrieveForecasts(o), o => CanExecuteRetrieveForecasts(o));
                }
                return _retrieveForecastsCommand;
            }
        }

        private bool CanExecuteRetrieveForecasts(object o)
        {
            return !IsBusy;
        }

        private async void RetrieveForecasts(object o)
        {
            var forecastsListLayout = o as BindableStackLayout;
            if (forecastsListLayout != null)
            {
                forecastsListLayout.Children.Clear();
            }
            // show loading indicator
            IsBusy = true;
            RaisePropertyChanged("IsBusy");
            var forecastsList = await GetWeatherByZipCode(ZipCodeInput);
            var hasCreated = await CreateForecastFrames(forecastsList);
            if (!hasCreated)
            {
                MainPage.Instance?.DisplayAlert("Notification", "please make sure you are connected with the Internet and you have entered a valide ZIP Code!", "Yes");
            }

            // hide loading indicator
            IsBusy = false;
            RaisePropertyChanged("IsBusy");
        }




        // list of the created forecasts controls
        private ObservableCollection<SingleForecastControl> _forecastControlsList = new ObservableCollection<SingleForecastControl>();
        public ObservableCollection<SingleForecastControl> ForecastControlsList
        {
            get
            {
                return _forecastControlsList;
            }
            set
            {
                _forecastControlsList = value;
                RaisePropertyChanged("ForecastControlsList");
            }
        }


        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }


        private string _zipCodeInput = "";
        public string ZipCodeInput
        {
            get
            {
                return _zipCodeInput;
            }
            set
            {
                _zipCodeInput = value;
                RaisePropertyChanged("ZipCodeInput");
            }
        }




        // creates forecasts controls dynamically
        private async Task<bool> CreateForecastFrames(ForecastList forecasts)
        {
           
            try
            {
                _forecastControlsList.Clear();
                RaisePropertyChanged("ForecastControlsList");
                var forecastsItems = forecasts.list;

                foreach(var forecastItem in forecastsItems)
                {
                    var forecastControl = new SingleForecastControl();
                    forecastControl.Margin = new Xamarin.Forms.Thickness(10, 5, 10, 0);
                    var forecastViewModel = new SingleForecastItemViewModel(forecastItem);
                    forecastControl.BindingContext = forecastViewModel;
                    _forecastControlsList.Add(forecastControl);
                    
                }

                RaisePropertyChanged("ForecastControlsList");
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }



        // retrieve weather forecasts using current location
        private async Task<ForecastList> GetWeatherByLocation(double latitude, double longitude)
        {
            try
            {
                
                string queryString = "http://api.openweathermap.org/data/2.5/forecast?lat="
                    + latitude + "&lon=" + longitude + "&appid=" + _apiKeyy + "&units=metric";

                dynamic results = await DataService.RetrieveDataFromService(queryString).ConfigureAwait(false);
                var weatherObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ForecastList>(results);
                return weatherObject;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }



        // retrieve weather forecasts by zip code
        private async Task<ForecastList> GetWeatherByZipCode(string zipCode)
        {
            try
            {
               
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?zip="
                + zipCode + "," + CountryCode + "&appid=" + _apiKeyy + "&units=metric";

            dynamic results = await DataService.RetrieveDataFromService(queryString).ConfigureAwait(false);
            var weatherObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ForecastList>(results);
                return weatherObject;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }
    }
}
