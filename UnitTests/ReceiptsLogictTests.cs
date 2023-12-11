using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;
using BLL.Utils.DataGrid;
using FlowMeterTeamProject.BLL.Utils.DataGrid;
using System.Diagnostics;
using DAL.Data;

namespace UnitTests
{
    [TestClass]
    public class ReceiptsLogicTests
    {
        [TestMethod]
        public void GetNonZeroServices_ShouldReturnNonZeroServices()
        {
            // Arrange
            string personalAccount = "1000000000";
            string houseAddress = "вул. Київська 27";

            // Act
            var result = ReceiptsLogic.GetNonZeroServices(personalAccount, houseAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey(personalAccount));
            Assert.IsTrue(result[personalAccount].Count > 0);
        }

        [TestMethod]
        public void preperetiv_receipt_ShouldCalculateReceipt()
        {
            // Arrange
            int houseId = 1;
            string personalAccount = "testAccount";
            string typeServices = "Electricity";

            // Act
            var result = ReceiptsLogic.preperetiv_receipt(houseId, personalAccount, typeServices);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void preperetiv_receipt_ShouldReturnZeroForNoCounters()
        {
            // Arrange
            int houseId = 1;
            string personalAccount = "testAccount";
            string typeServices = "NoCountersService";

            // Act
            var result = ReceiptsLogic.preperetiv_receipt(houseId, personalAccount, typeServices);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FindHouseId_ShouldReturnHouseId()
        {
            // Arrange
            string houseAddress = "вул. Київська 27";

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void FindHouseId_ShouldReturnZeroForNonexistentAddress()
        {
            // Arrange
            string houseAddress = "nonexistentAddress";

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
