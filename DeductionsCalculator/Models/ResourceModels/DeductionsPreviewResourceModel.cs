using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResourceModels
{
    public class DeductionsPreviewResourceModel
    {
        public Employee Employee { get; set; }
        public decimal EmployeeCost { get; set; }
        public decimal DependentsCost { get; set; }
        public decimal TotalCost { get; set; }



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
