using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.DAL.DataServices
{
    class ConsumerDataService
    {
        public static List<Consumer> GetAllConsumerRecords()
        {
            using (var dbContext = new AppDbContext())
            {
                List<Consumer> consumers = dbContext.consumers.ToList();
                return consumers;
            }
        }

        public static Consumer GetConsumerByOwner(string owner)
        {
            using (var dbContext = new AppDbContext())
            {
                Consumer consumer = dbContext.consumers
                     .FirstOrDefault(a => a.ConsumerOwner == owner);
                return consumer;
            }
        }

        public static Account GetConsumerAccount(Consumer consumer)
        {
            if (consumer == null) {
                throw new ArgumentNullException();
            }
            using (var dbContext = new AppDbContext())
            {
                Account account = dbContext.accounts
                    .FirstOrDefault(a => a.PersonalAccount == consumer.PersonalAccount);
                return account;
            }
        }

        public static List<Consumer> GetConsumersByHouseId(int houseId)
        {
            using (var dbContext = new AppDbContext())
            {
                List<Consumer> consumers = dbContext.consumers.Where(c => c.HouseId == houseId).ToList();
                return consumers;
            }
        }
    }
}
