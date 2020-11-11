using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactorySingletonBl
    {
        private static IBl singelonBl = null;
        private FactorySingletonBl() { }
        public static IBl GetBl()
        {
            if (singelonBl == null)
            {
                singelonBl = new Bl_imp();
            }
            return singelonBl;
        }
    }
}
