using EvernoteClone.Models;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {

        private bool isEditing;

        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditing EndEditing { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }

        private Visibility isVisible;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {

                isVisible = value;
                OnPropertyChanged("IsVisible");

            }
        }

        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;
        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
              //  NotesChanged?.Invoke(this, new EventArgs());  * *** olhar depois

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler NotesChanged;

        public NotesVM()
        {
            isEditing=false;

            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            EditCommand = new EditCommand(this);
            EndEditing = new EndEditing(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNote(string notebookId)
        {
            Note newNote = new Note()
            {
             


                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };

            // DataBaseHelper.Insert<Note>(newNote);

            DataBaseHelper.Insert(newNote);

            GetNotes();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            DataBaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        private void OnPropertyChanged(string propertyName)
        {
            //if (PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetNotebooks()
        {
            var notebooks = DataBaseHelper.Read<Notebook>().Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();

            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        public void GetNotes()
        {

            if (selectedNotebook != null)
            {
                Notes.Clear();

                var notes = DataBaseHelper.Read<Note>().Where(n => n.NotebookId == selectedNotebook.Id).ToList();

                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }

        }

        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;

            DataBaseHelper.Update(notebook);

        }
    }
}
