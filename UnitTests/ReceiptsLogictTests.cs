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

        [TestMethod]
        public void GetNonZeroServices_ShouldReturnEmptyDictionaryForNonexistentAccount()
        {
            // Arrange
            string personalAccount = "nonexistentAccount";
            string houseAddress = "вул. Київська 27";

            // Act
            var result = ReceiptsLogic.GetNonZeroServices(personalAccount, houseAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ContainsKey(personalAccount), "Dictionary should be empty for nonexistent account");
        }

        [TestMethod]
        public void FindHouseId_ShouldReturnZeroForNullOrEmptyAddress()
        {
            // Arrange
            string houseAddress = "";

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void GetNonZeroServices_ShouldReturnEmptyDictionaryForNullOrEmptyAddress()
        {
            // Arrange
            string personalAccount = "testAccount";
            string houseAddress = "";

            // Act
            var result = ReceiptsLogic.GetNonZeroServices(personalAccount, houseAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ContainsKey(personalAccount), "Dictionary should be empty for null or empty address");
        }


        [TestMethod]
        public void GetNonZeroServices_ShouldReturnZeroForNullHouseAddress()
        {
            // Arrange
            string personalAccount = "testAccount";
            string houseAddress = null;

            // Act
            var result = ReceiptsLogic.GetNonZeroServices(personalAccount, houseAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ContainsKey(personalAccount), "Dictionary should be empty for null house address");
        }


        [TestMethod]
        public void FindHouseId_ShouldReturnZeroForNullAddress()
        {
            // Arrange
            string houseAddress = null;

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FindHouseId_ShouldReturnZeroForWhitespaceAddress()
        {
            // Arrange
            string houseAddress = "   ";

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FindHouseId_ShouldReturnZeroForNonexistentWhitespaceAddress()
        {
            // Arrange
            string houseAddress = "   nonexistentAddress   ";

            // Act
            var result = ReceiptsLogic.FindHouseId(houseAddress);

            // Assert
            Assert.AreEqual(0, result);
        }


    }
}
