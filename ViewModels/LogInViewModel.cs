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
using System.Media;
using System.IO;
using System.Text.RegularExpressions;
using TravelApp.Models;

namespace TravelApp.ViewModels
{
    class LogInViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string userPass;
        public string UserPass { get => userPass; set => Set(ref userPass, value); }

        private string userImage = "/TravelApp;component/Resources/empty_user.png";
        public string UserImage { get => userImage; set => Set(ref userImage, value); }
        
        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public LogInViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            UserNick = "RustamZakAs";
            //SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();
        }

        private MyRelayCommand sendCommand;
        public MyRelayCommand SendCommand
        {
            get => sendCommand ?? (sendCommand = new MyRelayCommand(
                param =>
                {
                    Messenger.Default.Send(new MenyuMessage { UserNick = UserNick, Back = this });
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
                },
                param => (!String.IsNullOrWhiteSpace(UserNick))
                ));
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get => registrationCommand ?? (registrationCommand = new RelayCommand(
                () =>
                {
                    if ( UserNick != null && isValidEmail(UserNick) )
                        Messenger.Default.Send(new RegistrationMessage { UserEmail = UserNick, Back = this });
                    else
                        Messenger.Default.Send(new RegistrationMessage { UserNick = UserNick, Back = this });

                    navigation.Navigate<RegistrationViewModel>();
                }
                ));
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
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
                    MessageBox.Show("Не забывал(а) бы! ");
                }
                ));
        }

        public override string ToString()
        {
            return "Страница входа в программу";
        }
    }
}
