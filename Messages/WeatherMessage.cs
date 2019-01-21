using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Messages
{
    class WeatherMessage
    {
        public string UserNick { get; set; }
        public string City { get; set; }
        public GalaSoft.MvvmLight.ViewModelBase Back { get; set; }
    }
}
