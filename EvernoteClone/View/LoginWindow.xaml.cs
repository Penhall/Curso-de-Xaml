using EvernoteClone.ViewModel;
using System;
using System.Windows;

namespace EvernoteClone.View
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginVM VM;
        public LoginWindow()
        {
            InitializeComponent();

            VM = (LoginVM)Resources["vm"];

            VM.Authenticated += VM_Authenticated;

        }

        private void VM_Authenticated(object? sender, EventArgs e)
        {

            Close();
        }
    }
}
