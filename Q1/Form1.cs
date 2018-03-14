using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Q1
{
    public partial class Form1 : Form
    {
        List<GasMeter> GasMeterRecords;
        List<Account> AccountRecords;

        public Form1()
        {
            InitializeComponent();

            GasMeterRecords = new List<GasMeter>() {
                new GasMeter
                {
                    MetreID = 100,
                    VolumeUsed = 6805,
                    HasBeenServiced = true,
                    OwnerAccountID = 50001
                },
                new GasMeter
                {
                    MetreID = 101,
                    VolumeUsed = 13609,
                    HasBeenServiced = true,
                    OwnerAccountID = 50002
                },
                new GasMeter
                {
                    MetreID = 102,
                    VolumeUsed = 28115,
                    HasBeenServiced = false,
                    OwnerAccountID = 50003
                },
                new GasMeter
                {
                    MetreID = 103,
                    VolumeUsed = 32191,
                    HasBeenServiced = false,
                    OwnerAccountID = 50004
                },
                new GasMeter
                {
                    MetreID = 104,
                    VolumeUsed = 19100,
                    HasBeenServiced = false,
                    OwnerAccountID = 50005
                }
            };

            AccountRecords = new List<Account>()
            {
                new Account {AccountID = 50001, EirCode = "DV4R102", PaymentPeriod = PaymentPeriod.BiAnnual, ArrearsCount = 2 },
                new Account {AccountID = 50002, EirCode = "DV4R103", PaymentPeriod = PaymentPeriod.Annual, ArrearsCount = 2},
                new Account {AccountID = 50003, EirCode = "DV4R104", PaymentPeriod = PaymentPeriod.Monthly, ArrearsCount = 1},
                new Account {AccountID = 50004, EirCode = "DV4S101", PaymentPeriod = PaymentPeriod.Quaterly, ArrearsCount = 0},
                new Account {AccountID = 50005, EirCode = "DV4S102", PaymentPeriod = PaymentPeriod.Annual, ArrearsCount = 0},
                new Account {AccountID = 50006, EirCode = "DV4S103", PaymentPeriod = PaymentPeriod.BiAnnual, ArrearsCount = 0}
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvGasMeters.DataSource = (from waterMeter in GasMeterRecords
                                         select waterMeter).ToList();

            dgvAccount.DataSource = null;
        }

        private void dgvGasMeters_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedAccountID = Convert.ToInt32(dgvGasMeters.Rows[e.RowIndex].Cells[3].Value);

                dgvAccount.DataSource = (from account in AccountRecords
                                         where account.AccountID == selectedAccountID
                                         select account).ToList();
            }
        }

        private void rbMetreID_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMetreID.Checked)
            {
                dgvGasMeters.DataSource = (from gasMeter in GasMeterRecords
                                             orderby gasMeter.MetreID
                                             select gasMeter).ToList();

                dgvAccount.DataSource = null;
            }
        }

        private void rbVolumeUsed_CheckedChanged(object sender, EventArgs e)
        {
            dgvGasMeters.DataSource = (from gasMeter in GasMeterRecords
                                         orderby gasMeter.VolumeUsed
                                         select gasMeter).ToList();

            dgvAccount.DataSource = null;
        }

        private void btnCustomerArrears_Click(object sender, EventArgs e)
        {
            using (CustomerArrearsReport custAR = new CustomerArrearsReport(AccountRecords))
            {
                custAR.ShowDialog();
            }
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            int totalVolGasUsed = (from gasMeter in GasMeterRecords
                                     select gasMeter.VolumeUsed).Sum();

            int totalAccountsInArrears = (from account in AccountRecords
                                          where account.ArrearsCount > 0
                                          select account.ArrearsCount).Count();

            string msg = $"Total volume of gas used = {totalVolGasUsed} & total number of accounts in arrears is {totalAccountsInArrears}";
            MessageBox.Show(msg);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvWaterMeters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}