using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class Appeal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string _appeal;
        public string appeal { get => _appeal; set => Set(ref _appeal, value); }

        public Appeal()
        {

        }

        public Appeal(string appeal)
        {
            this.appeal = appeal;
        }

        public ObservableCollection<Appeal> getList()
        {
            ObservableCollection<Appeal> list = new ObservableCollection<Appeal>();
            list.Add(new Appeal(appeal = "Mr.–Mister(обращение к мужчине)"));
            list.Add(new Appeal(appeal = "Mrs.–Misses(обращение к замужней женщине)"));
            list.Add(new Appeal(appeal = "Miss–Miss(обращение к незамужней женщине)"));
            list.Add(new Appeal(appeal = "Ms–Mizz(обращение как к замужней, так и незамужней женщине)"));
            return list;
        }

        public override string ToString()
        {
            return appeal;
        }
    }
}
