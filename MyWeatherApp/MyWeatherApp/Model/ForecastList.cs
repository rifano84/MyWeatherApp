using System;
using System.Collections.Generic;
using System.Text;

namespace MyWeatherApp.Model
{

    //class modelling a list of forecasts (5 day forecasts)
    public class ForecastList
    {
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<WeatherForecast> list { get; set; }
        public City city { get; set; }



        public class City
        {
            public string name { get; set; }
            public Coordinates coord { get; set; }
            public string country { get; set; }


        }


        public class Coordinates
        {
            public double lat { get; set; }
            public double lon { get; set; }

        }




    }
}
