using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
       /* private static readonly Regex numericRegex = new Regex("^[0-9]+$");
        private static readonly Regex hebrewRegex = new Regex("^[א-ת\\ \\-]+$");

        public IBl bl = FactorySingletonBl.GetBl();

        private static readonly string[] translateReferences = { "רצוי", "אפשרי", "לא מעוניין" };

        private GuestRequest guestRequest = new GuestRequest();
        private Dictionary<string, List<RadioButton>> radioList = new Dictionary<string, List<RadioButton>>();
        */

        public GuestRequestWindow()
        {
            InitializeComponent();

            /*guestRequest.Type = HostingUnitTypeCode.Hotel;

            //guestRequest.PrivateName = "מתן";
            GuestWindow.DataContext = guestRequest;

            //AreaSelection
            InitAreaSelection();

            //UnitTypeSelection
            InitUnitTypeSelection();

            InitGuestPreferenceOptions();

            InitDatePickerSelectionRange();

            RegisterOnChangedFieldsForSaveButton();
            */

        }

        /*public void InitAreaSelection()
        {
            foreach (var AreaCode in MainWindow.listAreaType)
            {
                AreaSelection.Items.Add(AreaCode);
            }
        }

        public void InitUnitTypeSelection()
        {
            foreach (var UnitCode in MainWindow.listUnitType)
            {
                UnitTypeSelection.Items.Add(UnitCode);
            }
        }

        public void InitDatePickerSelectionRange()
        {
            //DateTime.Today.;

            DatePickerArrive.DisplayDateStart = DateTime.Today.AddMonths(-1);
            DatePickerArrive.DisplayDateEnd = DateTime.Today.AddMonths(11).AddDays(-1);

            DatePickerRelease.DisplayDateStart = DateTime.Today.AddMonths(-1);
            DatePickerRelease.DisplayDateEnd = DateTime.Today.AddMonths(11).AddDays(-1);

            DatePickerRelease.IsEnabled = false;
        }

        public RadioButton GenerateButtonWithText(string groupName,int index, bool isChecked = false)
        {
            return GenerateButtonWithText(groupName, translateReferences[index], isChecked);
        }

        public RadioButton GenerateButtonWithText(string groupName, string text, bool isChecked = false)
        {
            RadioButton rd = new RadioButton
            {
                Content = text,
                //GroupName = "rd" + text,
                //DataContext = (GuestPreferencesCode)index,
                IsChecked = isChecked
            };

            if (!radioList.ContainsKey(groupName))
            {
                radioList.Add(groupName, new List<RadioButton>());
            }
            radioList[groupName].Add(rd);
            
            
            return rd;
        }

        public void InitGuestPreferenceOptions()
        {
            
            int index = 0;
            bool isChecked = false;
            foreach(var PreferenceCode in MainWindow.listPreferencesCode)
            {
                //RadioButton rd = GenerateButtonWithText(index);
                isChecked = false;

                if (PreferenceCode.Equals("Optional"))
                    isChecked = true;

                GridPool.Children.Add(GenerateButtonWithText("Pool", index, isChecked));
                Grid.SetColumn(GridPool.Children[index], index);
                GridJacuzzi.Children.Add(GenerateButtonWithText("Jacuzzi", index, isChecked));
                Grid.SetColumn(GridJacuzzi.Children[index], index);
                GridGarden.Children.Add(GenerateButtonWithText("Garden", index, isChecked));
                Grid.SetColumn(GridGarden.Children[index], index);
                GridPorch.Children.Add(GenerateButtonWithText("Porch", index, isChecked));
                Grid.SetColumn(GridPorch.Children[index], index);
                GridChildrenAttractions.Children.Add(GenerateButtonWithText("ChildrenAttractions", index, isChecked));
                Grid.SetColumn(GridChildrenAttractions.Children[index], index);
                index++;
            }



            
            
        }

        public void RegisterOnChangedFieldsForSaveButton()
        {
            tbAdults.TextChanged += tbFormCheck;
            tbChildren.TextChanged += tbFormCheck;
            tbEmail.TextChanged += tbFormCheck;
            tbPrivateName.TextChanged += tbFormCheck;
            tbFamilyName.TextChanged += tbFormCheck;
            tbSubArea.TextChanged += tbFormCheck;
            UnitTypeSelection.SelectionChanged += slFormCheck;
            AreaSelection.SelectionChanged += slFormCheck;
            DatePickerArrive.SelectedDateChanged += slFormCheck;
            DatePickerRelease.SelectedDateChanged += slFormCheck;
        }

        public void tbFormCheck(object sender, TextChangedEventArgs e)
        {

           btSave.IsEnabled = FormValidation();


        }

        public void slFormCheck(object sender, SelectionChangedEventArgs e)
        {

            btSave.IsEnabled = FormValidation();

        }






        public bool FormValidation()
        {
            //MessageBox.Show("1");
            if (tbPrivateName.Text == "" || !tbPrivateName.Text.IsHebrew())
                return false;
            //MessageBox.Show("2");
            if (tbFamilyName.Text == "" || !tbFamilyName.Text.IsHebrew())
                return false;
            //MessageBox.Show("3");
            if (tbEmail.Text == "")
                return false;
            //MessageBox.Show("4");
            if (DatePickerArrive.SelectedDate == null)
                return false;
            //MessageBox.Show("5");
            if (DatePickerRelease.SelectedDate == null)
                return false;
            //MessageBox.Show("6");
            if (AreaSelection.SelectedIndex < 0)
                return false;
            //MessageBox.Show("7");
            if (tbSubArea.Text == "" || !tbSubArea.Text.IsHebrew())
                return false;
            //MessageBox.Show("8");
            if (UnitTypeSelection.SelectedIndex < 0)
                return false;
            //MessageBox.Show("9");
            if (tbAdults.Text == "" || !tbAdults.Text.IsNumber())
                return false;
            //MessageBox.Show("10");
            if (tbChildren.Text == "" || !tbChildren.Text.IsNumber())
                return false;
            //MessageBox.Show("11");

            if (!tbEmail.Text.IsEmail())
            {
                tbEmail.Focus();
                return false;
            }

            return true;
        }


        public void PreviewNumericInput(object sender, TextCompositionEventArgs e)
        {
            String text = (String)e.Text;

            if(!text.IsNumber())
            {
                e.Handled = true;
                MessageBox.Show("רק מספרים");
            }
            

            
        }

        

        public void PreviewEmailInput(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            String text = tb.Text;
            
            if (!text.IsEmail())
            {
                e.Handled = true;
                MessageBox.Show("כתובת אימייל אינה חוקית");
            }



        }


        public static bool ValidateMail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        public void PreviewHebrewInput(object sender, TextCompositionEventArgs e)
        {
            String text = (String)e.Text;
            
            if (!text.IsHebrew())
            {
                e.Handled = true;
                MessageBox.Show("רק אותיות בעברית");
            }



        }

        private void ArriveDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            DateTime nextDay = DatePickerArrive.SelectedDate.Value.AddDays(1);
            DatePickerRelease.SelectedDate = nextDay;

            if (DatePickerRelease.DisplayDateEnd < nextDay)
            {

                guestRequest.ReleaseDate = new DateTime();
                DatePickerRelease.IsEnabled = false;
            }
            else
            {
                guestRequest.EntryDate = DatePickerArrive.SelectedDate.Value;
                DatePickerRelease.IsEnabled = true;
                DatePickerRelease.DisplayDateStart = nextDay;
                DatePickerRelease.IsDropDownOpen = true;
                
            }

            
            
        }

        private void LeaveDate_Changed(object sender, SelectionChangedEventArgs e)
        {

            guestRequest.ReleaseDate = DatePickerRelease.SelectedDate.Value;
            
        }



        private void ListBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(this.DataContext.ToString());
        }


        private GuestPreferencesCode getSelectedRadioValue(List<RadioButton> listR)
        {
            

            //return listR.Select(item => (item.IsChecked == true)).FirstOrDefault();
            return (GuestPreferencesCode)(listR.Select((item, i) => new
            {
                Item = item,
                Position = i
            }).Where(m => m.Item.IsChecked == true).FirstOrDefault().Position);
        }



        private void UpdateGuestRadioSelected()
        {
            guestRequest.Pool = getSelectedRadioValue(radioList["Pool"]);
            guestRequest.Jacuzzi = getSelectedRadioValue(radioList["Jacuzzi"]);
            guestRequest.Garden = getSelectedRadioValue(radioList["Garden"]);
            guestRequest.Porch = getSelectedRadioValue(radioList["Porch"]);
            guestRequest.ChildrensAttractions = getSelectedRadioValue(radioList["ChildrenAttractions"]);

        }



        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                //GridPool.Children[0].    
                UpdateGuestRadioSelected();
                guestRequest.RegistrationDate = DateTime.Today;


                MessageBox.Show(guestRequest.ToString());
                bl.AddRequest(guestRequest);
            }
            catch (ExceptionMessage ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }*/
    }


    
}
