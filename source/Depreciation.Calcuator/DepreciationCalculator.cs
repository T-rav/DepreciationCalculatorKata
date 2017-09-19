namespace Depreciation.Calcuator
{
    public class DepreciationCalculator
    {
        public double GetStraightLineAmount(DepreciationCalcuatorInputModel inputModel)
        {
            var maxStraightLineAmountPerYear = (inputModel.AssetCost - inputModel.SalvageValue)/inputModel.UsefulLifeInYears;
            return maxStraightLineAmountPerYear;
        }
    }
}