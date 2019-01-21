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
    class CitiesViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private ObservableCollection<CityInfo> cityList;
        public ObservableCollection<CityInfo> CityList { get => cityList; set => Set(ref cityList, value); }

        private ObservableCollection<CityInfo> searchCityList;
        public ObservableCollection<CityInfo> SearchCityList { get => searchCityList; set => Set(ref searchCityList, value); }

        private bool selectCity;
        public bool SelectCity { get => selectCity; set => Set(ref selectCity, value); }

        private CityInfo selectedCity;
        public CityInfo SelectedCity
        {
            get => selectedCity;
            set
            {
                Set(ref selectedCity, value);

                if(value != null)
                {
                    string cityName = SelectedCity.name.ToLower();

                    Task.Run(() =>
                    {
                        CityImage.Root images = cityImages.GetImages(cityName);
                        if (images != null)
                        {
                            ImagePath = images.photos[0].image.mobile;
                            selectedCity.imageMobile = ImagePath;
                            selectedCity.imageWeb = images.photos[0].image.web;
                        }
                    });
                }
            }
        }

        private string imagePath;// = @"https://d13k13wj6adfdf.cloudfront.net/urban_areas/baku-0aa6019508.jpg";
        public string ImagePath { get => imagePath; set => Set(ref imagePath, value); }

        public CityImage cityImages = new CityImage();

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                Set(ref searchText, value);
                SearchCityList = MySearch(CityList, value, false);
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

            Messenger.Default.Register<CityMessage>(this,
              msg =>
              {
                  UserNick = msg.UserNick;
                  Back = msg.Back;
                  SelectCity = msg.SelectCity;
                  SelectedCity = msg.CityInfo;
                  if (SelectedCity == null || SelectedCity.name == null)
                  {
                      SearchText = Ip.GetUserCountryByIp().City;
                  }
              });
        }

        private ObservableCollection<CityInfo> MySearch(ObservableCollection<CityInfo> cities, string value, bool isMany = false)
        {
            if (cities != null && cities.Count > 0)
            {
                if (isMany)
                {
                    ObservableCollection<CityInfo> result = new ObservableCollection<CityInfo>();
                    string[] words = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length; i++)
                    {
                        var linqResults = from city in cities
                                          where city.country.Contains(words[i]) ||
                                          city.name.Contains(words[i])
                                          select city;

                        foreach (var item in new ObservableCollection<CityInfo>(linqResults))
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    var linqResults = from city in cities
                                      where city.country.ToLower().Contains(value.ToString()) ||
                                      city.name.ToLower().Contains(value.ToLower())
                                      select city;
                    //var linqResults = from city in cities
                    //                  where city.country.Contains(value) ||
                    //                  city.name.Contains(value)
                    //                  select city;
                    return new ObservableCollection<CityInfo>(linqResults);
                }
            }
            else return cities;
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

        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get => selectCommand ?? (selectCommand = new RelayCommand(
                 () =>
                 {
                     //Messenger.Default.Send(new CityMessage { CityInfo = SelectedCity }); //Rustam
                     Messenger.Default.Send(new NotificationMessage<CityInfo>(SelectedCity, "SelectCity")); //Diana
                     navigation.Navigate<TripsViewModel>();
                 }
                 ));
        }

        public override string ToString()
        {
            return "Страница выбора и просмотра города";
        }
    }
}
