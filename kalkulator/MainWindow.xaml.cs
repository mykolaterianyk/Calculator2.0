using System;
using System.Collections.Generic;
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

namespace Сalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbScreen.Text = "0";
            

        }
        private string _firstValue;
        private string _secondValue;
        private bool _isTheResultOnTheScreen;

        private void OnBtnNumberClick(object sender, RoutedEventArgs e)
        {
            var clickedValue = (sender as Button).Content;
            tbScreen.Text += clickedValue;
            if (tbScreen.Text == "0")
 tbScreen.Text = string.Empty;
           
            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;
                if (clickedValue == ",")
                    clickedValue = "0,";
            }



            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;            SetOperationBtnState(true);
            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;
            else
                SetOperationBtnState(true);
            SetResultBtnState(true);
            SetOperationBtnState(true);
            SetResultBtnState(true);
        }

        private void OnBtnOperationClick(object sender, RoutedEventArgs e){
            var operation = (sender as Button).Content;
            _firstValue = tbScreen.Text;
            tbScreen.Text += $" {operation} ";

            _currentOperation = operation switch
            {
                "+" => Operation.Addition,
                "-" => Operation.Subtraction,
                "/" => Operation.Division,
                "*" => Operation.Multiplication,
                _ => Operation.None,
            };            if (_isTheResultOnTheScreen)
                _isTheResultOnTheScreen = false;
        }

        private void OnBtnResultClick(object sender, RoutedEventArgs e){
            var firstNumber = double.Parse(_firstValue);
            var secondNumber = double.Parse(_secondValue);
            var result = Calculate(firstNumber, secondNumber);
            tbScreen.Text = result.ToString();
            _secondValue = string.Empty;
            _currentOperation = Operation.None;
            _isTheResultOnTheScreen = true;
            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;
            }            if (_currentOperation == Operation.None)
                return;
        }


        private void OnBtnClearClick(object sender, RoutedEventArgs e) {
            tbScreen.Text = "0";
            _firstValue = string.Empty;
            _secondValue = string.Empty;
            _currentOperation = Operation.None;
        }
        public enum Operation
        {
            None,
            Addition,
            Subtraction,
            Division,
            Multiplication

        }
        private Operation _currentOperation = Operation.None;
        private double Calculate(double firstNumber, double secondNumber)
        {
            switch (_currentOperation)
            {
                case Operation.None:
                    return firstNumber;
                case Operation.Addition:
                    return firstNumber + secondNumber;
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                case Operation.Division:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Nie można dzielić przez 0!");
                        return 0;
                    }
                    return firstNumber / secondNumber;
                case Operation.Multiplication:
                    return firstNumber * secondNumber;
            }
            return 0;
        }        private void SetOperationBtnState(bool value)
        {
            btnAdd.IsEnabled = value;
            btnMultiplication.IsEnabled = value;
            btnDivision.IsEnabled = value;
            btnSubtraction.IsEnabled = value;
        }
        private void SetResultBtnState(bool value)
        {
            btnResult.IsEnabled = value;
            SetOperationBtnState(true);
            SetResultBtnState(true);
        }

    }

}
