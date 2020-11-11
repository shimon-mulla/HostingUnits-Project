using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        /// <summary>
        /// הוספת דרישה
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        long AddRequest(GuestRequest guestRequest);

        /// <summary>
        /// עדכון דרישה
        /// </summary>
        /// <param name="guestRequest"></param>
        void UpdateRequest(GuestRequest guestRequest);

        /// <summary>
        /// הוספת יחידת אירוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        long AddUnit(HostingUnit hostingUnit);

        /// <summary>
        /// הסרת יראידת אירוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        void RemoveUnit(HostingUnit hostingUnit);

        /// <summary>
        /// עדכון יחידה
        /// </summary>
        /// <param name="hostingUnit"></param>
        void UpdateUnit(HostingUnit hostingUnit);
        //GuestRequest GetGuestRequest(long )
        //Order CreateOrder(GuestRequest guestRequest);

        /// <summary>
        /// הוספת הזמנה
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);
        /// <summary>
        /// עדכון הזמנה
        /// </summary>
        /// <param name="order"></param>
        void UpdateOrder(Order order);

        List<HostingUnit> GetAllHostingUnits();
        List<GuestRequest> GetAllGuestRequests();
        List<Order> GetAllOrders();
        List<BankAccount> GetAllBankBranches();
        bool IsOrderExist(Order order);
        bool IsOrderExist(long key);
        bool IsUnitExist(HostingUnit hostingUnit);
        bool IsUnitExist(long key);
        bool IsRequestExit(GuestRequest guestRequest);
        bool IsRequestExit(long key);
        GuestRequest GetGuestRequest(long key);
        bool IsCollectionClearance(HostingUnit hostingUnit);
        bool IsOrderStatus(Order order, OrderStatusCode orderStatus);


        float GetSumConnission();
        HostingUnit GetHostingUnit(long key);

        bool IsBookingAssociatedHostingUnit(long key);

        bool IsExistOrderOpenForHost(long hostingUnitKey);
        void UpdateAllOrdersOfGuestRequest(Order order);
        Order GetOrder(long key);
        void initilizeListToXml();
        float GetCommission();

        void SetMailAndPasswordToConfiguration(string mail, string password);
        string GetMail();
        string GetPassword();
        List<string> GetBanksList();
        List<string> GetBranchesList(string BankName);
        BankBranch GetBranch(int bankKey, int branchKey);
        string msg();
        string GetWebUsername();
        string GetWebPassword();
    }


}
