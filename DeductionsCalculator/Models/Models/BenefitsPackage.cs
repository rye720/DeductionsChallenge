using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class BenefitsPackage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal YearlyEmployeeCost { get; set; }
        public decimal YearlyDependentCost { get; set; }
        public char DiscountInitial { get; set; }
        public decimal? DiscountInitialPercentage { get; set; }
    }
}
