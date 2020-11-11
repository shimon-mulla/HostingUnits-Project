using System;
using BE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;
using System.Reflection;

namespace DAL
{
    public static class Cloning
    {
        public static IClonable Clone(this IClonable original)
        {
            IClonable target = (IClonable)Activator.CreateInstance(original.GetType());

            foreach(var Property in original.GetType().GetProperties())
            {
                ParameterInfo[] myParameters = Property.GetIndexParameters();
                if(myParameters.Length == 0)
                {
                        Property.SetValue(target, Property.GetValue(original));
                }
                /*else
                {
                    foreach(var Parameter in myParameters)
                    {
                        Parameter.
                    }
                }*/
                
            }
            return target;
        }
    }
}
