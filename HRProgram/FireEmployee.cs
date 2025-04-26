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
    public partial class FireEmployee : Form
    {

        private int _empId;
        private Employee _emp;
        private FileHelper<List<Employee>> _filehelper = new FileHelper<List<Employee>>(Program.FilePath);

        public FireEmployee(int id)
        {
            InitializeComponent();
            _empId = id;
            GetEmployeeData();
        }

        private void GetEmployeeData()
        {
            Text = "Termination";

            var employees = _filehelper.DeserializeFromFile();
            _emp = employees.FirstOrDefault(x => x.Id == _empId);

            if (_emp == null)
            {
                throw new Exception("No user found of this ID");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var employees = _filehelper.DeserializeFromFile();

            employees.RemoveAll(x => x.Id == _empId);

            _emp.FiringDate = mcFireDate.SelectionRange.Start.Date;

            employees.Add(_emp);
            _filehelper.SerializeToFile(employees);
            Close();
        }
    }
}
