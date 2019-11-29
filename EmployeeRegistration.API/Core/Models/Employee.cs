using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRegistration.API.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Number { get; set; }
        [Required]
        [StringLength(255)]
        public string Position { get; set; }
        [Required]
        [StringLength(510)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(64)]
        public string Email { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
