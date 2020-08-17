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

namespace ImageEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image _img;
        private ImageInfo _imgInfo;

        private double _height;
        private double _width;

        private double _x;
        private double _y;

        public MainWindow()
        {
            InitializeComponent();
            _imgInfo = new ImageInfo(1);

            First.IsChecked = true;

            xShift.ValueChanged += xShift_ValueChanged;
            yShift.ValueChanged += yShift_ValueChanged;

            Initialize();
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            _imgInfo = new ImageInfo(int.Parse(rb.Content.ToString()));

            xShift.ValueChanged += xShift_ValueChanged;
            yShift.ValueChanged += yShift_ValueChanged;

            Initialize();
        }

        private void Initialize()
        {
            _img = _imgInfo.GetImage();

            _height = _img.Height;
            _width = _img.Width;

            Canvas1.Children.Clear();
            Canvas1.Children.Add(_img);

            Canvas.SetLeft(_img, Canvas1.ActualWidth / 2 - _img.Width / 2);
            Canvas.SetTop(_img, Canvas1.ActualHeight / 2 - _img.Height / 2);

            setSliderProp();
        }

        private void setSliderProp()
        {
            xShift.ValueChanged -= xShift_ValueChanged;
            yShift.ValueChanged -= yShift_ValueChanged;

            xShift.Maximum = (Canvas1.ActualWidth - _img.Width) - (Canvas1.ActualWidth - _img.Width) / 2;
            xShift.Minimum = -xShift.Maximum;
            xShift.Value = 0;

            yShift.Maximum = (Canvas1.ActualWidth - (_img.Height + _img.Height / 2)) - (Canvas1.ActualWidth - _img.Height) / 2;
            yShift.Minimum = -yShift.Maximum;
            yShift.Value = 0;

            _x = (Canvas1.ActualWidth - _img.Width) / 2;
            _y = (Canvas1.ActualHeight - _img.Height) / 2;

            xShift.ValueChanged += xShift_ValueChanged;
            yShift.ValueChanged += yShift_ValueChanged;
        }

        private void Height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if(_img!=null)
            {
                 var slider = sender as Slider;
                _img.Height = slider.Value / 100 * _height;

                setSliderProp();
            }
        }

        private void Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_img != null)
            {
                 var slider = sender as Slider;
                _img.Width = slider.Value / 100 * _width;

                setSliderProp();
            }
        }

        private void Rotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_img != null)
            {
                var slider = sender as Slider;

                _img.RenderTransformOrigin = new Point(0.5, 0.5);
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.Angle = slider.Value;
                _img.RenderTransform = rotateTransform;

                setSliderProp();
            }
        }

        private void xShift_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_img != null)
            {
                var slider = sender as Slider;

                if (slider.Value == slider.Maximum || slider.Value == slider.Minimum)
                    return;

                Canvas.SetLeft(_img, _x + slider.Value);
            }
        }

        private void yShift_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_img != null)
            {
                var slider = sender as Slider;

                if (slider.Value == slider.Maximum || slider.Value == slider.Minimum)
                    return;

                Canvas.SetTop(_img, _y + slider.Value);
            }
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_img != null)
            {
                Width.Value = 100;
                Width.Minimum = 0;
                Width.Maximum = 100;

                Height.Value = 100;
                Height.Minimum = 0;
                Height.Maximum = 100;

                Rotate.Minimum = -360;
                Rotate.Maximum = 360;
                Rotate.Value = 0;

                Initialize();
            }
        }
    }
}
