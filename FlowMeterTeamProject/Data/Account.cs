using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlowMeterTeamProject.Data
{
    public class Account
    {
        [Key]
        public string PersonalAccount { get; set; }
        public decimal? HotWater { get; set; }
        public decimal? ColdWater { get; set; }
        public decimal? Heating { get; set; }
        public decimal? Electricity { get; set; }
        public decimal? PublicService { get; set; }
    }
}
