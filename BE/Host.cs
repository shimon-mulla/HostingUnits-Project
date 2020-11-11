using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Host : IClonable
    {
        [XmlElement("HostKey")]
        public string HostKey { get; set; }// מספר ת.ז. של המארח
        [XmlElement("PrivateName")]
        public string PrivateName { get; set; }// שם פרטי של המארח
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }// שם משפחה של המארח
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }// מספר טלפון
        [XmlElement("MailAddress")]
        public string MailAddress { get; set; }// כתובת מייל
        [XmlElement("BankAccountDetails")]
        public BankAccount BankAccountDetails { get; set; }// פרטי חשבון בנק
        [XmlElement("BankAccountNumber")]
        public int BankAccountNumber { get; set; } // מספר חשבון בנק
        [XmlElement("CollectionClearance")]
        public bool CollectionClearance { get; set; }// אישור גבייה

        public string FullName { get {return PrivateName + " " + FamilyName; }  }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
