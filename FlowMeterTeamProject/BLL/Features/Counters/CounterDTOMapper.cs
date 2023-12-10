using System;
using System.Diagnostics.PerformanceData;
using DAL.Data;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    public class CounterEntity
    {
        public int CounterId { get; set; }
        public string Account { get; set; }
        public string TypeOfAccount { get; set; }
        public decimal? CurrentValue { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class CounterRecord
    {
        public int CounterId { get; set; }
        public string Account { get; set; }
        public string TypeOfAccount { get; set; }
        public decimal? CurrentValue { get; set; }
        public DateTime LastModified { get; set; }
    }
    class CounterDTOMapper
    {
        public static CounterRecord ToRecord(Counter c) {
            return new CounterRecord
            {
                CounterId = c.CountersId,
                Account = c.Account,
                TypeOfAccount = c.TypeOfAccount,
                LastModified = c.Date,
                CurrentValue = c.CurrentIndicator
            };
        }

        public static CounterEntity ToEntity(Counter c)
        {
            return new CounterEntity
            {
                CounterId = c.CountersId,
                Account = c.Account,
                TypeOfAccount = c.TypeOfAccount,
                LastModified = c.Date,
                CurrentValue = c.CurrentIndicator
            };
        }
    }
}
