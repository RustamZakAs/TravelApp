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
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class CitiesViewModel : ViewModelBase
    {
        private ObservableCollection<CityInfo> cityList;
        public ObservableCollection<CityInfo> CityList { get => cityList; set => Set(ref cityList, value); }

        private CityInfo selectedCity;
        public CityInfo SelectedCity { get => selectedCity; set => Set(ref selectedCity, value); }

        private readonly IMyNavigationService navigation;

        public CitiesViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;
            //SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();

            City city = new City();
            CityList = city.СityList;
            MessageBox.Show(CityList[1].name);
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
    }
}
