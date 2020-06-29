using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;

namespace CalculatingPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ExecuteText = "Execute";
            this.Title = "Pi Calculator";
        }

        private string _executeText;

        public string ExecuteText
        {
            get { return _executeText; }
            set
            {
                _executeText = value;
                NotifyPropertyChanged();
            }
        }

        public double calcFactorial(int number)
        {
            double result = 1;
            while (number != 1)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }

        //Updating the amount of threads selected.
        int nrOfThreadsSelected()
        {
            if (radioBtn1.IsChecked == true)
            {
                return 1;
            }
            else if (radioBtn2.IsChecked == true)
            {
                return 2;
            }
            else if (radioBtn4.IsChecked == true)
            {
                return 4;
            }
            else if (radioBtn8.IsChecked == true)
            {
                return 8;
            }
            else if (radioBtn16.IsChecked == true)
            {
                return 16;
            }
            else
            {
                return 0;
            }
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            
            ExecuteText = "Calculating...";
            if (Nilakantha.IsChecked == true)
            {
                uint numberOfIterations = Convert.ToUInt32(nrOfIterations.Text);

                Nilakantha_Series nilakantha_Series = new Nilakantha_Series(numberOfIterations);

                //Creating an empty tuple with 3 variables, the first item is the calculated Pi value in decimal, the second item is the calculated error value in decimal.
                //The last item is the amount of ms it took to execute the algorithm. 
                Tuple<decimal, decimal, Int32> receivedData = Tuple.Create(0.0m,0.0m,0);


                //Depending on how many threads are selected with the radiobuttons, the correct function is called and the receivedData variable gets filled. 
                switch (nrOfThreadsSelected())
                {
                    case 1:
                        receivedData = nilakantha_Series.PiNilakanthaThreadOne();
                        break;
                    case 2:
                        receivedData = nilakantha_Series.PiNilakanthaThreadTwo();
                        break;
                    case 4:
                        receivedData = nilakantha_Series.PiNilakanthaThreadFour();
                        break;
                    case 8:
                        receivedData = nilakantha_Series.PiNilakanthaThreadEight();
                        break;
                    case 16:
                        receivedData = nilakantha_Series.PiNilakanthaThreadSixteen();
                        break;
                    default:
                        Console.WriteLine("ERROR: uncorrect amount of threads selected.");
                        break;
                }

                labelPi.Content = receivedData.Item1;
                labelError.Content = receivedData.Item2;
                labelTime.Content = receivedData.Item3 + "ms";

            }

            if (GregoryLeibniz.IsChecked == true)
            {
                uint numberOfIterations = Convert.ToUInt32(nrOfIterations.Text);

                GregoryLeibniz_Series gregoryLeibniz = new GregoryLeibniz_Series(numberOfIterations);

                Tuple<decimal, decimal, Int32> receivedData = Tuple.Create(0.0m, 0.0m, 0);

                switch (nrOfThreadsSelected())
                {
                    case 1:
                        receivedData = gregoryLeibniz.PiGregoryLeibnizThreadOne();
                        break;
                    case 2:
                        receivedData = gregoryLeibniz.PiGregoryLeibnizThreadTwo();
                        break;
                    case 4:
                        receivedData = gregoryLeibniz.PiGregoryLeibnizThreadFour();
                        break;
                    case 8:
                        receivedData = gregoryLeibniz.PiGregoryLeibnizThreadEight();
                        break;
                    case 16:
                        receivedData = gregoryLeibniz.PiGregoryLeibnizThreadSixteen();
                        break;

                    default:
                        Console.WriteLine("ERROR: uncorrect amount of threads selected.");
                        break;
                }

                labelPi.Content = receivedData.Item1;
                labelError.Content = receivedData.Item2;
                labelTime.Content = receivedData.Item3 + "ms";
            }
            if (Vietes.IsChecked == true)
            {
                uint numberOfIterations = Convert.ToUInt32(nrOfIterations.Text);

                Vietes_Series vietes_Series = new Vietes_Series(numberOfIterations);
                
                Tuple<decimal, decimal, Int32> receivedData = Tuple.Create(0.0m, 0.0m, 0);

                switch (nrOfThreadsSelected())
                {
                    case 1:
                        receivedData = vietes_Series.PiVietesThreadOne();
                        break;
                    case 2:
                        receivedData = vietes_Series.PiVietesThreadTwo();
                        break;
                    case 4:
                        receivedData = vietes_Series.PiVietesThreadFour();
                        break;
                    case 8:
                        receivedData = vietes_Series.PiVietesThreadEight();
                        break;
                    case 16:
                        receivedData = vietes_Series.PiVietesThreadSixteen();
                        break;

                    default:
                        Console.WriteLine("ERROR: uncorrect amount of threads selected.");
                        break;
                }

                labelPi.Content = receivedData.Item1;
                labelError.Content = receivedData.Item2;
                labelTime.Content = receivedData.Item3 + "ms";
            }

            if (Rabinowitz.IsChecked == true)
            {
                uint numberOfIterations = Convert.ToUInt32(nrOfIterations.Text);

                Rabinowitz_Wagon_Spigot rabinowitz_Wagon_Spigot = new Rabinowitz_Wagon_Spigot(numberOfIterations);

                Tuple<string, Int32> receivedData = Tuple.Create("", 0);

                receivedData =  rabinowitz_Wagon_Spigot.PiRabinowitz_Wagon_SpigotThreadOne();

                labelPi.Content = "Look in console for the calculated result.";
                labelError.Content = "No error.";
                labelTime.Content = receivedData.Item2 + "ms";
            }

            if (MonteCarlo.IsChecked == true)
            {
                uint numberOfIterations = Convert.ToUInt32(nrOfIterations.Text);

                PiMonteCarlo monteCarlo = new PiMonteCarlo(numberOfIterations);

                Tuple<decimal, decimal, Int32> receivedData = Tuple.Create(0.0m, 0.0m, 0);

                switch (nrOfThreadsSelected())
                {
                    case 1:
                        receivedData = monteCarlo.PiMonteCarloThreadOne();
                        break;
                    case 2:
                        receivedData = monteCarlo.PiMonteCarloThreadTwo();
                        break;
                    case 4:
                        receivedData = monteCarlo.PiMonteCarloThreadFour();
                        break;
                    case 8:
                        receivedData = monteCarlo.PiMonteCarloThreadEight();
                        break;
                    case 16:
                        receivedData = monteCarlo.PiMonteCarloThreadSixteen();
                        break;

                    default:
                        Console.WriteLine("ERROR: uncorrect amount of threads selected.");
                        break;
                }

                labelPi.Content = receivedData.Item1;
                labelError.Content = receivedData.Item2;
                labelTime.Content = receivedData.Item3 + "ms";
            }
            ExecuteText = "Execute";
        }

        //When the Rabinowitz Wagon algorithm is selected, only 1 thread can be used. 
        private void Rabinowitz_Checked(object sender, RoutedEventArgs e)
        {
            radioBtn1.IsChecked = true;
            radioBtn2.IsEnabled = false;
            radioBtn4.IsEnabled = false;
            radioBtn8.IsEnabled = false;
            radioBtn16.IsEnabled = false;

            numberOfLabel.Content = "Number of digits:";
        }

        private void Rabinowitz_Unchecked(object sender, RoutedEventArgs e)
        {
            radioBtn2.IsEnabled = true;
            radioBtn4.IsEnabled = true;
            radioBtn8.IsEnabled = true;
            radioBtn16.IsEnabled = true;
            numberOfLabel.Content = "Number of iterations:";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
