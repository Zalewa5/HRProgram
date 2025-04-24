using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram
{
    internal class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime? FiringDate { get; set; }
        public int WorkerId { get; set; }
        public string Comments { get; set; }
        public decimal Salary { get; set; }
    }
}
