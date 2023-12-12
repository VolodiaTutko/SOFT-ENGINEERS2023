using FlowMeterTeamProject.DAL.DataServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using FlowMeterTeamProject.BLL.Features.Consumers;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace FlowMeterTeamProject.BLL.Features.Houses
{
    class HouseService
    {
        private readonly HouseRepository _houseRepository;

        public HouseService() {
            _houseRepository = new HouseRepository();
        }

        public List<string> GetAllHouseAddresses()
        {
            return _houseRepository.GetAll().Select(h => h.HouseAddress).ToList();
        }

        public List<Consumer> GetHouseConsumersByAddress(string address)
        {
            int houseId = _houseRepository.GetByAddress(address).HouseId;
            return ConsumerDataService.GetConsumersByHouseId(houseId);
        }
    }
}
