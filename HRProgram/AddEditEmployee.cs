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
    public partial class AddEditEmployee : Form
    {
        private int _empId;
        private Employee _emp;
        private bool correctFormatCheck = false;
        private FileHelper<List<Employee>> _filehelper = new FileHelper<List<Employee>>(Program.FilePath);

        public AddEditEmployee(int id = 0)
        {
            InitializeComponent();
            _empId = id;

            GetEmployeeData();
            tbFirstName.Select();
        }

        private void GetEmployeeData()
        {
            if (_empId != 0)
            {
                Text = "Editing";

                var employees = _filehelper.DeserializeFromFile();
                _emp = employees.FirstOrDefault(x => x.Id == _empId);

                if (_emp == null)
                {
                    throw new Exception("No user found of this ID");
                }

                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbId.Text = _emp.Id.ToString();
            tbFirstName.Text = _emp.FirstName;
            tbLastName.Text = _emp.LastName;
            rtbComments.Text = _emp.Comments;
            tbHireDate.Text = _emp.HiringDate.ToString();
            tbFireDate.Text = _emp.FiringDate.ToString();
            tbSalary.Text = _emp.Salary.ToString();
        }

        private void AddNewUserToList(List<Employee> employees)
        {
            var emp = new Employee
            {
                Id = _empId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                Salary = Decimal.Parse(tbSalary.Text),
                HiringDate = DateTime.Parse(tbHireDate.Text),
                FiringDate = ParseFiringDate(tbFireDate.Text),
            };

            employees.Add(emp);
        }

        private bool CheckSalary(string text)
        {
            var correctFormat = Decimal.TryParse(text, out decimal salary);
            if (!correctFormat)
            {
                MessageBox.Show("Salary is in wrong format");
            }
            return correctFormat;
        }
        private bool CheckHiringDate(string text)
        {
            var correctFormat = DateTime.TryParse(text, out DateTime date);
            if (!correctFormat)
            {
                MessageBox.Show("Date of hiring is in wrong format.");
            }
            return correctFormat;
        }

        private bool CheckFiringDate(string text)
        {
            var correctFormat = DateTime.TryParse(text, out DateTime date);
            if (text == "")
                correctFormat = true;

            if (!correctFormat)
            {
                MessageBox.Show("Date of firing is in wrong format.");
            }
            return correctFormat;
        }

        private DateTime? ParseFiringDate(string text)
        {
            if (text == "")
                return null;

            return DateTime.Parse(text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            correctFormatCheck = (CheckSalary(tbSalary.Text) && CheckHiringDate(tbHireDate.Text) && CheckFiringDate(tbFireDate.Text));
            if (correctFormatCheck)
            {
                var employees = _filehelper.DeserializeFromFile();

                if (_empId != 0)
                {
                    employees.RemoveAll(x => x.Id == _empId);
                }
                else
                {
                    AssignIdToNewEmployee(employees);
                }

                AddNewUserToList(employees);
                _filehelper.SerializeToFile(employees);

                Close();
            }
        }

        private void AssignIdToNewEmployee(List<Employee> employees)
        {
            var empWithHighestId = employees.OrderByDescending(x => x.Id).FirstOrDefault();
            _empId = empWithHighestId == null ? 1 : empWithHighestId.Id + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
