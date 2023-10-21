using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public int? HouseId { get; set; }
        public string TypeOfAccount { get; set; }
        public int? Price { get; set; }
    }
}
