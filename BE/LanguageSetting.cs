using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Windows;

namespace BE
{
    public static class LanguageSetting
    {
        private static List<string> options = new List<string> { "he", "en" };
        private static string selectedLanguage;
        private static readonly List<string> files = new List<string>{"GuestRequest","Menu"};
        public static Dictionary<string, XElement> Data = new Dictionary<string, XElement>();
        private static readonly string basePath = "../../../BE/Language/";
        private static FlowDirection direction;

        public static FlowDirection Direction { get { return direction; } }

        public static void SetLanguage(string lang = "he")
        {
            if (lang.Equals(selectedLanguage))
                return;

            

            if (options.IndexOf(lang) == -1)
                lang = options[0];

            if (lang.Equals("he"))
                direction = FlowDirection.RightToLeft;
            else
                direction = FlowDirection.LeftToRight;

            selectedLanguage = lang;
            LoadLanguage();
        }

        private static void LoadLanguage()
        {
            Data.Clear();

            foreach(string file in files)
            {
                string path = basePath + selectedLanguage + "/" + file + ".xml";
                if (File.Exists(path))
                    Data[file] = XElement.Load(path);
            }
                
        }

        public static string Get(string file, string key)
        {
            if (!Data.ContainsKey(file))
                return "NONE";
            //return Data[key].Elements().Select(x => x.Element(key).Value.ToString()).FirstOrDefault();
            //return "asda";

            if (Data[file].Element(key) == null)
                return "NOT_FOUND";

            return Data[file].Elements()
                   .Where(e => e.Name.LocalName.Equals(key))
                   .Select(x => x.Value).FirstOrDefault();
            
        }


        
        

       

        

        
        

        
        

        
    }
}





namespace XmlTest
{
    class ProductsManagement
    {

        

    }
}

