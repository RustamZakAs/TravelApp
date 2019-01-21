using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Messages;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class SearchInfoViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private CityInfo selectedCity;
        public CityInfo SelectedCity { get => selectedCity; set => Set(ref selectedCity, value); }

        private readonly IMyNavigationService navigation;
        public SearchInfoViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            Messenger.Default.Register<SearchInfoMessage>(this,
               msg =>
               {
                   UserNick = msg.UserNick;
                   Back = msg.Back;
                   SelectedCity = msg.CityInfo;
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
    }
}
