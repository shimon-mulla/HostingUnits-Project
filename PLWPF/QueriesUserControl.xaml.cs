using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for QueriesUserControl.xaml
    /// </summary>
    public partial class QueriesUserControl : UserControl
    {

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static  DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(QueriesUserControl), new PropertyMetadata());


        public QueriesUserControl()
        {
            InitializeComponent();


            List<GuestRequest> gList = MainWindow.bl.GetAllGuestRequests();

            
            if (Source != null)
                DataGuest.ItemsSource = Source as IEnumerable<object>;
            
        }
        

        public  void LoadList(object list, TableDisplay tableDisplay)
        {
            DataGuest.Columns.Clear();


            foreach (DataGridColumn column in tableDisplay.getGridColumns())
                DataGuest.Columns.Add(column);

            DataGuest.ItemsSource = list as IEnumerable<object>;

            InitGridContextMenu(tableDisplay.ToView, tableDisplay.ToUpdate, tableDisplay.ToDelete);

        }


        private void InitGridContextMenu(bool toView = true, bool toUpdate = false, bool toDelete = false)
        {
            ContextMenu m = new ContextMenu();

            MenuItem viewItem = new MenuItem { InputGestureText = "צפייה", IsEnabled = toView };
            if (toView)
            {
                viewItem.Foreground = Brushes.Black;
                viewItem.FontWeight = FontWeights.Bold;
            }

            MenuItem updateItem = new MenuItem { InputGestureText = "עדכון", IsEnabled = toUpdate };
            if(toUpdate)
            {
                updateItem.Foreground = Brushes.Black;
                updateItem.FontWeight = FontWeights.Bold;
            }

            MenuItem deleteItem = new MenuItem { InputGestureText = "מחיקה", IsEnabled = toDelete };
            if (toDelete)
            {
                deleteItem.Foreground = Brushes.Black;
                deleteItem.FontWeight = FontWeights.Bold;
            }

            m.Items.Add(viewItem);
            m.Items.Add(updateItem);
            m.Items.Add(deleteItem);

            (m.Items.GetItemAt(0) as MenuItem).Click += contexMenu_ItemClicked;
            (m.Items.GetItemAt(1) as MenuItem).Click += contexMenu_ItemClicked;
            (m.Items.GetItemAt(2) as MenuItem).Click += contexMenu_ItemClicked;

            m.StaysOpen = true;
            DataGuest.ContextMenu = m;
        }


        void contexMenu_ItemClicked(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            object record = ((item.Parent as ContextMenu).PlacementTarget as DataGrid).CurrentItem;

            if (item.InputGestureText.Equals("צפייה"))
                ViewRecord(record);
            if (item.InputGestureText.Equals("עדכון"))
                UpdateRecord(record);
            else if (item.InputGestureText.Equals("מחיקה"))
                DeleteRecord(record);
        }


        private void ViewRecord(object record)
        {
            ShowHostingUnit ModelWindow = new ShowHostingUnit();

            if (record is GuestRequest)
            {
                ModelWindow.Title = "צפייה בדרישת לקוח";
                ModelWindow.SetModelGuestRequest((GuestRequest)record);
                ModelWindow.Show();
            }
            else if (record is HostingUnit)
            {
                ModelWindow.Title = "צפייה ביחידת אירוח";


                ModelWindow.SetModelHostingUnit((HostingUnit)record);

                ModelWindow.Show();

            }
            else if (record is Order)
            {
                ModelWindow.Title = "צפייה בהזמנה";

                ModelWindow.SetModelOrder((Order)record);
                ModelWindow.Show();
            }
        }



        private void UpdateRecord(object record)
        {

            ShowHostingUnit ModelWindow = new ShowHostingUnit();

            if (record is HostingUnit)
            {
                ModelWindow.Title = "עדכון יחידת אירוח";
                ModelWindow.SetModelHostingUnit((HostingUnit)record, true);

                ModelWindow.Show();
            }
            else if(record is Order)
            {
                ModelWindow.Title = "עדכון הזמנה";
                ModelWindow.SetModelOrder((Order)record, true);
                ModelWindow.Show();
            }
        }

        private void DeleteRecord(object record)
        {
            if (record is HostingUnit)
            {
                HostingUnit unit = (HostingUnit)record;

                try
                {
                    
                    MainWindow.bl.RemoveUnit(unit);

                    List<HostingUnit> unitsList = (List<HostingUnit>)DataGuest.ItemsSource;
                    DataGuest.ItemsSource = null;
                    unitsList.Remove(unit);
                    DataGuest.ItemsSource = unitsList;

                    MessageBox.Show("יחידת האירוח נמחקה", "הודעה", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
