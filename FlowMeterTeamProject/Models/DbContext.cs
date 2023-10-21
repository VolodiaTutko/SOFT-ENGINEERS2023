using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics.Metrics;
using System.Security.Principal;
using System.Windows.Input;

namespace FlowMeterTeamProject.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Service> Services { get; set; }

    }
}
