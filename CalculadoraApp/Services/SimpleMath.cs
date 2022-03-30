using System.Windows;

namespace CalculadoraApp.Services
{
    public class SimpleMath
    {
        public static double Adicao(double a, double b) { return a + b; }
        public static double Subtracao(double a, double b) { return a - b; }
        public static double Multiplicacao(double a, double b) { return a * b; }
        public static double Divisao(double a, double b)
        {

            if (b == 0)
            {
                MessageBox.Show("Nao e permitido divisao por Zero", "Operaçao nao permitida", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return a / b;
        }

    }
}
