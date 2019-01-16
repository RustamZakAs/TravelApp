using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Messages;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class MenyuViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        //private ViewModelBase back;
        //public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public MenyuViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<MenyuMessage>(this, 
                msg =>
                {
                    UserNick = msg.UserNick;
                });
        }
        
        private RelayCommand weatherCommand;
        public RelayCommand WeatherCommand
        {
            get => weatherCommand ?? (weatherCommand = new RelayCommand(
                () =>
                {
                    navigation.Navigate<WeatherViewModel>();
                }
                ));
        }

        private RelayCommand tripsCommand;
        public RelayCommand TripsCommand
        {
            get => tripsCommand ?? (tripsCommand = new RelayCommand(
                () =>
                {
                    navigation.Navigate<TripsViewModel>();
                }
                ));
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
                () =>
                {
                    navigation.Navigate<LogInViewModel>();
                }
                ));
        }

        private RelayCommand citiesCommand;
        public RelayCommand CitiesCommand
        {
            get => citiesCommand ?? (citiesCommand = new RelayCommand(
                () =>
                {
                    navigation.Navigate<CitiesViewModel>();
                }
                ));
        }

        public override string ToString()
        {
            return "Страница меню";
        }
    }
}
