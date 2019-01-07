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
    class LogInViewModel : ViewModelBase
    {
        private string userNick = "root";
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public LogInViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();
        }

        private RelayCommand sendCommand;
        public RelayCommand SendCommand
        {
            get => sendCommand ?? (sendCommand = new RelayCommand(
                () =>
                {
                    Messenger.Default.Send(new MenyuMessage { UserNick = UserNick });
                    navigation.Navigate<MenyuViewModel>();
                }
                ));
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get => registrationCommand ?? (registrationCommand = new RelayCommand(
                () =>
                {
                    if (UserNick.Contains('@') && UserNick.Contains('.'))
                        Messenger.Default.Send(new RegistrationMessage { UserEmail = UserNick });
                    else
                        Messenger.Default.Send(new RegistrationMessage { UserNick = UserNick });

                    navigation.Navigate<RegistrationViewModel>();
                }
                ));
        }
        

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get => closeCommand ?? (closeCommand = new RelayCommand(
                () =>
                {
                    Environment.Exit(0);
                }
                ));
        }
    }
}
