namespace FlowMeterTeamProject.Migrations
{
    using FlowMeterTeamProject.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FlowMeterTeamProject.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FlowMeterTeamProject.Models.MyDbContext context)
        {
            /*// Приклад додавання записів до таблиці Accounts
            context.Accounts.AddOrUpdate(
                new Account { personal_account = "12345", hot_water = 25, cold_water = 15, heating = 30 },
                new Account { personal_account = "67890", hot_water = 20, cold_water = 10, heating = 28 }
            );

            // Додайте інші записи тут для інших таблиць

            context.SaveChanges(); // Збереження змін у базі даних*/
        }
    }
}
