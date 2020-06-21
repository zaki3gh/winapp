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

            _powerRequest = new PowerRequestWrapper("powerrequestapp");
        }

        PowerRequestWrapper _powerRequest;


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.display.IsChecked.GetValueOrDefault())
            {
                _powerRequest.Clear(PowerRequestType.SystemRequired);
                _powerRequest.Set(PowerRequestType.DisplayRequired);
            }
            else if (this.system.IsChecked.GetValueOrDefault())
            {
                _powerRequest.Set(PowerRequestType.SystemRequired);
                _powerRequest.Clear(PowerRequestType.DisplayRequired);
            }
            else
            {
                _powerRequest.Clear(PowerRequestType.SystemRequired);
                _powerRequest.Clear(PowerRequestType.DisplayRequired);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonitorPower.TurnOffDisplay();
        }
    }
}
