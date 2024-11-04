using BusinessLogicOfDVLD;
using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew;
        private int _LDLA_ID;
        private clsLocalDrivingLicenseApplication _LDLA;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
             Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LDLA_ID)
        {
            InitializeComponent();
            _LDLA_ID = LDLA_ID;
            Mode = enMode.Update;
        }
        private void _FillcbLicenseClasses()
        {
            DataTable dataTable = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow row in dataTable.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }
        private void _Reset()
        {
            lblShowingAppDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            _FillcbLicenseClasses();
            cbLicenseClasses.SelectedIndex = 2;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        _LDLA = new clsLocalDrivingLicenseApplication();
                        this.Text = "Add New Local Driving Licesnse Application";
                        lblTitle.Text = "Add New Local Driving Licesnse Application";
                        ctrlPersonCardWithFilter.EnableFilter = true;
                        tcApplication.Enabled = false;
                        btnSave.Enabled = false;
                        return;
                    }
                    case enMode.Update: 
                    {
                        this.Text = "Update Local Driving Licesnse Application";
                        lblTitle.Text = "Update Local Driving Licesnse Application";
                        ctrlPersonCardWithFilter.EnableFilter = false;
                        tcApplication.Enabled = true;
                        btnSave.Enabled = true;
                        return;

                    }
            } 
        }
        private void _LoadData()
        {
            _LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLA_ID);
            if (_LDLA == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + _LDLA_ID + " !",
                    "Local Driving License Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter.LoadPersonInfo(_LDLA.PersonInfo.PersonID);
            lblShowingAppID.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblShowingAppDate.Text = _LDLA.ApplicationDate.ToString("dd/MM/yyyy");
            cbLicenseClasses.SelectedItem = cbLicenseClasses.FindString(
                clsLicenseClass.Find(_LDLA.LicenseClassID).ClassName);
            lblApplicationFees.Text = _LDLA.PaidFees.ToString();
            lblCreatedByUserName.Text = _LDLA.CreatedByUserID.ToString();
        }
        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _Reset();
            if (Mode == enMode.Update) _LoadData();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if ( Mode == enMode.Update || ctrlPersonCardWithFilter.SelectedPerson != null)
            {
                tcApplication.Enabled = true;
                tabControl.SelectedIndex = 1;
                btnSave.Enabled = true;
            }
            else MessageBox.Show("Please select an applicant!", "No Applicant selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            byte LicenseClassID = (byte)clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            // Check if the applicant has an active Application

            int ActiveApplicationID = clsApplication.DoesApplicantHaveAnActiveLocalApplicationforTheSelectedLicenseClass(
                ctrlPersonCardWithFilter.PeronID,
                clsLocalDrivingLicenseApplication.enApplicationType.NewDrivingLicense
                , LicenseClassID);


            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another license class, The selected applicant already has" +
                    " an active Application for the selected license class with ID = " + ActiveApplicationID
                    , "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }



            // Check if the applicant has already a license

            if (clsLicense.DoesApplicantAlreadyHaveALicenseInTheSameLicenseClass(
                ctrlPersonCardWithFilter.PeronID, LicenseClassID))
            {
                MessageBox.Show("The applicant already has a license with the same applied license" +
                    " class, Choose different license class!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        _LDLA.LicenseClassID = LicenseClassID;

                        _LDLA.ApplicantPersonID = ctrlPersonCardWithFilter.PeronID;
                        _LDLA.ApplicationDate = DateTime.Now;
                        _LDLA.ApplicationTypeID = 1;
                        _LDLA.ApplicationStatus = clsApplication.enApplicationStatus.New;
                        _LDLA.LastStatusDate = DateTime.Now;
                        _LDLA.PaidFees =Convert.ToDecimal(lblApplicationFees.Text);
                        _LDLA.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                        if (_LDLA.Save())
                        {
                            this.Text = "Update Local Driving Licesnse Application";
                            lblTitle.Text = "Update Local Driving Licesnse Application";
                            Mode = enMode.Update;

                            lblShowingAppID.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
                            MessageBox.Show($"Local Driving License Application added successfully with ID : {_LDLA.LocalDrivingLicenseApplicationID} !",
                                "Added successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Adding failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                case enMode.Update:
                    {

                        _LDLA.LicenseClassID = LicenseClassID;
                        if (_LDLA.Save())
                        {
                            MessageBox.Show($"Local Driving License Application Updated successfully !",
                                "Updated successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Updated failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
