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
        public void Constructor_ShouldNotThrowExceptionForRowsWithNullValues()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", null},
                    {"Адреса будинку", null}
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
                Assert.Fail("Constructor should not throw an exception for rows with null values.");
            }
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForRowsWithMissingKeys()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount"}
                    // Missing "Адреса будинку" key
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
                Assert.Fail("Constructor should not throw an exception for rows with missing keys.");
            }
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForRowsWithDifferentKeySets()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount"},
                    {"Адреса будинку", "testAddress"}
                    // Add other key-value pairs as needed
                },
                new Dictionary<string, string>
                {
                    {"Key1", "Value1"},
                    {"Key2", "Value2"}
                    // Different key set
                }
            };

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for rows with different key sets.");
            }
        }

        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForRowsWithSpecialCharacters()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "test@account"},
                    {"Адреса будинку", "!@#$%^&*()"}
                    // Add other key-value pairs with special characters as needed
                }
            };

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for rows with special characters.");
            }
        }


        [TestMethod]
        public void Constructor_ShouldNotThrowExceptionForRowsWithWhitespaceKeys()
        {
            // Arrange
            var rowsData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"Особовий рахунок", "testAccount"},
                    {"   ", "testAddress"}
                    // Whitespace key
                }
            };

            // Act and Assert
            try
            {
                new FlowMeterTeamProject.BLL.Utils.DataGrid.RowDetails(rowsData);
            }
            catch (Exception)
            {
                Assert.Fail("Constructor should not throw an exception for rows with whitespace keys.");
            }
        }
    }
}
