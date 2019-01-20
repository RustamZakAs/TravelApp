using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Services;

namespace TravelApp.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        private ViewModelBase currentPage;
        public ViewModelBase CurrentPage
        {
            get => currentPage;
            set => Set(ref currentPage, value);
        }

        private readonly IMyNavigationService navigator;
        public AppViewModel(IMyNavigationService navigation)
        {
            this.navigator = navigation;
            Messenger.Default.Register<ViewModelBase>(this, viewModel => CurrentPage = viewModel);
        }
    }
}
