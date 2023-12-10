using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Data
{
    public class Account
    {
        [Key]
        public string PersonalAccount { get; set; }
        public string? HotWater { get; set; }
        public string? ColdWater { get; set; }
        public string? Heating { get; set; }
        public string? Electricity { get; set; }
        public string? Gas { get; set; }
        public string? PublicService { get; set; }
        
    }
}
