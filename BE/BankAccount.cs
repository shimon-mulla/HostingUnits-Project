using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class BankAccount : IClonable
    {
        [XmlElement("BankNumber")]
        public int BankNumber { get; set; } // מספר בנק
        [XmlElement("BankName")]
        public string BankName { get; set; }// שם הבנק
        [XmlElement("BranchNumber")]
        public int BranchNumber { get; set; }// מספר הסניף
        [XmlElement("BranchName")]
        public string BranchName { get; set; }// שם סניף
        [XmlElement("BranchAddress")]
        public string BranchAddress { get; set; }// כתובת הסניף
        [XmlElement("BranchCity")]
        public string BranchCity { get; set; }// עיר הסניף
        [XmlElement("BankAccountNumber")]
        public int BankAccountNumber { get; set; } // מספר החשבון בנק
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
