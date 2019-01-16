using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Messages;
using TravelApp.Models;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class TripsViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ObservableCollection<Trip> tripList = new ObservableCollection<Trip>();
        public ObservableCollection<Trip> TripList { get => tripList; set => Set(ref tripList, value); }

        private Trip selectedTrip = new Trip();
        public Trip SelectedTrip { get => selectedTrip; set => Set(ref selectedTrip, value); }

        private CityInfo inCityInfo;
        public CityInfo InCityInfo { get => inCityInfo; set => Set(ref inCityInfo, value); }

        //private ViewModelBase back;
        //public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        //public TripsViewModel(IMyNavigationService navigation)
        //{
        //    this.navigation = navigation;

        //    Messenger.Default.Register<CityInfo>(this,
        //        msg =>
        //        {
        //            InCityInfo = msg;
        //            if (msg != null)
        //            {
        //                TripList[TripList.Count - 1].CityInfo.Add(msg);
        //                TripList[TripList.Count - 1].Name = (TripList.Count - 1).ToString();
        //            }

        //        });

        //    Messenger.Default.Register<MenyuMessage>(this,
        //       msg =>
        //       {
        //           UserNick = msg.UserNick;
        //       });
        //}

        public TripsViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<TripsMessage>(this,
                msg =>
                {
                    UserNick = msg.UserNick;
                });

            Messenger.Default.Register<NotificationMessage<CityInfo>>(this, OnHitIt);

            Messenger.Default.Register<MenyuMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
               });
        }

        private void OnHitIt(NotificationMessage<CityInfo> nc)
        {
            if (nc.Notification == "SelectCity")
            {
                var tr = nc.Content;
                if (TripList[TripList.Count - 1].CityInfo == null)
                    TripList[TripList.Count - 1].CityInfo = new ObservableCollection<CityInfo>();
                TripList[TripList.Count - 1].CityInfo.Add(tr);
            }
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

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get => addCommand ?? (addCommand = new RelayCommand(
                 () =>
                 {
                     TripList.Add(new Trip());
                     navigation.Navigate<CitiesViewModel>();
                     if (SelectedTrip.CityInfo != null && SelectedTrip.CityInfo[0] != null) MessageBox.Show(SelectedTrip.CityInfo[0].name);
                 }
                 ));
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get => removeCommand ?? (removeCommand = new RelayCommand(
                 () =>
                 {
                     navigation.Navigate<MenyuViewModel>();
                 }
                 ));
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get => okCommand ?? (okCommand = new RelayCommand(
                 () =>
                 {
                     navigation.Navigate<MenyuViewModel>();
                 }
                 ));
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

        private RelayCommand mapCommand;
        public RelayCommand MapCommand
        {
            get => mapCommand ?? (mapCommand = new RelayCommand(
                 () =>
                 {
                     navigation.Navigate<MapViewModel>();
                 }
                 ));
        }

        private RelayCommand searchInfoCommand;
        public RelayCommand SearchInfoCommand
        {
            get => searchInfoCommand ?? (searchInfoCommand = new RelayCommand(
                 () =>
                 {
                     navigation.Navigate<SearchInfoViewModel>();
                 }
                 ));
        }
        

        public override string ToString()
        {
            return "Страница списка путешествий";
        }
    }
}
