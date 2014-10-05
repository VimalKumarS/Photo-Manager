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
using System.Windows.Threading;
using System.Threading;

namespace PhotoManager
{
    /// <summary>
    /// Interaction logic for SyncOption.xaml
    /// </summary>
    public partial class SyncOption : UserControl
    {
        public SyncOption()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;

        }
        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

         public void SetParent(UIElement parent)
           {
              _parent = parent;
          }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(
            "Message", typeof(string), typeof(SyncOption),
            new UIPropertyMetadata(string.Empty));
        public bool ShowHandlerDialog(string message)
        {
            Message = message;
            Visibility = Visibility.Visible;

            _parent.IsEnabled = false;

            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            HideHandlerDialog();
        }
        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
        }
    }
}
