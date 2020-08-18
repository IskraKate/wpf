using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public MainWindow()
        {
            InitializeComponent();
            _imgInfo = new ImageInfo(1);

            First.IsChecked = true;

            Initialize();
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            _imgInfo = new ImageInfo(int.Parse(rb.Content.ToString()));

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

            xShift.Maximum = (Canvas1.ActualWidth - _img.Width) - (Canvas1.ActualWidth - _img.Width) / 2;
            xShift.Minimum = -xShift.Maximum;
            xShift.Value = 0;


            double yShiftMax = (Canvas1.ActualWidth - (_img.Height + _img.Height / 2)) - (Canvas1.ActualWidth - _img.Height) / 2;
            yShift.Maximum = yShiftMax > 0 ? yShiftMax : -yShiftMax;
            yShift.Minimum = -yShift.Maximum;
            yShift.Value = 0;

            xSkew.Maximum = 50;
            xSkew.Minimum = -50;
            xSkew.Value = 0;

            ySkew.Maximum = 50;
            ySkew.Minimum = -50;
            ySkew.Value = 0;
        }

        private void Height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(_img!=null)
            {
                 var slider = sender as Slider;
                _img.Height = slider.Value / 100 * _height;
            }
        }

        private void Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_img != null)
            {
                 var slider = sender as Slider;
                _img.Width = slider.Value / 100 * _width;
            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TransformGroup transformGroup = new TransformGroup();

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = Rotate.Value;

            TranslateTransform translateTransform = new TranslateTransform(xShift.Value, yShift.Value);

            SkewTransform skewTransform = new SkewTransform(xSkew.Value, ySkew.Value);

            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(translateTransform);
            transformGroup.Children.Add(skewTransform);

            _img.RenderTransformOrigin = new Point(0.5, 0.5);
            _img.RenderTransform = transformGroup;
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
