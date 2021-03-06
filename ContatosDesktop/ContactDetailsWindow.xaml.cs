using ContatosDesktop.Models;
using SQLite;
using System.Windows;

namespace ContatosDesktop
{
    /// <summary>
    /// Lógica interna para ContactDetailsWindows.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {

        Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;


            this.contact = contact;

            nameTextBox.Text = contact.Name;

            emailTextBox.Text = contact.Email;

            foneTextBox.Text = contact.Fone;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;

            contact.Email = emailTextBox.Text;

            contact.Fone = foneTextBox.Text;


            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {

                connection.CreateTable<Contact>();
                connection.Update(contact);

            }

            Close();

        }

        private void deleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {

                connection.CreateTable<Contact>();
                connection.Delete(contact);

            }

            Close();
        }
    }
}
