using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAL.Data.DataMock
{
    static class Mock
    {
        public static bool checkIfDbConsumersEmpty () {
            using (var context = new AppDbContext())
            {
                return context.consumers.ToList().Count == 0 ? true : false;
            }

        }
        public static void FillRandomConsumersIntoDb(int count)
        {
            Consumer[] consumerData = GenerateRandomConsumers(count);

            using (var context = new AppDbContext())
            {
                foreach (var data in consumerData)
                {
                    context.consumers.Add(data);
                }
                context.SaveChanges();
            }
        }

        private static Consumer[] GenerateRandomConsumers(int count)
        {
            Random random = new Random();

            return Enumerable.Range(1, count)
                .Select(i => new Consumer
                {
                    PersonalAccount = GenerateRandomNumberString(),
                    Flat = random.Next(1, 100), 
                    ConsumerOwner = GenerateRandomString(),
                    HeatingArea = random.Next(50, 200)
                })
                .ToArray();
        }

        private static Account[] GenerateRandomAccounts(int count)
        {
            Random random = new Random();

            return Enumerable.Range(1, count)
                .Select(i => new Account
                {
                    PersonalAccount = GenerateRandomNumberString(),
                    //HotWater = (int)random.NextDouble() * 100,
                    //ColdWater = (int)random.NextDouble() * 100,
                    //Heating = (int)random.NextDouble() * 100,
                    //Electricity = (int)random.NextDouble() * 100,
                    //PublicService = (int)random.NextDouble() * 100
                })
                .ToArray();
        }

        private static string GenerateRandomNumberString()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999).ToString();
        }

        private static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}