using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class WeatherViewModel : ViewModelBase
    {
        //private ViewModelBase back;
        //public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string city = "Moscow";
        public string City { get => city; set => Set(ref city, value); }

        private string cityName;
        public string CityName { get => cityName; set => Set(ref cityName, value); }

        private string country;
        public string Country { get => country; set => Set(ref country, value); }

        private string temp;
        public string Temp { get => temp; set => Set(ref temp, value); }

        private string image;
        public string Image { get => image; set => Set(ref image, value); }

        private string condition;
        public string Condition { get => condition; set => Set(ref condition, value); }

        private string discription;
        public string Discription { get => discription; set => Set(ref discription, value); }

        private string temp2;
        public string Temp2 { get => temp2; set => Set(ref temp2, value); }

        private string windSpeed;
        public string WindSpeed { get => windSpeed; set => Set(ref windSpeed, value); }

        private string _Image2;
        public string Image2 { get => _Image2; set => Set(ref _Image2, value); }

        private ObservableCollection<weatherForcast.list> weatherList = new ObservableCollection<weatherForcast.list>();
        public ObservableCollection<weatherForcast.list> WeatherList { get => weatherList; set => Set(ref weatherList, value); } 

        private readonly IMyNavigationService navigation;

        public WeatherViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            ViewWeather(City);
        }

        private RelayCommand _BackCommand;
        public RelayCommand BackCommand
        {
            get => _BackCommand ?? (_BackCommand = new RelayCommand(
                 () =>
                 {
                     navigation.Navigate<MenyuViewModel>();
                 }
                 ));
        }

        private RelayCommand _WeatherOkCommand;
        public RelayCommand WeatherOkCommand
        {
            get => _WeatherOkCommand ?? (_WeatherOkCommand = new RelayCommand(
                 () =>
                 {
                     ViewWeather(City);
                 }
                 ));
        }

        void ViewWeather(string city)
        {
            Weather weatherInfo = new Weather();
            WeatherInfo.Root result = new WeatherInfo.Root();
            try
            {
                result = weatherInfo.GetWeather(city);
            }
            catch (Exception)
            {
                MessageBox.Show("city not found");
                return;
            }
            
            CityName = String.Format("{0}", result.name);
            Country = String.Format("{0}", result.sys.country);
            Temp = String.Format("{0} °С", result.main.temp); //°F  °С  ℃
            Image = String.Format("http://openweathermap.org/img/w/{0}.png", result.weather[0].icon);

            var Object = weatherInfo.GetForcast(city) ?? new weatherForcast.Root();

            if (Object != null)
            {
                Condition = String.Format("{0}", Object.list[0].weather[0].main);
                Discription = String.Format("{0}", Object.list[0].weather[0].description);
                Temp2 = String.Format("{0} °С", Object.list[0].main.temp);
                WindSpeed = String.Format("{0} km/h", Object.list[0].wind.speed);
                Image2 = String.Format("http://openweathermap.org/img/w/{0}.png", Object.list[0].weather[0].icon);

                WeatherList.Clear();
                for (int i = 0; i < Object.list.Count; i++)
                {
                    WeatherList.Add(Object.list[i]);
                    WeatherList[i].weather[0].iconPaht = String.Format("http://openweathermap.org/img/w/{0}.png", Object.list[0].weather[0].icon);
                }
            }
        }

        public override string ToString()
        {
            return "Страница просмотра по городу погоды";
        }
    }
}
