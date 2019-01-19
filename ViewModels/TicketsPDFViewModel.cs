using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    class TicketsPDFViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private string pathPDF;// = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
        public string PathPDF { get => pathPDF; set => Set(ref pathPDF, value); }

        private readonly IMyNavigationService navigation;
        public TicketsPDFViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            PathPDF = @"C:\Users\Zaka_oz49\Downloads\WPF_urok.pdf";

            //Messenger.Default.Register<string>(this,
            //    msg =>
            //    {
            //        UserNick = msg;
            //    });
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

        private RelayCommand clickCommand;
        public RelayCommand ClickCommand
        {
            get => clickCommand ?? (clickCommand = new RelayCommand(
                () =>
                {
                    PathPDF = @"C:\Users\Zaka_oz49\Downloads\WPF_urok.pdf";
                }
                ));
        }

        public override string ToString()
        {
            return "Страница просмотра билета";
        }
    }

    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                var nUri = new Uri(@"C:\Users\Zaka_oz49\Downloads\WPF_urok.pdf");
                browser.Source = nUri; //!String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }

    }
}
