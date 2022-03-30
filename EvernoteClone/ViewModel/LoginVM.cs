using EvernoteClone.Models;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helper;
using System;
using System.ComponentModel;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private bool isShowingRegister = false;

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private Visibility loginVis;
        private Visibility registerVis;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler Authenticated;
        public Visibility LoginVis
        {
            get { return loginVis; }
            set
            {
                loginVis = value;
                OnPropertyChanged("LoginVis");

            }
        }
        public Visibility RegisterVis
        {
            get { return registerVis; }
            set
            {
                registerVis = value;
                OnPropertyChanged("RegisterVis");

            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                User = new User
                {
                    Name = this.name,
                    Lastname = this.LastName,
                    Username = username,
                    Password = this.password,
                    ConfirmPassword = this.confirmPassword
                };
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;

                User = new User
                {
                    Name = this.name,
                    Lastname = this.LastName,
                    Username = this.username,
                    Password = password,
                    ConfirmPassword = this.confirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                User = new User
                {
                    Name = name,
                    Lastname = this.LastName,
                    Username = this.username,
                    Password = this.password,
                    ConfirmPassword = this.confirmPassword
                };
                OnPropertyChanged("Name");
            }
        }

        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;

                User = new User
                {
                    Name = this.name,
                    Lastname = this.LastName,
                    Username = this.username,
                    Password = this.password,
                    ConfirmPassword = this.confirmPassword

                };
                OnPropertyChanged("Lastname");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;

                User = new User
                {
                    Name = this.name,
                    Lastname = this.LastName,
                    Username = this.username,
                    Password = this.password,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        public LoginVM()
        {
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;

            if (isShowingRegister)
            {
                RegisterVis = Visibility.Visible;
                LoginVis = Visibility.Collapsed;
            }
            else
            {
                LoginVis = Visibility.Visible;
                RegisterVis = Visibility.Collapsed;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async void Login()
        {
            bool result = await FirebaseAuthHelper.Login(User);

            if (result)
            {
                Authenticated?.Invoke(this, new EventArgs());

            }
        }
        public async void Register()
        {
            bool result = await FirebaseAuthHelper.Register(User);

            if (result)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

    }
}
