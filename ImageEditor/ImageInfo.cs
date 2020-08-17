using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor
{
    class ImageInfo
    {
        private List<Img> _imgList = new List<Img>();
        private int _num;

        public ImageInfo(int num)
        {
            _num = num;
            FillImgListDefault();
        }

        private void FillImgListDefault()
        {
            _imgList.Add(new Img { Number = 1 , Path = "coffeeImages/coffee1.jpg"});
            _imgList.Add(new Img { Number = 2 , Path = "coffeeImages/coffee2.jpg"});
            _imgList.Add(new Img { Number = 3 , Path = "coffeeImages/coffee3.jpg"});
            _imgList.Add(new Img { Number = 4 , Path = "coffeeImages/coffee4.jpg"});
            _imgList.Add(new Img { Number = 5 , Path = "coffeeImages/coffee5.jpg"});
            _imgList.Add(new Img { Number = 6 , Path = "coffeeImages/coffee6.jpg"});
            _imgList.Add(new Img { Number = 7 , Path = "coffeeImages/coffee7.jpg"});
            _imgList.Add(new Img { Number = 8 , Path = "coffeeImages/coffee8.jpg"});
            _imgList.Add(new Img { Number = 9 , Path = "coffeeImages/coffee9.jpg"});
            _imgList.Add(new Img { Number = 10 , Path = "coffeeImages/coffee10.jpg"});
        }

        public Image GetImage()
        {
            var path = string.Empty;

            foreach (var im in _imgList)
            {
                if(_num == im.Number)
                {
                    path = im.Path;
                    break;
                }
            }

            Image image = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(path, UriKind.Relative);
            bi.EndInit();
            image.Source = bi;
            image.Stretch = Stretch.Fill;

            image.Height = 410;
            image.Width = 429;
            return image;
        }

    }
}
