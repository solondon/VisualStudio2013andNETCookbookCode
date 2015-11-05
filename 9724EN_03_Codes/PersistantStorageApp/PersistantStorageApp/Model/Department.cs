using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace PersistantStorageApp.Model
{
    [Table]
    public class Department
    {
        public Department()
        {
            _employees = new EntitySet<Employee>(
                new Action<Employee>(this.attach_Employee),
                new Action<Employee>(this.detach_Employee)
                );
        }

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(IsVersion = true)]
        private Binary _version;

        private EntitySet<Employee> _employees;
        [Association(Storage = "_employees", OtherKey = "_departmentId", ThisKey = "Id")]
        public EntitySet<Employee> Employees
        {
            get { return this._employees; }
            set { this._employees.Assign(value); }
        }

        private void attach_Employee(Employee emp)
        {
            emp.Department = this;
        }

        private void detach_Employee(Employee emp)
        {
            emp.Department = null;
        }


    }
}
