using BE;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public string TitleAuth { get; set; }
        public string userID { get; set; } = "";
        public string userPass { get; set; }
        public bool success { get; set; } = false;
        public int LoginType { get; set; } = 0;
        

        public LoginUserControl()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public void SetToolTipValues(string userTip, string passTip)
        {
            tbUser.ToolTip = "שששש! אל תגלה לאף אחד אבל המזהה הוא " + userTip;
            pbValue.ToolTip = "שששש! אל תגלה לאף אחד אבל הסיסמה היא " + passTip;
        }


        public void Login(object sender, RoutedEventArgs e)
        {

            try
            {
                bool res;
                if (LoginType == 1)
                    res = isValidWebOwnerAccess();
                else
                    res = isValidUnitAccess();

                if(res)
                {
                    success = true;
                    var window = Window.GetWindow(this);
                    window.Close();
                }
            }
            catch (ExceptionMessage ex)
            {
                MessageBox.Show(ex.Message, "מערכת הזדהות", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }

        public bool isValidUnitAccess()
        {
            if (userID.Equals(""))// || pbValue.Password.Equals(""))
            {
                throw new ExceptionMessage("נא למלא את פרטי ההזדהות");
            }

            if (!userID.IsNumber())
                throw new ExceptionMessage("מזהה חייב להיות מספר");


            if (MainWindow.bl.getHostingUnit(int.Parse(userID)) == null)
                throw new ExceptionMessage("מזהה יחידת אירוח אינו קיים");



            return true;
        }

        public bool isValidWebOwnerAccess()
        {
            if (userID.Equals("") || pbValue.Password.Equals(""))
            {
                throw new ExceptionMessage("נא למלא את פרטי ההזדהות");
            }

            if(!userID.Equals(MainWindow.bl.GetWebUsername()) || !pbValue.Password.Equals(MainWindow.bl.GetWebPassword()))
            {
                throw new ExceptionMessage("מזהה בעל האתר או סיסמה אינם נכונים, אנא נסה שוב");
            }
            return true;
        }
    }
}
