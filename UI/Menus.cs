using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Menus
    {

        //מסך פתיחה
        public static void OpenningMenu()
        {
            Console.WriteLine("\n{0, 17}", "Openning Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Guest Request","- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "Hosting Unit","- 2");
            Console.WriteLine("-{0, -15}{1, -15}", "Web Admin","- 3");
            Console.WriteLine("-{0, -15}{1, -15}", "Back","- 0");
        }

        //מסך דרישת לקוח
        public static void GuestRequestMenu()
        {
            Console.WriteLine("\n{0, 17}", "Guest request Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Add guest Request","- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "Back", "- 0");
        }

        //מסך יחידת אירוח
        public static void HostingUnitMenu()
        {
            Console.WriteLine("\n{0, 17}", "Hosting Unit Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Add hosting unit", "- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "Private Area", "- 2");
            Console.WriteLine("-{0, -15}{1, -15}", "Back", "- 0");
        }

        //מסך אזור אישי
        public static void PrivateAreaMenu()
        {
            Console.WriteLine("\n{0, 17}", "Private Area Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Update hosting unit", "- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "Order", "- 2");
            Console.WriteLine("-{0, -15}{1, -15}", "Remove hosting unit", "- 3");
            Console.WriteLine("-{0, -15}{1, -15}", "Back", "- 0");
        }

        //מסך הזמנה
        public static void OrderMenu()
        {
            Console.WriteLine("\n{0, 17}", "Order Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Query Guests list", "- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "orders list", "- 2");
            Console.WriteLine("-{0, -15}{1, -15}", "Back", "- 0");
        }


        public static void WebAdminMenu()
        {
            Console.WriteLine("\n{0, 17}", "Web Admin Menu");
            Console.WriteLine("-{0, -15}{1, -15}", "Query guests list", "- 1");
            Console.WriteLine("-{0, -15}{1, -15}", "Query hosting units list", "- 2");
            Console.WriteLine("-{0, -15}{1, -15}", "Query orders list", "- 3");
            Console.WriteLine("-{0, -15}{1, -15}", "etc...", "- 4");
            Console.WriteLine("-{0, -15}{1, -15}", "Back", "- 0");
        }

        public static void PrintList(List<String> lst, string title, string inputMessage)
        {
            Console.WriteLine(title + ", choose from the following: ");
            Console.WriteLine("{0, -15}{1, -15}", "-Name-", "-Code-");
            for (int i = 0; i < lst.Count; i++)
                Console.WriteLine("{0, -15}{1, -15}", lst[i], "- " + i);

            Console.Write(inputMessage +": ");
            
        }
    }
}
