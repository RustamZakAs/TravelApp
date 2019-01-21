using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class TicketsViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private string pathPDF = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
        public string PathPDF { get => pathPDF; set => Set(ref pathPDF, value); }

        private readonly IMyNavigationService navigation;
        public TicketsViewModel(IMyNavigationService navigation)
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
                    navigation.Navigate(Back.GetType());
                }
                ));
        }

        public override string ToString()
        {
            return "Страница предпросмотра и добавления билетов";
        }
    }
}
