using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class TicketsPDFViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string pathPDF = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
        public string PathPDF { get => pathPDF; set => Set(ref pathPDF, value); }

        private readonly IMyNavigationService navigation;
        public TicketsPDFViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            //Messenger.Default.Register<string>(this,
            //    msg =>
            //    {
            //        UserNick = msg;
            //    });
        }

        public override string ToString()
        {
            return "Страница просмотра и добавления билетов";
        }
    }
}
