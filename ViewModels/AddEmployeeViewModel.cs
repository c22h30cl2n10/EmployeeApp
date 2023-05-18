using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using EmployeeApp.Models;
using EmployeeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EmployeeApp.Utils;

namespace EmployeeApp.ViewModels
{
    [QueryProperty(nameof(AddEmployee), "AddEmployee")]
    public partial class AddEmployeeViewModel : ObservableValidator
    {

        [ObservableProperty]
        private Employee employeeDetails = new Employee();

        private readonly IEmployeeService _employeeService;
        public AddEmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //Если все поля соответствуют валидации, то происходит сохранение профиля работника в SQLite БД
        [ICommand]
        public async void AddEmployee()
        {
            if (ValidateFields() == null)
            {
                var response = await _employeeService.AddEmployee(employeeDetails);
                if (response > 0)
                {
                    await Shell.Current.DisplayAlert("Успех!", "Запись успешно добавлена", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Неудача!", "Возникла ошибка при добавлении записи", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка!", ValidateFields(), "OK");
            }
        }
        //Валидация полей
        private string ValidateFields()
        {
            if (StringValidator.Validate(employeeDetails.FirstName) != null)
            {
                return StringValidator.Validate(employeeDetails.FirstName);
            }
            if (StringValidator.Validate(employeeDetails.MiddleName) != null)
            {
                return StringValidator.Validate(employeeDetails.MiddleName);
            }
            if (StringValidator.Validate(employeeDetails.LastName) != null)
            {
                return StringValidator.Validate(employeeDetails.LastName);
            }
            return null;
        }

    }
}
