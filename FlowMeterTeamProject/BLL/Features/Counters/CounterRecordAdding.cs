using DAL.Data;
using FlowMeterTeamProject.BLL.Features.Consumers;
using FlowMeterTeamProject.BLL.Features.Houses;
using FlowMeterTeamProject.DAL.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.BLL.Features.Counters
{
    abstract class CounterRecordAdding
    {
        protected string consumerStr;
        protected string addressStr;
        protected Consumer consumer;
        protected string typeOfAccount;
        protected DateTime date;
        protected decimal indicator;

        protected ConsumersInfo consumersInfo;
        protected HouseService houseService;

        public CounterRecordAdding()
        {
            this.consumerStr = string.Empty;
            this.typeOfAccount = string.Empty;
            this.addressStr = string.Empty;
            this.consumersInfo = new ConsumersInfo();
            this.houseService = new HouseService();
        }
        public void SetHouse(string address)
        {
            this.addressStr = address;
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

        public List<string> GetAvailableHouses()
        {
            return houseService.GetAllHouseAddresses();
        }

        public List<string> GetAvailableConsumers()
        {
            List<Consumer> consumersPool;
            if (this.addressStr != string.Empty)
            {
                consumersPool = houseService.GetHouseConsumersByAddress(addressStr);
            }
            else
            {
                consumersPool = consumersInfo.GetAllConsumers();
            }

            List<string> available = new List<string>();
            foreach (var consumer in consumersPool)
            {
                available.Add(consumer.ConsumerOwner);
            }
            return available;
        }

        abstract public List<string> GetOperableServices();
        abstract public List<string> GetUnoperableServices();

        public Boolean IsReadyToAdd()
        {
            if (this.consumerStr == string.Empty
                || this.typeOfAccount == string.Empty
                || this.indicator == null
                || this.date == null
                || this.consumerStr == null
                || this.typeOfAccount == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add()
        {
            if (!IsReadyToAdd())
            {
                throw new Exception("not ready to add");
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
