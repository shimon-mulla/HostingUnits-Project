﻿using BE;
using DAL.ClassXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DAL.ClassXml.XmlConfiguration;

namespace DAL
{
    public class FactorySingletonXmlOrder
    {
        private static XmlOrder singelonXmlOrder = null;
        private FactorySingletonXmlOrder() { }
        public static XmlOrder GetXmlOrder()
        {
            if (singelonXmlOrder == null)
            {
                singelonXmlOrder = new XmlOrder();
            }
            return singelonXmlOrder;
        }
    }


    public class XmlOrder
    {
        public XmlConfiguration XC = FactorySingletonXmlConfiguration.GetXmlConfiguration();

        public XElement OrderRoot;
        public string OrderPath = @"OrderXml.xml";

        public XmlOrder()
        {
            if (!File.Exists(OrderPath))
                CreateXmlOrder();
            else
                LoadXmlOrder();
        }

        public void CreateXmlOrder()
        {
            OrderRoot = new XElement("Orders");
            OrderRoot.Save(OrderPath);
        }

        public void LoadXmlOrder()
        {
            OrderRoot = XElement.Load(OrderPath);
        }

        public void AddOrder(Order order)
        {
            long key = XC.GetConfiguration<long>("OrderKey");
            OrderRoot.Add(new XElement("Order",
                new XElement("OrderKey", key),
                new XElement("HostingUnitKey", order.HostingUnitKey),
                new XElement("GuestRequestKey", order.GuestRequestKey),
                new XElement("Status", order.Status),
                new XElement("CreateDate", order.CreateDate),
                new XElement("OrderDate", order.OrderDate),
                new XElement("CommissionValue", order.CommissionValue)));
            OrderRoot.Save(OrderPath);
            XC.UpdateConfiguration<long>("OrderKey", ++key);
        }

        public List<Order> GetAllOrders()
        {
            return (from p in OrderRoot.Elements()
                    select new Order()
                    {
                        OrderKey = (long)Convert.ToInt32(p.Element("OrderKey").Value),
                        HostingUnitKey = (long)Convert.ToInt32(p.Element("HostingUnitKey").Value),
                        GuestRequestKey = (long)Convert.ToInt32(p.Element("GuestRequestKey").Value),
                        CreateDate = DateTime.Parse(p.Element("CreateDate").Value),
                        OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                        CommissionValue = float.Parse(p.Element("CommissionValue").Value)
                    }).ToList();
        }

        public Order GetOrder(long key)
        {
            return (Order)(from p in OrderRoot.Elements()
                           where (long)Convert.ToInt32(p.Element("OrderKey").Value) == key
                           select new Order()
                           {
                               OrderKey = (long)Convert.ToInt32(p.Element("OrderKey").Value),
                               HostingUnitKey = (long)Convert.ToInt32(p.Element("HostingUnitKey").Value),
                               GuestRequestKey = (long)Convert.ToInt32(p.Element("GuestRequestKey").Value),
                               CreateDate = DateTime.Parse(p.Element("CreateDate").Value),
                               OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                               Status = (OrderStatusCode)Enum.Parse(typeof(OrderStatusCode), p.Element("Status").Value),
                               CommissionValue = float.Parse(p.Element("CommissionValue").Value)
                           }).FirstOrDefault();
        }

        public bool IsExistOrderOpenForHost(long key)
        {
            return OrderRoot.Elements().Any(item => Convert.ToInt32(item.Element("HostingUnitKey").Value) == key && item.Element("Status").Value == OrderStatusCode.NotHandledYet.ToString());
        }

        public bool IsOrderExist(long key)
        {
            return OrderRoot.Elements().Any(item => (long)Convert.ToInt32(item.Element("OrderKey").Value) == key);
        }

        public bool IsOrderStatus(Order order, OrderStatusCode orderStatus)
        {
            return OrderRoot.Elements().Any(
                 item => Convert.ToInt32(item.Element("OrderKey").Value) == order.OrderKey &&
                         item.Element("Status").Value == orderStatus.ToString()
             );
        }

        public void UpdateAllOrdersOfGuestRequest(Order order)//לבדוק שעובד!!!!
        {
            OrderRoot.Elements()
                .Where(item => Convert.ToInt32(item.Element("GuestRequestKey").Value) == order.GuestRequestKey &&
                        item.Element("Status").Value == OrderStatusCode.NotHandledYet.ToString()).ToList()
                .ForEach(item => item.Element("Status").Value = OrderStatusCode.ClosedDueGuestNotResponding.ToString());
            OrderRoot.Save(OrderPath);
        }

        public void UpdateOrder(Order order)
        {
            XElement xElement = OrderRoot.Elements().Where(item => (long)Convert.ToInt32(item.Element("OrderKey").Value) == order.OrderKey).FirstOrDefault();
            xElement.Element("GuestRequestKey").SetValue(order.GuestRequestKey);
            xElement.Element("HostingUnitKey").SetValue(order.HostingUnitKey);
            xElement.Element("CreateDate").SetValue(order.CreateDate);
            xElement.Element("OrderDate").SetValue(order.OrderDate);
            xElement.Element("Status").SetValue(order.Status);
            xElement.Element("CommissionValue").SetValue(order.CommissionValue);
            OrderRoot.Save(OrderPath);
        }

    }


    

}
