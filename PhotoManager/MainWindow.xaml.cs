using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SyncOption.SetParent(parentgrid);


        }

        private void CreateCollage()
        {
            if (EventAction.Equals("Collage"))
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                List<Uri> uriSource = new List<Uri>(){
                 new Uri(@".\Collage\tiles.jpg",UriKind.Relative),
                      new Uri(@".\AlbumPhoto\1.jpg",UriKind.Relative),
                       new Uri(@".\AlbumPhoto\2.jpg",UriKind.Relative),
                        new Uri(@".\AlbumPhoto\3.jpg",UriKind.Relative),
                         new Uri(@".\AlbumPhoto\4.jpg",UriKind.Relative),

            };

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(uriSource[0]);
                canvas.Background = ib;



                Image img = new Image();
                img.Width = 200;
                img.Height = 200;
                img.Stretch = Stretch.UniformToFill;
                img.Source = new BitmapImage(uriSource[1]);
                //img.RenderTransform = new RotateTransform(rand.NextDouble() * 360);
                Canvas.SetLeft(img, 200);
                Canvas.SetTop(img, 100);
                canvas.Children.Add(img);

                img = new Image();
                img.Width = 200;
                img.Height = 200;
                img.Stretch = Stretch.UniformToFill;
                img.Source = new BitmapImage(uriSource[2]);
                //img.RenderTransform = new RotateTransform(rand.NextDouble() * 360);
                Canvas.SetLeft(img, 400);
                Canvas.SetTop(img, 120);
                canvas.Children.Add(img);

                img = new Image();
                img.Width = 200;
                img.Height = 200;
                img.Stretch = Stretch.UniformToFill;
                img.Source = new BitmapImage(uriSource[3]);
                //img.RenderTransform = new RotateTransform(rand.NextDouble() * 360);
                Canvas.SetLeft(img, 600);
                Canvas.SetTop(img, 100);
                canvas.Children.Add(img);

                img = new Image();
                img.Width = 200;
                img.Height = 200;
                img.Stretch = Stretch.UniformToFill;
                img.Source = new BitmapImage(uriSource[4]);
                //img.RenderTransform = new RotateTransform(rand.NextDouble() * 360);
                Canvas.SetLeft(img, 670);
                Canvas.SetTop(img, 120);
                canvas.Children.Add(img);
                // img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                img.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                img.MouseMove += new MouseEventHandler(img_MouseMove);
                img.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseLeftButtonUp);
            }
            else if (EventAction.Equals("PhotoBook"))
            {
                TabItem ti = new TabItem();
                ti.Header = "Untilted_2";
                Thickness margin = ti.Margin;
                margin.Left = 3;
                ti.Margin = margin;
                //ti.Margin.Left = 3;
                ti.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF3F9"));

                tabControl.Items.Insert(tabControl.Items.Count, ti);
                tabControl.SelectedIndex = tabControl.Items.Count - 1;
                _control = new UserControl1();

                Grid _grid = new Grid();

                _grid.Children.Add(_control);
                ti.Content = _grid;
            }
        }

        void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
        }

        void img_MouseMove(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                x_shape += x - x_canvas;
                Canvas.SetLeft(source, x_shape);
                x_canvas = x;
                y_shape += y - y_canvas;
                Canvas.SetTop(source, y_shape);
                y_canvas = y;
            }
        }
        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;
        UIElement source = null;
        string EventAction = string.Empty;
        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            source = (UIElement)sender;
            Mouse.Capture(source);
            captured = true;
            x_shape = Canvas.GetLeft(source);
            x_canvas = e.GetPosition(canvas).X;
            y_shape = Canvas.GetTop(source);
            y_canvas = e.GetPosition(canvas).Y;
        }

        void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Double LeftPos = Convert.ToDouble(((Image)(sender)).GetValue(Canvas.LeftProperty));
            ((Image)(sender)).SetValue(Canvas.LeftProperty, LeftPos + 10);
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.Multiselect = true;

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                List<PhotoSelected> items = new List<PhotoSelected>();
                items.Add(new PhotoSelected() { ImageName = "Chrysanthemum.jpg", ImagePath = @".\AlbumPhoto\1.jpg" });
                items.Add(new PhotoSelected() { ImageName = "Desert.jpg", ImagePath = @".\AlbumPhoto\2.jpg" });
                items.Add(new PhotoSelected() { ImageName = "Hydrangeas.jpg", ImagePath = @".\AlbumPhoto\3.jpg" });
                items.Add(new PhotoSelected() { ImageName = "Jellyfish.jpg", ImagePath = @".\AlbumPhoto\4.jpg" });

                items.Add(new PhotoSelected() { ImageName = "Lighthouse.jpg", ImagePath = @".\AlbumPhoto\5.jpg" });
                items.Add(new PhotoSelected() { ImageName = "Penguins.jpg", ImagePath = @".\AlbumPhoto\6.jpg" });
                items.Add(new PhotoSelected() { ImageName = "Tulips.jpg", ImagePath = @".\AlbumPhoto\7.jpg" });

                lvDataBinding.ItemsSource = items;
            }
        }

        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {
            EventAction = "Collage";
            //templateDataBinding.Items.Clear();
            templateDataBinding.ItemsSource = null;
            templateDataBinding.UnselectAll();
            templateDataBinding.Items.Clear();
            List<PhotoSelected> templpateitems = new List<PhotoSelected>();
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage1", ImagePath = @".\Collage\Collage1.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage2", ImagePath = @".\Collage\Collage2.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage3", ImagePath = @".\Collage\Collage3.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage4", ImagePath = @".\Collage\Collage4.jpg" });

            templpateitems.Add(new PhotoSelected() { ImageName = "Collage5", ImagePath = @".\Collage\Collage5.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage6", ImagePath = @".\Collage\Collage6.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Collage7", ImagePath = @".\Collage\Collage7.jpg" });

            templateDataBinding.ItemsSource = templpateitems;

            //CreateCollage();
        }

        private void templateDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count!=0)
            CreateCollage();
        }

        private void RibbonButton_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }

        private void RibbonButton_Click_3(object sender, RoutedEventArgs e)
        {
            // SyncOption.ShowHandlerDialog("Open");
            MyPopup.IsOpen = true;
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
        }
        private Control _control;
        private void RibbonButton_Click_4(object sender, RoutedEventArgs e)
        {
            EventAction = "PhotoBook";
           // templateDataBinding.UnselectAll();
           // templateDataBinding.DataSource = null;
          
            //templateDataBinding.Items.Clear();
            List<PhotoSelected> templpateitems = new List<PhotoSelected>();
            templpateitems.Add(new PhotoSelected() { ImageName = "Photo Book1", ImagePath = @".\images\PBook1.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Photo Book2", ImagePath = @".\images\PBook2.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Photo Book3", ImagePath = @".\images\PBook3.jpg" });
            templpateitems.Add(new PhotoSelected() { ImageName = "Photo Book4", ImagePath = @".\images\PBook4.jpg" });

            templateDataBinding.ItemsSource = null;
            templateDataBinding.UnselectAll();
            templateDataBinding.Items.Refresh();

            templateDataBinding.Items.Clear();
            templateDataBinding.ItemsSource = templpateitems;
            
        }
    }
    public class PhotoSelected
    {
        public int ID { get; set; }
        public string ImageName { get; set; }

        public string ImagePath { get; set; }
    }
}
