using Solid.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {
            // Constructor to initilate connection string, etc
        }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }
}
