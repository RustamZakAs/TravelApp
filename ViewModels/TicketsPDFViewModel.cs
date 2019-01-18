using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    class TicketsPDFViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string pathPDF;// = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
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

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
                () =>
                {
                    navigation.Navigate<MenyuViewModel>();
                }
                ));
        }

        private RelayCommand clickCommand;
        public RelayCommand ClickCommand
        {
            get => clickCommand ?? (clickCommand = new RelayCommand(
                () =>
                {

                }
                ));
        }

        public override string ToString()
        {
            return "Страница просмотра билета";
        }
    }
}
