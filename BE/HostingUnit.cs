using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utilities;

namespace BE
{
    public class HostingUnit : IClonable
    {
        [XmlElement("HostingUnitKey")]
        public long HostingUnitKey { get; set; } // מספר יחידת אירוח - מספר רץ ייחודי
        [XmlElement("Owner")]
        public Host Owner { get; set; }// בעל היחידה 
        [XmlElement("HostingUnitName")]
        public string HostingUnitName { get; set; }// שם יחידת האירוח
        [XmlElement("PhotoList")]
        public List<string> PhotoList;
        [XmlIgnore]
        public bool[,] Diary { get; set; } // מצב יחידת האירוח - לוח שנה
        [XmlArray(ElementName ="Diary")]
        [XmlArrayItem(ElementName = "item")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(10); }
        }
        //##########################################

        [XmlElement("Area")]
        public AreaCode Area { get; set; } //אזור
        [XmlElement("SubArea")]
        public string SubArea { get; set; } // תת אזור
        [XmlElement("Type")]
        public HostingUnitTypeCode Type { get; set; } // סוג יחידת אירוח
        [XmlElement("Adults")]
        public int Adults { get; set; } // מספר מבוגרים שיכולים להיות
        [XmlElement("Children")]
        public int Children { get; set; }// מספר ילדים שיכולים להיות
        [XmlElement("Pool")]
        public bool Pool { get; set; }// האם יש בבריכה
        [XmlElement("Jacuzzi")]
        public bool Jacuzzi { get; set; }//האם יש בגקוזי
        [XmlElement("Garden")]
        public bool Garden { get; set; }// האם יש בגינה
        [XmlElement("Porch")]
        public bool Porch { get; set; }// האם יש מרפסת
        [XmlElement("ChildrensAttractions")]
        public bool ChildrensAttractions { get; set; }// האם יש באטרקציות לילדים
        
        //##########################################

        //public bool this[int i, int j] {
        //    set {
        //        this.Diary[i, j] = value;
        //    }
        //   get {

        //        return this.Diary[i, j];
        //    }
        //}
        public override string ToString()
        {
            return "";
        }
    }
}
