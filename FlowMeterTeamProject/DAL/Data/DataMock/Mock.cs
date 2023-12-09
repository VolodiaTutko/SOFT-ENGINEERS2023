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

        public static void FillHauseIntoDb()
        {
            House newHouse1 = new House
            {
                HouseAddress = "вул. Київська 27",
                HeatingAreaOfHouse = 150,
                NumberOfFlat = 5,
                NumberOfResidents = 27
            };
            House newHouse2 = new House
            {
                HouseAddress = "вул. Львівська 117",
                HeatingAreaOfHouse = 1200,
                NumberOfFlat = 30,
                NumberOfResidents = 400
            };
            House newHouse3 = new House
            {
                HouseAddress = "вул. Підвальна 13",
                HeatingAreaOfHouse = 2000,
                NumberOfFlat = 36,
                NumberOfResidents = 450
            };

            using (var dbContext = new AppDbContext())
            {
                // Додаємо новий об'єкт House до DbSet
                dbContext.houses.Add(newHouse1);
                dbContext.houses.Add(newHouse2);
                dbContext.houses.Add(newHouse3);


                // Зберігаємо зміни у базі даних
                dbContext.SaveChanges();
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