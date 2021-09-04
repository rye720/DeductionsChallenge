using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResourceModels
{
    public class DeductionsPreviewResourceModel
    {
        public decimal EmployeeCost { get; set; }
        public decimal DependentsCost { get; set; }
        public decimal TotalCost { get; set; }
        public Employee Employee { get; set; }

        // use resource models for employee and dependent, bring bool hasDiscount back with dependent model
        // would be nice to make hasDiscount dependent display something




        public DeductionsPreviewResourceModel()
        {

        }

        public DeductionsPreviewResourceModel(Employee employee
            , decimal employeeCost
            , decimal dependentsCost)
        {
            Employee = employee;
            EmployeeCost = employeeCost;
            DependentsCost = dependentsCost;

            TotalCost = employeeCost + dependentsCost;
        }
    }
}
