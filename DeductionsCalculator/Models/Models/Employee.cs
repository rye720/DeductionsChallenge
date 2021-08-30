using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Employee : BaseModel
    {
        public IList<Dependent> Dependents { get; set; } = new List<Dependent>();
    }
}
