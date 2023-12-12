using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    class CounterRecordUpdate : CounterRecordAdding
    {
        override public List<string> GetOperableServices()
        {
            List<string> services = new List<string> { };
            if (consumer != null)
            {
                services = consumersInfo.GetServiceTypesWithCounters(consumer);
            }
            return services;
        }

        override public List<string> GetUnoperableServices()
        {
            List<string> services = new List<string> { };
            if (consumer != null)
            {
                List<string> all = consumersInfo.GetConsumerServiceTypes(consumer);
                List<string> withCounters = consumersInfo.GetServiceTypesWithCounters(consumer);
                services = all.Except(withCounters).ToList();
            }
            return services;
        }
    }
}
