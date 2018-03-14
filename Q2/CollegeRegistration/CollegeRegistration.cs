using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

namespace Q2
{
    public partial class CollegeRegistration : Form
    {
        readonly int FirstNameMinLength = 2;
        readonly int SurnameMinLength = 2;

        static readonly string FILE_PATH = @"C:\AdvProg\registration.bin";

        string ConfIDRegEx;

        public CollegeRegistration()
        {
            InitializeComponent();

            this.Text = "Conference Registration";
            ConfIDRegEx = ConfigurationManager.AppSettings["ConfID"];
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(!ValidateConfIDField())
            {
                MessageBox.Show("Conference ID field is invalid. Registration Failed");
                txtConfID.Focus();
            }
            else
            {
                // validate firstname and surname are both 2 chars+ in length
                bool fNameValid = ValidateFirstName(), surnameValid = ValidateSurname();

                if(!fNameValid || !surnameValid)
                {
                    string msg;
                    if(!fNameValid && !surnameValid)
                    {
                        msg = "Firstname and surname are invalid";
                        txtSurname.Focus();
                    }
                    else if(!fNameValid)
                    {
                        msg = "Firstname is invalid";
                        txtFName.Focus();
                    }
                    else
                    {
                        msg = "Surname is invalid";
                        txtSurname.Focus();
                    }

                    MessageBox.Show(msg);
                }
                else
                {
                    SaveRegistrationInfo();
                }
            }
        }

        private bool ValidateSurname()
        {
            return txtSurname.Text.Length >= SurnameMinLength;
        }

        private bool ValidateFirstName()
        {
            return txtFName.Text.Length >= FirstNameMinLength;
        }

        private void SaveRegistrationInfo()
        {
            using (Stream fs = File.Open(FILE_PATH, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    try
                    {
                        writer.Write(txtFName.Text);
                        writer.Write(txtSurname.Text);
                        writer.Write(txtOrg.Text);
                        writer.Write(txtConfID.Text);
                        MessageBox.Show("Binary file created and written to successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private bool ValidateConfIDField()
        {
            bool confIDIsValid = false;

            string confID = txtConfID.Text;

            if (Regex.IsMatch(confID, ConfIDRegEx))
            {
                confIDIsValid = true;
            }

            return confIDIsValid;
        }
    }
}

