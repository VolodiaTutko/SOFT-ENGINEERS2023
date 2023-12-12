using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;
using DAL.Data;
using FlowMeterTeamProject.BLL.Utils.DataGrid;

namespace FlowMeterTeamProject.BLL.Utils.DataGrid
{
    public static class PasswordHashing
    {
        public static void HashPasswordAndAddUser(string username, string password, string typeofuser)
        {
            // Генеруємо хеш пароля

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            Employee newemployee = new Employee
            {
                EmployeeLogin = username,
                EmployeePassword = hashedPassword,
                TypeOfUser = typeofuser,
            };
            using (var dbContext = new AppDbContext())
            {
                dbContext.employees.Add(newemployee);
                dbContext.SaveChanges();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);

            if (passwordMatches)
            {
                return true;
            }

            return false;
        }
    }
}
