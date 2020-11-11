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
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {

        public CalendarWindow()
        {
            InitializeComponent();
        }

        public void SetBlackoutDates(bool[,] myDates)
        {
            int m = DateTime.Today.Month;

            

            for (int i =0;i < myDates.GetLength(0);i++)
                for(int j = 0;j < myDates.GetLength(1);j++)
                {
                    if(myDates[i,j])
                    {
                        DateTime temp;
                        if (DateTime.TryParse((j+1) + "/" + (i+1) + "/" + DateTime.Today.Year, out temp))
                        {
                            myCalendar.BlackoutDates.Add(new CalendarDateRange(temp));
                        }
                    }
                }

        }

        public void ClearBlackoutDates()
        {
            myCalendar.BlackoutDates.Clear();
        }
    }
}
