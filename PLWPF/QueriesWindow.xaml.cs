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
using Utilities;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for QueriesWindow.xaml
    /// </summary>
    /// 
    public partial class QueriesWindow : Window
    {

        public int type { get; set; }

        public QueriesWindow()
        {
            InitializeComponent();

            LoadComboboxItems();

        }

        public void LoadComboboxItems()
        {
            List<string> listValues = new List<string>
            { "כל דרישות הלקוח",
                "כל יחידות האירוח",
                "כל ההזמנות",
                "כל המארחים",
                "קיבוץ דרישות לקוח לפי אזור",
                "קיבוץ יחידות אירוח לפי אזור",
                "קיבוץ דרישות לקוח לפי מספר נופשים"
                
            };
            
            cbQueries.ItemsSource = listValues;
            
            cbQueries.SelectedIndex = 0;
        }



        public void LoadList(object list, TableDisplay tableDisplay)
        {
            try
            {
                    QueryControl.LoadList(list, tableDisplay);
            

            }
            catch (Exception e)
            {

            }
        }


        

        public void selected_query(Query query)
        {
           
            switch(query)
            {
                case Query.AllGuestRequest:
                    QueryControl.LoadList(MainWindow.bl.GetAllGuestRequests(), new GuestRequestTableDisplay());
                    break;
                case Query.AllHostingUnit:
                    QueryControl.LoadList(MainWindow.bl.GetAllHostingUnits(), new HostingUnitTableDisplay());
                    break;
                case Query.AllOrders:
                    QueryControl.LoadList(MainWindow.bl.GetAllOrders(), new OrderTableDisplay());
                    break;
                case Query.GroupGuestRequestByArea:
                    Dictionary<AreaCode, List<GuestRequest>> group = MainWindow.bl.getGuestRequestGroupedByArea();
                    QueryControl.LoadList(group.DictionaryToListItems<AreaCode, GuestRequest>(), new GuestRequestTableDisplay());
                    break;
                case Query.GroupHostingUnitByArea:
                    Dictionary<AreaCode, List<HostingUnit>> group2 = MainWindow.bl.getHostingUnitGroupedByArea();
                    QueryControl.LoadList(group2.DictionaryToListItems<AreaCode, HostingUnit>(), new HostingUnitTableDisplay());
                    break;
                case Query.GroupGuestRequestByAmountPeople:
                    Dictionary<int, List<GuestRequest>> group3 = MainWindow.bl.getGuestRequestGroupedByNumOfPeople();
                    QueryControl.LoadList(group3.DictionaryToListItems<int, GuestRequest>(), new GuestRequestTableDisplay());
                    break;
                case Query.GroupHostByNumUnits:
                    Dictionary<int, List<Host>> group4 = MainWindow.bl.getHostGroupedByNumUnits();
                    QueryControl.LoadList(group4.DictionaryToListItems<int, Host>(), new HostTableDisplay());
                    break;
                default:
                    break;
            }
        }

        private void cbQueries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbBox = (sender as ComboBox);

            Query query;

            switch(cbBox.SelectedIndex)
            {
                case 0:
                    query = Query.AllGuestRequest;
                    break;
                case 1:
                    query = Query.AllHostingUnit;
                    break;
                case 2:
                    query = Query.AllOrders;
                    break;
                case 3:
                    query = Query.GroupHostByNumUnits;
                    break;
                case 4:
                    query = Query.GroupGuestRequestByArea;
                    break;
                case 5:
                    query = Query.GroupHostingUnitByArea;
                    break;
                case 6:
                    query = Query.GroupGuestRequestByAmountPeople;
                    break;
                default:
                    query = 0;
                    break;
            }

            selected_query(query);
        }
    }
}
