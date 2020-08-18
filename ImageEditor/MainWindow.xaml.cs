﻿using System;
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

            xShift.Maximum = 100;
            xShift.Minimum = 0;
            xShift.Value = 0;

            yShift.Maximum = 100;
            yShift.Minimum = 0;
            yShift.Value = 0;

            xSkew.Maximum = 50;
            xSkew.Minimum = 0;
            xSkew.Value = 0;

            ySkew.Maximum = 50;
            ySkew.Minimum = 0;
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
