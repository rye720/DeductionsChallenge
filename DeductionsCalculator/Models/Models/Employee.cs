using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Employee : BaseModel
    {

        public ICollection<Dependent> Dependents { get; set; }
    }
}
