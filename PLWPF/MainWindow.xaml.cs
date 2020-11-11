using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static List<string> listAreaType = Enum.GetValues(typeof(AreaCode))
                                      .Cast<AreaCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        public static List<string> listUnitType = Enum.GetValues(typeof(HostingUnitTypeCode))
                                      .Cast<HostingUnitTypeCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        public static List<string> listPreferencesCode = Enum.GetValues(typeof(GuestPreferencesCode))
                                      .Cast<GuestPreferencesCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        public static List<string> listOrderStatusCode = Enum.GetValues(typeof(OrderStatusCode))
                                      .Cast<OrderStatusCode>()
                                      .Select(v => v.ToString())
                                      .ToList();



        public List<GuestRequest> AllGuests
        {
            get { return bl.GetAllGuestRequests(); }
            set { }
        }

        public List<HostingUnit> AllUnits
        {
            get { return bl.GetAllHostingUnits(); }
            set { }
        }

        public List<Order> AllOrders
        {
            get { return bl.GetAllOrders(); }
            set { }
        }



        public static IBl bl = FactorySingletonBl.GetBl();
        public bool flagUnit = false;
        public bool flagQuery = false;
        private LoginWindow login;

        public MainWindow()
        {
            InitializeComponent();
            LanguageSetting.SetLanguage("he");

            setBinding();


            unitBtns.Visibility = Visibility.Hidden;

        }

        public void setBinding()
        {
            this.FlowDirection = LanguageSetting.Direction;
            //mainTab.Header = LanguageSetting.Get("Menu", "main");
            //guestTab.Header = LanguageSetting.Get("Menu", "guest_request");
            //hostingTab.Header = LanguageSetting.Get("Menu", "hosting_unit");
            //webOwnerTab.Header = LanguageSetting.Get("Menu", "web_owner");
        }


        private void PerformAddUnit(object sender, RoutedEventArgs e)
        {
            ShowHostingUnit addWindow = new ShowHostingUnit();

            addWindow.Title = "הופסת יחידת אירוח";
            addWindow.SetModelHostingUnit(new HostingUnit{Diary = new bool[12,31],Owner = new Host { BankAccountDetails = new BankAccount() } }, true);

            addWindow.Show();
        }

        private void OpenPrivateArea(long key)
        {
            PrivateAreaWindow privateAreaWindow = new PrivateAreaWindow();
            privateAreaWindow.hostingUnit = bl.getHostingUnit(key);
            privateAreaWindow.Title = "איזור אישי - יחידת אירוח " + key;
            privateAreaWindow.Show();
           
        }


        private void LoginUnit(object sender, RoutedEventArgs e)
        {
            if (login != null && login.IsVisible)
            {
                if (login.WindowState == WindowState.Minimized)
                    login.WindowState = WindowState.Normal;
                login.Focus();
                return;
            }   
            login = new LoginWindow();
            login.Closing += ClosingLogin;
            login.LoginUserControl.TitleAuth = "הזדהות - יחידת אירוח";
            login.LoginUserControl.SetToolTipValues("מספר יחידת אירוח קיימת - יש לבדוק ברשימות", "כל סיסמה");
            login.LoginUserControl.spPass.Visibility = Visibility.Collapsed;
            login.Show();

        }


        private void ClosingLogin(object sender, CancelEventArgs e)
        {
            LoginWindow l = sender as LoginWindow;


            if (l.LoginUserControl.success)
            {
                OpenPrivateArea(int.Parse(l.LoginUserControl.userID));
            }

        }


        

        private void OpenWebOwnerQueries()
        {
            QueriesWindow queryW = new QueriesWindow();

            queryW.cbQueries.SelectedIndex = 0;

            queryW.Show();

        }


        private void LoginWebOwner(object sender, RoutedEventArgs e)
        {
            if (login != null && login.IsVisible)
            {
                if (login.WindowState == WindowState.Minimized)
                    login.WindowState = WindowState.Normal;
                login.Focus();
                return;
            }
            login = new LoginWindow();
            login.Closing += ClosingLogin1;
            login.LoginUserControl.TitleAuth = "הזדהות - בעל האתר";
            login.LoginUserControl.LoginType = 1;
            login.LoginUserControl.SetToolTipValues(bl.GetWebUsername(), bl.GetWebPassword());
            login.Show();
            //login
        }


        private void ClosingLogin1(object sender, CancelEventArgs e)
        {
            LoginWindow l = sender as LoginWindow;


            if (l.LoginUserControl.success)
            {
                OpenWebOwnerQueries();
            }

            //MessageBox.Show(l.LoginUserControl.success.ToString());
        }



        private void AddGuestBtn(object sender, RoutedEventArgs e)
        {
            GuestRequestWindow guestRequestWindow = new GuestRequestWindow();
            guestRequestWindow.Show();
            //guestRequestWindow.Closing += showMain;
            //this.Hide();
        }

        /*
        private void showMain(object sender, CancelEventArgs e)
        {
            this.Show();
        }
        */

     

        private void btnClick_Unit(object sender, RoutedEventArgs e)
        {
            if (!flagUnit)
            {
                unitBtns.Visibility = Visibility.Visible;
                flagUnit = true;
            }
            else
            {
                flagUnit = false;
                unitBtns.Visibility = Visibility.Hidden;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LanguageSetting.SetLanguage("he");
            setBinding();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            LanguageSetting.SetLanguage("en");
            setBinding();
        }


    }
}
