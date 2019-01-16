using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class Trip : INotifyPropertyChanged
    {
        private string name;
        public string Name { get => name; set => Set(ref name, value); }

        private ObservableCollection<CityInfo> cityInfo;
        public ObservableCollection<CityInfo> CityInfo { get => cityInfo; set => Set(ref cityInfo, value); }

        private string cityImage;
        public string CityImage { get => cityImage; set => Set(ref cityImage, value); }

        private string note;
        public string Note { get => note; set => Set(ref note, value); }

        public Trip()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
