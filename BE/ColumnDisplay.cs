using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BE
{
    public class ColumnDisplay
    {
        private string header;
        private string path;
        public bool isBindNeeded { get; set; }
        private DataGridLength width;
        //private DataGridLength height;

        public ColumnDisplay()
        {
            isBindNeeded = false;
        }


        public string Header {
            get { return header; }
            set { header = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public DataGridLength Width
        {
            get { return width; }
            set { width = value; }
        }
        /*
        public double Width
        {
            get { return width.Value; }
            set { width = new DataGridLength(value); }
        }
        */
        
        /*
        public double HeightStar
        {
            get { return height.Value; }
            set { height = new DataGridLength(value, DataGridLengthUnitType.Star); }
        }

        public double Height
        {
            get { return height.Value; }
            set { height = new DataGridLength(value); }
        }
        */
    }
}
