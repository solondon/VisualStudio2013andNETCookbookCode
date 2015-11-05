using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PersistantStorageApp.Resources;
using PersistantStorageApp.Model;
using System.Text;

namespace PersistantStorageApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.BindDepartments();
        }

        private void BindDepartments()
        {
            using (EmployeeDataContext context = new EmployeeDataContext(App.ConnectionString))
            {
                var query = from d in context.Departments select d;
                this.lstDepartment.ItemsSource = query.ToList();
            }
        }

        private void AddEmployee()
        {
            using (EmployeeDataContext context = new EmployeeDataContext(App.ConnectionString))
            {
                var department = this.lstDepartment.SelectedItem as Department;
                var contextualDepartment = context.Departments.FirstOrDefault(e => e.Id == department.Id);
                Employee emp = new Employee
                {
                    EName = this.txtName.Text,
                    Age = Convert.ToInt32(this.txtAge.Text),
                    Department = contextualDepartment
                };

                context.Employees.InsertOnSubmit(emp);

                context.SubmitChanges();
            }
        }
        private IList<Employee> GetEmployees()
        {
            using (EmployeeDataContext context = new EmployeeDataContext(App.ConnectionString))
            {
                var query = from e in context.Employees select e;
                return query.ToList();
            }
        }
        private void UpdateEmployee()
        {
            using (EmployeeDataContext context = new EmployeeDataContext(App.ConnectionString))
            {
                var department = this.lstDepartment.SelectedItem as Department;
                var contextualDepartment = context.Departments.FirstOrDefault(e => e.Id == department.Id);
                IQueryable<Employee> empQuery = from e in context.Employees where e.EName == this.txtName.Text select e;
                Employee empToUpdate = empQuery.FirstOrDefault();
                if (empToUpdate != null)
                {
                    empToUpdate.Age = Convert.ToInt32(this.txtAge.Text);
                    empToUpdate.Department = contextualDepartment;
                }
                context.SubmitChanges();
            }
        }
        private void DeleteEmployee()
        {
            using (EmployeeDataContext context = new EmployeeDataContext(App.ConnectionString))
            {
                IQueryable<Employee> empQuery = from e in context.Employees where e.EName == this.txtName.Text select e;
                Employee empToDelete = empQuery.FirstOrDefault();
                if (empToDelete != null)
                {
                    context.Employees.DeleteOnSubmit(empToDelete);
                }

                context.SubmitChanges();
            }
        }
        private void ClearData()
        {
            this.txtName.Text = this.txtAge.Text = string.Empty;
            this.lstDepartment.SelectedIndex = 0;
        }
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.AddEmployee();
            this.ClearData();
        }

        private void btnSelectEmployees_Click(object sender, RoutedEventArgs e)
        {
            var employees = this.GetEmployees();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Employees\tAge");
            foreach (Employee employee in employees)
                builder.AppendLine(employee.EName + "\t" + employee.Age);

            MessageBox.Show(builder.ToString());
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateEmployee();
            this.ClearData();

            MessageBox.Show("Update successful !");
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.DeleteEmployee();
            this.ClearData();

            MessageBox.Show("Delete successful !");
        }
    }
}