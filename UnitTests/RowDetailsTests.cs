using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestClass]
    public class RowDetailsTests
    {
        [TestMethod]
        public void Constructor_ShouldCreateLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount"},
                    {"Адреса будинку", "testAddress"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            Assert.IsTrue(File.Exists("testlogicReceipt.txt"), "Log file should be created");
        }

        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount"},
                    {"Адреса будинку", "testAddress"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: testAccount");
            StringAssert.Contains(logContent, "Адреса будинку: testAddress");
            // Add other assertions based on expected log content
        }


        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataForMultipleRowsInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount1"},
                    {"Адреса будинку", "testAddress1"}
                    // Add other key-value pairs as needed
                },
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount2"},
                    {"Адреса будинку", "testAddress2"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: testAccount1");
            StringAssert.Contains(logContent, "Адреса будинку: testAddress1");
            StringAssert.Contains(logContent, "Особовий рахунок: testAccount2");
            StringAssert.Contains(logContent, "Адреса будинку: testAddress2");
            // Add other assertions based on expected log content
        }

        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataForNoRowsInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>();

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            Assert.IsFalse(logContent.Contains("Особовий рахунок"));
            Assert.IsFalse(logContent.Contains("Адреса будинку"));
            // Add other assertions based on expected log content
        }

        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataWithEmptyValuesInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", ""},
                    {"Адреса будинку", ""}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: ");
            StringAssert.Contains(logContent, "Адреса будинку: ");
            // Add other assertions based on expected log content
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForNoRows()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>();

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for no rows.");
            }
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForRowsWithEmptyValues()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", ""},
                    {"Адреса будинку", ""}
                    // Add other key-value pairs as needed
                }
            };

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for rows with empty values.");
            }
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForMultipleRows()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount1"},
                    {"Адреса будинку", "testAddress1"}
                    // Add other key-value pairs as needed
                },
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount2"},
                    {"Адреса будинку", "testAddress2"}
                    // Add other key-value pairs as needed
                }
            };

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for multiple rows.");
            }
        }


        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForEmptyData()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>();

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for empty data");
            }
        }



        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataWithSpecialCharactersInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount!@#"},
                    {"Адреса будинку", "testAddress$%^"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: testAccount!@#");
            StringAssert.Contains(logContent, "Адреса будинку: testAddress$%^");
            // Add other assertions based on expected log content
        }

        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataWithWhitespaceInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "test Account"},
                    {"Адреса будинку", "test Address"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: test Account");
            StringAssert.Contains(logContent, "Адреса будинку: test Address");
            // Add other assertions based on expected log content
        }


        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataWithUnicodeCharactersInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount 🌟"},
                    {"Адреса будинку", "testAddress 日本"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            StringAssert.Contains(logContent, "Особовий рахунок: testAccount 🌟");
            StringAssert.Contains(logContent, "Адреса будинку: testAddress 日本");
            // Add other assertions based on expected log content
        }

        [TestMethod]
        public void Constructor_ShouldIncludeCorrectDataForDuplicateKeysInLogFile()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount1"},
                    {"Адреса будинку", "testAddress1"}
                    // Add other key-value pairs as needed
                },
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount2"},
                    {"Адреса будинку", "testAddress2"}
                    // Add other key-value pairs as needed
                }
            };

            // Act
            new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);

            // Assert
            string logContent = File.ReadAllText("testlogicReceipt.txt");
            Assert.AreEqual(2, Regex.Matches(logContent, "Особовий рахунок").Count);
            Assert.AreEqual(2, Regex.Matches(logContent, "Адреса будинку").Count);
            // Add other assertions based on expected log content
        }

    }
}
