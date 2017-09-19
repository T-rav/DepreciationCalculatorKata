﻿using System;
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
                var inputModel = CreateInputModel();
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }

            private static DepreciationCalculatorInputModel CreateInputModel()
            {
                var inputModel = new DepreciationCalculatorInputModel
                {
                    AssetCost = 100,
                    UsefulLifeInYears = 1,
                    SalvageValue = 0,
                    PurchaseDate = new DateTime(2017,01,01),
                    FinancialYearEnd = new DateTime(2017,12,31)
                };
                return inputModel;
            }

            [TestCase(100,25,75)]
            [TestCase(100, 50, 50)]
            public void GetStraightLineAmount_WhenNonZeroSalvageValue_ShouldReturnAssetCostMinusSalvageValue(int assetCost, int salvageValue, double expected)
            {
                //---------------Arrange-------------------
                var inputModel = new DepreciationCalculatorInputModel
                {
                    AssetCost = assetCost,
                    UsefulLifeInYears = 1,
                    SalvageValue = salvageValue,
                    PurchaseDate = new DateTime(2017, 01, 01),
                    FinancialYearEnd = new DateTime(2017, 12, 31)
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
                var inputModel = new DepreciationCalculatorInputModel
                {
                    AssetCost = 200,
                    UsefulLifeInYears = 2,
                    SalvageValue = 100,
                    PurchaseDate = new DateTime(2017, 01, 01),
                    FinancialYearEnd = new DateTime(2017, 12, 31)
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }

        [TestFixture]
        public class UsefulLifeTwoYearsAndAssetOwnedForHalfYearF
        {

            [Test]
            public void GetStraightLineAmount_WhenFirstYearOwnershipAndSalvageValueHalfAssetCost_ShouldReturnEighthOfAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 25;
                var inputModel = new DepreciationCalculatorInputModel
                {
                    AssetCost = 200,
                    UsefulLifeInYears = 2,
                    SalvageValue = 100,
                    PurchaseDate = new DateTime(2017, 07, 01),
                    FinancialYearEnd = new DateTime(2017, 12, 31)
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }

            [Test]
            public void GetStraightLineAmount_WhenThirdYearOwnershipAndSalvageValueHalfAssetCost_ShouldReturnEighthOfAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 25;
                var inputModel = new DepreciationCalculatorInputModel
                {
                    AssetCost = 200,
                    UsefulLifeInYears = 2,
                    SalvageValue = 100,
                    PurchaseDate = new DateTime(2017, 07, 01),
                    FinancialYearEnd = new DateTime(2019, 12, 31)
                };
                var calculator = CreateDepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(inputModel);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        [Ignore("Learning test")]
        public void LearningTest_WhenFindingNumberOfMonthsBetweenDates_ShouldReturnExactAmount()
        {
            //---------------Arrange-------------------
            var expected = 6;
            var startDate = new DateTime(2017,07,01);
            var endDate = new DateTime(2017,12,31);
            //---------------Act----------------------
            endDate = endDate.AddDays(2);
            var result = (endDate.Month + endDate.Year * 12) - (startDate.Month + startDate.Year * 12);

            //---------------Assert-----------------------
            Assert.AreEqual(expected, result);
        }

        private static DepreciationCalculator CreateDepreciationCalculator()
        {
            var calculator = new DepreciationCalculator();
            return calculator;
        }
    }
}
