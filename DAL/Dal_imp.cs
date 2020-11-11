using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDal
    {
        public OrderStatusCode Status { get; private set; }

        public void AddOrder(Order order)
        {
            Order orderCopy = (Order)order.Clone(); // Clon
            orderCopy.OrderKey = Configuration.OrderKey++;
            DataSource.OrdersCollection.Add(orderCopy);
            //DataSource.OrdersCollection.Add((Order)order.Clone());
        }

        public Order GetOrder(long key)
        {
            return (Order)DataSource.OrdersCollection.FirstOrDefault(item => item.OrderKey == key).Clone();
        }

        public long AddRequest(GuestRequest guestRequest)
        {
            GuestRequest guestRequestCopy = (GuestRequest)guestRequest.Clone();
            guestRequestCopy.GuestRequestKey = Configuration.GuestRequestKey++;
            DataSource.GuestRequestsCollection.Add(guestRequestCopy);
            return guestRequest.GuestRequestKey;
            //DataSource.GuestRequestsCollection.Add((GuestRequest)guestRequest.Clone());
        }

        public long AddUnit(HostingUnit hostingUnit)
        {
            HostingUnit hostingUnitCopy = (HostingUnit)hostingUnit.Clone();
            hostingUnitCopy.HostingUnitKey = Configuration.HostUnitKey++;
            DataSource.HostingUnitsCollection.Add(hostingUnitCopy);
            return hostingUnit.HostingUnitKey;
        }

        public List<BankAccount> GetAllBankBranches()
        {
            //List<BankAccount> BankAccountCollection = new List<BankAccount>();
            //return BankAccountCollection;
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetAllGuestRequests()
        {
            return new List<GuestRequest>(DataSource.GuestRequestsCollection.Select(guestRequest => (GuestRequest)guestRequest.Clone()));
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return new List<HostingUnit>(DataSource.HostingUnitsCollection.Select(hostingUnit => (HostingUnit)hostingUnit.Clone()));
        }

        public List<Order> GetAllOrders()
        {
            return new List<Order>(DataSource.OrdersCollection.Select(order => (Order)order.Clone()));
        }

        public void RemoveUnit(HostingUnit hostingUnit)
        {
            if (DataSource.HostingUnitsCollection.RemoveAll(item => item.HostingUnitKey == hostingUnit.HostingUnitKey) == 0)
                throw new ExceptionMessage("לא קיים");
        }

        public bool IsOrderStatus(Order order, OrderStatusCode orderStatus)
        {
            return DataSource.OrdersCollection.Any(
                item => item.OrderKey == order.OrderKey &&
                        item.Status == orderStatus
            );
        }

        public void UpdateOrder(Order order)
        {
            for (int i = 0; i < DataSource.OrdersCollection.Count; i++)
            {
                if (DataSource.OrdersCollection[i].OrderKey == order.OrderKey)
                {
                    DataSource.OrdersCollection[i].Status = order.Status;
                    break;
                }
            }
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            //if (DataSource.GuestRequestsCollection.RemoveAll(item => item.GuestRequestKey == guestRequest.GuestRequestKey) == 0)// לשנות
            AddRequest(guestRequest);
        }

        public void UpdateUnit(HostingUnit hostingUnit)
        {
            HostingUnit unit = DataSource.HostingUnitsCollection.FirstOrDefault(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);

            foreach (var Property in unit.GetType().GetProperties())
            {
                ParameterInfo[] myParameters = Property.GetIndexParameters();
                if (myParameters.Length == 0)
                {
                    Property.SetValue(unit, Property.GetValue(hostingUnit));
                }
            }

            /*
            RemoveUnit(hostingUnit);
            AddUnit(hostingUnit);
            */
        }

        public bool IsUnitExist(HostingUnit hostingUnit)// בודקת אם קיים כבר מארח בעל אותו ת.ז
        {
            return IsUnitExist(hostingUnit.HostingUnitKey);
            //return DataSource.HostingUnitsCollection.Any(item => item.Owner.HostKey == hostingUnit.Owner.HostKey);
        }

        public bool IsUnitExist(long key)
        {
            return DataSource.HostingUnitsCollection.Any(item => item.HostingUnitKey == key);//(item => item.Owner.HostKey == key);
        }

        public bool IsOrderExist(long key)
        {
            return DataSource.OrdersCollection.Any(item => item.OrderKey == key);//(item => item.Owner.HostKey == key);
        }

        public bool IsRequestExit(GuestRequest guestRequest)
        {
            return DataSource.GuestRequestsCollection.Any(item => item.GuestRequestKey == guestRequest.GuestRequestKey);
        }

        public bool IsRequestExit(long key)
        {
            return DataSource.GuestRequestsCollection.Any(item => item.GuestRequestKey == key);
        }

        public GuestRequest GetGuestRequest(long key)
        {
            return (GuestRequest)DataSource.GuestRequestsCollection
            .FirstOrDefault(item => item.GuestRequestKey == key).Clone();
        }

        public bool IsOrderExist(Order order)
        {
            return DataSource.OrdersCollection.Any(item => item.OrderKey == order.OrderKey);
        }

        public void GetListOfHostingUnigForOrder(GuestRequest guestRequest)
        {
            // צריך אולי לעשות שיחזיר רשימה של חדרים המתאימים לדרישות הלקוח
            foreach (var hostUnit in DataSource.HostingUnitsCollection)
            {
                //CheckGuestPreferencesCode(hostUnit, guestRequest);
                // CheckDates(hostUnit, guestRequest);
            }
        }

        public void UpdateAllOrdersOfGuestRequest(Order order)
        {
            DataSource.OrdersCollection
                .Where(item => item.GuestRequestKey == order.GuestRequestKey &&
                       item.Status == OrderStatusCode.NotHandledYet).ToList()
                .ForEach(item => item.Status = OrderStatusCode.ClosedDueGuestNotResponding);

        }

        public bool IsCollectionClearance(HostingUnit hostingUnit)
        {
            return hostingUnit.Owner.CollectionClearance;
        }

        public float GetSumConnission()
        {
            return Configuration.SumConnission;
        }

        public HostingUnit GetHostingUnit(long key)
        {
            return (HostingUnit)DataSource.HostingUnitsCollection.FirstOrDefault(item => item.HostingUnitKey == key).Clone();
        }

        public bool IsBookingAssociatedHostingUnit(long key)
        {
            return DataSource.OrdersCollection
                .Where(item => item.HostingUnitKey == key)
                    .Select(item => GetGuestRequest(item.GuestRequestKey))
                        .Any(item => item.Status == StatusCode.Open);
        }

        public bool IsExistOrderOpenForHost(long key)
        {
            return DataSource.OrdersCollection.Any(item => item.HostingUnitKey == key && item.Status == OrderStatusCode.NotHandledYet);
        }

        public void initilizeListToXml()
        {
            throw new NotImplementedException();
        }

        public float GetCommission()
        {
            return Configuration.SumConnission;
        }

        public void SetMailAndPasswordToConfiguration(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public string GetMail()
        {
            throw new NotImplementedException();
        }

        public string GetPassword()
        {
            throw new NotImplementedException();
        }

        public List<string> GetBanksList()
        {
            throw new NotImplementedException();
        }

        public List<string> GetBranchesList(string BankName)
        {
            throw new NotImplementedException();
        }

        public BankBranch GetBranch(int bankKey, int branchKey)
        {
            throw new NotImplementedException();
        }

        public string msg()
        {
            throw new NotImplementedException();
        }

        public string GetWebUsername()
        {
            throw new NotImplementedException();
        }

        public string GetWebPassword()
        {
            throw new NotImplementedException();
        }
    }
}