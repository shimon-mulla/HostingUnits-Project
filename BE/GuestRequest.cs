using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class GuestRequest : IClonable
    {
        [XmlElement("GuestRequestKey")]
        public long GuestRequestKey { get; set; }// מספר בקשה - מספר רץ ייחודי
        [XmlElement("PrivateName")]
        public string PrivateName { get; set; }//שם פרטי
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }//שם משפחה
        [XmlElement("MailAdress")]
        public string MailAdress { get; set; }// כתובת מייל
        [XmlElement("Status")]
        public StatusCode Status { get; set; }// סטטוס בקשת אירוח
        [XmlElement("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }//תאריך רישום למערכת
        [XmlElement("EntryDate")]
        public DateTime EntryDate { get; set; } //תאריך רצוי לתחילת הנופש
        [XmlElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }//תאריך רצוי לסיום החופש
        [XmlElement("Area")]
        public AreaCode Area { get; set; } //אזור
        [XmlElement("SubArea")]
        public string SubArea { get; set; } // תת אזור
        [XmlElement("Type")]
        public HostingUnitTypeCode Type { get; set; } // סוג יחידת אירוח
        [XmlElement("Adults")]
        public int Adults { get; set; } // מספר מבוגרים
        [XmlElement("Children")]
        public int Children { get; set; }// מספר ילדים
        [XmlElement("Pool")]
        public GuestPreferencesCode Pool { get; set; }// האם מעוניין בבריכה
        [XmlElement("Jacuzzi")]
        public GuestPreferencesCode Jacuzzi { get; set; }//האם מעוניין בגקוזי
        [XmlElement("Garden")]
        public GuestPreferencesCode Garden { get; set; }// האם מעוניין בגינה
        [XmlElement("Porch")]
        public GuestPreferencesCode Porch { get; set; }// האם מעוניין במרפסת
        [XmlElement("ChildrensAttractions")]
        public GuestPreferencesCode ChildrensAttractions { get; set; }// האם מעוניין באטרקציות לילדים

        public string FullName
        {
            get { return PrivateName + " " + FamilyName; }
            set { }
        }

        public override string ToString()
        {
            //return base.ToString();
            return string.Format("Private name: {0}\n" +
                "FamilyName: {1}\n" +
                "MailAddress: {2}\n" +
                "RegistrationDate: {3}\n" +
                "EntryDate: {4}\n" +
                "ReleaseDate: {5}\n" +
                "Area: {6}\n" +
                "SubArea: {7}\n" +
                "Type: {8}\n" +
                "Adults: {9}\n" +
                "Children: {10}\n" +
                "Pool: {11}\n" +
                "Jacuzzi: {12}\n" +
                "Garden: {13}\n" +
                "Porch: {14}\n" +
                "ChildrenAttractions: {15}",
                PrivateName,
                FamilyName,
                MailAdress,
                RegistrationDate,
                EntryDate,
                ReleaseDate,
                Area,
                SubArea,
                Type,
                Adults,
                Children,
                Pool,
                Jacuzzi,
                Garden,
                Porch,
                ChildrensAttractions
                );// + PrivateName;
        }

    }
}
