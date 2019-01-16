using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Services;
using TravelApp.ViewModels;

namespace TravelApp
{
    class ViewModelLocator
    {
        private AppViewModel          appViewModel;
        private LogInViewModel        logInViewModel;
        private MenyuViewModel        menyuViewModel;
        private TripsViewModel        tripsViewModel;
        private RegistrationViewModel registrationViewModel;
        private WeatherViewModel      weatherViewModel;
        private CitiesViewModel       citiesViewModel;
        private IntroViewModel        introViewModel;

        private IMyNavigationService  myNavigationService;

        public ViewModelLocator()
        {
            myNavigationService = new MyNavigationService();

            appViewModel          = new AppViewModel(myNavigationService);
            logInViewModel        = new LogInViewModel(myNavigationService);
            menyuViewModel        = new MenyuViewModel(myNavigationService);
            tripsViewModel        = new TripsViewModel(myNavigationService);
            registrationViewModel = new RegistrationViewModel(myNavigationService);
            weatherViewModel      = new WeatherViewModel(myNavigationService);
            citiesViewModel       = new CitiesViewModel(myNavigationService);
            introViewModel        = new IntroViewModel(myNavigationService);

            myNavigationService.Register<LogInViewModel>(logInViewModel);
            myNavigationService.Register<MenyuViewModel>(menyuViewModel);
            myNavigationService.Register<TripsViewModel>(tripsViewModel);
            myNavigationService.Register<RegistrationViewModel>(registrationViewModel);
            myNavigationService.Register<WeatherViewModel>(weatherViewModel);
            myNavigationService.Register<CitiesViewModel>(citiesViewModel);
            myNavigationService.Register<IntroViewModel>(introViewModel);

            //myNavigationService.Register("LogIn", logInViewModel);
            //myNavigationService.Register("Menyu", menyuViewModel);
            //myNavigationService.Register("Trips", tripsViewModel);
            //myNavigationService.Register("Registration", registrationViewModel);

            //myNavigationService.Navigate<LogInViewModel>();
            myNavigationService.Navigate<IntroViewModel>();

            //myNavigationService.Navigate("LogIn");
        }

        public ViewModelBase GetAppViewModel()
        {
            return appViewModel;
        }
    }
}
