using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }



        public ICollection<Dependent> Dependents { get; set; }
    }
}
