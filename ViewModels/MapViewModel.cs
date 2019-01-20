using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Messages;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class MapViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private Microsoft.Maps.MapControl.WPF.Location mapCenter;
        public Microsoft.Maps.MapControl.WPF.Location MapCenter { get => mapCenter; set => Set(ref mapCenter, value); }

        public ObservableCollection<MapLocation> Locations { get; private set; } = new ObservableCollection<MapLocation>();

        private readonly IMyNavigationService navigation;
        public MapViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Microsoft.Maps.MapControl.WPF.Location location = new Microsoft.Maps.MapControl.WPF.Location();
            location.Latitude = 40.414898;
            location.Longitude = 49.853107;
            location.Altitude = 4000.00;

            MapCenter = location;

            MapLocation mapLocation = new MapLocation();
            mapLocation.Location.Latitude = 40.414898;
            mapLocation.Location.Longitude = 49.853107;

            MapLocation mapLocation1 = new MapLocation();
            mapLocation1.Location.Latitude = 40.414898;
            mapLocation1.Location.Longitude = 48.853107;
            //mapLocation.Location.Altitude = 40.00;
            //mapLocation.Location.Course = 0;
            //mapLocation.Location.HorizontalAccuracy = 1;
            //mapLocation.Location.VerticalAccuracy = 1;
            //mapLocation.Location.Speed = 10;
            mapLocation.Name = "IT STEP Academy";
            mapLocation1.Name = "IT STEP Academy 1";
            Locations.Add(mapLocation);
            Locations.Add(mapLocation1);


            Messenger.Default.Register<MenyuMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
               });
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
            return "Страница просмотра карты";
        }
    }

    public class MapLocation
    {
        public MapLocation()
        {
            Location = new System.Device.Location.GeoCoordinate();
        }
        public System.Device.Location.GeoCoordinate Location { get; set; }
        public string Name { get; set; }
    }
}
