namespace DAL.Data.DataMock
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;

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
                NumberOfResidents = 27,
            };
            House newHouse2 = new House
            {
                HouseAddress = "вул. Львівська 117",
                HeatingAreaOfHouse = 1200,
                NumberOfFlat = 30,
                NumberOfResidents = 400,
            };
            House newHouse3 = new House
            {
                HouseAddress = "вул. Підвальна 13",
                HeatingAreaOfHouse = 2000,
                NumberOfFlat = 36,
                NumberOfResidents = 450,
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

        public static void FillConsumersIntoDb()
        {
            Consumer newConsumer1 = new Consumer
            {
                PersonalAccount = "1000000000",
                Flat = 1,
                ConsumerOwner = "Боровець Роман Назарович",
                HeatingArea = 267,
                HouseId = 1,
                NumberOfPersons = 100,
            };
            Consumer newConsumer2 = new Consumer
            {
                PersonalAccount = "1000000007",
                Flat = 2,
                ConsumerOwner = "Боровець Роман Назарович",
                HeatingArea = 87,
                HouseId = 2,
                NumberOfPersons = 150,
            };
            Consumer newConsumer3 = new Consumer
            {
                PersonalAccount = "10000000014",
                Flat = 3,
                ConsumerOwner = "Тутко Володимир Григорович",
                HeatingArea = 110,
                HouseId = 3,
                NumberOfPersons = 85,
            };

            using (var dbContext = new AppDbContext())
            {
                // Додаємо новий об'єкт House до DbSet
                dbContext.consumers.Add(newConsumer1);
                dbContext.consumers.Add(newConsumer2);
                dbContext.consumers.Add(newConsumer3);

                // Зберігаємо зміни у базі даних
                dbContext.SaveChanges();
            }
        }

        public static void FillAccountIntoDb()
        {
            Account newAccount1 = new Account
            {
                PersonalAccount = "1000000000",
                ColdWater = "1000000001",
                HotWater = "1000000002",
                Electricity = "10000000003",
                Heating = "1000000004",
                PublicService = "1000000005",

            };
            Account newAccount2 = new Account
            {
                PersonalAccount = "1000000007",
                ColdWater = "1000000008",
                HotWater = "1000000009",
                Electricity = "10000000010",
                Heating = "1000000011",
                PublicService = "1000000012",
            };
            Account newAccount3 = new Account
            {
                PersonalAccount = "10000000014",
                ColdWater = "1000000015",
                HotWater = "1000000016",
                Electricity = "10000000017",
                Heating = "1000000018",
                PublicService = "1000000019",
            };

            using (var dbContext = new AppDbContext())
            {
                // Додаємо новий об'єкт House до DbSet
                dbContext.accounts.Add(newAccount1);
                dbContext.accounts.Add(newAccount2);
                dbContext.accounts.Add(newAccount3);

                // Зберігаємо зміни у базі даних
                dbContext.SaveChanges();
            }
        }

        public static void FillCounterIntoDb()
        {
            Counter newCounter1 = new Counter
            {
                Account = "1000000001",
                TypeOfAccount = "ColdWater",
                CurrentIndicator = 0,
                Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter2 = new Counter
            {
                Account = "1000000002",
                TypeOfAccount = "HotWater",
                CurrentIndicator = 0,
                Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter3 = new Counter
            {
                Account = "1000000001",
                TypeOfAccount = "ColdWater",
                CurrentIndicator = 125,
                Date = new DateTime(2023, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter4 = new Counter
            {
                Account = "1000000002",
                TypeOfAccount = "HotWater",
                CurrentIndicator = 75,
                Date = new DateTime(2023, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter5 = new Counter
            {
                Account = "10000000003",
                TypeOfAccount = "Electricity",
                CurrentIndicator = 0,
                Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter6 = new Counter
            {
                Account = "1000000004",
                TypeOfAccount = "Heating",
                CurrentIndicator = 0,
                Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter7 = new Counter
            {
                Account = "10000000003",
                TypeOfAccount = "Electricity",
                CurrentIndicator = 175,
                Date = new DateTime(2023, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter8 = new Counter
            {
                Account = "1000000004",
                TypeOfAccount = "Heating",
                CurrentIndicator = 2000,
                Date = new DateTime(2023, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter9 = new Counter
            {
                Account = "1000000006",
                TypeOfAccount = "Kerosene",
                CurrentIndicator = 0,
                Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            Counter newCounter10 = new Counter
            {
                Account = "1000000006",
                TypeOfAccount = "Kerosene",
                CurrentIndicator = 7,
                Date = new DateTime(2023, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.counters.Add(newCounter1);
                dbContext.counters.Add(newCounter2);
                dbContext.counters.Add(newCounter3);
                dbContext.counters.Add(newCounter4);
                dbContext.counters.Add(newCounter5);
                dbContext.counters.Add(newCounter6);
                dbContext.counters.Add(newCounter7);
                dbContext.counters.Add(newCounter8);

                // dbContext.counters.Add(newCounter9);
                // dbContext.counters.Add(newCounter10);

                // Зберігаємо зміни у базі даних
                dbContext.SaveChanges();
            }
        }

        public static void FillServicesIntoDb()
        {
            Service newService1 = new Service
            {
                HouseId = 1,
                TypeOfAccount = "ColdWater",
                Price = 7,
            };
            Service newService2 = new Service
            {
                HouseId = 1,
                TypeOfAccount = "HotWater",
                Price = 15,
            };
            Service newService3 = new Service
            {
                HouseId = 1,
                TypeOfAccount = "Electricity",
                Price = 3,
            };
            Service newService4 = new Service
            {
                HouseId = 1,
                TypeOfAccount = "Heating",
                Price = 2,
            };
            Service newService5 = new Service
            {
                HouseId = 1,
                TypeOfAccount = "PublicService",
                Price = 300,
            };
            using (var dbContext = new AppDbContext())
            {
                // Додаємо новий об'єкт House до DbSet
                dbContext.services.Add(newService1);
                dbContext.services.Add(newService2);
                dbContext.services.Add(newService3);
                dbContext.services.Add(newService4);
                dbContext.services.Add(newService5);

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
                    HeatingArea = random.Next(50, 200),
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

                    // HotWater = (int)random.NextDouble() * 100,
                    // ColdWater = (int)random.NextDouble() * 100,
                    // Heating = (int)random.NextDouble() * 100,
                    // Electricity = (int)random.NextDouble() * 100,
                    // PublicService = (int)random.NextDouble() * 100
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