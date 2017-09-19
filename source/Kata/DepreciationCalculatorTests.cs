using System;
using NUnit.Framework;

namespace Depreciation.Calculator.Tests
{
    [TestFixture]
    public class DepreciationCalculatorTests
    {
        [TestFixture]
        public class UsefulLifeOneYear_AssetOwnedForFullYear
        {

            [Test]
            public void GetStraightLineAmount_WhenNoSalvageValue_ShouldReturnAssetCost()
            {
                //---------------Arrange-------------------
                var expected = 100;
                var assetCost = 100;
                var salvageValue = 0;
                var calculator = new DepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(assetCost, salvageValue);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }

            [Test]
            public void GetStraightLineAmount_WhenR25SalvageValue_ShouldReturnAssetCostMinusSalvageValue()
            {
                //---------------Arrange-------------------
                var expected = 75;
                var assetCost = 100;
                var salvageValue = 25;
                var calculator = new DepreciationCalculator();
                //---------------Act----------------------
                var result = calculator.GetStraightLineAmount(assetCost, salvageValue);
                //---------------Assert-----------------------
                Assert.AreEqual(expected, result);
            }
        }
    }

    public class DepreciationCalculator
    {
        public double GetStraightLineAmount(int assetCost, int salvageValue)
        {
            return assetCost - salvageValue;
        }
    }
}
