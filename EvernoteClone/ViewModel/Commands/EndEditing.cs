using EvernoteClone.Models;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditing : ICommand
    {


        public NotesVM VM { get; set; }

        public event EventHandler? CanExecuteChanged;

        public EndEditing(NotesVM vM)
        {
            VM = vM;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Notebook? notebook = parameter as Notebook;
            if (notebook != null)
                VM.StopEditing(notebook);


            // VM.StopEditing(VM.SelectedNotebook);
            VM.GetNotebooks();


        }
    }
}
