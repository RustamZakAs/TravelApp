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

        private ObservableCollection<CityInfo> searchCityList;
        public ObservableCollection<CityInfo> SearchCityList { get => searchCityList; set => Set(ref searchCityList, value); }

        private CityInfo selectedCity;
        public CityInfo SelectedCity { get => selectedCity; set => Set(ref selectedCity, value); }

        private string imagePath;
        public string ImagePath { get => imagePath; set => Set(ref imagePath, value); }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                Set(ref searchText, value);
                SearchCityList = MySearch(CityList, value);
            }
        }

        private readonly IMyNavigationService navigation;

        public CitiesViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;
            SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();

            City city = new City();
            SearchCityList = CityList = city.CityList;
            //MessageBox.Show(CityList[1].name);

        }

        private ObservableCollection<CityInfo> MySearch(ObservableCollection<CityInfo> cities, string value)
        {
            if (cities != null && cities.Count > 0)
            {
                var linqResults = from city in cities
                                  where city.country.Contains(value) ||
                                  city.name.Contains(value)
                                  select city;
                return new ObservableCollection<CityInfo>(linqResults);
            }
            else return cities;
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
