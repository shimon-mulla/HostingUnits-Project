using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostTableDisplay : TableDisplay
    {
        public HostTableDisplay()
        {

            setColumns(ColumnDisplayData.HostColumnsDisplay);

            ToView = true;
            //ToUpdate = true;
            //ToDelete = true;

        }
    }
}