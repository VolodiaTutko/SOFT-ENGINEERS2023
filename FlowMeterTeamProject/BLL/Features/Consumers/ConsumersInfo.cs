using FlowMeterTeamProject.BLL.Features.Counters;
using FlowMeterTeamProject.DAL.DataServices;
using System.Collections.Generic;

using DAL.Data;
using System.Reflection;
using System;

namespace FlowMeterTeamProject.BLL.Features.Consumers
{
    class ConsumersInfo
    {
        public List<Consumer> GetAllConsumers()
        {
            List<Consumer> consumers = ConsumerDataService.GetAllConsumerRecords();
            return consumers;
        }

        public List<Consumer> GetConsumersByHouse(int id)
        {
            List<Consumer> consumers = ConsumerDataService.GetConsumersByHouseId(id);
            return consumers;
        }

        public Consumer GetConsumerByOwner(string owner)
        {
            Consumer consumer = ConsumerDataService.GetConsumerByOwner(owner);
            return consumer;
        }

        public List<string> GetServiceTypesWithCounters(Consumer consumer)
        {
            List<string> types = new List<string>();
            Account account = ConsumerDataService.GetConsumerAccount(consumer);
            if (account == null)
            {
                return types;
            }
            PropertyInfo[] subAccountTypes = account.GetType().GetProperties();
            foreach (PropertyInfo type in subAccountTypes)
            {
                if (type.GetValue(account) != null &&
                    type.GetValue(account) != "0" &&
                    type.Name != "PersonalAccount"
                )
                {
                    try {
                        string accountStr = (string)type.GetValue(account);
                        CounterRecords.FindAnyRecordByAccount(accountStr);
                        types.Add(type.Name);
                    } catch
                    {
                        continue;
                    }
                }
            }
            return types;
        }

        public List<string> GetConsumerServiceTypes(Consumer consumer) {
            List<string> types = new List<string>();
            Account account = ConsumerDataService.GetConsumerAccount(consumer);
            if (account == null) {
                return types;
            }
            PropertyInfo[] subAccountTypes = account.GetType().GetProperties();
            foreach (PropertyInfo type in subAccountTypes) {
                if (type.GetValue(account) != null &&
                    type.GetValue(account) != "0" &&
                    type.Name != "PersonalAccount"
                )
                {
                    types.Add(type.Name);
                }
            }
            return types;
        }
    }
}
