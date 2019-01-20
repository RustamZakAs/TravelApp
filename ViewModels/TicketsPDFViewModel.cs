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
using TravelApp.Messages;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    class TicketsPDFViewModel : ViewModelBase
    {
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        private ViewModelBase back;
        public ViewModelBase Back { get => back; set => Set(ref back, value); }

        private string pathPDF;// = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
        public string PathPDF { get => pathPDF; set => Set(ref pathPDF, value); }

        private readonly IMyNavigationService navigation;
        public TicketsPDFViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            PathPDF = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";

            Messenger.Default.Register<TicketsPDFMessage>(this,
                msg =>
                {
                    UserNick = msg.UserNick;
                    Back = msg.Back;
                });
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

        private RelayCommand clickCommand;
        public RelayCommand ClickCommand
        {
            get => clickCommand ?? (clickCommand = new RelayCommand(
                () =>
                {
                    PathPDF = @"C:\Users\User\source\repos\TravelApp\Resources\dypexamarea.pdf";
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
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }

    }
}
