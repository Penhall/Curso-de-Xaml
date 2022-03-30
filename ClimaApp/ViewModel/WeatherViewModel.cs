using ClimaApp.Model;
using ClimaApp.ViewModel.Commands;
using ClimaApp.ViewModel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClimaApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");

            }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {

                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public ObservableCollection<City> Cities { get; set; }
        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };

                currentConditions = new CurrentConditions
                {
                    WeatherText = "Partily Cloud",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 25
                        }
                    }

                };

            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();

        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();

            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);


        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();

            foreach (City city in cities)
            {
                Cities.Add(city);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
