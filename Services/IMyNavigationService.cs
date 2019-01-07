using GalaSoft.MvvmLight;
using System;

namespace TravelApp.Services
{
    public interface IMyNavigationService
    {
        //void Navigate(string viewName);
        //void Register(string viewName, ViewModelBase viewModel);

        void Navigate<T>();
        void Navigate(Type type);
        void Register<T>(ViewModelBase viewModel);
    }
}
