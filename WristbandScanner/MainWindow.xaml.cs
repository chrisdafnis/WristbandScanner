using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
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
using CsvHelper;

namespace WristbandScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var uri = new Uri(@"c:\users\chrisd.dakotais\documents\visual studio 2017\Projects\WristbandScanner\WristbandScanner\Images\Dakota Healthcare Smaller Logo.jpg");
            DakotaLogo.Source = new BitmapImage(uri);
            tbFirstName.Focus();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbFirstName.Text;
            string surname = tbSurname.Text;
            string jobTitle = tbJobTitle.Text;
            string trust = tbTrust.Text;
            string interests = '"' + tbInterests.Text + '"';
            bool? followUpCall = cbFollowUpCall.IsChecked;
            bool? followUpVisit = cbFollowUpVisit.IsChecked;
            string phoneNumber = tbPhoneNumber.Text;
            string emailAddress = tbEmailAddress.Text;

            string csv = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}",
                firstName,
                surname,
                jobTitle,
                trust,
                interests,
                followUpCall.ToString(),
                followUpVisit.ToString(),
                phoneNumber,
                emailAddress,
                Environment.NewLine);

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\EventAttendees.csv";
            StreamWriter write = new StreamWriter(filePath, append: true);
            CsvWriter csw = new CsvWriter(write);
            write.Write(csv);

            write.Close();
            //File.AppendAllText(filePath, csv);
            ResetFields();
        }

        private void ResetFields()
        {
            tbFirstName.Text = String.Empty;
            tbSurname.Text = String.Empty;
            tbJobTitle.Text = String.Empty;
            tbTrust.Text = String.Empty;
            tbInterests.Text = String.Empty;
            cbFollowUpCall.IsChecked = false;
            cbFollowUpVisit.IsChecked = false;
            tbPhoneNumber.Text = String.Empty;
            tbEmailAddress.Text = String.Empty;

            tbFirstName.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
