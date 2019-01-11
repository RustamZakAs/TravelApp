using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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
        private readonly IMyNavigationService navigation;

        public CitiesViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;
            SQLiteDatabase _SQLiteDatabase = new SQLiteDatabase();

            City city = new City();
            MessageBox.Show(city.СityList[0].name);
        }
    }
}
