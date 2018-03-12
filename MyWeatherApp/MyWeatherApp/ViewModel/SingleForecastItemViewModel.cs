using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyWeatherApp.Model;
using MyWeatherApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyWeatherApp.ViewModel
{

    // viewmodel class of a single forecast item
    public class SingleForecastItemViewModel:ViewModelBase
    {
        private WeatherForecast _forecastItem;
        private string _weatherIconUrl = "http://openweathermap.org/img/w/";

        public SingleForecastItemViewModel(WeatherForecast forecastItem)
        {
            _forecastItem = forecastItem;
            
        }




        private ICommand _showForecastDetailsCommand;
        public ICommand ShowForecastDetailsCommand
        {
            get
            {
                if (_showForecastDetailsCommand == null)
                {
                    _showForecastDetailsCommand = new RelayCommand<object>(o => ShowForecastDetails(o), o => CanExecuteShowForecastDetails(o));
                }
                return _showForecastDetailsCommand;
            }
        }

        private bool CanExecuteShowForecastDetails(object o)
        {
            return true;
        }




        private void ShowForecastDetails(object o)
        {
            var detailsPage = new ForecastDetailsPage();
            var detailsPageViewModel = new ForecastDetailsViewModel(_forecastItem);
            detailsPage.BindingContext = detailsPageViewModel;
            try
            {
                
                MainPage.Instance.Navigation.PushAsync(detailsPage);
            }
            catch(Exception ex)
            {
            }

        }


        private string _dateText;
        public string DateText {
            get
            {
                _dateText = _forecastItem.dt_txt;
                return _dateText;
            }
            set
            {
                _dateText = value;
                RaisePropertyChanged("DateText");
            }
        }



        private string _temperatureText;
        public string TemperatureText
        {
            get
            {
                _temperatureText = _forecastItem.main.temp;
                return _temperatureText;
            }
            set
            {
                _temperatureText = value;
                RaisePropertyChanged("TemperatureText");
            }
        }



        private string _weatherImageSource;
        public string WeatherImageSource
        {
            get
            {
                _weatherImageSource = _weatherIconUrl + _forecastItem.weather[0].icon + ".png";
                return _weatherImageSource;
            }
            set
            {
                _weatherImageSource = value;
                RaisePropertyChanged("WeatherImageSource");
            }
        }


    }
}
