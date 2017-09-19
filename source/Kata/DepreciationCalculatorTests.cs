using System;
using Depreciation.Calcuator;
using NUnit.Framework;

namespace Depreciation.Calculator.Tests
{
    [TestFixture]
    public class DepreciationCalculatorTests
    {
        [TestFixture]
        public class UsefulLifeOneYearAndAssetOwnedForFullYear
        {

            [Test]
            public void GetStraightLineAmount_WhenNoSalvageValue_ShouldReturnAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 100;
                var inputModel = new DepreciationCalcuatorInputModel
                {
                    AssetCost = 100,
                    UsefulLifeInYears = 1,
                    SalvageValue = 0
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }

            [TestCase(100,25,75)]
            [TestCase(100, 50, 50)]
            public void GetStraightLineAmount_WhenNonZeroSalvageValue_ShouldReturnAssetCostMinusSalvageValue(int assetCost, int salvageValue, double expected)
            {
                //---------------Arrange-------------------
                var inputModel = new DepreciationCalcuatorInputModel
                {
                    AssetCost = assetCost,
                    UsefulLifeInYears = 1,
                    SalvageValue = salvageValue
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }

        [TestFixture]
        public class UsefulLifeTwoYearsAndAssetOwnedForFullYear
        {

            [Test]
            public void GetStraightLineAmount_WhenSalvageValueHalfAssetCost_ShouldReturnQuarterOfAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 50;
                var inputModel = new DepreciationCalcuatorInputModel
                {
                    AssetCost = 200,
                    UsefulLifeInYears = 2,
                    SalvageValue = 100
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }

        [TestFixture]
        public class UsefulLifeTwoYearsAndAssetOwnedForHalfYear
        {

            [Test]
            public void GetStraightLineAmount_WhenSalvageValueHalfAssetCost_ShouldReturnEighthOfAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 50;
                var inputModel = new DepreciationCalcuatorInputModel
                {
                    AssetCost = 200,
                    UsefulLifeInYears = 2,
                    SalvageValue = 100,
                    PurchaseDate = new DateTime(2017, 06, 01)
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }

        private static DepreciationCalculator CreateDepreciationCalculator()
        {
            var calculator = new DepreciationCalculator();
            return calculator;
        }
    }
}
