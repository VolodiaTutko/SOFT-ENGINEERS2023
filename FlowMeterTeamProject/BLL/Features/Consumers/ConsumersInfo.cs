using FlowMeterTeamProject.BLL.Features.Counters;
using FlowMeterTeamProject.DAL.DataServices;
using System.Collections.Generic;

using DAL.Data;
using System.Reflection;

namespace FlowMeterTeamProject.BLL.Features.Consumers
{
    class ConsumersInfo
    {
        public List<Consumer> GetAllConsumers()
        {
            List<Consumer> consumers = ConsumerDataService.GetAllConsumerRecords();
            return consumers;
        }

        public Consumer GetConsumerByOwner(string owner)
        {
            Consumer consumer = ConsumerDataService.GetConsumerByOwner(owner);
            return consumer;
        }

        public List<string> GetConsumerServiceTypes(Consumer consumer) {
            List<string> types = new List<string>();
            Account account = ConsumerDataService.GetConsumerAccount(consumer);
            if (account == null) {
                return types;
            }
            PropertyInfo[] properties = account.GetType().GetProperties();
            foreach (PropertyInfo property in properties) {
                if (property.GetValue(account) != null &&
                    property.GetValue(account) != "0" &&
                    property.Name != "PersonalAccount"
                )
                {
                    //string value = (string)property.GetValue(account);
                    types.Add(property.Name);
                }
            }
            return types;
        }
    }
}
