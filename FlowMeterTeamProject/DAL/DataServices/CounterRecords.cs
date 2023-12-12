using DAL.Data;
using FlowMeterTeamProject.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowMeterTeamProject.DAL.DataServices
{
    class CounterRecords
    {
        public static List<Counter> GetAllCounterRecords()
        {
            using (var dbContext = new AppDbContext())
            {
                List<Counter> records = dbContext.counters.ToList();
                return records;
            }
        }
        public static List<Counter> GetDistinctCounterRecords(IEqualityComparer<Counter> comparer){
            using (var dbContext = new AppDbContext())
            {
                List<Counter> counterEntities = dbContext.counters.ToList()
                    .Distinct(comparer)
                    .ToList();

                return counterEntities;
            }
        }

        public static List<Counter> GetCounterEntities()
        {
            return GetDistinctCounterRecords(new CounterEntityComparer());
        }

        public static Counter FindAnyRecordByAccount(string account)
        {
            var comparer = new CounterEntityComparer();
            using (var dbContext = new AppDbContext())
            {
                Counter? record = dbContext.counters.First(c => c.Account == account);
                if (record == null) {
                    throw new Exception("found no record");
                }
                return record;
            }
        }

        public static Counter GetCounterLatestRecord(Counter record)
        {
            var comparer = new CounterEntityComparer();
            using (var dbContext = new AppDbContext())
            {
                Counter latestRecord = dbContext.counters.ToList()
                    .Where(x => comparer.Equals((Counter)x, record))
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault();

                return latestRecord;
            }
        }

        public static void AddNewCounter(decimal CurrentIndicator, string Account, string TypeOfAccount, DateTime Date) {
            DateTime UtcDate = DateTime.SpecifyKind(Date.Date, DateTimeKind.Utc);
            Counter counter = new Counter() { Account = Account, CurrentIndicator = CurrentIndicator, Date = UtcDate, TypeOfAccount = TypeOfAccount };
            using (var dbContext = new AppDbContext())
            {
                dbContext.counters.Add(counter);
                dbContext.SaveChanges();
            }
        }

        // todo t4: deleting
    }
}
