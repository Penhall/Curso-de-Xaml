using EvernoteClone.Models;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.View.Controls
{
    /// <summary>
    /// Interação lógica para NotebookControl.xaml
    /// </summary>
    public partial class NotebookControl : UserControl
    {


        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Notebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookControl notebookControl = (NotebookControl)d;

            if (notebookControl != null)
            {
                notebookControl.DataContext = notebookControl.Notebook;
            }

        }

        public NotebookControl()
        {
            InitializeComponent();
        }
    }
}
