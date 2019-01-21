using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using AForge.Video.DirectShow;
using System.Threading.Tasks;
using TravelApp.Messages;
using TravelApp.Services;
using GalaSoft.MvvmLight;
using System.Data.SQLite;
using System.Data.Entity;
using AForge.Controls;
using AForge.Vision;
using AForge.Video;
using System.Linq;
using System.Text;
using System;
using TravelApp.Models;
using System.Windows;
using System.Collections.ObjectModel;

namespace TravelApp.ViewModels
{
    class RegistrationViewModel : ViewModelBase
    {
        //ApplicationContext db;

        private RegistrationMessage regMessage = new RegistrationMessage();
        public RegistrationMessage RegMessage { get => regMessage; set => Set(ref regMessage, value); }

        private string test;
        public string Test { get => test; set => Set(ref test, value); }

        private ObservableCollection<Appeal> appealList;
        public ObservableCollection<Appeal> AppealList { get => appealList; set => Set(ref appealList, value); }

        private Registration _regInfo = new Registration();
        public Registration RegInfo { get => _regInfo; set => Set(ref _regInfo, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public RegistrationViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;
            //db = new ApplicationContext();
            //db.DSRegistration.Load();
            Messenger.Default.Register<RegistrationMessage>(this,
                msg =>
                {
                    RegMessage.UserNick = msg.UserNick;
                });

            AppealList = new Appeal().getList();

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

        private MyRelayCommand saveCommand;
        public MyRelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new MyRelayCommand(
                 param =>
                 {
                     SQLiteDatabase sqld = new SQLiteDatabase();
                     sqld.sqliteConn.Open();

                     string sql = "INSERT INTO Registration (UserName, UserSurname, UserBirdth, UserNick, Appeal, UserEmail) " +
                     "                               VALUES (@UserName, @UserSurname, @UserBirdth, @UserNick, @Appeal, @UserEmail)";
                     SQLiteCommand command = new SQLiteCommand(sql, sqld.sqliteConn);

                     SQLiteParameter sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@UserName";
                     sqliteparam.Value = RegInfo.UserName;

                     command.Parameters.Add(sqliteparam);

                     sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@UserSurname";
                     sqliteparam.Value = RegInfo.UserSurname;

                     command.Parameters.Add(sqliteparam);

                     sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@UserBirdth";
                     sqliteparam.Value = RegInfo.UserBirdth.ToString("dd.MM.yyyy");

                     command.Parameters.Add(sqliteparam);

                     sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@UserNick";
                     sqliteparam.Value = RegInfo.UserNick;

                     command.Parameters.Add(sqliteparam);

                     sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@Appeal";
                     //sqliteparam.Value = RegInfo.Appeal;
                     //MessageBox.Show(RegInfo.Appeal);
                     sqliteparam.Value = RegInfo.Appeal.ToString().Substring(0, RegInfo.Appeal.ToString().IndexOf('–'));

                     command.Parameters.Add(sqliteparam);

                     sqliteparam = new SQLiteParameter();
                     sqliteparam.ParameterName = "@UserEmail";
                     sqliteparam.Value = RegInfo.UserEmail;

                     command.Parameters.Add(sqliteparam);

                     command.ExecuteNonQuery();

                     sqld.sqliteConn.Close();
                     //db.DSRegistration.Add(RegInfo);
                     //db.SaveChanges();

                     navigation.Navigate<LogInViewModel>();
                 },
                 param => (!String.IsNullOrWhiteSpace(RegInfo.UserNick) &&
                           !String.IsNullOrWhiteSpace(RegInfo.UserName) &&
                           !String.IsNullOrWhiteSpace(RegInfo.UserEmail) &&
                           !String.IsNullOrWhiteSpace(RegInfo.UserSurname))
                 ));
        }

        public override string ToString()
        {
            return "Страница регистрации пользователя";
        }
    }
}
