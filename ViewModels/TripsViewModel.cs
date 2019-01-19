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

        private CityInfo selectedCity = new CityInfo();
        public CityInfo SelectedCity { get => selectedCity; set => Set(ref selectedCity, value); }

        private CityInfo inCityInfo = new CityInfo();
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
                //SelectedTrip = TripList[TripList.Count - 1];
                if (TripList[TripList.Count - 1].CityInfo == null)
                    TripList[TripList.Count - 1].CityInfo = new ObservableCollection<CityInfo>();
                TripList[TripList.Count - 1].CityInfo.Add(tr);
                //if (tr != null)
                //{
                //    //TripList.Where(x => x == this.SelectedTrip).First().CityInfo.Add(tr);
                //    var z = (ObservableCollection<Trip>)TripList.Where(x => x == this.SelectedTrip);
                //    z[0].CityInfo.Add(tr);
                //}
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
                     string s = String.Format(@"Trip {0}",TripList.Count);
                     TripList.Add(new Trip(s));
                     //navigation.Navigate<CitiesViewModel>();
                     //if (SelectedTrip.CityInfo != null && SelectedTrip.CityInfo[0] != null) MessageBox.Show(SelectedTrip.CityInfo[0].name);
                 }
                 ));
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get => removeCommand ?? (removeCommand = new RelayCommand(
                 () =>
                 {
                     TripList.Remove(SelectedTrip);
                     //navigation.Navigate<MenyuViewModel>();
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

        
        private RelayCommand addCityCommand;
        public RelayCommand AddCityCommand
        {
            get => addCityCommand ?? (addCityCommand = new RelayCommand(
                 () =>
                 {
                     //TripList.Where(x => x == this.selectedTrip).First().CityInfo.Add(new CityInfo());
                     //var selectedTrip = 
                     //var selectedCity = selectedTrip.CityInfo.Where(x => x == this.SelectedCity).First();
                     //selectedTrip.CityInfo.Add(new CityInfo());
                     navigation.Navigate<CitiesViewModel>();
                     //if (SelectedTrip.CityInfo != null && SelectedTrip.CityInfo[0] != null)
                     //    MessageBox.Show(SelectedTrip.CityInfo[0].name);
                 }
                 ));
        }
        
        private RelayCommand removeCityCommand;
        public RelayCommand RemoveCityCommand
        {
            get => removeCityCommand ?? (removeCityCommand = new RelayCommand(
                 () =>
                 {
                     //TripList.Where(x => x == SelectedTrip); 
                     //TripList.Where(x => x == this.selectedTrip).First().CityInfo.Where(y => y == this.SelectedCity);
                     TripList.Where(x => x == this.SelectedTrip).First().CityInfo.Remove(SelectedCity);
                     //TripList.Re
                     //var selectedTrip = 
                     //var selectedCity = selectedTrip.CityInfo.Where(x => x == this.SelectedCity).First();
                     //selectedTrip.CityInfo.Add(new CityInfo());
                     //navigation.Navigate<CitiesViewModel>();
                     //if (SelectedTrip.CityInfo != null && SelectedTrip.CityInfo[0] != null)
                     //    MessageBox.Show(SelectedTrip.CityInfo[0].name);
                 }
                 ));
        }

        public override string ToString()
        {
            return "Страница списка путешествий";
        }
    }
}
