using BE;
using DAL.ClassXml;
using DS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DAL.ClassXml.XmlConfiguration;

namespace DAL
{
    public class DAL_XML_imp :IDal
    {
        XmlOrder XO = FactorySingletonXmlOrder.GetXmlOrder();
        //XmlHostingUnit XH = FactorySingletonXmlHostingUnit.GetXmlHostingUnit();
        XmlGuestRequest XG = FactorySingletonXmlGuestRequest.GetXmlGuestRequest();
        public XmlConfiguration XC = FactorySingletonXmlConfiguration.GetXmlConfiguration();
        XmlBankBranch XB = FactorySingletonXmlBankBranch.GetXmlBankBranch();


        public readonly string HostingUnitPath = @"HostingUnitXml.xml";

        public void AddOrder(Order order)
        {
            XO.AddOrder(order);
        }

        public long AddRequest(GuestRequest guestRequest)
        {
            return XG.AddRequest(guestRequest);
        }

        public long AddUnit(HostingUnit hostingUnit)
        {
            HostingUnit hostingUnitCopy = (HostingUnit)hostingUnit.Clone();
            hostingUnitCopy.HostingUnitKey = XC.GetConfiguration<long>("HostUnitKey");
            DataSource.HostingUnitsCollection.Add(hostingUnitCopy);
            XmlDataSource.SaveToXML<List<HostingUnit>>(DataSource.HostingUnitsCollection, HostingUnitPath);
            XC.UpdateConfiguration<long>("HostUnitKey", (hostingUnitCopy.HostingUnitKey+1));
            return hostingUnitCopy.HostingUnitKey;
            //XH.AddUnit(hostingUnit);
        }

        public float GetCommission()
        {
            return XC.GetConfiguration<float>("SumConnission");
        }


        public List<BankAccount> GetAllBankBranches()
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetAllGuestRequests()
        {
            return XmlDataSource.LoadFromXML<List<GuestRequest>>(XG.GuestRequestPath);
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
        }

        public List<Order> GetAllOrders()
        {
            return XO.GetAllOrders();
        }

        public GuestRequest GetGuestRequest(long key)
        {
            return XG.GetGuestRequest(key);
        }

        public HostingUnit GetHostingUnit(long key)
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath).Where(item => item.HostingUnitKey == key).FirstOrDefault();
            //XH.GetHostingUnit(key);
        }

        public Order GetOrder(long key)
        {
            return XO.GetOrder(key);
        }

        public float GetSumConnission()
        {
            return Configuration.SumConnission; 
        }

        public bool IsBookingAssociatedHostingUnit(long key)
        {
            return XmlDataSource.LoadFromXML<List<Order>>(XO.OrderPath)
                .Where(item => item.HostingUnitKey == key)
                    .Select(item => GetGuestRequest(item.GuestRequestKey))
                        .Any(item => item.Status == StatusCode.Open);
        }

        public bool IsCollectionClearance(HostingUnit hostingUnit)
        {
            return hostingUnit.Owner.CollectionClearance;
        }

        public bool IsExistOrderOpenForHost(long hostingUnitKey)
        {
            return XO.IsExistOrderOpenForHost(hostingUnitKey);
        }

        public bool IsOrderExist(Order order)
        {
            return XO.IsOrderExist(order.OrderKey);
        }

        public bool IsOrderExist(long key)
        {
            return XO.IsOrderExist(key);
        }

        public bool IsOrderStatus(Order order, OrderStatusCode orderStatus)
        {
            return XO.IsOrderStatus(order, orderStatus);
        }

        public bool IsRequestExit(GuestRequest guestRequest)
        {
            return IsRequestExit(guestRequest.GuestRequestKey);
        }

        public bool IsRequestExit(long key)
        {
           return XG.IsRequestExit(key);
        }

        public bool IsUnitExist(HostingUnit hostingUnit)
        {
            return IsUnitExist(hostingUnit.HostingUnitKey);
        }

        public bool IsUnitExist(long key)
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath).Any(item => item.HostingUnitKey == key);
        }

        public void RemoveUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> lis = XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            lis.RemoveAll(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            XmlDataSource.SaveToXML<List<HostingUnit>>(lis, HostingUnitPath);
        }

        public void UpdateAllOrdersOfGuestRequest(Order order)
        {
            XO.UpdateAllOrdersOfGuestRequest(order);
        }

        public void UpdateOrder(Order order)
        {
            XO.UpdateOrder(order);
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            XG.UpdateRequest(guestRequest);
        }

        public void UpdateUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> lis = XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            HostingUnit unit = lis.FirstOrDefault(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            foreach (var Property in unit.GetType().GetProperties())
            {
                ParameterInfo[] myParameters = Property.GetIndexParameters();
                if (myParameters.Length == 0)
                {
                    Property.SetValue(unit, Property.GetValue(hostingUnit));
                }
            }
            XmlDataSource.SaveToXML<List<HostingUnit>>(lis, HostingUnitPath);
        }

        public void initilizeListToXml()
        {
            XmlDataSource.SaveToXML<List<HostingUnit>>(DataSource.HostingUnitsCollection, HostingUnitPath);
           // XmlDataSource.SaveToXML<List<GuestRequest>>(DataSource.GuestRequestsCollection ,XG.GuestRequestPath);
            //XmlDataSource.SaveToXML<List<Order>>(DataSource.OrdersCollection,XO.OrderPath);
            //XC.AddConfiguration();
        }

        public void SetMailAndPasswordToConfiguration(string mail, string password)
        {
            XC.UpdateConfiguration<string>("mailAddress", mail);
            XC.UpdateConfiguration<string>("Password", password);
        }

        public string GetMail()
        {
            return XC.GetConfiguration<string>("mailAddress");
        }

        public string GetPassword()
        {
            return XC.GetConfiguration<string>("Password");
        }

        public string GetWebUsername()
        {
            return XC.GetConfiguration<string>("WebUsername");
        }

        public string GetWebPassword()
        {
            return XC.GetConfiguration<string>("WebPassword");
        }

        public List<string> GetBanksList()
        {
            return XB.getBanksList();
        }

        public List<string> GetBranchesList(string BankName)
        {
            return XB.getBranchesList(BankName);
        }

        public BankBranch GetBranch(int bankKey, int branchKey)
        {
            return XB.GetBranch(bankKey, branchKey);
        }

        public string msg()
        {
            return XC.GetConfiguration<string>("Msg");
        }
    }
}
