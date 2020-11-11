using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace UI
{
    class Actions
    {
        private static List<string> listAreaType = Enum.GetValues(typeof(AreaCode))
                                      .Cast<AreaCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        private static List<string> listUnitType = Enum.GetValues(typeof(HostingUnitTypeCode))
                                      .Cast<HostingUnitTypeCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        private static List<string> listPreferencesCode = Enum.GetValues(typeof(GuestPreferencesCode))
                                      .Cast<GuestPreferencesCode>()
                                      .Select(v => v.ToString())
                                      .ToList();

        public static void AddGuestRequest()
        {
            try
            {
                GuestRequest gr = new GuestRequest();

                Console.WriteLine("\n");

                string date;

                Console.Write("Enter Private name: ");
                gr.PrivateName = Console.ReadLine();
                Console.Write("Enter Last name: ");
                gr.FamilyName = Console.ReadLine();
                Console.Write("Enter mail address: ");
                gr.MailAdress = Console.ReadLine();

                Console.Write("Enter entry date: ");
                //gr.EntryDate = Console.ReadLine();
                date = Console.ReadLine();

                Console.Write("Enter leave date: ");
                //gr.ReleaseDate = Console.ReadLine();
                date = Console.ReadLine();

                //Area
                Menus.PrintList(listAreaType, "Enter area", "Area Code");
                gr.Area = (AreaCode)MenuActions.GetInput();
                Console.WriteLine();

                //Unit type
                Menus.PrintList(listUnitType, "Enter Hosting unit type", "Unit Type Code");
                gr.Type = (HostingUnitTypeCode)MenuActions.GetInput();
                Console.WriteLine();

                Console.Write("Enter num of adults name: ");
                gr.Adults = int.Parse(Console.ReadLine());//MenuActions.getInput();
                Console.Write("Enter num of childrens name: ");
                gr.Children = int.Parse(Console.ReadLine());//MenuActions.getInput();

                //---------Prefences---------
                //Pool
                Menus.PrintList(listPreferencesCode, "Enter prefernce in pool", "Preference Code");
                gr.Pool = (GuestPreferencesCode)MenuActions.GetInput();
                Console.WriteLine();

                //Jacuzzi
                Menus.PrintList(listPreferencesCode, "Enter prefernce in jacuzzi", "Preference Code");
                gr.Jacuzzi = (GuestPreferencesCode)MenuActions.GetInput();
                Console.WriteLine();

                //Garden
                Menus.PrintList(listPreferencesCode, "Enter prefernce in garden", "Preference Code");
                gr.Garden = (GuestPreferencesCode)MenuActions.GetInput();
                Console.WriteLine();

                //Porch
                Menus.PrintList(listPreferencesCode, "Enter prefernce in porch", "Preference Code");
                gr.Porch = (GuestPreferencesCode)MenuActions.GetInput();
                Console.WriteLine();

                //Children Attractions
                Menus.PrintList(listPreferencesCode, "Enter prefernce in Children Attractions", "Preference Code");
                gr.ChildrensAttractions = (GuestPreferencesCode)MenuActions.GetInput();
                Console.WriteLine();

                Program.bl.AddRequest(gr);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static HostingUnit getHostingUnitInput(HostingUnit unit = null)
        {
            bool oldUnit = true;
            if(unit == null)
            {
                oldUnit = false;
                unit = new HostingUnit();


                unit.Diary = new bool[12, 31];

                /*
                 Need to get user input of sub area, owner

                 */
                unit.Owner = new Host();
            }



            string ansUpdate = "y";

            Console.WriteLine("\n");

            if(oldUnit)
            {
                Console.WriteLine("Update Unit name? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            
            if(ansUpdate == "y")
            {
                Console.Write("Enter Unit name: ");
                unit.HostingUnitName = Console.ReadLine();

            }

            if (oldUnit)
            {
                Console.WriteLine("Update Area? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            //Area
            if (ansUpdate == "y")
            {
                Menus.PrintList(listAreaType, "Enter area", "Area Code");
                unit.Area = (AreaCode)MenuActions.GetInput();
                Console.WriteLine();
            }


            if (oldUnit)
            {
                Console.WriteLine("Update Sub Area? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            //Area
            if (ansUpdate == "y")
            {
                Console.Write("Enter Sub Area: ");
                unit.SubArea = Console.ReadLine();
            }

            if (oldUnit)
            {
                Console.WriteLine("Update Unit Type? (y/n)");
                ansUpdate = Console.ReadLine();

            }

            if (ansUpdate == "y")
            {
                //Unit type
                Menus.PrintList(listUnitType, "Enter Hosting unit type", "Unit Type Code");
                unit.Type = (HostingUnitTypeCode)MenuActions.GetInput();
                Console.WriteLine();
            }

            if (oldUnit)
            {
                Console.WriteLine("Update num of adults? (y/n)");
                ansUpdate = Console.ReadLine();

            }

            if (ansUpdate == "y")
            {
                Console.Write("Enter num of adults: ");
                unit.Adults = int.Parse(Console.ReadLine());//MenuActions.getInput();
            }

            if (oldUnit)
            {
                Console.WriteLine("Update num of children? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            if (ansUpdate == "y")
            {
                Console.Write("Enter num of childrens: ");
                unit.Children = int.Parse(Console.ReadLine());//MenuActions.getInput();
            }

            string ans;
            //---------Prefences---------

            if (oldUnit)
            {
                Console.WriteLine("Update pool? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            if (ansUpdate == "y")
            {
                //Pool
                Console.Write("Enter pool exists 1/0: ");
                ans = Console.ReadLine();
                unit.Pool = string.Equals(ans, "y") ? false : true;
                //Console.WriteLine();
            }


            if (oldUnit)
            {
                Console.WriteLine("Update jacuzzi? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            if (ansUpdate == "y")
            {
                //Jacuzzi
                Console.Write("Enter jacuzzi exists 1/0: ");
                ans = Console.ReadLine();
                unit.Jacuzzi = string.Equals(ans, "y") ? false : true;
                //Console.WriteLine();
            }
            if (oldUnit)
            {
                Console.WriteLine("Update garden? (y/n)");
                ansUpdate = Console.ReadLine();

            }
            if (ansUpdate == "y")
            {
                //Garden
                Console.Write("Enter garden exists 1/0: ");
                ans = Console.ReadLine();
                unit.Garden = string.Equals(ans, "y") ? false : true;
                //Console.WriteLine();
            }
            if (oldUnit)
            {
                Console.WriteLine("Update porch? (y/n)");
                ansUpdate = Console.ReadLine();

            }

            if (ansUpdate == "y")
            {
                //Porch
                Console.Write("Enter porch exists 1/0: ");
                ans = Console.ReadLine();
                unit.Porch = string.Equals(ans, "y") ? false : true;
                //Console.WriteLine();
            }

            if (oldUnit)
            {
                Console.WriteLine("Update children attractios? (y/n)");
                ansUpdate = Console.ReadLine();

            }

            if (ansUpdate == "y")
            {
                //Children Attractions
                Console.Write("Enter ChildrensAttractions exists 1/0: ");
                ans = Console.ReadLine();
                unit.ChildrensAttractions = string.Equals(ans, "y") ? false : true;
                //Console.WriteLine();
            }
            return unit;
        }

        public static void AddHostingUnit()
        {
            try
            {
                Program.bl.AddUnit(getHostingUnitInput());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateHostingUnit()
        {
            try
            {
                int unitKey;
                Console.Write("Please enter Hosting unit key: ");
                unitKey = int.Parse(Console.ReadLine());

                HostingUnit unit = Program.bl.getHostingUnit(unitKey);
                Console.WriteLine("Hostign Unit ({0}): ", unitKey);
                Console.WriteLine(unit);

                unit = getHostingUnitInput(unit);

                Program.bl.UpdateUnit(unit);
                //Console.WriteLine("Done Updating...");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void RemoveHostingUnit()
        {
            try
            {
                int unitKey;
                Console.Write("Please enter Hosting unit key: ");
                unitKey = int.Parse(Console.ReadLine());

                HostingUnit unit = Program.bl.getHostingUnit(unitKey);
                Console.WriteLine("Hosting Unit ({0}): ", unitKey);
                Console.WriteLine(unit);

                Program.bl.RemoveUnit(unit);
                Console.WriteLine("Done Removing...");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void QueryGuestsList()
        {
            try
            {
                List<GuestRequest> guestList = Program.bl.GetAllGuestRequests();

                Console.WriteLine("List of Guest Requests");
                foreach(GuestRequest gr in guestList)
                    Console.WriteLine(gr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void QueryOrdersList()
        {
            try
            {
                List<Order> orderList = Program.bl.GetAllOrders();

                Console.WriteLine("List of Orders");
                foreach (Order order in orderList)
                {
                    Console.WriteLine(order);
                    Console.WriteLine("Hosting unit: " + Program.bl.getHostingUnit(order.HostingUnitKey));
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
