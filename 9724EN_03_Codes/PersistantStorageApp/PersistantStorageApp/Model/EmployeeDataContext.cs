using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistantStorageApp.Model
{
    public class EmployeeDataContext : DataContext
    {
        public EmployeeDataContext(string connectionString)
            : base(connectionString) { }

        public Table<Employee> Employees;

        public Table<Department> Departments;
    }
}
