using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public List<CityInfo> СityList { get; set; }
        public City()
        {
            string url = @"C:\Users\User\source\repos\TravelApp\Resources\city.list.json";
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);
                СityList = JsonConvert.DeserializeObject<List<CityInfo>>(json);
            }
        }
    }
}
