using ContatosDesktop.Models;
using System.Windows;
using System.Windows.Controls;

namespace ContatosDesktop.Controls
{
    public partial class ContactControl : UserControl
    {



        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl control = (ContactControl)d;

            if (control != null)
            {
                control.nameTextBlock.Text = (e.NewValue as Contact).Name;
                control.emailTextBlock.Text = (e.NewValue as Contact).Email;
                control.foneTextBlock.Text = (e.NewValue as Contact).Fone;
            }

        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
