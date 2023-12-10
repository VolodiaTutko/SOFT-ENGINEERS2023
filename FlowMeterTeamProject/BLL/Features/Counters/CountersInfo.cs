using DAL.Data;
using System.Collections.Generic;
using FlowMeterTeamProject.DAL.DataServices;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    class CountersInfo
    {
        public List<CounterEntity> GetCounterEntities () {
            List<Counter> counters = CounterRecords.GetCounterEntities();
            List<CounterEntity> entitiesWithLatestIndicators = new List<CounterEntity> { };
            foreach (var counter in counters)
            {
                Counter latest = CounterRecords.GetCounterLatestRecord(counter);
                entitiesWithLatestIndicators.Add(CounterDTOMapper.ToEntity(counter));
            }
            return entitiesWithLatestIndicators;
        }

        public List<CounterRecord> GetCounterRecords()
        {
            List<Counter> counters = CounterRecords.GetAllCounterRecords();
            List<CounterRecord> records = new List<CounterRecord> { };
            foreach (var counter in counters)
            {
                records.Add(CounterDTOMapper.ToRecord(counter));
            }
            return records;
        }

    }
}
