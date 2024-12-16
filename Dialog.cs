using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TidsBok
{
    public partial class Dialog : Form
    {
        private string _machine;
        private DateTime _startDate;
        private DateTime _endDate;
        private List<string> _checkedPatients;

        public List<string> CheckedPatients
        {
            get { return _checkedPatients; }
            set { _checkedPatients = value; }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public string Machine
        {
            get { return _machine; }
            set { _machine = value; }
        }
        public Dialog()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            InitializeGUI();
        }
        private void InitializeGUI()
        {
            _checkedPatients = new List<string>();
            rbSB1.Checked = true;
            GetDates();
            cbMarkAll.Checked = true;
            UpdateCheckBox();
            CountCheckPatients();
        }


        private void GetDates()
        {
            this._startDate = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0); //formaterar datumet till starten av dagen
            this._endDate = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 0, 0, 0);//formaterar datumet till starten av dagen
        }

        private void CheckDateRange() //Ser till att slutdatumet inte sträcker sig längre än 5 dagar
        {
            TimeSpan ts = dtpEndDate.Value - dtpStartDate.Value;
            if (ts.Days > 5)
            {
                dtpEndDate.Value = dtpStartDate.Value.AddDays(4);
            }
            if (ts.Days < 0)
            {
                dtpStartDate.Value = dtpEndDate.Value;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            GetDates();
            UpdateMachine();
            this._checkedPatients = clbPatientList.CheckedItems.Cast<string>().ToList();
            this.Close();
        }

        private void UpdateMachine()
        {
            if (rbSB1.Checked)
            {
                this._machine = "Strålbehandling 1";
            }
            else
            {
                this._machine = "Strålbehandling 2";
            }
        }

        private void rbSB1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }

        private void rbSB2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }
        private void UpdateCheckBox()
        {
            GetDates();
            if (_startDate > new DateTime(1900, 1, 1) && _endDate > new DateTime(1900, 1, 1))
            {
                clbPatientList.Items.Clear();
                UpdateMachine();
                DataTable PatientList = AriaInterface.GetPatientList(_startDate.ToString(), _endDate.AddDays(1).ToString(), _machine);

                foreach (DataRow Row in PatientList.Rows)
                {
                    clbPatientList.Items.Add(Row[0]);
                }
                CheckAllPatients();
            }
        }

        private void cbMarkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckAllPatients();
        }

        private void CheckAllPatients()
        {
            if (cbMarkAll.Checked)
            {
                for (int i = 0; i < clbPatientList.Items.Count; i++)
                {
                    clbPatientList.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < clbPatientList.Items.Count; i++)
                {
                    clbPatientList.SetItemChecked(i, false);
                }
            }
            CountCheckPatients();
        }

        private void clbPatientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountCheckPatients();
        }

        private void CountCheckPatients()
        {
            int count = 0;

            foreach (object item in clbPatientList.CheckedItems)
            {
                // Increment count for each checked item
                count++;
            }
            lblNoOfSelectedPat.Text = $"{count}/{clbPatientList.Items.Count} Valda";
        }

        private void dtpStartDate_CloseUp(object sender, EventArgs e)
        {
            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
            CheckDateRange();
            UpdateCheckBox();
        }

        private void dtpEndDate_CloseUp(object sender, EventArgs e)
        {
            CheckDateRange();
            UpdateCheckBox();
        }
    }
}
