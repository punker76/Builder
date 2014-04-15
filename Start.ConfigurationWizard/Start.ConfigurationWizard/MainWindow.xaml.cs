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

namespace Start.ConfigurationWizard
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

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void tcMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                var element = VisualTreeHelper.HitTest(tcMain, Mouse.GetPosition(tcMain)).VisualHit;

                do
                {
                    element = VisualTreeHelper.GetParent(element);
                } while (element != null && !(element is TabItem));

                this.tcMain.Items.Remove(element);
            }
        }
    }
}
