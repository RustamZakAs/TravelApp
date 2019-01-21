using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Messages
{
    class TicketsPDFMessage
    {
        public string UserNick { get; set; }
        public ViewModelBase Back { get; set; }
    }
}
