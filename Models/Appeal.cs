using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class Appeal
    {
        public string appeal { get; set; }

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
