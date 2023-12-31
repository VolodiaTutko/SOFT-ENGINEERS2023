﻿namespace DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Counter
    {
        [Key]
        public int CountersId { get; set; }

        public decimal? CurrentIndicator { get; set; }

        public string Account { get; set; }

        public string TypeOfAccount { get; set; }

        public DateTime Date { get; set; }
    }
}
