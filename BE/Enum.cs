using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum StatusCode { Open, ClosedDeal, ClosedExpired };
    public enum AreaCode {All, North, South, Center, Jerusalem };
    public enum HostingUnitTypeCode { Zimmer, Hotel, Camping, Etc };
    public enum GuestPreferencesCode { Necessary, Optional, NotInterested };
    public enum OrderStatusCode { NotHandledYet, MailSent, ClosedDueGuestNotResponding, CloseDueGuestResponding };




    //Menus
    public enum OpenningMenuOption {Back ,GuestRequest, HostingUnit, WebAdmin};
    public enum GuestRequestMenuOption { Back, AddGuestRequest };
    public enum HostingUnitMenuOption { Back, AddHostingUnit, PrivateArea };
    public enum PrivateAreaMenuOption { Back, UpdateHostingUnit, Order, RemoveHostingUnit };
    public enum OrderMenuOption { Back, QueryGuestList, QueryOrderList };
    public enum WebAdminMenuOption { Back, QueryGuestList, QueryHostingUnitList, QueryOrderList, Etc };


    //Query
    public enum Query { AllGuestRequest, AllHostingUnit, AllOrders,GroupGuestRequestByArea, GroupHostingUnitByArea, GroupGuestRequestByAmountPeople, GroupHostByNumUnits };



    //Properties
    public enum PropertiesData
    {
        GuestRequestKey, PrivateName, FamilyName, FullName, MailAdress, Status, RegistrationDate, EntryDate, ReleaseDate,
        Area, SubArea, Type, Adults, Children, Pool, Jacuzzi, Garden, Porch, ChildrensAttractions,
        HostingUnitKey, OwnerKey, OwnerPrivateName, OwnerFamilyName, OwnerFullName, OwnerPhoneNumber,
        OwnerMailAddress, OwnerBankAccountNumber, OwnerCollectionClearance, HostingUnitName, PhotoList, Diary,
        OrderKey, CreateDate, OrderDate, CommissionValue
        
    }

    /*
    public enum GuestRequestProperties
    {
        GuestRequestKey, PrivateName, FamilyName, FullName, MailAdress, Status, RegistrationDate, EntryDate,ReleaseDate,
        Area, SubArea, Type, Adults, Children, Pool, Jacuzzi, Garden, Porch, ChildrensAttractions
    };

    public enum HostingUnitProperties
    {
        HostingUnitKey, OwnerKey, OwnerPrivateName, OwnerFamilyName, OwnerPhoneNumber,
        OwnerMailAddress,OwnerCollectionClearance, HostingUnitName, PhotoList, Diary, Area, SubArea, Type,
        Adults, Children, Pool, Jacuzzi, Garden, Porch, ChildrensAttractions

    };

    public enum OrderProperties
    {
        HostingUnitKey, GuestRequestKey, OrderKey, Status, CreateDate, OrderDate, CommissionValue
    };
    */
}
