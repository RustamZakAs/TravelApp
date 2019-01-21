using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Messages;
using TravelApp.Models;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class WeatherViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private string city;
        public string City { get => city; set => Set(ref city, value); }

        private WeatherShow bindWeather;
        public WeatherShow BindWeather { get => bindWeather; set => Set(ref bindWeather, value); }

        private ObservableCollection<weatherForcast.list> weatherList = new ObservableCollection<weatherForcast.list>();
        public ObservableCollection<weatherForcast.list> WeatherList { get => weatherList; set => Set(ref weatherList, value); } 

        private readonly IMyNavigationService navigation;
        public WeatherViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;
            BindWeather = new WeatherShow();
            Messenger.Default.Register<WeatherMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
                   City = msg.City;
                   Back = msg.Back;
               });

            City = Ip.GetUserCountryByIp(Ip.GetIP());

            ViewWeather(City);
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

        private RelayCommand weatherOkCommand;
        public RelayCommand WeatherOkCommand
        {
            get => weatherOkCommand ?? (weatherOkCommand = new RelayCommand(
                 () =>
                 {
                     if (City == null || City.Length == 0) City = Ip.GetUserCountryByIp(Ip.GetIP());
                     try
                     {
                         Task.Run(() =>
                         {
                             //ViewWeather(City);
                         });
                     }
                     catch (Exception)
                     {
                         ViewWeather(City);
                     }
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
                //MessageBox.Show("city not found");
                return;
            }

            BindWeather.CityName = String.Format("{0}", result.name);
            BindWeather.DateTime = String.Format("{0}", result.dt);
            BindWeather.Country = String.Format("{0}", result.sys.country);
            BindWeather.Temp = String.Format("{0} °С", result.main.temp); //°F  °С  ℃
            BindWeather.Image = String.Format("http://openweathermap.org/img/w/{0}.png", result.weather[0].icon);

            weatherForcast.Root MyObject = weatherInfo.GetForcast(city) ?? new weatherForcast.Root();

            if (MyObject != null)
            {
                //BindWeather.Condition = String.Format("{0}", MyObject.list[0].weather[0].main);
                //BindWeather.DateTime = String.Format("{0}", MyObject.list[0].dt_txt);
                //BindWeather.Discription = String.Format("{0}", MyObject.list[0].weather[0].description);
                //BindWeather.Temp2 = String.Format("{0} °С", MyObject.list[0].main.temp);
                //BindWeather.WindSpeed = String.Format("{0} km/h", MyObject.list[0].wind.speed);
                //BindWeather.Image2 = String.Format("http://openweathermap.org/img/w/{0}.png", MyObject.list[0].weather[0].icon);

                if (WeatherList.Count > 0)
                    WeatherList.Clear();
                for (int i = 0; i < MyObject.list.Count; i++)
                {
                    WeatherList.Add(MyObject.list[i]);
                    WeatherList[i].weather[0].iconPaht = String.Format("http://openweathermap.org/img/w/{0}.png", MyObject.list[0].weather[0].icon);
                }
            }
        }

        public override string ToString()
        {
            return "Страница просмотра по городу погоды";
        }
    }
}
