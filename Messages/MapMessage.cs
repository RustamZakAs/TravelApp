using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Messages
{
    class MapMessage
    {
        public string UserNick { get; set; }
        public GalaSoft.MvvmLight.ViewModelBase Back { get; set; }
        public Microsoft.Maps.MapControl.WPF.Location MapCenter { get; set; }
    }
}
