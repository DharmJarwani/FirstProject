using RestAPIcrudDEMO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAPIcrudDEMO.EmployeeData
{
    public class SqlEmployeeData : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public SqlEmployeeData(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            /*
            var existingEmployee = _employeeContext.Employees.Find(employee);
            if(existingEmployee != null)
            {
                _employeeContext.Employees.Remove(existingEmployee);
            }
            */
            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();
        }

        public Employee EditEmployee(Employee employee)
        {
            var Existingemployee = _employeeContext.Employees.Find(employee.Id);
            if(Existingemployee != null)
            {
                Existingemployee.Name = employee.Name;
                _employeeContext.Employees.Update(Existingemployee);
                _employeeContext.SaveChanges();
            }
            return employee; 
        }

        public Employee GetEmployee(Guid id)
        {
            var employee =  _employeeContext.Employees.Find(id);
            return employee;
            //return _employeeContext.Employees.SingleOrDefault(x=>x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
           return _employeeContext.Employees.ToList();
        }
    }
}
