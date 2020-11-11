using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactorySingletonDal
    {
        private static IDal singelonDal = null;
        private FactorySingletonDal() { }
        public static IDal GetDal()
        {
            if (singelonDal == null)
            {
                singelonDal = new DAL_XML_imp();

                //singelonDal = new Dal_imp();
            }
            return singelonDal;
        }
    }
}
