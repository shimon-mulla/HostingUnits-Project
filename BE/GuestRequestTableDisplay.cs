using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BE
{
    public class GuestRequestTableDisplay : TableDisplay
    {
        public GuestRequestTableDisplay()
        {
            setColumns(ColumnDisplayData.GuestRequestColumnsDisplay);
            setToShow(
                 PropertiesData.GuestRequestKey,
                 PropertiesData.FullName,
                 PropertiesData.EntryDate,
                 PropertiesData.ReleaseDate,
                PropertiesData.Adults, 
                PropertiesData.Children);
            ToView = true;
            //ToDelete = true;
            /*setColumns(
                new ColumnDisplay
                {
                    Header = "מספר דרישת לקוח",
                    Path = "GuestRequestKey",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                },
                new ColumnDisplay
                {
                    Header = "שם הלקוח",
                    Path = "FullName",
                    isBindNeeded = true,

                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "מצב",
                    Path = "Status",
                    isBindNeeded = true,
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star)
                }, new ColumnDisplay
                {
                    Header = "תאריך רישום למערכת",
                    Path = "RegistrationDate",
                    isBindNeeded = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                }
           );*/
        }

        /*public void setColumns(Dictionary<GuestRequestProperties, ColumnDisplay> cols)
        {
            columns.Clear();
            columns = cols;

        }*/
    }
}
