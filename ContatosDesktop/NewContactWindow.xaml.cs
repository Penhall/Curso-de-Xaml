using ContatosDesktop.Models;
using SQLite;
using System.Windows;

namespace ContatosDesktop
{
    /// <summary>
    /// Lógica interna para NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Contact  contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Fone = foneTextBox.Text
            };

            using (SQLiteConnection connection =new SQLiteConnection(App.databasePath)){

                connection.CreateTable<Contact>();
                connection.Insert(contact);

            }

            Close();
        }
    }
}
