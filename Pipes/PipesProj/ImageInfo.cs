using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PipesProj
{
    public class ImageInfo
    {
        public Image Img { get; private set; } = new Image();
        public double Angle { get; set; }


        public ImageInfo()
        {
            var num = new Random().Next(1, 4);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"\images\" + num.ToString() + ".png", UriKind.Relative);
            bi.EndInit();

            Img.Source = bi;
            Img.Stretch = Stretch.Uniform;

            Img.MouseLeftButtonDown += Img_MouseLeftButtonDown1;
            Img.MouseRightButtonDown += Img_MouseRightButtonDown1;
        }

        private void Img_MouseRightButtonDown1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform();
            Angle -= 90;
            rotateTransform.Angle = Angle;
            img.RenderTransform = rotateTransform;
        }

        private void Img_MouseLeftButtonDown1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform();
            Angle += 90;
            rotateTransform.Angle = Angle;
            img.RenderTransform = rotateTransform;
        }
    }
}
