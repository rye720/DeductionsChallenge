using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class BenefitsPackage : BaseModel
    {
        public decimal YearlyEmployeeCost { get; set; }
        public decimal YearlyDependentCost { get; set; }
        public char DiscountInitial { get; set; }
        public decimal? DiscountInitialPercentage { get; set; }
        public bool IsDefault { get; set; }
    }
}
