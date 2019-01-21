using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Messages
{
    class CityMessage
    {
        public string UserNick { get; set; }
        public CityInfo CityInfo { get; set; }
        public bool SelectCity { get; set; }
        public GalaSoft.MvvmLight.ViewModelBase Back { get; set; }
    }
}
