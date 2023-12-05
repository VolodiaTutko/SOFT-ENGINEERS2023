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
        public static bool checkIfDbAccountsEmpty () {
            using (var context = new AppDbContext())
            {
                return context.accounts.ToList().Count == 0 ? true : false;
            }

        }
        public static void FillRandomAccountsIntoDb(int count) {
            Account[] accountData = GenerateRandomAccounts(count);

            using (var context = new AppDbContext())
            {
                foreach (var data in accountData)
                {
                    context.accounts.Add(data);
                }
                context.SaveChanges();
            }
        }

        private static Account[] GenerateRandomAccounts(int count)
        {
            Random random = new Random();

            return Enumerable.Range(1, count)
                .Select(i => new Account
                {
                    PersonalAccount = GenerateRandomNumberString(),
                    HotWater = (int)random.NextDouble() * 100,
                    ColdWater = (int)random.NextDouble() * 100,
                    Heating = (int)random.NextDouble() * 100,
                    Electricity = (int)random.NextDouble() * 100,
                    PublicService = (int)random.NextDouble() * 100
                })
                .ToArray();
        }

        private static string GenerateRandomNumberString()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999).ToString();
        }
    }
}
