using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeLogin { get; set; }
        public string EmployeePassword { get; set; }
        public string TypeOfUser { get; set; }
    }
}
