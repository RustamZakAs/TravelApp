using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace TravelApp.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {

        }
        public DbSet<Registration> DSRegistration { get; set; }
    }

    public class Registration : INotifyPropertyChanged
    {
        public Registration()
        {
            UserBirdth = DateTime.Parse(DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
        }

        [Key]
        private string id;
        public string Id { get => id; set => Set(ref id, value); }

        [MaxLength(30)]
        private string userName;
        public string UserName { get => userName; set => Set(ref userName, value); }

        [MaxLength(30)]
        private string userSurname;
        public string UserSurname { get => userSurname; set => Set(ref userSurname, value); }

        private DateTime userBirdth;
        public DateTime UserBirdth { get => userBirdth; set => Set(ref userBirdth, value); }

        [MaxLength(30)]
        private string userNick;
        public string UserNick { get => userNick; set => Set(ref userNick, value); }

        [MaxLength(30)]
        private string appeal;
        public string Appeal { get => appeal; set => Set(ref appeal, value); }

        [EmailAddress]
        private string userEmail;
        public string UserEmail { get => userEmail; set => Set(ref userEmail, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
