using Solid.Entities;
using Solid.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Services
{
    public class EmployeeService
    {
        private IEmployeeRepository _employeeRepository { get; }
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
                

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }
    }
}
