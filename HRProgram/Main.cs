using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRProgram
{
    public partial class Main : Form
    {
        private FileHelper<List<Employee>> _filehelper = new FileHelper<List<Employee>>(Program.FilePath);

        public Main()
        {
            InitializeComponent();
            cmbStatus.SelectedIndex = 0;
            RefreshEmployees();
        }

        private void RefreshEmployees()
        {
            var employees = _filehelper.DeserializeFromFile();
            if (cmbStatus.SelectedIndex == 0)
            {
                dgvEmployees.DataSource = employees;
            }
            else if (cmbStatus.SelectedIndex == 1)
            {
                dgvEmployees.DataSource = employees.Where(emp => (emp.FiringDate == null)).ToList();
            }
            else
            {
                dgvEmployees.DataSource = employees.Where(emp => (emp.FiringDate != null)).ToList();
            }

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditEmployee = new AddEditEmployee();
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }

        private void AddEditEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshEmployees();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select employee to edit their information.");
                return;
            }

            var addEditEmployee = new AddEditEmployee(Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value));
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }

        private void btnFire_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select employee to fire them.");
                return;
            }

            var fireEmployee = new FireEmployee(Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value));
            fireEmployee.FormClosing += FireEmployee_FormClosing;
            fireEmployee.ShowDialog();
        }

        private void FireEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshEmployees();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEmployees();
        }
    }
}
