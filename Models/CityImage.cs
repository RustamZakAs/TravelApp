using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class CityImage
    {
        public class _links
        {
            public List<curies> curies { get; set; }
            public self self { get; set; }
        }

        public class curies
        {
            public string href { get; set; }
            public string name { get; set; }
            public bool templated { get; set; }
        }

        public class self
        {
            public string href { get; set; }
        }

        public class photos
        {
            public attribution attribution { get; set; }
            public image image { get; set; }
        }

        public class attribution
        {
            public string license { get; set; }
            public string photographer { get; set; }
            public string site { get; set; }
            public string source { get; set; }
        }

        public class image
        {
            public string mobile { get; set; }
            public string web { get; set; }
        }

        public class Root : INotifyPropertyChanged
        {
            private _links __links;
            public _links _links { get => __links; set => Set(ref __links, value); }

            private List<photos> _photos;
            public List<photos> photos { get => _photos; set => Set(ref _photos, value); }

            public event PropertyChangedEventHandler PropertyChanged;
            public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public CityImage.Root GetImages(string city)
        {
            if (city != null && city.Length > 0)
            {
                using (WebClient web = new WebClient())
                {
                    string url = String.Format("https://api.teleport.org/api/urban_areas/slug:{0}/images/", city);
                    try
                    {
                        var json = web.DownloadString(url);
                        var result = JsonConvert.DeserializeObject<CityImage.Root>(json);
                        return (CityImage.Root)result;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            return null;
        }
    }
}
