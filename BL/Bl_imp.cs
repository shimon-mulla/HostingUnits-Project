using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;


namespace BL
{
    public delegate bool UnitCondition(HostingUnit hostingUnit);
    public delegate bool GuestCondition(GuestRequest hostingUnit);


    public class Bl_imp : IBl
    {
        IDal dal;
        public Bl_imp()
        {
            dal = FactorySingletonDal.GetDal();
        }


        public void UpdateDatesDS()
        {
            List<Order> orders = dal.GetAllOrders();
            foreach (Order order in orders)
            {
                HostingUnit unit = getHostingUnit(order.HostingUnitKey);
                GuestRequest guest = getGR(order.GuestRequestKey);
                //DateTime dtStart = ConveryDateStrToDateTime(guest.EntryDate);
                //DateTime dtEnd = ConveryDateStrToDateTime(guest.ReleaseDate);
                order.Status = OrderStatusCode.CloseDueGuestResponding;
                UpdateOrder(order);
                //order
                //while(dtStart < dtEnd)

            }
        }

        /// <summary>
        /// הוספת הזמנה
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            if (dal.IsOrderExist(order))
                throw new ExceptionMessage("ORDER IS ALREADY EXISTS");
            dal.AddOrder(order);
        }

        /// <summary>
        /// הוספת בקשת לקוח
        /// </summary>
        /// <param name="guestRequest"></param>
        public void AddRequest(GuestRequest guestRequest)
        {
            //string[] dateIn = guestRequest.EntryDate.Split('/');
            //string[] dateOut = guestRequest.ReleaseDate.Split('/');
            //if ((ConveryDateStrToDateTime(guestRequest.ReleaseDate) - ConveryDateStrToDateTime(guestRequest.EntryDate)).Days < 1) //int.Parse(dateOut[2] + dateOut[1] + dateOut[0]) <= int.Parse(dateIn[2] + dateIn[1] + dateIn[0]))
            if ((guestRequest.ReleaseDate - guestRequest.EntryDate).Days < 1)
            {
                throw new ExceptionMessage("DATE END ERALIER THAN DATE START");
            }
            if (dal.IsRequestExit(guestRequest))
                throw new ExceptionMessage("REQUEST IS ALREADY EXISTS");
            guestRequest.GuestRequestKey = dal.AddRequest(guestRequest);
            CreateOrder(guestRequest);
        }

        public Order GetOrder(long key)
        {
            if (!dal.IsOrderExist(key))
                throw new ExceptionMessage("ORDER IS NOT EXISTS");
            return dal.GetOrder(key);
        }



        /// <summary>
        /// הוספת יחידת אירוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void AddUnit(HostingUnit hostingUnit)
        {
            if (dal.IsUnitExist(hostingUnit))
                throw new ExceptionMessage("HOSTING UNIT IS ALREADY EXISTS");
            hostingUnit.HostingUnitKey = dal.AddUnit(hostingUnit);
            CreateOrderForNewHostingUnit(hostingUnit);
        }

        /// <summary>
        /// החזרת רשימת בנקים
        /// </summary>
        /// <returns></returns>
        public List<BankAccount> GetAllBankBranches()
        {
            return dal.GetAllBankBranches();
        }

        /// <summary>
        /// החזרת רשימת בקשות אירוח
        /// </summary>
        /// <returns></returns>
        public List<GuestRequest> GetAllGuestRequests()
        {
            return dal.GetAllGuestRequests();
        }

        /// <summary>
        /// החזרת רשימת יחידות אירוח
        /// </summary>
        /// <returns></returns>
        public List<HostingUnit> GetAllHostingUnits()
        {
            return dal.GetAllHostingUnits();
        }

        /// <summary>
        /// החזרת רשימת הזמנות
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAllOrders()
        {
            return dal.GetAllOrders();
        }

