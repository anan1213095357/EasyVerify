

using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace EasyVerify.Data.View
{
    public class UserInfo : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChange(nameof(Name));
            }
        }
        private string _account;
        public string Account
        {
            get { return _account; }
            set
            {
                _account = value;
                NotifyPropertyChange(nameof(Account));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChange(nameof(Password));
            }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                NotifyPropertyChange(nameof(Phone));
            }
        }
        private string _MachineCode;
        public string MachineCode
        {
            get { return _MachineCode; }
            set
            {
                _MachineCode = value;
                NotifyPropertyChange(nameof(MachineCode));
            }
        }
        private bool _Frozen;
        public bool Frozen
        {
            get { return _Frozen; }
            set
            {
                _Frozen = value;
                NotifyPropertyChange(nameof(Frozen));
            }
        }
        private UserLevel _level;
        public UserLevel UserLevel
        {
            get { return _level; }
            set
            {
                _level = value;
                NotifyPropertyChange(nameof(UserLevel));
            }
        }
        private DateTime _ExpirationTime;
        public DateTime ExpirationTime
        {
            get { return _ExpirationTime; }
            set
            {
                _ExpirationTime = value;
                NotifyPropertyChange(nameof(ExpirationTime));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
