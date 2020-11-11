using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public int BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchType { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public int BranchZipCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
    }
}
