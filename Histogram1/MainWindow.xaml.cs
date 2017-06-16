// Source of Original Code:
// http://trompelecode.com/blog/2012/04/how-to-create-an-image-histogram-using-csharp-and-wpf/

using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
// using System.Drawing;
using Microsoft.Win32;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Linq;

namespace Histogram1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private variables

        private String localImagePath = null;
        private PointCollection luminanceHistogramPoints = null;
        private PointCollection redColorHistogramPoints = null;
        private PointCollection greenColorHistogramPoints = null;
        private PointCollection blueColorHistogramPoints = null;


        #endregion

        #region Public Properties

        public String LocalImagePath
        {
            get
            {
                return this.localImagePath;
            }
            set
            {
                if (this.localImagePath != value)
                {
                    this.localImagePath = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("LocalImagePath"));
                    }
                }
            }
        }

        public PointCollection RedColorHistogramPoints
        {
            get
            {
                return this.redColorHistogramPoints;
            }
            set
            {
                if (this.redColorHistogramPoints != value)
                {
                    this.redColorHistogramPoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("RedColorHistogramPoints"));
                    }
                }
            }
        }

        public PointCollection GreenColorHistogramPoints
        {
            get
            {
                return this.greenColorHistogramPoints;
            }
            set
            {
                if (this.greenColorHistogramPoints != value)
                {
                    this.greenColorHistogramPoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("GreenColorHistogramPoints"));
                    }
                }
            }
        }
        public PointCollection BlueColorHistogramPoints
        {
            get
            {
                return this.blueColorHistogramPoints;
            }
            set
            {
                if (this.blueColorHistogramPoints != value)
                {
                    this.blueColorHistogramPoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("BlueColorHistogramPoints"));
                    }
                }
            }
        }
        public PointCollection LuminanceHistogramPoints
        {
            get
            {
                return this.luminanceHistogramPoints;
            }
            set
            {
                if (this.luminanceHistogramPoints != value)
                {
                    this.luminanceHistogramPoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("LuminanceHistogramPoints"));
                    }
                }
            }
        }



        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;


        }

        #endregion

        #region Event Handlers

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }




        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files |*.png;*.jpeg;*.jpg;*.tif |All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                this.LocalImagePath = openFileDialog.FileName;
            }


            // is this necessary?
            var source = new BitmapImage(new System.Uri(LocalImagePath));
            //int bitsPerPixel = source.Format.BitsPerPixel;


            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(LocalImagePath))
            {

                int[] red_collection = new int[256];
                int[] green_collection = new int[256];
                int[] blue_collection = new int[256];
                int[] lum_collection = new int[256];

                //int stride = bmp.Width * 4;
                //int size = bmp.Height * stride;
                //byte[] pixels = new byte[size];

                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        System.Drawing.Color pixel_color = bmp.GetPixel(j, i);

                        red_collection[pixel_color.R]++;
                        green_collection[pixel_color.G]++;
                        blue_collection[pixel_color.B]++;
                        //this may not be the correct formula
                        lum_collection[(int)Math.Sqrt(.241 * (pixel_color.R * pixel_color.R) + .691*(pixel_color.G * pixel_color.G ) + .068*(pixel_color.B * pixel_color.B))]++;
                    }
                }


                RedColorHistogramPoints = ConvertArrayToPointCollection(red_collection);
                GreenColorHistogramPoints = ConvertArrayToPointCollection(green_collection);
                BlueColorHistogramPoints = ConvertArrayToPointCollection(blue_collection);
                LuminanceHistogramPoints = ConvertArrayToPointCollection(lum_collection);
            }
            
        }

  
        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink source = sender as Hyperlink;
            if (source != null)
            {
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
            }
        }


        #endregion

        #region Private Methods
        private PointCollection ConvertArrayToPointCollection(int[] values)
        {
            int max = values.Max();

            PointCollection points = new PointCollection();
            // first point (lower-left corner)
            points.Add(new Point(0, max));
            // middle points
            for (int i =0; i < values.Length; i++)
            {
                points.Add(new Point(i, max - values[i]));
            }
            // last point (lower right corner)
            points.Add(new Point(values.Length - 1, max));

            return points;

        }
        #endregion
    }
}
