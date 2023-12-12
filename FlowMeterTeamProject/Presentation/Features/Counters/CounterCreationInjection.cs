using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.Presentation.Features.Counters
{
    using FlowMeterTeamProject.BLL.Features.Counters;

    public enum CounterCreationType {
        Entity,
        Record
    }
    static class CounterCreationInjection
    {
        public static CounterRecordAdding GetInstanceOf(CounterCreationType type) {
            CounterRecordAdding adding = new CounterRecordUpdate();
            if (type == CounterCreationType.Record) {
                adding = new CounterRecordUpdate();
            } 
            else if (type == CounterCreationType.Entity)
            {
                adding = new CounterCreation();
            }

            return adding;
        }
    }
}
