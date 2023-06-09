﻿using EmployeeApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Services
{
    public class EmployeeService : IEmployeeService
    {

        public SQLiteAsyncConnection _dbConnection;

        //Если база даных еще не была создана, то она будет создана!(для первого запуска или в случае ее удаления)
        private async Task SetupDatabase()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employee.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<Employee>();
            }
        }
        //Запись нового сотрудника
        public async Task<int> AddEmployee(Employee employee)
        {
            await SetupDatabase();
            return  await _dbConnection.InsertAsync(employee);

        }
        //Удаление сотрудника
        public async Task<int> DeleteEmployee(Employee employee)
        {
            await SetupDatabase();
            return await _dbConnection.DeleteAsync(employee);
        }

        //Изменение данных уже добавленного сотрудника
        public async Task<int> UpdateEmployee(Employee employee)
        {
            await SetupDatabase();
            return await _dbConnection.UpdateAsync(employee);
        }

        //Получение списка всех сотрудников
        public async Task<List<Employee>> GetEmployeesList()
        {
            await SetupDatabase();
            var employeeslist = await _dbConnection.Table<Employee>().ToListAsync();
                return employeeslist;
        }
    }
}
