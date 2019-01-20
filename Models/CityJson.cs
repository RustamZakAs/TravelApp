using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp
{
    public class CityInfo : INotifyPropertyChanged
    {
        private int _id;
        public int id { get => _id; set => Set(ref _id, value); }

        private string _name;
        public string name { get => _name; set => Set(ref _name, value); }

        private string _country;
        public string country { get => _country; set => Set(ref _country, value); }

        private coord _coord;
        public coord coord { get => _coord; set => Set(ref _coord, value); }

        private string _imageMobile;
        public string imageMobile { get => _imageMobile; set => Set(ref _imageMobile, value); }

        private string _imageWeb;
        public string imageWeb { get => _imageWeb; set => Set(ref _imageWeb, value); }

        public CityInfo()
        {
            id = 0;
            name = "";
            country = "";
            coord = new coord();
            imageMobile = @"/Resources/empty_image.png";
            imageWeb = @"/Resources/empty_image.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class coord : INotifyPropertyChanged
    {
        private double _lat;
        public double lat { get => _lat; set => Set(ref _lat, value); }

        private double _lon;
        public double lon { get => _lon; set => Set(ref _lon, value); }

        public coord()
        {
            lat = 0;
            lon = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class City
    {
        public ObservableCollection<CityInfo> CityList { get; set; }
        public City()
        {
            string file = @"\Resources\city.list.json";
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (File.Exists(directory + file))
            {
                using (WebClient web = new WebClient())
                {
                    web.Encoding = Encoding.UTF8;
                    var json = web.DownloadString(directory + file);
                    CityList = JsonConvert.DeserializeObject<ObservableCollection<CityInfo>>(json);
                }
            }
        }
    }
}
