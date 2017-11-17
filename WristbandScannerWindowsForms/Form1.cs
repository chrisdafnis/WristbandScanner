using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WristbandScannerWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxBarcode.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            IAttendee attendee = new Attendee();
            attendee.FirstName = textBoxFirstName.Text;
            attendee.Surname = textBoxSurname.Text;
            attendee.JobTitle = textBoxJobTitle.Text;
            attendee.Trust = textBoxTrust.Text;
            attendee.Interests = '"' + textBoxInterests.Text + '"';
            attendee.FollowUpCall = checkBoxFollowUpCall.Checked;
            attendee.FollowUpVisit = checkBoxFollowUpVisit.Checked;
            attendee.PhoneNumber = textBoxPhoneNumber.Text;
            attendee.EmailAddress = textBoxEmailAddress.Text;
            attendee.SaveAttendee();
            ClearForm();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            textBoxBarcode.Focus();
        }

        private void ClearForm()
        {
            textBoxBarcode.Clear();
            textBoxFirstName.Clear();
            textBoxSurname.Clear();
            textBoxJobTitle.Clear();
            textBoxTrust.Clear();
            textBoxInterests.Clear();
            checkBoxFollowUpCall.Checked = false;
            checkBoxFollowUpVisit.Checked = false;
            textBoxEmailAddress.Clear();
            textBoxPhoneNumber.Clear();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Return")
            {
                string barcode = textBoxBarcode.Text;
                if (textBoxBarcode.Text.Length == 22)
                {
                    barcode = textBoxBarcode.Text.Remove(0, 17);
                    barcode = barcode.Remove(barcode.Length - 1);
                }

                WristbandDataClassesDataContext context = new WristbandDataClassesDataContext();
                List<GetWristbandDataResult> wristbandData = context.GetWristbandData(barcode).ToList<GetWristbandDataResult>();

                if (wristbandData.Count == 1)
                {
                    textBoxFirstName.Text = wristbandData[0].FirstName;
                    textBoxSurname.Text = wristbandData[0].LastName;
                    textBoxJobTitle.Text = wristbandData[0].JobTitle;
                    textBoxTrust.Text = wristbandData[0].Trust;
                    textBoxInterests.Focus();
                }
                else
                {
                    textBoxBarcode.Text = barcode;
                    textBoxFirstName.Focus();
                }
            }
        }
    }

    public interface IAttendee
    {
        string GLN { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        string JobTitle { get; set; }
        string Trust { get; set; }
        string Interests { get; set; }
        bool FollowUpCall { get; set; }
        bool FollowUpVisit { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }

        void SaveAttendee();
    }

    public class Attendee : IAttendee
    {
        public string GLN { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string Trust { get; set; }
        public string Interests { get; set; }
        public bool FollowUpCall { get; set; }
        public bool FollowUpVisit { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public void SaveAttendee()
        {
            string csv = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}",
                GLN,
                FirstName,
                Surname,
                JobTitle,
                Trust,
                Interests,
                FollowUpCall.ToString(),
                FollowUpVisit.ToString(),
                PhoneNumber,
                EmailAddress,
                Environment.NewLine);

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\EventAttendees.csv";
            StreamWriter write = new StreamWriter(filePath, append: true);
            CsvWriter csw = new CsvWriter(write);
            write.Write(csv);
            write.Close();
        }
    }

}
