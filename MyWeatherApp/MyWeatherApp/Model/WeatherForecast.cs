using System;
using System.Collections.Generic;
using System.Text;

namespace MyWeatherApp.Model
{


    //class modelling a single weather forecast item
    public class WeatherForecast
    {

        public string dt { get; set; } 
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; } 
        public Rain rain { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }


        public class Rain
        {
            public double __invalid_name__3h { get; set; }
        }

        public class Sys
        {
            public string pod { get; set; }
        }

        public class Main
        {
            public string temp { get; set; } = " ";
            public string temp_min { get; set; } = " ";
            public string temp_max { get; set; } = " ";
            public string pressure { get; set; } = " ";
            public string sea_level { get; set; } = " ";
            public string grnd_level { get; set; } = " ";
            public string humidity { get; set; } = " ";
            public string temp_kf { get; set; } = " ";

        }

        public class Wind
        {
            public float speed { get; set; }
            public double deg { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }

        }

        public class Clouds
        {
            public int all { get; set; }
        }

    }
}
