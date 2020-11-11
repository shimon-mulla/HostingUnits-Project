using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace UI
{
    class MenuActions
    {
        private static ConsoleKeyInfo UserInput;


        public static int GetInput()
        {
            UserInput = Console.ReadKey(); // get user input

            int opt = -1; // Variable to hold number

            // We should check char for a Digit, so that we will not get exceptions from Parse method
            if (char.IsDigit(UserInput.KeyChar))
                opt = int.Parse(UserInput.KeyChar.ToString());

            return opt;
        }

        public static void OpenningAction()
        {
            OpenningMenuOption choice;

            do
            {
                Menus.OpenningMenu();

                choice = (OpenningMenuOption)GetInput();
                switch (choice)
                {
                    case OpenningMenuOption.GuestRequest:
                        GuestRequestAction();
                        break;
                    case OpenningMenuOption.HostingUnit:
                        HostingUnitAction();
                        break;
                    case OpenningMenuOption.WebAdmin:
                        WebAdminAction();
                        break;
                    default:
                        //Console.WriteLine(choice);

                        break;
                }


            } while (choice != OpenningMenuOption.Back);
        }


        public static void GuestRequestAction()
        {
            GuestRequestMenuOption choice;

            do
            {
                Menus.GuestRequestMenu();
                choice = (GuestRequestMenuOption)GetInput();
                switch (choice)
                {
                    case GuestRequestMenuOption.AddGuestRequest:
                        //add function
                        Actions.AddGuestRequest();
                        Console.WriteLine("Option 'AddGuestRequest' selected");
                        break;
                    default:

                        break;
                }


            } while (choice != GuestRequestMenuOption.Back);
        }

        public static void HostingUnitAction()
        {
            HostingUnitMenuOption choice;

            do
            {
                Menus.HostingUnitMenu();
                choice = (HostingUnitMenuOption)GetInput();
                switch (choice)
                {
                    case HostingUnitMenuOption.AddHostingUnit:
                        //add function
                        //Console.WriteLine("Option 'AddHostingUnit' selected");
                        Actions.AddHostingUnit();
                        break;
                    case HostingUnitMenuOption.PrivateArea:
                        PrivaeAreaAction();
                        break;
                    default:

                        break;
                }


            } while (choice != HostingUnitMenuOption.Back);
        }

        public static void PrivaeAreaAction()
        {
            PrivateAreaMenuOption choice;

            do
            {
                //Program.bl.
                Menus.PrivateAreaMenu();
                choice = (PrivateAreaMenuOption)GetInput();
                switch (choice)
                {
                    case PrivateAreaMenuOption.UpdateHostingUnit:
                        //update function
                        //Console.WriteLine("Option 'UpdateHostingUnit' selected");
                        Actions.UpdateHostingUnit();
                        break;
                    case PrivateAreaMenuOption.Order:
                        OrderAction();
                        break;
                    case PrivateAreaMenuOption.RemoveHostingUnit:
                        //delete function
                        //Console.WriteLine("Option 'RemoveHostingUnit' selected");
                        Actions.RemoveHostingUnit();
                        break;
                    default:

                        break;
                }


            } while (choice != PrivateAreaMenuOption.Back);
        }

        public static void OrderAction()
        {
            OrderMenuOption choice;

            do
            {
                //Program.bl.
                Menus.OrderMenu();
                choice = (OrderMenuOption)GetInput();
                switch (choice)
                {
                    case OrderMenuOption.QueryGuestList:
                        //update function
                        //Console.WriteLine("Option 'QueryGuestList' selected");
                        Actions.QueryGuestsList();
                        break;
                    case OrderMenuOption.QueryOrderList:
                        //order list
                        //Console.WriteLine("Option 'QueryOrderList' selected");
                        Actions.QueryOrdersList();
                        break;
                    default:

                        break;
                }


            } while (choice != OrderMenuOption.Back);
        }

        public static void WebAdminAction()
        {
            WebAdminMenuOption choice;

            do
            {
                //Program.bl.
                Menus.WebAdminMenu();
                choice = (WebAdminMenuOption)GetInput();
                switch (choice)
                {
                    case WebAdminMenuOption.QueryGuestList:
                        //QueryGuestList function
                        Console.WriteLine("Option 'QueryGuestList' selected");
                        break;
                    case WebAdminMenuOption.QueryHostingUnitList:
                        //QueryHostingUnitList function
                        Console.WriteLine("Option 'QueryHostingUnitList' selected");
                        break;
                    case WebAdminMenuOption.QueryOrderList:
                        //QueryOrderList list
                        Console.WriteLine("Option 'QueryOrderList' selected");
                        break;
                    case WebAdminMenuOption.Etc:
                        //Etc function
                        Console.WriteLine("Option 'Etc' selected");
                        break;
                    default:

                        break;
                }


            } while (choice != WebAdminMenuOption.Back);
        }
    }
}
