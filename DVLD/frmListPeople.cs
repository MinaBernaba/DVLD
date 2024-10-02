using BusinessLogicOfDVLV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            DataTable dataTable = clsPerson.GetAllPeople();
            DataTable dt = dataTable.DefaultView.ToTable(false,"PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
            "DateOfBirth", "Gender", "Phone", "Email", "Nationality");
            dgvShowPeopleInSystem.DataSource = dt;
          
        }
    }
}
