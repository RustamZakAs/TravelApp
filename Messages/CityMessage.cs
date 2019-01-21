using GalaSoft.MvvmLight;
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
        public ViewModelBase Back { get; set; }
    }
}
