using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    /*class Test
    {
        public int num1 { get; set; }
        public string name { get; set; }
    }*/



    class Program
    {
        public static IBl bl = FactorySingletonBl.GetBl();
        static void Main(string[] args)
        {
            /*List<Test> listT = new List<Test> { 
                new Test
                {
                    num1 = 3,
                    name = "Matan"
                },
                new Test
                {
                    num1 = 2,
                    name = "Dudu"
                },
                new Test
                {
                    num1 = 3,
                    name = "Avashalom"
                }
            };

            listT.Where(item => item.num1 == 3).ToList().ForEach(
                                   item =>
                                  item.name ="TestMe");

            foreach(var ListItem in listT)
                Console.WriteLine(ListItem.name);
                */
            //Console.WriteLine(DateTime.Today.ToShortDateString());
            //UpdateDates();
            MenuActions.OpenningAction();

        }


        public static void UpdateDates()
        {
            Program.bl.UpdateDatesDS();//יש בעיה בשמירת יומן יחידת אירוח כי זה מוחק ויוצר מחדש ואז המפתח משתנה
            Console.WriteLine("Saved\n");
        }

        
    }
}
