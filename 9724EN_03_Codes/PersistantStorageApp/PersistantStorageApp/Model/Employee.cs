using Microsoft.Phone.Data.Linq.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistantStorageApp.Model
{
    [Table]
    [Index(Columns="EName", IsUnique=true, Name="employee_EName")]
    public class Employee
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, DbType = "INT NOT NULL Identity", AutoSync = AutoSync.OnInsert)]
        public int EmpId { get; set; }

        [Column(CanBeNull=false, DbType="NVarchar(100) NOT NULL")]
        public string EName { get; set; }

        [Column(CanBeNull= false)]
        public int Age { get; set; }

        [Column(IsVersion=true)]
        public Binary Version { get; set; }

        // Internal column for the associated department ID value
        [Column]
        internal int _departmentId;

        private EntityRef<Department> _department;
        [Association(Storage = "_department", ThisKey = "_departmentId", OtherKey = "Id", IsForeignKey = true)]
        public Department Department
        {
            get { return _department.Entity; }
            set
            {
                _department.Entity = value;
                if (value != null)
                    _departmentId = value.Id;
            }
        }
    }
}