        /// <summary>
        /// מחיקת יחידת אירוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void RemoveUnit(HostingUnit hostingUnit)
        {
            if (dal.IsUnitExist(hostingUnit))
            {
                if (dal.IsBookingAssociatedHostingUnit(hostingUnit.HostingUnitKey))
                    throw new ExceptionMessage(dal.msg());
                else
                    dal.RemoveUnit(hostingUnit);
            }
            else
                throw new ExceptionMessage("יחידת אירוח לא קיימת");
        }

        /// <summary>
        /// עדכן תאריכים ביחידה לפי הזמנה
        /// </summary>
        /// <param name="order"></param>
        public void UpdateDatesInUnitByOrder(Order order)
        {
            HostingUnit hostingUnit = dal.GetHostingUnit(order.HostingUnitKey);
            GuestRequest guestRequest = dal.GetGuestRequest(order.GuestRequestKey);
            DateTime dateIn = guestRequest.EntryDate;//ConveryDateStrToDateTime(guestRequest.EntryDate);
            DateTime dateOut = guestRequest.ReleaseDate;//ConveryDateStrToDateTime(guestRequest.ReleaseDate);
            if (CheckDates(hostingUnit, dateIn, dateOut))
            {
                int numOfDays = getNumDaysBetween(dateIn, dateOut);
                // עידכון תאריכים
                for (int i = 1; i < numOfDays; i++)
                {
                    hostingUnit.Diary[dateIn.Month - 1, dateIn.Day - 1] = true;// צריך לעדכן את המערך שיהיה 12*31
                    dateIn = dateIn.AddDays(1);
                }
                // עידכון יחידת האירוח
                UpdateUnit(hostingUnit);//////////////////////////////////////////כאשר רוציםל עדכן את הזמנים ביומן אז עדכון החידה הוא בעצם מחיקת הישן ויצירת חדש
                //לכן יש באג שה-HostingUnitKey נמחק כביכול ולא נמצא
            }
            else
                throw new ExceptionMessage("התאריכים לאט פנויים");
        }

        /// <summary>
        /// חישוב עמלה לפי מספר ימי הזמנה
        /// </summary>
        /// <param name="order"></param>
        public void CalcularCommission(Order order)
        {
            GuestRequest guestRequest = dal.GetGuestRequest(order.GuestRequestKey);
            /*string[] dateIn = guestRequest.EntryDate.Split('/');
            string[] dateOut = guestRequest.ReleaseDate.Split('/');
            DateTime dateTimeIn = new DateTime(int.Parse(dateIn[2]), int.Parse(dateIn[1]), int.Parse(dateIn[0]));
            DateTime dateTimeOut = new DateTime(int.Parse(dateOut[2]), int.Parse(dateOut[1]), int.Parse(dateOut[0]));
            order.CommissionValue = (dateTimeOut - dateTimeIn).Days * Configuration.SumConnission;// חישוב עמלה לפי מספר ימים
            */
            order.CommissionValue = getNumDaysBetween(guestRequest.EntryDate, guestRequest.ReleaseDate) * dal.GetCommission();// חישוב עמלה לפי מספר ימים
        }

