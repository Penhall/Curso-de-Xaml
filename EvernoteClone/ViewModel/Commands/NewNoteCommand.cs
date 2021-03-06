using EvernoteClone.Models;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NotesVM VM { get; set; }

        public NewNoteCommand(NotesVM vM)
        {
            VM = vM;
        }

        public bool CanExecute(object? parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            if (selectedNotebook != null)
                return true;
            return false;
        }

        public void Execute(object? parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
