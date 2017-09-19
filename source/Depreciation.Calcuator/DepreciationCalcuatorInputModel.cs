using System;

namespace Depreciation.Calcuator
{
    public class DepreciationCalcuatorInputModel
    {
        public double AssetCost { get; set; }
        public double SalvageValue { get; set; }
        public int UsefulLifeInYears { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}