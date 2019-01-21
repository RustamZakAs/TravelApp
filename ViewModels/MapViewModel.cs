using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Messages;
using TravelApp.Models;
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

        private readonly IMyNavigationService navigation;
        public MapViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<MapMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
                   Back = msg.Back;
                   MapCenter = msg.MapCenter;
                   if (MapCenter == null)
                   {
                       IpInfo ipInfo = Ip.GetUserCountryByIp();
                       MapCenter = new Microsoft.Maps.MapControl.WPF.Location();
                       MapCenter.Latitude = ipInfo.Latitude;
                       MapCenter.Longitude = ipInfo.Longitude;
                   }
               });
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

        public override string ToString()
        {
            return "Страница просмотра карты";
        }
    }

    public class MapLocation : INotifyPropertyChanged
    {
        public MapLocation()
        {
            Location = new System.Device.Location.GeoCoordinate();
        }
        private string name;
        public string Name { get => name; set => Set(ref name, value); }

        private System.Device.Location.GeoCoordinate location;
        public System.Device.Location.GeoCoordinate Location { get => location; set => Set(ref location, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
