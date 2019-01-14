using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp
{
    public class CityInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public coord coord { get; set; }
    }

    public class coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
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
                    var json = web.DownloadString(directory + file);
                    CityList = JsonConvert.DeserializeObject<ObservableCollection<CityInfo>>(json);
                }
            }
        }
    }
}
