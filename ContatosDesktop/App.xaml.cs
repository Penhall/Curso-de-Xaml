using System;
using System.Windows;

namespace ContatosDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        static string databaseName = "Contacts.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);

    }
}
