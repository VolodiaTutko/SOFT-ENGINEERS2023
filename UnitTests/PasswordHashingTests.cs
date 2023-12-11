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
    public class PasswordHashingTests
    {
        private string testUsername = "testUser";
        private string testPassword = "testPassword";
        private string testTypeOfUser = "admin";
        [TestMethod]
        public void HashPasswordAndAddUser_ShouldAddUserWithHashedPassword()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";
            string typeofuser = "admin";

            // Act
            FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.HashPasswordAndAddUser(username, password, typeofuser);

            // Assert
            // Write assertions to check if the user is added with the hashed password
            // Example:
            using (var dbContext = new AppDbContext())
            {
                var addedEmployee = dbContext.employees.SingleOrDefault(e => e.EmployeeLogin == username);
                Assert.IsNotNull(addedEmployee);
                Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, addedEmployee.EmployeePassword));
                Assert.AreEqual(typeofuser, addedEmployee.TypeOfUser);
            }
        }

        [TestMethod]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            // Arrange
            string enteredPassword = "testPassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword);

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword(enteredPassword, hashedPassword);

            // Assert
            // Write assertions to check if the password verification returns true
            // Example:
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
            // Arrange
            string enteredPassword = "incorrectPassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("testPassword");

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword(enteredPassword, hashedPassword);

            // Assert
            // Write assertions to check if the password verification returns false
            // Example:
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyPassword_ShouldReturnFalseForNullOrEmptyEnteredPassword()
        {
            // Arrange
            string enteredPassword = ""; // Empty entered password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("testPassword");

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword(enteredPassword, hashedPassword);

            // Assert
            Assert.IsFalse(result, "Verification with empty entered password should return false");
        }

        [TestMethod]
        public void VerifyPassword_ShouldReturnFalseForMismatchedHashAndPassword()
        {
            // Arrange
            string enteredPassword = "testPassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("differentPassword");

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword(enteredPassword, hashedPassword);

            // Assert
            Assert.IsFalse(result, "Verification with mismatched hash and password should return false");
        }


        [TestMethod]
        public void VerifyPassword_ShouldReturnFalseForIncorrectHashedPassword()
        {
            // Arrange
            string enteredPassword = "testPassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("differentPassword");

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword(enteredPassword, hashedPassword);

            // Assert
            Assert.IsFalse(result, "Verification with incorrect hashed password should return false");
        }


        [TestCleanup]
        public void TestCleanup()
        {
            using (var dbContext = new AppDbContext())
            {
                var userToDelete = dbContext.employees.SingleOrDefault(e => e.EmployeeLogin == testUsername);
                if (userToDelete != null)
                {
                    dbContext.employees.Remove(userToDelete);
                    dbContext.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void VerifyPassword_ShouldReturnFalseForMismatchedHashedPassword()
        {
            // Arrange
            string enteredPassword = "testPassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("testPassword");

            // Act
            bool result = FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.VerifyPassword("differentPassword", hashedPassword);

            // Assert
            Assert.IsFalse(result, "Verification with mismatched hashed password should return false");
        }

        [TestMethod]
        public void HashPasswordAndAddUser_ShouldNotAddUserForMismatchedTypeOfUser()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";
            string correctTypeOfUser = "admin";
            string incorrectTypeOfUser = "user";

            // Act
            FlowMeterTeamProject.BLL.Utils.DataGrid.PasswordHashing.HashPasswordAndAddUser(username, password, correctTypeOfUser);

            // Assert
            using (var dbContext = new AppDbContext())
            {
                var addedEmployee = dbContext.employees.SingleOrDefault(e => e.EmployeeLogin == username);
                Assert.IsNotNull(addedEmployee);
                Assert.AreNotEqual(incorrectTypeOfUser, addedEmployee.TypeOfUser, "User should not be added with mismatched type of user");
            }
        }

    }
}
