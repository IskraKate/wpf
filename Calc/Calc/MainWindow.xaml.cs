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

namespace Calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double? _val1;
        private double? _val2;
        private char _sign;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (panel.Content.ToString().Length == 1 && panel.Content.ToString() == "0")
            {
                _val1 = Convert.ToDouble(button.Content);
                panel.Content = _val1;
            }
            else
            {
                panel.Content = panel.Content.ToString() + button.Content.ToString();
                _val2 = null;
            }

            //if (panel.Content.ToString().Length == 1 && panel.Content.ToString() == "0")
            //{
            //    val1 = Convert.ToDouble(button.Content);
            //    panel.Content = val1;
            //}
            ////else if (panel.Content.ToString().Contains(','))
            ////{
            ////    var allAfterDot = panel.Content.ToString().Split(',');
            ////    var pow = allAfterDot[1].Length;
            ////    var newAfterDot = Convert.ToDouble(button.Content) * Math.Pow(0.1, pow);
            ////    val1 += newAfterDot;
            ////    panel.Content = val1;
            ////}
            //else if (panel.Content.ToString().Contains('X'))
            //{
            //    if(button.Content.ToString() != "X")
            //    {
            //        val2 = Convert.ToDouble(val2.ToString() + button.Content.ToString());
            //        panel.Content = val1.ToString() +'X'+ val2.ToString();
            //        var index = panel.Content.ToString().IndexOf('X');
            //        var str = panel.Content.ToString().Substring(index+1, 1);
            //    }
            //}
            //else
            //{
            //    val1 = Convert.ToDouble(val1.ToString() + button.Content.ToString());
            //    panel.Content = val1;
            //}
        }

        private void dot_Click(object sender, RoutedEventArgs e)
        {
            if (!panel.Content.ToString().Contains(','))
            {
                panel.Content = panel.Content.ToString() + ',';
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            _val1 = 0;
            _val2 = null;
            panel.Content = _val1;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            var str = panel.Content.ToString();

            if (str.Length > 1)
            {
                if(str[str.Length - 2] != ' ')
                {
                    str = str.Remove(str.Length - 1, 1);
                }
                else
                {
                    str = str.Remove(str.Length - 5, 5);
                }

                panel.Content = str;
            }
            else if (str.Length==1)
            {
                _val1 = 0;
                panel.Content = "0";
            }

        }

        private void multMin_Click(object sender, RoutedEventArgs e)
        {
            var expressionArray = panel.Content.ToString().Split(' ');
            if (expressionArray.Length == 0)
            {
                return;
            }

            double num1 = Convert.ToDouble(expressionArray[0]);

            num1 *= -1;
            panel.Content= num1.ToString();
        }

        private void power_Click(object sender, RoutedEventArgs e)
        {
            var val = Convert.ToDouble(panel.Content);
            _val1 = Math.Pow(val, 2);
            panel.Content = _val1;
        }

        private void mPower_Click(object sender, RoutedEventArgs e)
        {
            var val = Convert.ToDouble(panel.Content);
            _val1 = 1 / val;
            panel.Content = _val1;
        }

        private void root_Click(object sender, RoutedEventArgs e)
        {
            var val = Convert.ToDouble(panel.Content);
            _val1 = Math.Sqrt(val);
            panel.Content = _val1;
        }

        private void percent_Click(object sender, RoutedEventArgs e)
        {
            var val = Convert.ToDouble(panel.Content);
            _val1 = val / 100;
            panel.Content = _val1;
        }

        private void mult_Click(object sender, RoutedEventArgs e)
        {
            if (_sign != '*')
            {
                panel.Content = panel.Content.ToString() + " X ";
                _sign = '*';
            }
            else
            {
                calculateEqual();
            }
        }
        private void divide_Click(object sender, RoutedEventArgs e)
        {
            if (_sign != '/')
            {
                panel.Content = panel.Content.ToString() + " / ";
                _sign = '/';
            }
            else
            {
                calculateEqual();
            }
        }
        private void min_Click(object sender, RoutedEventArgs e)
        {
            if (_sign != '-')
            {
                panel.Content = panel.Content.ToString() + " - ";
                _sign = '-';
            }
            else
            {
                calculateEqual();
            }
        }
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if(_sign != '+')
            {
                panel.Content = panel.Content.ToString() + " + ";
                _sign = '+';
            }
            else
            {
                calculateEqual();
            }
        }
        private void calculateEqual()
        {
            var expressionArray = panel.Content.ToString().Split(' ');

            //if (expressionArray.Length != 3)
            //{
            //    return;
            //}

            _val1 = Convert.ToDouble(expressionArray[0]);

            if(expressionArray.Length == 3)
            {
                if(expressionArray[2] != "")
                {
                    _val2 = Convert.ToDouble(expressionArray[2]);
                }
            }

            double? result = 0;

            switch (_sign)
            {
                case '+':
                    result = _val1 + _val2;
                    break;
                case '-':
                    result = _val1 - _val2;
                    break;
                case '*':
                    result = _val1 * _val2;
                    break;
                case '/':
                    if (_val2 != 0)
                    {
                        result = _val1 / _val2;
                    }
                    break;
            }

            panel.Content = result;
            _val1 = result;
        }

        private void equals_Click(object sender, RoutedEventArgs e)
        {
            calculateEqual();
        }
    }
}
