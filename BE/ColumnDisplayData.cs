using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BE
{
    class ColumnDisplayData
    {
        public static Dictionary<PropertiesData, ColumnDisplay> GuestRequestColumnsDisplay = new Dictionary<PropertiesData, ColumnDisplay>
        {

            { PropertiesData.GuestRequestKey, new ColumnDisplay{
                        Header = "מזהה דרישה",
                        Path = "GuestRequestKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.PrivateName,new ColumnDisplay{
                        Header = "שם פרטי",
                        Path = "PrivateName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.FamilyName,new ColumnDisplay{
                        Header = "שם משפחה",
                        Path = "FamilyName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.FullName,new ColumnDisplay{
                        Header = "שם הלקוח",
                        Path = "FullName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.MailAdress,new ColumnDisplay{
                        Header = "כתובת דוא\"ל",
                        Path = "MailAdress",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.Status, new ColumnDisplay{
                        Header = "מצב",
                        Path = "Status",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.RegistrationDate, new ColumnDisplay{
                        Header = "תאריך רישום למערכת",
                        Path = "RegistrationDate",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.EntryDate, new ColumnDisplay{
                        Header = "תאריך הגעה",
                        Path = "EntryDate",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.ReleaseDate, new ColumnDisplay{
                        Header = "תאריך עזיבה",
                        Path = "ReleaseDate",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.Area, new ColumnDisplay{
                        Header = "אזור",
                        Path = "Area",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.SubArea, new ColumnDisplay{
                        Header = "תת אזור",
                        Path = "SubArea",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Type, new ColumnDisplay{
                        Header = "סוג",
                        Path = "Type",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Adults, new ColumnDisplay{
                        Header = "מבוגרים",
                        Path = "Adults",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Children, new ColumnDisplay{
                        Header = "ילדים",
                        Path = "Children",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Pool, new ColumnDisplay{
                        Header = "בריכה",
                        Path = "Pool",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Jacuzzi, new ColumnDisplay{
                        Header = "ג'קוזי",
                        Path = "Jacuzzi",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Garden, new ColumnDisplay{
                        Header = "גינה",
                        Path = "Garden",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Porch, new ColumnDisplay{
                        Header = "מרפסת",
                        Path = "Porch",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.ChildrensAttractions, new ColumnDisplay{
                        Header = "פעילות לילדים",
                        Path = "ChildrensAttractions",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}
            }
        };

        public static Dictionary<PropertiesData, ColumnDisplay> HostingUnitColumnsDisplay = new Dictionary<PropertiesData, ColumnDisplay>
        {

            { PropertiesData.HostingUnitKey, new ColumnDisplay{
                        Header = "מזהה יחידה",
                        Path = "HostingUnitKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.HostingUnitName,new ColumnDisplay{
                        Header = "שם היחידה",
                        Path = "HostingUnitName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerPrivateName,new ColumnDisplay{
                        Header = "שם פרטי מארח",
                        Path = "Owner.PrivateName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerFamilyName,new ColumnDisplay{
                        Header = "שם משפחה מארח",
                        Path = "Owner.FamilyName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerPhoneNumber,new ColumnDisplay{
                        Header = "מספר טלפון",
                        Path = "Owner.PhoneNumber",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerMailAddress, new ColumnDisplay{
                        Header = "כתובת דוא\"ל",
                        Path = "Owner.MailAddress",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerCollectionClearance, new ColumnDisplay{
                        Header = "עמלה",
                        Path = "Owner.CollectionClearance",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.PhotoList, new ColumnDisplay{
                        Header = "תמונות",
                        Path = "PhotoList",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.Area, new ColumnDisplay{
                        Header = "אזור",
                        Path = "Area",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.SubArea, new ColumnDisplay{
                        Header = "תת אזור",
                        Path = "SubArea",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Type, new ColumnDisplay{
                        Header = "סוג",
                        Path = "Type",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Adults, new ColumnDisplay{
                        Header = "מבוגרים",
                        Path = "Adults",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Children, new ColumnDisplay{
                        Header = "ילדים",
                        Path = "Children",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Pool, new ColumnDisplay{
                        Header = "בריכה",
                        Path = "Pool",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Jacuzzi, new ColumnDisplay{
                        Header = "ג'קוזי",
                        Path = "Jacuzzi",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Garden, new ColumnDisplay{
                        Header = "גינה",
                        Path = "Garden",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Porch, new ColumnDisplay{
                        Header = "מרפסת",
                        Path = "Porch",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.ChildrensAttractions, new ColumnDisplay{
                        Header = "פעילות לילדים",
                        Path = "ChildrensAttractions",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}
            }
        };

        public static Dictionary<PropertiesData, ColumnDisplay> OrderColumnsDisplay = new Dictionary<PropertiesData, ColumnDisplay>
        {

            { PropertiesData.OrderKey, new ColumnDisplay{
                        Header = "מזהה הזמנה",
                        Path = "OrderKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.HostingUnitKey,new ColumnDisplay{
                        Header = "מזהה יחידה",
                        Path = "HostingUnitKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.GuestRequestKey,new ColumnDisplay{
                        Header = "מזהה דרישה",
                        Path = "GuestRequestKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.Status,new ColumnDisplay{
                        Header = "מצב",
                        Path = "Status",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.CommissionValue,new ColumnDisplay{
                        Header = "עמלה",
                        Path = "CommissionValue",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.CreateDate, new ColumnDisplay{
                        Header = "תאריך פתיחת הזמנה",
                        Path = "CreateDate",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OrderDate, new ColumnDisplay{
                        Header = "תאריך ביצוע הזמנה",
                        Path = "OrderDate",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}
            }
        };

        public static Dictionary<PropertiesData, ColumnDisplay> HostColumnsDisplay = new Dictionary<PropertiesData, ColumnDisplay>
        {

            { PropertiesData.OwnerKey, new ColumnDisplay{
                        Header = "מזהה מארח",
                        Path = "HostKey",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerPrivateName,new ColumnDisplay{
                        Header = "שם פרטי",
                        Path = "PrivateName",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerFamilyName,new ColumnDisplay{
                        Header = "שם משפחה",
                        Path = "FamilyName",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerFullName,new ColumnDisplay{
                        Header = "שם המארח",
                        Path = "FullName",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerPhoneNumber,new ColumnDisplay{
                        Header = "מספר טלפון",
                        Path = "PhoneNumber",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerMailAddress, new ColumnDisplay{
                        Header = "כתובת דוא\"ל",
                        Path = "MailAddress",
                        isBindNeeded = true,
                        Width = new DataGridLength(2, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerBankAccountNumber, new ColumnDisplay{
                        Header = "מספר חשבון",
                        Path = "BankAccountNumber",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}},
            { PropertiesData.OwnerCollectionClearance, new ColumnDisplay{
                        Header = "אישור גבייה",
                        Path = "CollectionClearance",
                        isBindNeeded = true,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star)}
            }
        };



    }



}
