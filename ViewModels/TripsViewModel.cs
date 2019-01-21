using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelApp.Messages;
using TravelApp.Models;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class TripsViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private ObservableCollection<Trip> tripList;
        public ObservableCollection<Trip> TripList { get => tripList; set => Set(ref tripList, value); }

        private Trip selectedTrip;
        public Trip SelectedTrip { get => selectedTrip; set => Set(ref selectedTrip, value); }

        private CityInfo selectedCity;
        public CityInfo SelectedCity { get => selectedCity; set => Set(ref selectedCity, value); }

        //private CityInfo inCityInfo;
        //public CityInfo InCityInfo { get => inCityInfo; set => Set(ref inCityInfo, value); }

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

            TripList = new ObservableCollection<Trip>();

            Messenger.Default.Register<NotificationMessage<CityInfo>>(this, OnHitIt);

            Messenger.Default.Register<TripsMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
                   Back = msg.Back;
               });
        }

        private void OnHitIt(NotificationMessage<CityInfo> nc)
        {
            if (nc.Notification == "SelectCity")
            {
                var tr = nc.Content;
                //SelectedTrip = TripList[TripList.Count - 1];
                if (SelectedTrip.CityInfo == null)
                    SelectedTrip.CityInfo = new ObservableCollection<CityInfo>();
                SelectedTrip.CityInfo.Add(tr);

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
                     navigation?.Navigate(Back.GetType());
                 }
                 ));
        }

        private MyRelayCommand addTripCommand;
        public MyRelayCommand AddTripCommand
        {
            get => addTripCommand ?? (addTripCommand = new MyRelayCommand(
                 param =>
                 {
                     //var task = param as Trip;
                     string s = String.Format(@"Trip {0}",TripList.Count + 1);
                     TripList.Add(new Trip(s));
                     

                     //navigation.Navigate<CitiesViewModel>();
                     //if (SelectedTrip.CityInfo != null && SelectedTrip.CityInfo[0] != null) MessageBox.Show(SelectedTrip.CityInfo[0].name);
                 }
                 ));
        }

        private MyRelayCommand removeTripCommand;
        public MyRelayCommand RemoveTripCommand
        {
            get => removeTripCommand ?? (removeTripCommand = new MyRelayCommand(
                 param =>
                 {
                     if (System.Windows.MessageBox.Show("Remove \""+ SelectedTrip.Name + "\" Trip?", "Travel Application", 
                                                             System.Windows.MessageBoxButton.YesNo,
                                                             System.Windows.MessageBoxImage.Warning) == System.Windows.MessageBoxResult.Yes)
                     {
                         TripList.Remove(SelectedTrip);
                     }
                     
                     //navigation.Navigate<MenyuViewModel>();
                 }
                 ,param => (SelectedTrip != null)
                 ));
        }

        private MyRelayCommand saveCommand;
        public MyRelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new MyRelayCommand(
                 param =>
                 {
                     
                 },
                 param => (true)
                 ));
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get => okCommand ?? (okCommand = new RelayCommand(
                 () =>
                 {
                 }
                 ));
        }

        private RelayCommand weatherCommand;
        public RelayCommand WeatherCommand
        {
            get => weatherCommand ?? (weatherCommand = new RelayCommand(
                 () =>
                 {
                     if (SelectedCity != null && !String.IsNullOrWhiteSpace(SelectedCity.name))
                     {
                         Messenger.Default.Send(new WeatherMessage { UserNick = UserNick, City = SelectedCity.name, Back = this });
                         navigation.Navigate<WeatherViewModel>();
                     }
                 }
                 ));
        }

        private RelayCommand mapCommand;
        public RelayCommand MapCommand
        {
            get => mapCommand ?? (mapCommand = new RelayCommand(
                 () =>
                 {
                     if (SelectedCity != null && !String.IsNullOrWhiteSpace(SelectedCity.name))
                     {
                         Microsoft.Maps.MapControl.WPF.Location MapCenter = new Microsoft.Maps.MapControl.WPF.Location();
                         MapCenter.Latitude = SelectedCity.coord.lat;
                         MapCenter.Longitude = SelectedCity.coord.lon;

                         Messenger.Default.Send(new MapMessage { UserNick = UserNick, MapCenter = MapCenter, Back = this });
                         navigation.Navigate<MapViewModel>();
                     }
                 }
                 ));
        }

        private RelayCommand searchInfoCommand;
        public RelayCommand SearchInfoCommand
        {
            get => searchInfoCommand ?? (searchInfoCommand = new RelayCommand(
                 () =>
                 {
                     Messenger.Default.Send(new SearchInfoMessage { UserNick = UserNick, CityInfo = SelectedCity, Back = this });
                     navigation.Navigate<SearchInfoViewModel>();
                 }
                 ));
        }

        
        private MyRelayCommand addCityCommand;
        public MyRelayCommand AddCityCommand
        {
            get => addCityCommand ?? (addCityCommand = new MyRelayCommand(
                 param =>
                 {
                     SelectedTrip = param as Trip;
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
        
        private MyRelayCommand removeCityCommand;
        public MyRelayCommand RemoveCityCommand
        {
            get => removeCityCommand ?? (removeCityCommand = new MyRelayCommand(
                 param =>
                 {
                     SelectedTrip = param as Trip;
                     if (SelectedCity != null)
                     {
                         string str = String.Format(@"Remove from '{0}' - '{1}' city?", SelectedTrip.Name, SelectedCity.name);
                         if (System.Windows.MessageBox.Show(str, "Travel Application",
                                                             System.Windows.MessageBoxButton.YesNo,
                                                             System.Windows.MessageBoxImage.Warning) == System.Windows.MessageBoxResult.Yes)
                         {
                             TripList.Where(x => x == this.SelectedTrip).First().CityInfo.Remove(SelectedCity);
                         }
                     }
                     //TripList.Where(x => x == SelectedTrip); 
                     //TripList.Where(x => x == this.selectedTrip).First().CityInfo.Where(y => y == this.SelectedCity);
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
