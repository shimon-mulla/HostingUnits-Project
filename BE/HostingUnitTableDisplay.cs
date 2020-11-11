using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BE
{
    public class HostingUnitTableDisplay : TableDisplay
    {
        public HostingUnitTableDisplay()
        {
            setColumns(ColumnDisplayData.HostingUnitColumnsDisplay);
            setToShow(
                PropertiesData.HostingUnitKey,
                PropertiesData.HostingUnitName,
                PropertiesData.Area,
                PropertiesData.Type,
                PropertiesData.Adults
                );
            ToView = true;
            ToUpdate = true;
            ToDelete = true;
            /*setColumns(
                new ColumnDisplay
                {
                    Header = "מספר יחידת אירוח",
                    Path = "HostingUnitKey",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                },
                new ColumnDisplay
                {
                    Header = "שם יחידת האירוח",
                    Path = "HostingUnitName",
                    isBindNeeded = true,
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "סוג היחידה",
                    Path = "Type",
                    isBindNeeded = true,
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "אזור",
                    Path = "Area",
                    isBindNeeded = true,
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }
           );*/
        }
    }
}