        /// <summary>
        /// עידכון הזמנה
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            if (!dal.IsOrderExist(order))// בידקה אם ההמנה קיימת
            {
                throw new ExceptionMessage("ORDER DOES NOT EXIST - METHOD UPDATE BL");
            }
            if (dal.IsOrderStatus(order, OrderStatusCode.CloseDueGuestResponding))// בדיקה אם סטטוס ההזמנה סגור
            {
                throw new ExceptionMessage("CANNOT UPDATE STATUS ORDER BECAUSE ORDER COMPLETED");
            }
            GuestRequest guest = dal.GetGuestRequest(order.GuestRequestKey);
            switch (order.Status)
            {
                case OrderStatusCode.CloseDueGuestResponding:// אם מבצעים סגירת הזמנה
                    CalcularCommission(order);
                    dal.UpdateOrder(order);
                    UpdateDatesInUnitByOrder(order);
                    guest.Status = StatusCode.ClosedDeal;
                    UpdateRequest(guest);
                    dal.UpdateAllOrdersOfGuestRequest(order);// עדכון סטטוס של כל ההזמנות האחרות של אותו לקוח
                    break;
                case OrderStatusCode.ClosedDueGuestNotResponding://
                    break;
                case OrderStatusCode.MailSent:// אם ססטוס הזמנה משתנה לשליחת מייל
                    sendEmail(dal.GetMail(), guest.MailAdress, "הזמנה ליחידת אירוח "+ order.HostingUnitKey,"" +
                        "<div>" +
                        "<p1>הזמנה ליחידת אירוח "+ order.HostingUnitKey +"</p1>" +
                        "<div>הזמנה מספר " + order.OrderKey + "</div>" +
                        "</div>");

                    //sendEmail(dal.GetMail(), guest.MailAdress, "הזמנה ליחידת אירוח " + order.HostingUnitKey, "אתה מוזמן");
                    dal.UpdateOrder(order);
                    break;
                default:
                    throw new ExceptionMessage("COULD NOT UPDATE ORDER");
            }
            dal.UpdateOrder(order);

        }

        /// <summary>
        /// המרת תאריך מפורמט מחרוזת לתאריך בפורמט תאריך
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ConveryDateStrToDateTime(string date)
        {
            string[] str = date.Split('/');

            return new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
        }

        /// <summary>
        /// יצירת הזמנה
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        public void CreateOrder(GuestRequest guestRequest)
        {
            Order order = null;
            foreach (HostingUnit unit in dal.GetAllHostingUnits())
            {
                if (IsHostingUnitMatchGR(unit, guestRequest))// בדיקה אם היחידה עומדת מול דרישות הלקוח
                {
                    order = new Order
                    {
                        HostingUnitKey = unit.HostingUnitKey,
                        GuestRequestKey = guestRequest.GuestRequestKey,
                        Status = OrderStatusCode.NotHandledYet,
                        CreateDate = DateTime.Today//.ToShortDateString()
                        //CommissionValue = dal.GetCommission() * getNumDaysBetween(guestRequest.EntryDate, guestRequest.ReleaseDate)
                        //OrderDate = DateTime.Today.ToShortDateString()
                    };
                    AddOrder(order);
                    //sendEmail("", "", "", "");
                }
            }
        }


        /// <summary>
        /// יצירת הזמנה מטעם יחידת אירוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public void CreateOrderForNewHostingUnit(HostingUnit hostingUnit)
        {
            Order order = null;
            foreach (GuestRequest guest in dal.GetAllGuestRequests())
            {
                if (IsHostingUnitMatchGR(hostingUnit, guest))// בדיקה אם היחידה עומדת מול דרישות הלקוח
                {
                    order = new Order
                    {
                        HostingUnitKey = hostingUnit.HostingUnitKey,
                        GuestRequestKey = guest.GuestRequestKey,
                        Status = OrderStatusCode.NotHandledYet,
                        CreateDate = DateTime.Today//.ToShortDateString()
                        //CommissionValue = dal.GetCommission() * getNumDaysBetween(guest.EntryDate, guest.ReleaseDate)
                        //OrderDate = DateTime.Today.ToShortDateString()
                    };
                    //sendEmail("", "", "", "");
                    AddOrder(order);
                }
            }
        }

        /// <summary>
        /// בדיקה אם היחידת אירוח עונה על דרישות הלקוח
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        public bool IsHostingUnitMatchGR(HostingUnit hostingUnit, GuestRequest guestRequest)
        {
            //אם אזור לא שווה ליחידת האירוח אז נחזיר FALSE
            if (!IsHostingUnitMatchArea(hostingUnit, guestRequest))
                return false;

            //אם כמות מתארחים לא שווה אז תחזיר FALSE
            if (!IsHostingUnitMatchCapacity(hostingUnit, guestRequest))
                return false;

            //אם לא עומד בדרישות החדר - ג'וזי, בריכה וכו...
            if (!IsHostingUnitMatchGRDiment(hostingUnit, guestRequest))
                return false;

            return CheckDates(
                hostingUnit,
                guestRequest.EntryDate,//ConveryDateStrToDateTime(guestRequest.EntryDate),
                guestRequest.ReleaseDate//ConveryDateStrToDateTime(guestRequest.ReleaseDate)
            );
        }

        public bool IsHostingUnitMatchCapacity(HostingUnit hostingUnit, GuestRequest guestRequest)
        {
            return (guestRequest.Children <= hostingUnit.Children && guestRequest.Adults <= hostingUnit.Adults);
        }

        public bool IsHostingUnitMatchArea(HostingUnit hostingUnit, GuestRequest guestRequest)
        {
            return guestRequest.Area == AreaCode.All || guestRequest.Area == hostingUnit.Area;
        }

        /// <summary>
        /// בדיקה אם היחידת אירוח עונה על דרישות הלקוח בירכה ג'קוזי  וכו
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        private bool IsHostingUnitMatchGRDiment(HostingUnit hostingUnit, GuestRequest guestRequest)
        {
            if (guestRequest.Pool != GuestPreferencesCode.Optional)
            {
                if (!((hostingUnit.Pool == true && guestRequest.Pool == GuestPreferencesCode.Necessary) ||
                    (hostingUnit.Pool == false && guestRequest.Pool == GuestPreferencesCode.NotInterested)))
                    return false;
            }
            if (guestRequest.ChildrensAttractions != GuestPreferencesCode.Optional)
            {
                if (!((hostingUnit.ChildrensAttractions == true && guestRequest.ChildrensAttractions == GuestPreferencesCode.Necessary) ||
                    (hostingUnit.ChildrensAttractions == false && guestRequest.ChildrensAttractions == GuestPreferencesCode.NotInterested)))
                    return false;
            }
            if (guestRequest.Garden != GuestPreferencesCode.Optional)
            {
                if (!((hostingUnit.Garden == true && guestRequest.Garden == GuestPreferencesCode.Necessary) ||
                    (hostingUnit.Garden == false && guestRequest.Garden == GuestPreferencesCode.NotInterested)))
                    return false;
            }
            if (guestRequest.Jacuzzi != GuestPreferencesCode.Optional)
            {
                if (!((hostingUnit.Jacuzzi == true && guestRequest.Jacuzzi == GuestPreferencesCode.Necessary) ||
                    (hostingUnit.Jacuzzi == false && guestRequest.Jacuzzi == GuestPreferencesCode.NotInterested)))
                    return false;
            }
            if (guestRequest.Porch != GuestPreferencesCode.Optional)
            {
                if (!((hostingUnit.Porch == true && guestRequest.Porch == GuestPreferencesCode.Necessary) ||
                    (hostingUnit.Porch == false && guestRequest.Porch == GuestPreferencesCode.NotInterested)))
                    return false;
            }

            return true;
        }// בדיקה האם קיימות הדרישות באופציונאליות של הלקוח בריכה גקוזי וכד



        public HostingUnit getHostingUnit(long key)
        {
            if (!dal.IsUnitExist(key))
            {
                throw new ExceptionMessage("Hosting unit " + key + " not exist");
            }
            return dal.GetHostingUnit(key);
        }




        /// <summary>
        /// הבאת בקשת לקוח לפי מספר בקשה
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GuestRequest getGR(long key)
        {
            if (!dal.IsRequestExit(key))
            {
                throw new ExceptionMessage("Guest request " + key + " not exist");
            }
            return dal.GetGuestRequest(key);
        }

        /// <summary>
        /// עדכון בקשת לקוח
        /// </summary>
        /// <param name="guestRequest"></param>
        public void UpdateRequest(GuestRequest guestRequest)
        {
            if (!dal.IsRequestExit(guestRequest))
                throw new ExceptionMessage("לא קיים");
            dal.UpdateRequest(guestRequest);
        }

        /// <summary>
        /// עדכון יחידת אירוח/מארח
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void UpdateUnit(HostingUnit hostingUnit)
        {
            if (dal.IsUnitExist(hostingUnit))
            {
                if (!hostingUnit.Owner.CollectionClearance && dal.IsCollectionClearance(dal.GetHostingUnit(hostingUnit.HostingUnitKey)))// ביטול הרשאת חיוב חשבון
                {
                    if (dal.IsExistOrderOpenForHost(hostingUnit.HostingUnitKey))
                        throw new ExceptionMessage("קיימת הזמנה פתוחות לא ניתן לשנות את ההרשאה");
                }
                dal.UpdateUnit(hostingUnit);
            }
            else
                throw new ExceptionMessage("לא קיימת כזאת יחידת אירוח");

        }

        /// <summary>
        /// שליחת הזמנה
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="order"></param>
        /*public void SendOrder(HostingUnit hostingUnit, Order order)
        {
            if (!dal.IsCollectionClearance(hostingUnit))
            {
                throw new ExceptionMessage("NO PREMISSION TO CHARGE THE BANK ACCOUNT");
            }
            try
            {
                this.sendEmail("", "", "", "");
            }
            catch
            {
                throw new ExceptionMessage("EMAIL WAS NOT SENT");
            }
            dal.UpdateOrder(order);// צריך לעדכן את הפונקציה כך שנוכל לשנות סטטוס
        }*/

        /// <summary>
        /// שליחת מייל
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="msg"></param>
        public void sendEmail(string from, string to, string subject, string msg)
        {
            dal.SetMailAndPasswordToConfiguration("lev.host.service@gmail.com", "myhostservice123");

            string str = from.Split('@')[1].Split('.')[0];
            //יצירת אוביקט MailMessage
            MailMessage mail = new MailMessage();
            //למי לשלוח (יש אפשרות להוסיף כמה נמענים) 
            mail.To.Add(to);
            //כתובת מייל לשלוח ממנה
            mail.From = new MailAddress(from);
            // נושא ההודעה
            mail.Subject = subject;
            //תוכן ההודעה ב- HTML
            mail.Body = msg;
            //הגדרת תוכן ההודעה ל - HTML
            mail.IsBodyHtml = true;
            // Smtp יצירת אוביקט
            SmtpClient smtp = new SmtpClient();
            switch (str.ToLower())
            {
                case "gmail":
                    sendEmailGmail(ref smtp, ref mail);
                    break;
                case "yahoo":
                    sendEmailYahoo(ref smtp, ref mail);
                    break;
                case "hotmail":
                    sendEmailHotmail(ref smtp, ref mail);
                    break;
            }
        }

        public void sendEmailGmail(ref SmtpClient smtp, ref MailMessage mail)
        {
            
            smtp.UseDefaultCredentials = true;

            //הגדרת פרטי הכניסה לחשבון גימייל
            smtp.Credentials = new System.Net.NetworkCredential(dal.GetMail(), dal.GetPassword());
            //הגדרת השרת של גוגל
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            //אפשור SSL (חובה(
            smtp.EnableSsl = true;
            try
            {
                //שליחת ההודעה
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //תפיסה וטיפול בשגיאות
                //txtMessage.Text = ex.ToString()
                throw new ExceptionMessage(ex.ToString());
            }
        }

        public void sendEmailHotmail(ref SmtpClient smtp, ref MailMessage mail)
        {
            ////יצירת אוביקט MailMessage
            //MailMessage mail = new MailMessage();
            ////למי לשלוח (יש אפשרות להוסיף כמה נמענים) 
            //mail.To.Add(to);
            ////כתובת מייל לשלוח ממנה
            //mail.From = new MailAddress(from);
            //// נושא ההודעה
            //mail.Subject = subject;
            ////תוכן ההודעה ב- HTML
            //mail.Body = msg;
            ////הגדרת תוכן ההודעה ל - HTML 
            //mail.IsBodyHtml = true;
            //// Smtp יצירת אוביקט 
            //SmtpClient smtp = new SmtpClient();
            ////הגדרת השרת של הוטמייל
            smtp.Host = "smtp.live.com";
            smtp.Port = 25;
            //הגדרת פרטי הכניסה לחשבון הוטמייל
            smtp.Credentials = new System.Net.NetworkCredential("myHotmailEmailAddress@Hotmail.com", "myHotmailPassword");
            //אפשור SSL (חובה(
            smtp.EnableSsl = true;
            try
            {
                //שליחת ההודעה
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //תפיסה וטיפול בשגיאות
                //txtMessage.Text = ex.ToString()
                throw new ExceptionMessage(ex.ToString());
            }
        }

        public void sendEmailYahoo(ref SmtpClient smtp, ref MailMessage mail)
        {
            //הגדרת השרת של יאהו
            smtp.Host = "plus.smtp.mail.yahoo.com";
            smtp.Port = 465;
            //הגדרת פרטי הכניסה לחשבון יאהו
            smtp.Credentials = new System.Net.NetworkCredential("myYahooEmailAddress@Yahoo.com", "myYahooPassword");
            //אפשור SSL (חובה(
            smtp.EnableSsl = true;
            try
            {
                //שליחת ההודעה
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //תפיסה וטיפול בשגיאות
                //txtMessage.Text = ex.ToString()
                throw new ExceptionMessage(ex.ToString());
            }
        }


        /// <summary>
        ///  פונקציה שמקבלת תאריך ומספר ימי נופש ומחזירה את רשימת כל יחידות האירוח הפנויות בתאריך זה
        /// </summary>
        /// <param name="dateIn"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        ///
        public List<HostingUnit> GetHostingUnitsByDate(DateTime dateIn, int days)
        {
            DateTime dateOut = dateIn.AddDays(days);
            List<HostingUnit> listHost2 = GetAllHostingUnits();
            var listHost = from item in listHost2
                           where CheckDates(item, dateIn, dateOut)
                           let tmp = item
                           select (HostingUnit)tmp.Clone();// לא בטוח אם צריךCLONE
            return (List<HostingUnit>)listHost;
        }

        /// <summary>
        /// פונקציה שמקבלת תאריך ומספר ימי נופש ומחזירה את רשימת כל יחידות האירוח הפנויות בתאריך זה
        /// </summary>
        /// <param name="dateIn"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<HostingUnit> GetHostingUnitsByDate(string dateIn, int days)
        {
            string[] str = dateIn.Split('/');
            DateTime d = new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
            return GetHostingUnitsByDate(d, days);
        }

        /// <summary>
        /// בדיקה לזמינות החדר בתאריכים מבוקשים
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="dateIn"></param>
        /// <param name="dateOut"></param>
        /// <returns></returns>
        public bool CheckDates(HostingUnit hostingUnit, DateTime dateIn, DateTime dateOut) // בדיקת זמינות החדר
        {
            int numOfDays = getNumDaysBetween(dateIn, dateOut);
            if (numOfDays <= 0)
                return false;
            for (int i = 1; i < numOfDays; i++)
            {
                if (hostingUnit.Diary[dateIn.Month - 1, dateOut.Day - 1] == true) // צריך לעדכן את המערך שיהיה 31*12
                    return false;
                dateIn = dateIn.AddDays(1);
            }
            return true;
        }

        /// <summary>
        /// החזרת מספר הימים שבין שני תאריכים
        /// </summary>
        /// <param name="dateIn"></param>
        /// <param name="dateOut"></param>
        /// <returns></returns>
        public int getNumDaysBetween(string dateIn, string dateOut = "")
        {
            if (dateOut == "")
                dateOut = DateTime.Today.ToShortDateString();
            //return getNumDaysBetween(ConveryDateStrToDateTime(dateIn), ConveryDateStrToDateTime(dateOut));
            return getNumDaysBetween(dateIn, dateOut);
        }

        public int getNumDaysBetween(DateTime dateIn)
        {
            return getNumDaysBetween(dateIn, DateTime.Today);
        }

        public int getNumDaysBetween(DateTime dateIn, DateTime dateOut)
        {
            return (dateOut - dateIn).Days;
        }

        /// <summary>
        /// פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז שנוצרו / מאז שנשלח מייל ללקוח גדול או שווה למספר הימים שהפונקציה קיבלה
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<Order> getListOrdersSinceCreated(int days)
        {
            List<Order> ordersList = GetAllOrders();
            /*var tmpList = from item in ordersList
                          where (getNumDaysBetween(ConveryDateStrToDateTime(item.CreateDate), DateTime.Today) >= days ||
                                getNumDaysBetween(ConveryDateStrToDateTime(item.OrderDate), DateTime.Today) >= days)
                          select item;*/

            var tmpList = from item in ordersList
                          where (getNumDaysBetween(item.CreateDate, DateTime.Today) >= days ||
                                getNumDaysBetween(item.OrderDate, DateTime.Today) >= days)
                          select item;
            return (List<Order>)tmpList;
        }

        public List<Order> getListOrdersByUnit(long key)
        {
            List<Order> ordersList = GetAllOrders();
            /*var tmpList = from item in ordersList
                          where (getNumDaysBetween(ConveryDateStrToDateTime(item.CreateDate), DateTime.Today) >= days ||
                                getNumDaysBetween(ConveryDateStrToDateTime(item.OrderDate), DateTime.Today) >= days)
                          select item;*/

            var tmpList = (from item in ordersList
                          where (Convert.ToInt32(item.HostingUnitKey) == key)
                          select item).ToList();
            return (List<Order>)tmpList;
        }

        /// <summary>
        /// פונקציה שיכולה להחזיר את כל דרישות הלקוח שמתאימים לתנאי מסוים
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<GuestRequest> getListGuestRequestsByCondition(Predicate<GuestRequest> predicate)
        {
            List<GuestRequest> guestRequestsList = GetAllGuestRequests();
            var tmpList = from item in guestRequestsList
                          where predicate(item)
                          select item;
            return (List<GuestRequest>)tmpList;
        }

        /// <summary>
        ///  פונקציה שמקבלת דרישת לקוח, ומחזירה את מספר ההזמנות שנשלחו אליו.
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        public int getNumOfReceivedOrdersByGuestRequest(GuestRequest guestRequest)
        {
            List<Order> ordersList = GetAllOrders();
            return ordersList.Count(item => item.GuestRequestKey == guestRequest.GuestRequestKey && item.Status == OrderStatusCode.MailSent);
        }

        /// <summary>
        /// פונקציה שמקבלת יחידת אירוח ומחזירה את מספר ההזמנות שנשלחו / מספר ההזמנות שנסגרו בהצלחה עבור יחידה זו דרך האתר
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public int getNumOfReceivedOrdersByHostingUnit(HostingUnit hostingUnit)
        {
            List<Order> ordersList = GetAllOrders();
            return ordersList.Count(item => item.HostingUnitKey == hostingUnit.HostingUnitKey &&
                   (item.Status == (OrderStatusCode.MailSent | OrderStatusCode.CloseDueGuestResponding)));
        }

        /*
        public IEnumerable<IGrouping<AreaCode, GuestRequest>> getGuestRequestGroupedByArea()
        {
            List<GuestRequest> guestRequestsList = GetAllGuestRequests();
            return from item in guestRequestsList
                   group item by item.Area into g
                   select g;

        }*/

        /// <summary>
        /// רשימת דרישות לקוח מקובצת ע"פ אזור הנופש הנדרש
        /// </summary>
        /// <returns></returns>
        public Dictionary<AreaCode, List<GuestRequest>> getGuestRequestGroupedByArea()
        {
            List<GuestRequest> guestRequestsList = GetAllGuestRequests();

            return (from item in guestRequestsList
                    group item by item.Area into g
                    select new { AreaKey = g.Key, Requests = g.ToList() })
                   .ToDictionary(x => x.AreaKey, x => x.Requests);

        }

        /// <summary>
        /// רשימת דרישות לקוח מקובצת ע"פ מספר הנופשים
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<GuestRequest>> getGuestRequestGroupedByNumOfPeople()
        {
            List<GuestRequest> guestRequestsList = GetAllGuestRequests();

            return (from item in guestRequestsList
                    let numPeople = item.Children + item.Adults
                    group item by numPeople into g
                    select new { AmountKey = g.Key, Requests = g.ToList() })
                                 .ToDictionary(x => x.AmountKey, x => x.Requests);

        }

        /// <summary>
        /// רשימת מארחים מקובצת ע"פ מספר יחידות האירוח שהם מחזיקים
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<Host>> getHostGroupedByNumUnits()
        {
            List<HostingUnit> hostingUnitsList = GetAllHostingUnits();

            return (from item in hostingUnitsList
                    let ownerKey = item.Owner.HostKey
                    let countUnit = hostingUnitsList.Count(unit => unit.Owner.HostKey == ownerKey)
                    group item.Owner by countUnit into g
                    select new { HostKey = g.Key, Hosts = g.ToList() })
                                 .ToDictionary(x => x.HostKey, x => x.Hosts);

        }

        /// <summary>
        /// רשימות יחידות אירוח מקובת ע"פ אזור הנופש הנדרש
        /// </summary>
        /// <returns></returns>
        public Dictionary<AreaCode, List<HostingUnit>> getHostingUnitGroupedByArea()
        {
            List<HostingUnit> hostingUnitsList = GetAllHostingUnits();

            return hostingUnitsList.GroupBy(item => item.Area).ToDictionary(g => g.Key, g => g.ToList());

            /*
            return (List<Object>)from item in hostingUnitsList
                                 group item by item.Area into g
                                 select new { AreaKey = g.Key, Units = g.ToList() };
                                 */
        }


        /// <summary>
        /// פונקציה היוצרת רשימה מתוך רשימת בקשות אירוח על פי תנאי מסויים שישלח אליה
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<HostingUnit> getHostingUnitByCondition(UnitCondition cond)
        {
            List<HostingUnit> hostingUnitsList = GetAllHostingUnits();
            return hostingUnitsList.Where(item => cond(item)).ToList();
        }

        /// <summary>
        /// פונקציה היוצרת רשימה מתוך רשימת בקשות אירוח על פי תנאי מסויים שישלח אליה
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<GuestRequest> getGuestRequestByCondition(GuestCondition cond)
        {
            List<GuestRequest> guestRequestsList = GetAllGuestRequests();
            return guestRequestsList.Where(item => cond(item)).ToList();
        }

        public void initXmls()
        {
            dal.initilizeListToXml();
        }

        public void SetMailAndPasswordToConfiguration(string mail, string password)
        {
            dal.SetMailAndPasswordToConfiguration(mail, password);
        }

        public List<string> GetBanksList()
        {
            return dal.GetBanksList();
        }

        public List<string> GetBranchesList(string bankName)
        {
            return dal.GetBranchesList(bankName);
        }

        public BankBranch GetBranch(int bankKey, int branchKey)
        {
            return dal.GetBranch(bankKey, branchKey);
        }

        public string GetWebUsername()
        {
            return dal.GetWebUsername();
        }

        public string GetWebPassword()
        {
            return dal.GetWebPassword();
        }
    }
}