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

        //private ViewModelBase back;
        //public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private readonly IMyNavigationService navigation;

        public TripsViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<TripsMessage>(this,
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
    }
}
