using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.PDF.Utility
{
    public static class DataStorage
    {
        public static List<Employee> GetAllEmployees() =>
            new List<Employee>
            {
                new Employee { Name = "Petkan", Age = 13 },
                new Employee { Name = "Иван", Age = 33 },
                new Employee { Name = "Martin", Age = 44 }
            };
    }
}
