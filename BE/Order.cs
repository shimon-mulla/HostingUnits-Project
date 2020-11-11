using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
  //  [Serializable]
    public class Order : IClonable
    {
        [XmlElement("HostingUnitKey")]
        public long HostingUnitKey { get; set; } // מספר מזהה של יחידת אירוח
        [XmlElement("GuestRequestKey")]
        public long GuestRequestKey { get; set; }// מספר מזהה של דרישת לקוח
        [XmlElement("OrderKey")]
        public long OrderKey { get; set; }// מספר רץ מזהה של הזמנה
        [XmlElement("Status")]
        public OrderStatusCode Status { get; set; }// סטטוס הזמנה
        [XmlElement("CreateDate")]
        public DateTime CreateDate { get; set; }// תאריך יצירת הזמנה
        [XmlElement("OrderDate")]
        public DateTime OrderDate { get; set; }// תאריך משלוח המייל
        [XmlElement("CommissionValue")]
        public float CommissionValue { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            return string.Format("OrderKey: {0}, Unitkey: {1}, GuestKey: {2}", OrderKey, HostingUnitKey, GuestRequestKey);
        }

    }
}
