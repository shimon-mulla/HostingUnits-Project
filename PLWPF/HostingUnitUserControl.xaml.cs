using BE;
using BL;
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
using System.ComponentModel;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Utilities;
using System;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl, INotifyPropertyChanged
    {
        IBl bl = FactorySingletonBl.GetBl();
        private Regex regex = new Regex("[^0-9]+");


        private List<string> BanksList;
        private List<string> BranchesByBankList;
        public string lastSelectedBank { get; set; }
        public string lastSelectedBranch { get; set; }
        private BankBranch selectedBranch;


        public HostingUnit hostingUnit { set; get; } = new HostingUnit();
        public string photo { set; get; }

        public HostingUnit hosting
        {
            set
            {
                hostingUnit = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("hostingUnit"));
                    InitToggles();
                }
            }
            get
            {
                return hostingUnit;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public HostingUnitUserControl()
        {
            hosting = new HostingUnit { Owner = new Host() };
            hosting.Diary = new bool[12, 31];
            hosting.Owner.BankAccountDetails = new BankAccount();
            hosting.PhotoList = new List<string>();
            hostingUnit.Owner = new Host { BankAccountDetails = new BankAccount() };
            hostingUnit.PhotoList = new List<string>();
            InitializeComponent();
            //InitToggles

            InitBankDetails();

            InitAreaSelection();
            InitAreaUnitTypeSelection();
            DataContext = this;

            RegisterOnChangedFieldsForSaveButton();

        }

        public void InitBankDetails()
        {
            BanksList = bl.GetBanksList();
            cbBanksList.ItemsSource = BanksList;

            cbBranchesList.IsEnabled = false;
        }

        public void RegisterOnChangedFieldsForSaveButton()
        {
            firstName.TextChanged += tbFormCheck;
            id.TextChanged += tbFormCheck;
            phonNumber.TextChanged += tbFormCheck;
            lastName.TextChanged += tbFormCheck;
            email.TextChanged += tbFormCheck;
            addressBranch.TextChanged += tbFormCheck;
            nameBank.TextChanged += tbFormCheck;
            cityBranch.TextChanged += tbFormCheck;
            numAccount.TextChanged += tbFormCheck;
            hostingName.TextChanged += tbFormCheck;
            SubAreaUnit.TextChanged += tbFormCheck;
            numAdults.TextChanged += tbFormCheck;
            numChildren.TextChanged += tbFormCheck;
            UnitTypeSelection.SelectionChanged += slFormCheck;
            AreaSelection.SelectionChanged += slFormCheck;
        }

        public void tbFormCheck(object sender, TextChangedEventArgs e)
        {

           if (hostingUnit.HostingUnitKey <= 0)
                SaveBtn.IsEnabled = FormValidation();
           else
                SaveBtn.IsEnabled = true;
        }
        public void slFormCheck(object sender, SelectionChangedEventArgs e)
        {

            if (hostingUnit.HostingUnitKey <= 0)
                SaveBtn.IsEnabled = FormValidation();
            else
                SaveBtn.IsEnabled = true;
        }

        public bool FormValidation()
        {
            if (firstName.Text == "" || !firstName.Text.IsHebrew())
                return false;
            if (lastName.Text == "" || !lastName.Text.IsHebrew())
                return false;
            if (email.Text == "")
                return false;

            if (cbBanksList.SelectedIndex < 0)
                return false;

            if (cbBranchesList.SelectedIndex < 0)
                return false;
            if (AreaSelection.SelectedIndex < 0)
                return false;

            if (UnitTypeSelection.SelectedIndex < 0)
                return false;
            if (numAdults.Text == "" || !numAdults.Text.IsNumber())
                return false;
            if (numChildren.Text == "" || !numChildren.Text.IsNumber())
                return false;
            if (hostingName.Text == "" || !hostingName.Text.IsHebrew())
                return false;
            if (SubAreaUnit.Text == "" || !SubAreaUnit.Text.IsHebrew())
                return false;
            if (id.Text == "" || !id.Text.IsNumber())
                return false;
            if (phonNumber.Text == "" || !phonNumber.Text.IsNumber())
                return false;
            if (numAccount.Text == "" || !numAccount.Text.IsNumber())
                return false;
            if (!email.Text.IsEmail())
            {
                email.Focus();
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


        public void InitToggles()
        {
            flagPool.Toggled1 = hostingUnit.Pool;
            flagPool.changeToggle();
            flagPorch.Toggled1 = hostingUnit.Porch;
            flagPorch.changeToggle();
            flagJacuzzi.Toggled1 = hostingUnit.Jacuzzi;
            flagJacuzzi.changeToggle();
            flagGarden.Toggled1 = hostingUnit.Garden;
            flagGarden.changeToggle();
            flagAttraction.Toggled1 = hostingUnit.ChildrensAttractions;
            flagAttraction.changeToggle();
        }

        public void InitAreaSelection()
        {
            foreach (var AreaCode in MainWindow.listAreaType)
            {
                AreaSelection.Items.Add(AreaCode);
            }
        }

        public void InitAreaUnitTypeSelection()
        {
            foreach (var UnitCode in MainWindow.listUnitType)
            {
                UnitTypeSelection.Items.Add(UnitCode);
            }
        }

        

        private void MousePool(object sender, MouseButtonEventArgs e)
        {
            hostingUnit.Pool = flagPool.Toggled1;
        }

        private void MouseJacuzzi(object sender, MouseButtonEventArgs e)
        {
            hostingUnit.Jacuzzi = flagJacuzzi.Toggled1;
        }

        private void MousePorch(object sender, MouseButtonEventArgs e)
        {
            hostingUnit.Porch = flagPorch.Toggled1;
        }

        private void MouseGarden(object sender, MouseButtonEventArgs e)
        {
            hostingUnit.Garden = flagGarden.Toggled1;
        }

        private void MouseAttraction(object sender, MouseButtonEventArgs e)
        {
            hostingUnit.ChildrensAttractions = flagAttraction.Toggled1;
        }

        /// <summary>
        /// שמירת יחידת אירוח
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_click(object sender, RoutedEventArgs e)
        {
            try
            {
                hostingUnit.Owner.BankAccountDetails.BankNumber = selectedBranch.BranchCode;
                hostingUnit.Owner.BankAccountDetails.BankName = selectedBranch.BankName;

                hostingUnit.Owner.BankAccountDetails.BranchNumber = selectedBranch.BranchCode;
                hostingUnit.Owner.BankAccountDetails.BranchName = selectedBranch.BranchName;

                hostingUnit.Owner.BankAccountDetails.BranchAddress = selectedBranch.BranchAddress;
                hostingUnit.Owner.BankAccountDetails.BranchCity = selectedBranch.BranchCity;

                string message;
                if (hostingUnit.HostingUnitKey == 0)
                {
                    bl.AddUnit(hostingUnit);
                    message = "יחידת אירוח נוספה בהצלחה";
                }
                else
                {
                    bl.UpdateUnit(hostingUnit);
                    message = "יחידת אירוח עודכנה בהצלחה";
                }
                MessageBox.Show(message, "הודעת מערכת", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        public ToolTip TooltipeCreate(string str)
        {
            ToolTip toolTip = new ToolTip
            {
                Content = str,
                Background = Brushes.Yellow,
                Foreground = Brushes.Red,
                FontSize = 18
            };
            return toolTip;
        }

        /// <summary>
        /// לא מאפשרת בחירת בנקים
        /// </summary>
        public void SetBankNoEditable()
        {
            cbBanksList.Visibility = Visibility.Collapsed;
            cbBranchesList.Visibility = Visibility.Collapsed;

            nameBank.Visibility = Visibility.Visible;
            nameBranch.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// מסממנת תאריכים תפוסים
        /// </summary>
        /// <param name="myDates"></param>
        public void SetBlackoutDates(bool[,] myDates)
        {
            int m = DateTime.Today.Month;

            popCalendar.IsOpen = true;
            
            for (int i = 0; i < myDates.GetLength(0); i++)
                for (int j = 0; j < myDates.GetLength(1); j++)
                {
                    if (myDates[i, j])
                    {
                        DateTime temp;
                        if (DateTime.TryParse((j + 1) + "/" + (i + 1) + "/" + DateTime.Today.Year, out temp))
                        {
                            myCalendar.BlackoutDates.Add( new CalendarDateRange(temp));
                        }
                    }
                }

        }

        public void ClearBlackoutDates()
        {
            popCalendar.IsOpen = false;
            myCalendar.BlackoutDates.Clear();
        }

        private void cbBanksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex < 0)
                return;
            cbBranchesList.IsEnabled = false;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string[] bankSplit = (sender as ComboBox).SelectedValue.ToString().Split('-');


            BranchesByBankList = bl.GetBranchesList(bankSplit[1].Trim());
            cbBranchesList.ItemsSource = BranchesByBankList;

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            this.IsEnabled = true;

            cbBranchesList.IsEnabled = true;
            cbBranchesList.IsDropDownOpen = true;
            cbBranchesList.Focus();
        }

        private void cbBranchesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cbBanksList.SelectedIndex < 0 || (sender as ComboBox).SelectedIndex < 0)
                return;

            string[] bankSplit = cbBanksList.SelectedValue.ToString().Split('-');
            string[] branchSplit = (sender as ComboBox).SelectedValue.ToString().Split('-');


            selectedBranch = bl.GetBranch(Convert.ToInt32(bankSplit[0]), Convert.ToInt32(branchSplit[0]));

            if(selectedBranch != null)
            {

                
                addressBranch.Text = selectedBranch.BranchAddress;
                cityBranch.Text = selectedBranch.BranchCity;

            }
        }

        private void UpdateBanksListBySearchDisplay(string t)
        {
            if (t == null)
                return;

            if (t.Equals(""))
                cbBanksList.ItemsSource = BanksList;
            else
                cbBanksList.ItemsSource = BanksList.Where(str => str.Contains(t)).ToList();
        }

        private void UpdateBranchesListBySearchDisplay(string t)
        {
            if (t == null)
                return;

            if (t.Equals(""))
                cbBranchesList.ItemsSource = BranchesByBankList;
            else
                cbBranchesList.ItemsSource = BranchesByBankList.Where(str => str.Contains(t)).ToList();
        }

        private void cbBanksList_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox myCB = (sender as ComboBox);
            if (myCB.Text == null)
                return;

            UpdateBanksListBySearchDisplay(myCB.Text.ToString());
        }


        private void cbBranchesList_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox myCB = (sender as ComboBox);
            if (myCB.Text == null)
                return;

            UpdateBranchesListBySearchDisplay(myCB.Text.ToString());
        }

        private void cbBanksList_DropDownOpened(object sender, EventArgs e)
        {
            cbBanksList.ItemsSource = BanksList;
        }

        private void cbBranchesList_DropDownOpened(object sender, EventArgs e)
        {
            cbBranchesList.ItemsSource = BranchesByBankList;
        }

        
    }
}
