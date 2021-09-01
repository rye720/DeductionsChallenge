using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Common;
using Models.Models;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.UnitTests
{
    [TestClass]
    public class DeductionsServiceTests
    {
        private Mock<IBenefitsPackageRepository> _mockBenefitsPackageRepository;
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<IOptions<ApplicationSettings>> _mockAppSettings;

        private DeductionsService _deductionsService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockBenefitsPackageRepository = new Mock<IBenefitsPackageRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockAppSettings = new Mock<IOptions<ApplicationSettings>>();

            _mockAppSettings.Setup(x => x.Value).Returns(new ApplicationSettings() { PaySettings = new PaySettings() { PaycheckAmount = 2000.00m, PaychecksPerYear = 26 } });

            _deductionsService = new DeductionsService(_mockBenefitsPackageRepository.Object, _mockEmployeeRepository.Object, _mockAppSettings.Object);
        }

        [TestMethod]
        public void CreateNewEmployeeAndGetCostPreviewAsync_ReturnsCorrectTotals_NoDiscounts()
        {
            // Arrange
            var employeeWithDependents = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Testmans",

            };
            employeeWithDependents.Dependents = new List<Dependent> {
                new Dependent() {
                Id = Guid.NewGuid(), Name = "Testmans Firstson", EmployeeId = employeeWithDependents.Id
                },
                new Dependent() {
                    Id = Guid.NewGuid(), Name = "Testmans Otherson", EmployeeId = employeeWithDependents.Id
                }
            };

            var defaultBenefitsPackage = new BenefitsPackage()
            {
                YearlyEmployeeCost = 1000.00m,
                YearlyDependentCost = 500.00m,
                DiscountInitial = 'A',
                DiscountInitialPercentage = 0.1000m,
                IsDefault = true
            };

            _mockEmployeeRepository.Setup(x => x.AddAsync(employeeWithDependents)).Returns(Task.FromResult(employeeWithDependents));
            _mockEmployeeRepository.Setup(x => x.GetEmployeeWithDependentsByEmployeeIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(employeeWithDependents));

            _mockBenefitsPackageRepository.Setup(x => x.GetDefaultBenefitsPackageAsync()).Returns(Task.FromResult(defaultBenefitsPackage));

            // Act
            var result = _deductionsService.CreateNewEmployeeAndGetCostPreviewAsync(employeeWithDependents, null);

            // Assert
            Assert.AreEqual(result.Result.TotalCost, 54000);
        }

        [TestMethod]
        public void CreateNewEmployeeAndGetCostPreviewAsync_ReturnsCorrectTotals_WithDiscount()
        {
            // Arrange
            var employeeWithDependents = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Testmans",

            };
            employeeWithDependents.Dependents = new List<Dependent> {
                new Dependent() {
                Id = Guid.NewGuid(), Name = "Testmans Firstson", EmployeeId = employeeWithDependents.Id
                },
                new Dependent() {
                    Id = Guid.NewGuid(), Name = "Testmans Otherson", EmployeeId = employeeWithDependents.Id
                },
                new Dependent() {
                    Id = Guid.NewGuid(), Name = "A Discounted Dependent", EmployeeId = employeeWithDependents.Id
                }
            };

            var defaultBenefitsPackage = new BenefitsPackage()
            {
                YearlyEmployeeCost = 1000.00m,
                YearlyDependentCost = 500.00m,
                DiscountInitial = 'A',
                DiscountInitialPercentage = 0.1000m,
                IsDefault = true
            };

            _mockEmployeeRepository.Setup(x => x.AddAsync(employeeWithDependents)).Returns(Task.FromResult(employeeWithDependents));
            _mockEmployeeRepository.Setup(x => x.GetEmployeeWithDependentsByEmployeeIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(employeeWithDependents));

            _mockBenefitsPackageRepository.Setup(x => x.GetDefaultBenefitsPackageAsync()).Returns(Task.FromResult(defaultBenefitsPackage));
            _mockAppSettings.Setup(x => x.Value).Returns(new ApplicationSettings() { PaySettings = new PaySettings() { PaycheckAmount = 2000.00m, PaychecksPerYear = 26 } });

            // Act
            var result = _deductionsService.CreateNewEmployeeAndGetCostPreviewAsync(employeeWithDependents, null);

            // Assert
            Assert.AreEqual(result.Result.TotalCost, 54450);
        }
    }
}
