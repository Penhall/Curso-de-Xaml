using System;
using System.Windows.Input;

namespace ClimaApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherViewModel VM { get; set; }

        public SearchCommand(WeatherViewModel vM)
        {
            VM = vM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;


            if (string.IsNullOrEmpty(query))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();

            //if (CanExecuteChanged != null) { VM.MakeQuery(); }
        }
    }
}
