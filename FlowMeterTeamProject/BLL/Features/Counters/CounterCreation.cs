using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;

using FlowMeterTeamProject.DAL.DataServices;

using FlowMeterTeamProject.BLL.Features.Consumers;
using System.Reflection;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    class CounterCreation
    {
        private string consumerStr;
        private Consumer consumer;
        private string typeOfAccount;
        private DateTime date;
        private decimal indicator;
        private ConsumersInfo consumersInfo;

        public CounterCreation() {
            this.consumerStr = string.Empty;
            this.typeOfAccount = string.Empty;
            this.consumersInfo = new ConsumersInfo();
        }

        public void SetConsumer(string consumer)
        {
            this.consumerStr = consumer;
            this.consumer = consumersInfo.GetConsumerByOwner(consumer);
        }

        public void SetTypeOfAccount(string type)
        {
            this.typeOfAccount = type;
        }

        public void SetDate(DateTime dt)
        {
            this.date = dt;
        }

        public void SetIndicator(decimal indicator)
        {
            this.indicator = indicator;
        }

        public List<string> GetAvailableConsumers()
        {
            //todo: get all consumers
            List<string> available = new List<string>();
            foreach (var consumer in consumersInfo.GetAllConsumers()) {
                available.Add(consumer.ConsumerOwner);
            }
            return available;
        }

        public List<string> GetConsumerServices() {
            return consumersInfo.GetConsumerServiceTypes(consumer);
        }

        public Boolean IsReadyToCreate() {
            if (this.consumerStr == string.Empty
                || this.typeOfAccount == string.Empty
                || this.indicator == null
                || this.date == null) {
                return false;
            } else
            {
                return true;
            }
        }

        public void Create() {
            if (!IsReadyToCreate())
            {
                throw new Exception("not ready to create");
            }
            else
            {
                var account = ConsumerDataService.GetConsumerAccount(consumer);
                PropertyInfo propertyInfo = account.GetType().GetProperty(typeOfAccount);
                string ServiceTypeAccount = (string)propertyInfo.GetValue(account);
                CounterRecords.AddNewCounter(indicator, ServiceTypeAccount, typeOfAccount, date);
            }
        }
    }
}
