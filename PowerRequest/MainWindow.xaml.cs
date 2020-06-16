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

            this.powerRequest = new PowerRequest1("powerrequestapp");
        }

        private PowerRequest1 powerRequest;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.powerRequest.IsValid)
            {
                return;
            }

            if (this.noreq.IsChecked.GetValueOrDefault())
            {
                this.powerRequest.Clear();
            }
            else if (this.display.IsChecked.GetValueOrDefault())
            {
                this.powerRequest.Clear();
                this.powerRequest.RequestType = PowerRequestType.PowerRequestDisplayRequired;
                this.powerRequest.Set();
            }
            else if (this.system.IsChecked.GetValueOrDefault())
            {
                this.powerRequest.Clear();
                this.powerRequest.RequestType = PowerRequestType.PowerRequestSystemRequired;
                this.powerRequest.Set();
            }
            else
            {
                this.powerRequest.Clear();
            }
        }
    }
}
