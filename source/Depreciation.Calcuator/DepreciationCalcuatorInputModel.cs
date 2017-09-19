using System;

namespace Depreciation.Calculator
{
    public class DepreciationCalcuatorInputModel
    {
        public double AssetCost { get; set; }
        public double SalvageValue { get; set; }
        public int UsefulLifeInYears { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}