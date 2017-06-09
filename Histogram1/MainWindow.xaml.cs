// Source of Original Code:
// http://trompelecode.com/blog/2012/04/how-to-create-an-image-histogram-using-csharp-and-wpf/

using AForge.Imaging;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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

        public String ImageURL { get; set; }

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

        public bool PerformHistogramSmoothing { get; set; }

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

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            try
            {
                this.ImageURL = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "Sample.jpg"), UriKind.Absolute).AbsoluteUri;
            }
            catch
            {
                // do nothing, user must enter a URL manually
            }
        }

        #endregion

        #region Event Handlers

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.ImageURL))
            {
                OnButtonClick(null, null);
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                if (String.IsNullOrWhiteSpace(this.ImageURL))
                {
                    MessageBox.Show("Image URL is mandatory.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                String localFilePath = null;
                try
                {
                    localFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(this.ImageURL, localFilePath);
                    }
                    this.LocalImagePath = localFilePath;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid image URL. Image could not be retrieved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(localFilePath))
                {
                    // Luminance
                    ImageStatisticsHSL hslStatistics = new ImageStatisticsHSL(bmp);
                    this.LuminanceHistogramPoints = ConvertToPointCollection(hslStatistics.Luminance.Values);
                    // RGB
                    ImageStatistics rgbStatistics = new ImageStatistics(bmp);
                    this.RedColorHistogramPoints = ConvertToPointCollection(rgbStatistics.Red.Values);
                    this.GreenColorHistogramPoints = ConvertToPointCollection(rgbStatistics.Green.Values);
                    this.BlueColorHistogramPoints = ConvertToPointCollection(rgbStatistics.Blue.Values);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error generating histogram : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
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

        private PointCollection ConvertToPointCollection(int[] values)
        {
            if (this.PerformHistogramSmoothing)
            {
                values = SmoothHistogram(values);
            }

            int max = values.Max();

            PointCollection points = new PointCollection();
            // first point (lower-left corner)
            points.Add(new Point(0, max));
            // middle points
            for (int i = 0; i < values.Length; i++)
            {
                points.Add(new Point(i, max - values[i]));
            }
            // last point (lower-right corner)
            points.Add(new Point(values.Length - 1, max));

            return points;
        }

        private int[] SmoothHistogram(int[] originalValues)
        {
            int[] smoothedValues = new int[originalValues.Length];

            double[] mask = new double[] { 0.25, 0.5, 0.25 };

            for (int bin = 1; bin < originalValues.Length - 1; bin++)
            {
                double smoothedValue = 0;
                for (int i = 0; i < mask.Length; i++)
                {
                    smoothedValue += originalValues[bin - 1 + i] * mask[i];
                }
                smoothedValues[bin] = (int)smoothedValue;
            }

            return smoothedValues;
        }

        #endregion
    }
}
