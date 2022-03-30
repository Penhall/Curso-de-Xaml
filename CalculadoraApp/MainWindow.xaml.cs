using CalculadoraApp.Enums;
using CalculadoraApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace CalculadoraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;

        SelectedOperator selectedOperator;
        public MainWindow()
        {

            InitializeComponent();

            btnAC.Click += BtnAC_Click;
            btnNegative.Click += BtnNegative_Click;
            btnPercentagem.Click += BtnPercentagem_Click;
            btnIgual.Click += BtnIgual_Click;
            btnPonto.Click += BtnPonto_Click;
        }

        private void BtnPonto_Click(object sender, RoutedEventArgs e)
        {
            if (lblResult.Content.ToString().Contains("."))
            {
                //Nada a fazer
            }
            else

            {

                lblResult.Content = $"{lblResult.Content}.";

            }
        }

        private void BtnIgual_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(lblResult.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Adiçao:
                        result = SimpleMath.Adicao(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Divisao:
                        result = SimpleMath.Divisao(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplicaçao:
                        result = SimpleMath.Multiplicacao(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraçao:
                        result = SimpleMath.Subtracao(lastNumber, newNumber);
                        break;
                }

                lblResult.Content = result.ToString();
            }


        }

        private void BtnPercentagem_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;

            if (double.TryParse(lblResult.Content.ToString(), out tempNumber))
            {
                tempNumber = (tempNumber / 100);
                if (lastNumber != 0)
                {
                    tempNumber *= (lastNumber);
                }

                lblResult.Content = tempNumber.ToString();
            }

        }

        private void BtnNegative_Click(object sender, RoutedEventArgs e)
        {

            if ((lblResult.Content.ToString() != "0") && (double.TryParse(lblResult.Content.ToString(), out lastNumber)))
            {
                lastNumber = lastNumber * (-1);
                lblResult.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if ((lblResult.Content.ToString() != "0") && (double.TryParse(lblResult.Content.ToString(), out lastNumber)))
            {
                lblResult.Content = "0"; //lastNumber.ToString();
            }

            if (sender == btnMais) selectedOperator = SelectedOperator.Adiçao;
            if (sender == btnMenos) selectedOperator = SelectedOperator.Subtraçao;
            if (sender == btnDividir) selectedOperator = SelectedOperator.Divisao;
            if (sender == btnMultiplicacao) selectedOperator = SelectedOperator.Multiplicaçao;


        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int SelectedValue = int.Parse((sender as Button).Content.ToString());


            if (lblResult.Content.ToString() == "0")
            {
                lblResult.Content = $"{SelectedValue}";
            }
            else
            {
                lblResult.Content = $"{lblResult.Content}{SelectedValue}";
            }
        }



        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "0";
            result = 0;
            lastNumber = 0;

        }


    }
}
