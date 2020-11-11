using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static long GuestRequestKey = 10000005; // מספר רץ לאורח
        public static long HostUnitKey = 10000005; // מספר רץ לחדר אירוח
        public static float SumConnission = 10f; // סכום עמלה לכל עסקה
        public static int NumDaysUntillExpired = 3; // מספר ימי תוקף הזמנה
        public static long OrderKey = 20000005; // מספר מזהה של הזמנה
        public static MailAddress mailAddress;
        public static string Password;
    }
}
