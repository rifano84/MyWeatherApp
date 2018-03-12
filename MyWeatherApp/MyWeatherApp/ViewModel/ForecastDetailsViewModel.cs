using GalaSoft.MvvmLight;
using MyWeatherApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWeatherApp.ViewModel
{

    //viewmodel class for the forecastdetails page


    public class ForecastDetailsViewModel:ViewModelBase
    {
        private WeatherForecast _forecastItem;
        public string MainWeather {
            get
            {
                return _forecastItem.weather[0].main;
            }
        }
        public string WeatherDescription {
            get
            {
                return _forecastItem.weather[0].description;
            }
        }
        public string TempAverage {
            get
            {
                return _forecastItem.main.temp;
            }
        }
        public string TempMin {
            get
            {
                return _forecastItem.main.temp_min;
            }
        }
        public string TempMax {
            get
            {
                return _forecastItem.main.temp_max;
            }
        }
        public string Pressure {
            get
            {
                return _forecastItem.main.pressure;
            }
        }
        public string SeaLevel {
            get
            {
                return _forecastItem.main.sea_level;
            }
        }
        public string GroundLevel {
            get
            {
                return _forecastItem.main.grnd_level;
            }
        }
        public string Humidity {
            get
            {
                return _forecastItem.main.humidity;
            }
        }
        public float WindSpeed {
            get
            {
                return _forecastItem.wind.speed;
            }
        }

      
        public string FullDate {
            get
            {
                return _forecastItem.dt_txt;
            }
           
        }

        private string _weatherIconUrl = "http://openweathermap.org/img/w/";

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


        public ForecastDetailsViewModel(WeatherForecast forecastItem)
        {
            try
            {
                _forecastItem = forecastItem;
            }

            catch(Exception ex)
            {

            }
        }

    }
}
