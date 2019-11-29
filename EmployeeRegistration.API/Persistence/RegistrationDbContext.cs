using EmployeeRegistration.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRegistration.API.Persistence
{
    public class RegistrationDbContext : DbContext
    {

        public DbSet<Employee> Employee { get; set; }


        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
         : base(options)
        {
        }

    }
}
