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
    /// Interaction logic for ShowHostingUnit.xaml
    /// </summary>
    public partial class ShowHostingUnit : Window
    {
        public HostingUnit unit { set; get; } = new HostingUnit();
        
        public ShowHostingUnit()
        {
            InitializeComponent();

            resetVisible();



            //showho

        }

        private void resetVisible()
        {
            ShowHostingUser.Visibility = Visibility.Hidden;
            ShowOrderUser.Visibility = Visibility.Hidden;
            ShowGuestRequestUser.Visibility = Visibility.Hidden;

            ShowHostingUser.IsEnabled = false;
            ShowOrderUser.IsEnabled = false;

            ShowHostingUser.gridBtn.Visibility = Visibility.Hidden;
            ShowOrderUser.OrderBtns.Visibility = Visibility.Hidden;
            ShowGuestRequestUser.GuestButns.Visibility = Visibility.Hidden;
        }

        public void SetModelHostingUnit(HostingUnit hostingUnit, bool isUpdateable = false)
        {
            resetVisible();

            ShowHostingUser.hostingUnit = hostingUnit;
            ShowHostingUser.Visibility = Visibility.Visible;
            ShowHostingUser.InitToggles();
            ShowHostingUser.IsEnabled = isUpdateable;

            if (isUpdateable)
                ShowHostingUser.gridBtn.Visibility = Visibility.Visible;
            else
            {
                
                //ShowHostingUser.ClearBlackoutDates();
                ShowHostingUser.SetBlackoutDates(hostingUnit.Diary);
                ShowHostingUser.SetBankNoEditable();
            }
            
        }
        public void SetModelOrder(Order order, bool isUpdateable = false)
        {
            resetVisible();
            ShowOrderUser.order = order;
            ShowOrderUser.Visibility = Visibility.Visible;
            ShowOrderUser.IsEnabled = isUpdateable;
            ShowOrderUser.OrderOrderKey.IsEnabled = false;

            if (isUpdateable)
            {
                ShowOrderUser.OrderBtns.Visibility = Visibility.Visible;
                ShowOrderUser.setNameBtn();
                //ShowOrderUser.OrderBtn
            }
                
        }

        public void SetModelGuestRequest(GuestRequest guestRequest, bool isUpdateable = false)
        {
            resetVisible();
            ShowGuestRequestUser.request = guestRequest;
            ShowGuestRequestUser.UpdateRadioButton();
            ShowGuestRequestUser.Visibility = Visibility.Visible;
            ShowGuestRequestUser.IsEnabled = isUpdateable;


            if (isUpdateable)
                ShowGuestRequestUser.GuestButns.Visibility = Visibility.Visible;
        }
    }
}
