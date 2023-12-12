using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Data;

namespace FlowMeterTeamProject.DAL.DataServices
{
    class HouseRepository
    {
        public List<House> GetAll() {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.houses.ToList();
            }
        }

        public House GetByAddress(string address) {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.houses.FirstOrDefault(h => h.HouseAddress == address);
            }
        }
    }
}
