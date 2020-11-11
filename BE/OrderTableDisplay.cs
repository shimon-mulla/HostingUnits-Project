using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BE
{
    public class OrderTableDisplay : TableDisplay
    {
        public OrderTableDisplay()
        {

            setColumns(ColumnDisplayData.OrderColumnsDisplay);
            ToView = true;
            ToUpdate = true;
            //ToDelete = true;



            /*
            setColumns(
                new ColumnDisplay
                {
                    Header = "מספר הזמנה",
                    Path = "OrderKey",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                },
                new ColumnDisplay
                {
                    Header = "מספר יחידת אירוח",
                    Path = "HostingUnitKey",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "מספר דרישת לקוח",
                    Path = "GuestRequestKey",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "מצב",
                    Path = "Status",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "תאריך פתיחת הזמנה",
                    Path = "CreateDate",
                    isBindNeeded = true,
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }
           );*/
        }
    }
}
