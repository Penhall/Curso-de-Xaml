using EvernoteClone.Models;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.View.Controls
{
    /// <summary>
    /// Interação lógica para NoteControl.xam
    /// </summary>
    public partial class NoteControl : UserControl
    {
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Notebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteControl noteControl = (NoteControl)d;

            if (noteControl != null)
            {
                noteControl.DataContext = noteControl.Note;
            }

        }
        public NoteControl()
        {
            InitializeComponent();
        }
    }
}
