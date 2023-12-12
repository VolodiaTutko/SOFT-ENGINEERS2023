using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;

using FlowMeterTeamProject.DAL.DataServices;

using FlowMeterTeamProject.BLL.Features.Consumers;
using System.Reflection;
using FlowMeterTeamProject.BLL.Features.Houses;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    class CounterCreation : CounterRecordAdding
    {
        override public List<string> GetOperableServices() {
            List<string> services = new List<string> { };
            if (consumer != null) {
                List<string> all = consumersInfo.GetConsumerServiceTypes(consumer);
                List<string> withCounters = consumersInfo.GetServiceTypesWithCounters(consumer);
                services = all.Except(withCounters).ToList();
            }
            return services;
        }

        override public List<string> GetUnoperableServices()
        {
            List<string> services = new List<string> { };
            if (consumer != null)
            {
                services = consumersInfo.GetServiceTypesWithCounters(consumer);
            }
            return services;
        }
    }
}
