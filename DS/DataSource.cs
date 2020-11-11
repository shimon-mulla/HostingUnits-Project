using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        public static List<HostingUnit> HostingUnitsCollection = new List<HostingUnit>()
        {
            new HostingUnit
            {
                HostingUnitKey = 10000000,
                Owner = new Host
                {
                    HostKey = "123456789",
                    PrivateName = "matan",
                    FamilyName = "omesi",
                    PhoneNumber = "050-1234567",
                    MailAddress = "sss@sss.sss",
                    BankAccountDetails = new BankAccount
                    {
                        BankNumber = 12,
                        BankName = "הפועלים",
                        BranchNumber = 520,
                        BranchAddress = "גבעת שאול",
                        BranchCity = "ירושלים",
                        BankAccountNumber = 12345
                    },
                    BankAccountNumber = 12345,
                    CollectionClearance = true
                },
                HostingUnitName = "יחידה 1",
                Diary = new bool[12,31],
                Area = AreaCode.Center,
                SubArea = "tel-aviv",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 3,
                Children = 4,
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false
            },
            new HostingUnit
            {
                HostingUnitKey = 10000001,
                Owner = new Host
                {
                    HostKey = "172355123",
                    PrivateName = "shimon",
                    FamilyName = "levi",
                    PhoneNumber = "050-8754854",
                    MailAddress = "aaa@sss.sss",
                    BankAccountDetails = new BankAccount
                    {
                        BankNumber = 12,
                        BankName = "הפועלים",
                        BranchNumber = 520,
                        BranchAddress = "גבעת שאול",
                        BranchCity = "ירושלים",
                        BankAccountNumber = 87854
                    },
                    BankAccountNumber = 87854,
                    CollectionClearance = false
                },
                HostingUnitName = "מלונית",
                Diary = new bool[12,31],
                Area = AreaCode.Center,
                SubArea = "tel-aviv",
                Type = HostingUnitTypeCode.Hotel,
                Adults = 2,
                Children = 0,
                Pool = true,
                Jacuzzi = false,
                Garden = false,
                ChildrensAttractions = true
            },
            new HostingUnit
            {
                HostingUnitKey = 10000002,
                Owner = new Host
                {
                    HostKey = "523265896",
                    PrivateName = "yaakov",
                    FamilyName = "mor",
                    PhoneNumber = "052-7845125",
                    MailAddress = "xxx@eee.wew",
                    BankAccountDetails = new BankAccount
                    {
                        BankNumber = 17,
                        BankName = "מרכנתיל",
                        BranchNumber = 878,
                        BranchAddress = "ארלוזורוב",
                        BranchCity = "תל-אביב",
                        BankAccountNumber = 14225
                    },
                    BankAccountNumber = 14225,
                    CollectionClearance = true
                },
                HostingUnitName = "צימר פנינה",
                Diary = new bool[12,31],
                Area = AreaCode.North,
                SubArea = "טבריה",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 4,
                Children = 2,
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = true
            },
            new HostingUnit
            {
                HostingUnitKey = 10000003,
                Owner = new Host
                {
                    HostKey = "124587532",
                    PrivateName = "yosef",
                    FamilyName = "mulla",
                    PhoneNumber = "053-7845822",
                    MailAddress = "hjgf@ssd.wew",
                    BankAccountDetails = new BankAccount
                    {
                        BankNumber = 17,
                        BankName = "מרכנתיל",
                        BranchNumber = 878,
                        BranchAddress = "ארלוזורוב",
                        BranchCity = "תל-אביב",
                        BankAccountNumber = 88752
                    },
                    BankAccountNumber = 88752,
                    CollectionClearance = true
                },
                HostingUnitName = "צימר זהב",
                Diary = new bool[12,31],
                Area = AreaCode.North,
                SubArea = "גולן",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 2,
                Children = 0,
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false
            },
            new HostingUnit
            {
                HostingUnitKey = 10000004,
                Owner = new Host
                {
                    HostKey = "231452698",
                    PrivateName = "meir",
                    FamilyName = "shron",
                    PhoneNumber = "058-7842514",
                    MailAddress = "meir@ppp.yyy",
                    BankAccountDetails = new BankAccount
                    {
                        BankNumber = 15,
                        BankName = "יהב",
                        BranchNumber = 525,
                        BranchAddress = "מלאכי",
                        BranchCity = "בני-ברק",
                        BankAccountNumber = 45789
                    },
                    BankAccountNumber = 45789,
                    CollectionClearance = false
                },
                HostingUnitName = "ירח וכוכבים",
                Diary = new bool[12,31],
                Area = AreaCode.South,
                SubArea = "אילת",
                Type = HostingUnitTypeCode.Etc,
                Adults = 3,
                Children = 0,
                Pool = false,
                Jacuzzi = false,
                Garden = false,
                ChildrensAttractions = false
            }
        };
        public static List<GuestRequest> GuestRequestsCollection = new List<GuestRequest>()
        {
            new GuestRequest
            {
                GuestRequestKey = 10000000,
                PrivateName = "yosef",
                FamilyName = "koen",
                MailAdress = "qwe@ddd.com",
                Status = StatusCode.Open,
                RegistrationDate = new DateTime(2019, 5, 1),//"29/05/2019",
                EntryDate = new DateTime(2020, 1, 1),//"01/01/2020",
                ReleaseDate = new DateTime(2020, 1, 5),//"05/01/2020",
                Area = AreaCode.All,
                SubArea = "",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 2,
                Children = 0,
                Pool = GuestPreferencesCode.Necessary,
                Jacuzzi = GuestPreferencesCode.Necessary,
                Garden = GuestPreferencesCode.Necessary,
                ChildrensAttractions = GuestPreferencesCode.NotInterested,
            },
            new GuestRequest
            {
                GuestRequestKey = 10000001,
                PrivateName = "moshe",
                FamilyName = "muler",
                MailAdress = "mmm@ddd.com",
                Status = StatusCode.Open,
                RegistrationDate = new DateTime(2019, 6, 5),//"05/06/2019",
                EntryDate = new DateTime(2020, 3, 25),//"28/03/2020",
                ReleaseDate = new DateTime(2020, 3, 28),//"29/03/2020",
                Area = AreaCode.Center,
                SubArea = "tel-aviv",
                Type = HostingUnitTypeCode.Hotel,
                Adults = 2,
                Children = 0,
                Pool = GuestPreferencesCode.Necessary,
                Jacuzzi = GuestPreferencesCode.NotInterested,
                Garden = GuestPreferencesCode.NotInterested,
                ChildrensAttractions = GuestPreferencesCode.Necessary,
            },
            new GuestRequest
            {
                GuestRequestKey = 10000002,
                PrivateName = "israel",
                FamilyName = "azria",
                MailAdress = "yyy@ddd.com",
                Status = StatusCode.Open,
                RegistrationDate = new DateTime(2019, 9, 24),//"24/09/2019",
                EntryDate = new DateTime(2020, 8, 25),//"25/08/2020",
                ReleaseDate = new DateTime(2020, 8,26),//"29/08/2020",
                Area = AreaCode.North,
                SubArea = "טבריה",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 2,
                Children = 2,
                Pool = GuestPreferencesCode.Necessary,
                Jacuzzi = GuestPreferencesCode.Necessary,
                Garden = GuestPreferencesCode.Necessary,
                ChildrensAttractions = GuestPreferencesCode.Necessary,
            },
            new GuestRequest
            {
                GuestRequestKey = 10000003,
                PrivateName = "yogev",
                FamilyName = "david",
                MailAdress = "ydy@ddd.com",
                Status = StatusCode.Open,
                RegistrationDate = new DateTime(2019, 10, 1),//"01/10/2019",
                EntryDate = new DateTime(2019, 11, 30),//"30/11/2019",
                ReleaseDate = new DateTime(2019, 12, 2),//"02/12/2019",
                Area = AreaCode.North,
                SubArea = "גולן",
                Type = HostingUnitTypeCode.Zimmer,
                Adults = 2,
                Children = 0,
                Pool = GuestPreferencesCode.Necessary,
                Jacuzzi = GuestPreferencesCode.Necessary,
                Garden = GuestPreferencesCode.Necessary,
                ChildrensAttractions = GuestPreferencesCode.NotInterested,
            },
            new GuestRequest
            {
                GuestRequestKey = 10000004,
                PrivateName = "maor",
                FamilyName = "mor",
                MailAdress = "maor@ddd.com",
                Status = StatusCode.Open,
                RegistrationDate = new DateTime(2019, 10, 1),//"01/10/2019",
                EntryDate = new DateTime(2020, 10, 1),//"01/10/2020",
                ReleaseDate = new DateTime(2020, 10, 15),//"15/10/2020",
                Area = AreaCode.All,
                SubArea = "אילת",
                Type = HostingUnitTypeCode.Etc,
                Adults = 2,
                Children = 0,
                Pool = GuestPreferencesCode.NotInterested,
                Jacuzzi = GuestPreferencesCode.NotInterested,
                Garden = GuestPreferencesCode.NotInterested,
                ChildrensAttractions = GuestPreferencesCode.NotInterested,
            }

        };
        public static List<Order> OrdersCollection = new List<Order>()
        {
            /*new Order
            {
                HostingUnitKey = 10000000,
                GuestRequestKey = 10000000,
                OrderKey = 20000000,
                Status = OrderStatusCode.NotHandledYet,
                CreateDate = new DateTime(2019, 3, 27),//"27/03/2019",
                OrderDate = new DateTime(2019, 3, 30),//"30/03/2019"
            },*/
             new Order
            {
                HostingUnitKey = 10000001,
                GuestRequestKey = 10000001,
                OrderKey = 20000001,
                Status = OrderStatusCode.NotHandledYet,
                CreateDate = new DateTime(2019, 5, 30),//"30/05/2019",
                OrderDate = new DateTime(2019, 6, 2),//"02/06/2019"
            },
              new Order
            {
                HostingUnitKey = 10000002,
                GuestRequestKey = 10000002,
                OrderKey = 20000002,
                Status = OrderStatusCode.NotHandledYet,
                CreateDate = new DateTime(2019, 8, 1),//"01/08/2019",
                OrderDate = new DateTime(2019, 8, 7),//""
            },
               new Order
            {
                HostingUnitKey = 10000003,
                GuestRequestKey = 10000003,
                OrderKey = 20000003,
                Status = OrderStatusCode.NotHandledYet,
                CreateDate = new DateTime(2019, 6, 15),//"15/06/2019",
                OrderDate = new DateTime(2019, 6, 16),//"16/06/2019"
            },
                new Order
            {
                HostingUnitKey = 10000004,
                GuestRequestKey = 10000004,
                OrderKey = 20000004,
                Status = OrderStatusCode.NotHandledYet,
                CreateDate = new DateTime(2019, 1, 1),//"01/01/2019",
                OrderDate = new DateTime(2019, 1, 3),//""
            }

        };
        public static List<string> Branches = new List<string>()
        {
            "jerusalem",
            "tel-aviv",
            "hulon",
            "hadera",
            "tverya"
        };
    }
}