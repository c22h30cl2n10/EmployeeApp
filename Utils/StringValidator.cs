using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeApp.Utils
{
    //Проверка на null, пустую строку, цифры и спец символы
    class StringValidator
    {
        public static string Validate(string input)
        {
            string error = null;
            if (input == null || input == "")
            {
                error = "Поле не должно быть пустым!";
            }
            else if (Regex.IsMatch(input, @"[\d\W]"))
            {
                error = "Поле не должно содержать цифр или спецсимволов";
            }
            return error;
        }
    }
}
