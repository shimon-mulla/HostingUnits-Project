using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestUserControl.xaml
    /// </summary>
    public partial class GuestRequestUserControl : UserControl, INotifyPropertyChanged
    {
        private static readonly Regex numericRegex = new Regex("^[0-9]+$");
        private static readonly Regex hebrewRegex = new Regex("^[א-ת\\ \\-]+$");

        public IBl bl = FactorySingletonBl.GetBl();

        private static readonly string[] translateReferences = { "רצוי", "אפשרי", "לא מעוניין" };

        public GuestRequest guestRequest { set; get; } = new GuestRequest();
        private Dictionary<string, List<RadioButton>> radioList = new Dictionary<string, List<RadioButton>>();

        public GuestRequest request
        {
            set
            {
                guestRequest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("guestRequest"));
                }
            }
            get
            {
                return guestRequest;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public GuestRequestUserControl()
        {
            InitializeComponent();

            guestRequest.Type = HostingUnitTypeCode.Hotel;

            DataContext = this;

            //AreaSelection
            InitAreaSelection();

            //UnitTypeSelection
            InitUnitTypeSelection();

            InitGuestPreferenceOptions();

            InitDatePickerSelectionRange();

            RegisterOnChangedFieldsForSaveButton();


        }

        public void InitAreaSelection()
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
        }

        public RadioButton GenerateButtonWithText(string groupName, int index, bool isChecked = false)
        {
            return GenerateButtonWithText(groupName, translateReferences[index], isChecked);
        }

        public RadioButton GenerateButtonWithText(string groupName, string text, bool isChecked = false)
        {
            RadioButton rd = new RadioButton
            {
                Content = text,

                IsChecked = isChecked,
                FontWeight = FontWeights.Bold,
                Background = Brushes.DodgerBlue
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
            foreach (var PreferenceCode in MainWindow.listPreferencesCode)
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

            if (tbPrivateName.Text == "" || !tbPrivateName.Text.IsHebrew())
                return false;
            if (tbFamilyName.Text == "" || !tbFamilyName.Text.IsHebrew())
                return false;
            if (tbEmail.Text == "")
                return false;
            if (DatePickerArrive.SelectedDate == null)
                return false;
            if (DatePickerRelease.SelectedDate == null)
                return false;
            if (AreaSelection.SelectedIndex < 0)
                return false;
            if (tbSubArea.Text == "" || !tbSubArea.Text.IsHebrew())
                return false;
            if (UnitTypeSelection.SelectedIndex < 0)
                return false;
            if (tbAdults.Text == "" || !tbAdults.Text.IsNumber())
                return false;
            if (tbChildren.Text == "" || !tbChildren.Text.IsNumber())
                return false;

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
            if (!text.IsNumber())
            {
                e.Handled = true;
                MessageBox.Show("יש להכניס מספרים בלבד", "שים לב", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void PreviewEmailInput(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            String text = tb.Text;
            if (!text.IsEmail())
            {
                e.Handled = true;
                MessageBox.Show("כתובת אימייל אינה חוקית", "שים לב", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void PreviewHebrewInput(object sender, TextCompositionEventArgs e)
        {
            String text = (String)e.Text;

            if (!text.IsHebrew())
            {
                e.Handled = true;
                MessageBox.Show("רק אותיות בעברית", "שים לב", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public static bool ValidateMail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        private void ArriveDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            DatePickerRelease.SelectedDate = null;
            DateTime nextDay = DatePickerArrive.SelectedDate.Value.AddDays(1);

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
                //MessageBox.Show(guestRequest.ToString());
                bl.AddRequest(guestRequest);

                MessageBox.Show("דרישת הלקוח נוספה בהצלחה", "הודעת מערכת", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ExceptionMessage ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateRadioButton()
        {
            DatePickerArrive.SelectedDate = guestRequest.EntryDate;
            DatePickerRelease.SelectedDate = guestRequest.ReleaseDate;
            AreaSelection.Text = guestRequest.Area.ToString();
            UnitTypeSelection.Text = guestRequest.SubArea.ToString();
            RadioButton rd = radioList["Pool"].Where(x => x.Content.ToString() == translateReferences[Convert.ToInt32(guestRequest.Pool)]).FirstOrDefault();
            rd.IsChecked = true;
            RadioButton rd1 = radioList["Porch"].Where(x => x.Content.ToString() == translateReferences[Convert.ToInt32(guestRequest.Porch)]).FirstOrDefault();
            rd1.IsChecked = true;
            RadioButton rd2 = radioList["Jacuzzi"].Where(x => x.Content.ToString() == translateReferences[Convert.ToInt32(guestRequest.Jacuzzi)]).FirstOrDefault();
            rd2.IsChecked = true;
            RadioButton rd3 = radioList["ChildrenAttractions"].Where(x => x.Content.ToString() == translateReferences[Convert.ToInt32(guestRequest.ChildrensAttractions)]).FirstOrDefault();
            rd3.IsChecked = true;
            RadioButton rd4 = radioList["Garden"].Where(x => x.Content.ToString() == translateReferences[Convert.ToInt32(guestRequest.Garden)]).FirstOrDefault();
            rd4.IsChecked = true;
        }
    }
}