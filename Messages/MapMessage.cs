using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Messages
{
    class MapMessage
    {
        public System.String UserNick { get; set; }
        public GalaSoft.MvvmLight.ViewModelBase Back { get; set; }
        public Microsoft.Maps.MapControl.WPF.Location MapCenter { get; set; }
    }
}
