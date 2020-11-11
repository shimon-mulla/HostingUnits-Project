using System;
using BE;
using BL;
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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl, INotifyPropertyChanged
    {
        private IBl bl = FactorySingletonBl.GetBl();
        private Regex regex = new Regex("[^0-9]+");
        private bool StatuaSendMail = false;

        private BackgroundWorker worker;

        public event PropertyChangedEventHandler PropertyChanged;

        public Order _order { set; get; }

        public Order order
        {
            set
            {
                _order = value;
                /*
                 * if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_order"));
                    }
                 * */
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_order"));
            }
            get
            {
                return _order;
            }
        }

        public OrderUserControl()
        {
            order = new Order();
            _order = new Order();
            InitializeComponent();
            setNameBtn();
            Inittatus();
            DataContext = this;
            worker = new BackgroundWorker();
            worker.DoWork += sendMail;
            worker.RunWorkerCompleted += UpdateMailCompleted;
            statusChange();
        }

        private void UpdateMailCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //bl.upd
            MessageBox.Show("האימייל נשלח בהצלחה", "הודעת מערכת", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void statusChange()
        {
            OrderStatus.SelectionChanged += IsStatusChanged;
        }

        public void IsStatusChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedValue.ToString() == "MailSent")
                StatuaSendMail = true;
            else
                StatuaSendMail = false;
            //try
            //{
            //    //sender as ComboBox
            //    ComboBox cb = sender as ComboBox;
            //    if (cb.SelectedValue.ToString() == "MailSent")
            //    {
            //        //Thread t = new Thread(sendMail);
            //        new Thread(() =>
            //        {
            //            bl.SetMailAndPasswordToConfiguration("shimonmulla2@gmail.com", "evedashem5");
            //            bl.UpdateOrder(order);
            //        }).Start();
            //        //MessageBox.Show(cb.SelectedValue.ToString());
            //    }
            //}
            //catch(ExceptionMessage ex)
            //{
            //    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void sendMail(object sender, DoWorkEventArgs e)
        {
            
            bl.UpdateOrder(order);
        }

        public void setNameBtn()
        {
            if (order.OrderKey <= 0)
                OrderBtn.Content = "הוספת הזמנה";
            else//if (this.Name.ToString() == "UpdateOrderUserControl" || this.Name.ToString() == "ShowOrderUser")
                OrderBtn.Content = "עדכון הזמנה";
        }

        public void Inittatus()
        {
            foreach (var AreaCode in MainWindow.listOrderStatusCode)
            {
                OrderStatus.Items.Add(AreaCode);
            }
        }

        private void OrderOrderKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (OrderOrderKey.Text.Length >= 8)
                {
                    order = bl.GetOrder((long)Convert.ToInt32(OrderOrderKey.Text)); // 20000000       לעדכן אצל מתן את הפונקציה
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PreviewNumericInput(object sender, TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text.ToString()))
            {
                e.Handled = true;
                MessageBox.Show("יש להכניס מספרים בלבד", "אופס", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(StatuaSendMail)
                {
                    /*new Thread(() =>
                    {
                        bl.SetMailAndPasswordToConfiguration("shimonmulla2@gmail.com", "evedashem5");
                        bl.UpdateOrder(order);
                    }).Start();*/
                    worker.RunWorkerAsync();
                }
                else
                bl.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
            //hostinUnitWindows.
            //this.Close();
        }

        //public void UpdateOrderFunc()
        //{
        //    try
        //    {
        //        bl.UpdateOrder(order);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    //MessageBox.Show(order.OrderKey.ToString());
        //}

    }
}