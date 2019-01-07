using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Messages;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class TripsViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public TripsViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<TripsMessage>(this,
                msg =>
                {
                    UserNick = msg.UserNick;
                });

            //City city = new City();
            //MessageBox.Show(city.СityList[0].name);
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
    }
}
