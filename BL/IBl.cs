using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBl
    {
        void AddRequest(GuestRequest guestRequest);
        void UpdateRequest(GuestRequest guestRequest);

        void AddUnit(HostingUnit hostingUnit);
        void RemoveUnit(HostingUnit hostingUnit);
        void UpdateUnit(HostingUnit hostingUnit);

        void AddOrder(Order order);
        void UpdateOrder(Order order);

        List<HostingUnit> GetAllHostingUnits();
        List<GuestRequest> GetAllGuestRequests();
        List<Order> GetAllOrders();
        List<BankAccount> GetAllBankBranches();
        List<HostingUnit> GetHostingUnitsByDate(DateTime date, int num);
        List<HostingUnit> GetHostingUnitsByDate(string date, int num);

        void CreateOrder(GuestRequest guestRequest);

        int getNumDaysBetween(string dateIn, string dateOut = "");
        int getNumDaysBetween(DateTime dateIn);
        int getNumDaysBetween(DateTime dateIn, DateTime dateOut);

       // GuestRequest GetGuestRequest(long key);

        Dictionary<AreaCode, List<GuestRequest>> getGuestRequestGroupedByArea();
        Dictionary<AreaCode, List<HostingUnit>> getHostingUnitGroupedByArea();
        Dictionary<int, List<GuestRequest>> getGuestRequestGroupedByNumOfPeople();
        Dictionary<int, List<Host>> getHostGroupedByNumUnits();
        List<HostingUnit> getHostingUnitByCondition(UnitCondition cond);
        List<GuestRequest> getGuestRequestByCondition(GuestCondition cond);
        
        List<Order> getListOrdersSinceCreated(int days);
        List<Order> getListOrdersByUnit(long key);
        List<GuestRequest> getListGuestRequestsByCondition(Predicate<GuestRequest> predicate);
        int getNumOfReceivedOrdersByGuestRequest(GuestRequest guestRequest);
        int getNumOfReceivedOrdersByHostingUnit(HostingUnit hostingUnit);
        void UpdateDatesDS();
        HostingUnit getHostingUnit(long unitKey);
        Order GetOrder(long v);
        void initXmls();
        void SetMailAndPasswordToConfiguration(string mail, string password);
        GuestRequest getGR(long guestRequestKey);

        string GetWebUsername();
        string GetWebPassword();

        List<string> GetBanksList();
        List<string> GetBranchesList(string BankName);
        BankBranch GetBranch(int bankKey, int branchKey);
    }
}