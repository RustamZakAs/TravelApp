using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Net;
using System;

namespace TravelApp
{
    class WeatherInfo
    {
        [InverseProperty("WeatherInfo")]
        public virtual ICollection<WeatherInfo> _WeatherInfo { get; set; }

        public class coord
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public class weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class sys
        {
            public string country { get; set; }
        }
        
        public class Root
        {
            public coord coord { get; set; }
            public List<weather> weather { get; set; }
            public string name { get; set; }
            public sys sys { get; set; }
            public double dt { get; set; }
            public wind wind { get; set; }
            public main main { get; set; }
        }
    }

    class weatherForcast
    {
        [InverseProperty("weatherForcast")]
        public virtual ICollection<weatherForcast> _weatherForcast { get; set; }

        public class Root
        {
            public int cod { get; set; }
            public double message { get; set; }
            public int cnt { get; set; }
            public List<list> list { get; set; } //forcast list
            public city city { get; set; }
        }

        public class city
        {
            public int id { get; set; }
            public string name { get; set; }
            public coord coord { get; set; }
            public string country { get; set; }
            public string population { get; set; }
        }

        public class coord
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public class list : INotifyPropertyChanged
        {
            public double dt { get; set; } //day in milli second
            public main   main { get; set; }
            public clouds clouds { get; set; }
            public wind   wind { get; set; }
            public double pressure { get; set; } //pressure hpa
            public double humidity { get; set; } //humidity %
            public double speed { get; set; } //wind speed km/h
            public string dt_txt { get; set; }

            private List<weather> _weather; //weather list
            public List<weather> weather { get => _weather; set => Set(ref _weather, value); }

            public event PropertyChangedEventHandler PropertyChanged;
            public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public class main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double pressure { get; set; }
            public double sea_level { get; set; }
            public double grnd_level { get; set; }
            public double humidity { get; set; }
            public double temp_kf { get; set; }
        }

        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class rain
        {
            public double h { get; set; }
        }

        public class sys
        {
            public string pod { get; set; }
        }

        public class clouds
        {
            public int all { get; set; }
        }

        public class weather : INotifyPropertyChanged
        {
            public int id { get; set; }
            public string main { get; set; } //weather condition
            public string description { get; set; } //weather description
            public string icon { get; set; }
            //public string iconPaht { get; set; }

            private string _iconPaht;
            public string iconPaht { get => _iconPaht; set => Set(ref _iconPaht, value); }

            //public weather()
            //{
            //    iconPaht = String.Format("http://openweathermap.org/img/w/{0}.png", icon);
            //}

            public event PropertyChangedEventHandler PropertyChanged;
            public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    class Weather
    {
        public string APIKey { get; set; }
        public string CityName { get; set; }

        public int day { get; set; } = 5;

        public Weather()
        {
            APIKey = "32da0bb363f40a2090b56bfec1e72e82";
        }

        public WeatherInfo.Root GetWeather(string city)
        {
            if (true)
            using (WebClient web = new WebClient())
            {
                string url;
                try
                {
                    url = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}", city, APIKey);
                }
                catch (Exception e)
                {
                    throw e;
                }

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<WeatherInfo.Root>(json);

                WeatherInfo.Root outPut = result;

                return outPut;
            }
            else
            {
                //SQLiteDatabase sqlite = new SQLiteDatabase();
                //sqlite.sqliteConn.Open();

                //sqlite.sqliteConn.Close();
            }
        }

        public weatherForcast.Root GetForcast(string city)
        {
            string url = String.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&cnt={1}&appid={2}", city, day, APIKey);

            using (WebClient web = new WebClient())
            {
                try
                {
                    string json = web.DownloadString(url);

                    var Object = JsonConvert.DeserializeObject<weatherForcast.Root>(json);

                    weatherForcast.Root outPut = Object;

                    return outPut;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
