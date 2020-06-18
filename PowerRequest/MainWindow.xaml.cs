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

namespace PowerRequest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private PowerRequestWrapper? _displayRequest;
        private PowerRequestWrapper? _systemRequest;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.noreq.IsChecked.GetValueOrDefault())
            {
                _displayRequest?.Clear();
                _systemRequest?.Clear();
            }
            else if (this.display.IsChecked.GetValueOrDefault())
            {
                if (_displayRequest == null)
                {
                    _displayRequest = new PowerRequestWrapper(PowerRequestType.DisplayRequired, "powerrequestapp");
                }
                _displayRequest?.Set();
            }
            else if (this.system.IsChecked.GetValueOrDefault())
            {
                if (_systemRequest == null)
                {
                    _systemRequest = new PowerRequestWrapper(PowerRequestType.SystemRequired, "powerrequestapp");
                }
                _systemRequest?.Set();
            }
            else
            {
                _displayRequest?.Clear();
                _systemRequest?.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonitorPower.TurnOffDisplay();
        }
    }
}
