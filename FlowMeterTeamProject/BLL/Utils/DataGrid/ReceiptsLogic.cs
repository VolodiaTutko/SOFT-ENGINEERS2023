﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Data.DataMock;
using Microsoft.EntityFrameworkCore.Storage;
using Presentation.Pages;

namespace FlowMeterTeamProject.BLL.Utils.DataGrid
{

    public static class ReceiptsLogic
    {
        public static Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price, decimal Tariff)>> GetNonZeroServices(string personalAccount, string houseAddres)
        {
            using (var dbContext = new AppDbContext())
            {
                var account = dbContext.accounts.FirstOrDefault(a => a.PersonalAccount == personalAccount);

                if (account != null)

                {
                    int hauseId = FindHouseId(houseAddres);
                    // Створюємо список послуг, де значення не дорівнює 0
                    var nonZeroServices = new List<(string ServiceName, string AccountNumber, decimal Price, decimal Tariff)>();

                    if (account.HotWater != "0")
                    {
                        var price = preperetiv_receipt(hauseId, account.HotWater, "HotWater");
                        nonZeroServices.Add(("HotWater", account.HotWater, price[0], price[1]));
                    }

                    if (account.ColdWater != "0")
                    {
                        var price = preperetiv_receipt(hauseId, account.ColdWater, "ColdWater");
                        nonZeroServices.Add(("ColdWater", account.ColdWater, price[0], price[1]));
                    }

                    if (account.Heating != "0")
                    {
                        var price = preperetiv_receipt(hauseId, account.Heating, "Heating");
                        nonZeroServices.Add(("Heating", account.Heating, price[0], price[1]));
                    }

                    if (account.Electricity != "0")
                    {
                        var price = preperetiv_receipt(hauseId, account.Electricity, "Electricity");
                        nonZeroServices.Add(("Electricity", account.Electricity, price[0], price[1]));
                    }

                    if (account.PublicService != "0")
                    {
                        var price = preperetiv_receipt(hauseId, account.PublicService, "PublicService");
                        nonZeroServices.Add(("PublicService", account.PublicService, price[0], price[1]));
                    }

                    var result = new Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price, decimal Tariff)>>();
                    result.Add(personalAccount, nonZeroServices);

                    return result;
                }

                return new Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price, decimal Tariff)>>();
            }
        }

        public static decimal[] preperetiv_receipt(int idHause, string personalAccount, string typeServices)
        {
            using (var dbContext = new AppDbContext())
            {
                if (typeServices != "PublicService")
                {
                    var query = from counter in dbContext.counters
                                where counter.Account == personalAccount && counter.TypeOfAccount == typeServices
                                orderby counter.Date descending
                                select counter;


                    var result = query.Take(2).ToList();

                    // result[0] - перший запис
                    IQueryable<int?> query1 = from service in dbContext.services
                                              where service.HouseId == idHause && service.TypeOfAccount == typeServices
                                              select service.Price;
                    if (query.Count() == 2)
                    {
                        var currentIndicator = result[0].CurrentIndicator;
                        var previousCurrentIndicator = result[1].CurrentIndicator;



                        int? firstValue = query1.FirstOrDefault();
                        decimal decimalValue = firstValue.HasValue ? (decimal)firstValue.Value : 0m;

                        var receipt = (currentIndicator.GetValueOrDefault() - previousCurrentIndicator.GetValueOrDefault()) * decimalValue;

                        return new decimal[] { receipt, decimalValue };
                    }
                    else if (query.Count() == 1)
                    {
                        if (result[0].CurrentIndicator == 0)
                        {
                            return new decimal[] { 0, 0 };
                        }
                        else
                        {
                            var currentIndicator = result[0].CurrentIndicator;
                            int? firstValue = query1.FirstOrDefault();
                            decimal decimalValue = firstValue.HasValue ? (decimal)firstValue.Value : 0m;

                            var receipt = currentIndicator.GetValueOrDefault() * decimalValue;

                            return new decimal[] { receipt, decimalValue };
                        }
                    }
                    else if (query.Count() == 0) { return new decimal[] { 0, 0 }; }
                }
                else
                {
                    IQueryable<int?> query1 = from service in dbContext.services
                                              where service.HouseId == idHause && service.TypeOfAccount == typeServices
                                              select service.Price;
                    int? firstValue = query1.FirstOrDefault();
                    decimal decimalValue = firstValue.HasValue ? (decimal)firstValue.Value : 0m;
                    return new decimal[] { decimalValue, decimalValue };
                }

                return new decimal[] { 0, 0 };
            }
        }

        public static int FindHouseId(string address)
        {
            using (var dbContext = new AppDbContext())
            {
                var query = from house in dbContext.houses
                            where house.HouseAddress == address
                            select house.HouseId;
                return query.SingleOrDefault();
            }
        }
    }
}
