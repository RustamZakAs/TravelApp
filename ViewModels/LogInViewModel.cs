﻿using GalaSoft.MvvmLight;
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
using System.Media;
using System.IO;

namespace TravelApp.ViewModels
{
    class LogInViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string userImage = "/TravelApp;component/Resources/empty_user.png";
        public string UserImage { get => userImage; set => Set(ref userImage, value); }
        
        //private ViewModelBase back;
        //public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public LogInViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            //SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();
        }

        private RelayCommand sendCommand;
        public RelayCommand SendCommand
        {
            get => sendCommand ?? (sendCommand = new RelayCommand(
                () =>
                {
                    Messenger.Default.Send(new MenyuMessage { UserNick = UserNick });
                    navigation.Navigate<MenyuViewModel>();

                    Task.Run(() =>
                    {
                        string file = @"\Resources\Sound_20015.wav";
                        var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        if (File.Exists(directory + file))
                        {
                            SoundPlayer my_wave_file = new SoundPlayer(directory + file);
                            my_wave_file.PlaySync(); //PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
                        }
                    });
                }
                ));
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get => registrationCommand ?? (registrationCommand = new RelayCommand(
                () =>
                {
                    if (UserNick != null && (UserNick.Contains('@') && UserNick.Contains('.')))
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

        private RelayCommand forgotPassCommand;
        public RelayCommand ForgotPassCommand
        {
            get => forgotPassCommand ?? (forgotPassCommand = new RelayCommand(
                () =>
                {
                    MessageBox.Show("Не забывал бы! ");
                }
                ));
        }

        public override string ToString()
        {
            return "Страница входа в программу";
        }
    }
}
