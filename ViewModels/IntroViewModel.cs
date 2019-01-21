using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    class IntroViewModel : ViewModelBase
    {
        private readonly IMyNavigationService navigation;

        private Uri introVideo;
        public Uri IntroVideo { get => introVideo; set => Set(ref introVideo, value); }

        private TimeSpan introVideoPosition;
        public TimeSpan IntroVideoPosition { get => introVideoPosition; set => Set(ref introVideoPosition, value); }

        DispatcherTimer timer = new DispatcherTimer();

        int introTime = 1;//6650;

        public IntroViewModel(IMyNavigationService navigation)
        {
            this.navigation = navigation;

            

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IntroVideo = new Uri(directory + @"\Resources\CutIntroMobile.avi", UriKind.Absolute);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Thread.Sleep(introTime);
                navigation.Navigate<LogInViewModel>();
            });
            timer.Stop();

            //if (thePlayer.Source != null)
            //{
            //    if (thePlayer.NaturalDuration.HasTimeSpan)
            //        if (thePlayer.Position == TimeSpan.Parse("00:10"))
            //            navigation.Navigate<LogInViewModel>();
            //    //lblStatus.Content = String.Format("{0} / {1}", thePlayer.Position.ToString(@"mm\:ss"), thePlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            //}
            //else
            //    lblStatus.Content = "No file selected...";
        }

        private RelayCommand playCommand;
        public RelayCommand PlayCommand
        {
            get => playCommand ?? (playCommand = new RelayCommand(
                () =>
                {
                    //Messenger.Default.Send(new MenyuMessage { UserNick = UserNick });
                    //navigation.Navigate<MenyuViewModel>();

                    //Task.Run(() =>
                    //{

                    //});
                    //thePlayer.Play();
                }
                ));
        }

        //private MediaState mediaState = MediaState.Stoped;
        //internal MediaState MediaState
        //{
        //    get
        //    {
        //        return mediaState;
        //    }
        //    set
        //    {
        //        mediaState = value;
        //        //FirePropertyChanged("PlayButtonName");
        //    }
        //}

        //public string PlayButtonName
        //{
        //    get
        //    {
        //        return (MediaState == MediaState.Playing) ? "Pause" : "Play";
        //    }
        //}

        //enum MediaState
        //{
        //    Playing = 1,
        //    Paused = 2,
        //    Stoped = 3
        //}

        public override string ToString()
        {
            return "©" + "RustamZakAs";
        }
    }
}
