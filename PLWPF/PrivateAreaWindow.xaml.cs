using BE;
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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PrivateAreaWindow.xaml
    /// </summary>
    public partial class PrivateAreaWindow : Window
    {
        public HostingUnit hostingUnit { get; set; }

        private ShowHostingUnit updateWindow;
        private ShowHostingUnit viewWindow;
        private ListShowWindow ordersWindow;


        public PrivateAreaWindow()
        {
            InitializeComponent();
        }

        private void CloseWindows()
        {
            if (updateWindow != null)
                updateWindow.Close();
            if (viewWindow != null)
                viewWindow.Close();
            if (ordersWindow != null)
                ordersWindow.Close();
        }

        private void PerformUpdateUnit(object sender, RoutedEventArgs e)
        {
            CloseWindows();
            if (updateWindow == null || !updateWindow.IsActive)
                updateWindow = new ShowHostingUnit();

            updateWindow.Title = "עדכון יחידת אירוח";
            updateWindow.SetModelHostingUnit(hostingUnit, true);
            
            updateWindow.Show();
        }

        private void PerformViewUnit(object sender, RoutedEventArgs e)
        {
            CloseWindows();
            if (viewWindow == null || !viewWindow.IsActive)
                 viewWindow = new ShowHostingUnit();
            viewWindow.Title = "צפייה ביחידת אירוח";
          
            viewWindow.SetModelHostingUnit(hostingUnit);
            
           

            viewWindow.Show();
            
        }

        private void PerformRemoveUnit(object sender, RoutedEventArgs e)
        {
            

            try
            {

                MainWindow.bl.RemoveUnit(hostingUnit);

                MessageBox.Show("יחידת האירוח נמחקה", "הודעה", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWindows();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PerformShowOrdersOfUnit(object sender, RoutedEventArgs e)
        {
            CloseWindows();

            List<Order> orders = MainWindow.bl.getListOrdersByUnit(hostingUnit.HostingUnitKey);

            if (orders.Count <= 0)
            {
                MessageBox.Show("ליחידת אירוח זו אין הזמנות", "הודעת מערכת", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ordersWindow = new ListShowWindow();
            ordersWindow.QueryControl.LoadList(orders, new OrderTableDisplay());
            ordersWindow.Title = "רשימת הזמנות - יחידת אירוח " + hostingUnit.HostingUnitKey;
            ordersWindow.Show();
        }
    }
}
