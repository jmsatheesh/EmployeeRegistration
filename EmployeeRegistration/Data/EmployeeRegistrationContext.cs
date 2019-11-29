using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Models
{
    public class EmployeeRegistrationContext : DbContext
    {
        public EmployeeRegistrationContext (DbContextOptions<EmployeeRegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeRegistration.Models.Employee> Employee { get; set; }
    }
}
