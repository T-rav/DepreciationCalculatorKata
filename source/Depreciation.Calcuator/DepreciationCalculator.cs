using System;

namespace Depreciation.Calculator
{
    public class DepreciationCalculator
    {
        private static int MonthAdjustmentDays = 2;
        private static int MonthsInYear = 12;

        public double GetStraightLineAmount(DepreciationCalculatorInputModel inputModel)
        {
            var straightLineAmountPerMonth = CalculateStraightLineAmountPerMonth(inputModel);
            var monthsOwned = CalculateMonthsOwned(inputModel);
            return CalculateDepreciationAmount(straightLineAmountPerMonth, monthsOwned);
        }

        private double CalculateStraightLineAmountPerMonth(DepreciationCalculatorInputModel inputModel)
        {
            var maxStraightLineAmountPerMonth = (inputModel.AssetCost - inputModel.SalvageValue) / inputModel.UsefulLifeInYears / MonthsInYear;
            return maxStraightLineAmountPerMonth;
        }

        private int CalculateMonthsOwned(DepreciationCalculatorInputModel inputModel)
        {
            var adjustFinanicalYearEnd = AdjustFinanicalYearEnd(inputModel.FinancialYearEnd);
            return GetMonthsOwnedForCurrentFinancialYear(adjustFinanicalYearEnd, inputModel.PurchaseDate, inputModel.UsefulLifeInYears);
        }

        private static double CalculateDepreciationAmount(double maxStraightLineAmountPerMonth, int monthsOwned)
        {
            return maxStraightLineAmountPerMonth*monthsOwned;
        }

        private int GetMonthsOwnedForCurrentFinancialYear(DateTime finanicalYearEnd, DateTime purchaseDate, int usefulLifeInYears)
        {
            var maxOwnershipMonths = CalculateMaxOwnershipMonths(purchaseDate, usefulLifeInYears);
            var totalDepreceiationMonths = CalculateTotalsMonthsElapsed(finanicalYearEnd) - CalculateTotalsMonthsElapsed(purchaseDate);
            return CalculateMonthsOfOwnershipForFinancialYear(totalDepreceiationMonths, maxOwnershipMonths);
        }

        private int CalculateMaxOwnershipMonths(DateTime purchaseDate, int usefulLifeInYears)
        {
            var endUsefulLife = CalculateEndUsefulLifeDate(purchaseDate, usefulLifeInYears);
            var maxOwnershipMonths = CalculateTotalsMonthsElapsed(endUsefulLife) - CalculateTotalsMonthsElapsed(purchaseDate);
            return maxOwnershipMonths;
        }

        private DateTime CalculateEndUsefulLifeDate(DateTime purchaseDate, int usefulLifeInYears)
        {
            return purchaseDate.AddYears(usefulLifeInYears);
        }

        private int CalculateMonthsOfOwnershipForFinancialYear(int totalMonths, int maxOwnershipMonths)
        {
            return HasOwnedForMoreThanOneYear(totalMonths) ? GetNumberOfMonthsBeyondFirstYear(totalMonths, maxOwnershipMonths) : totalMonths;
        }

        private bool HasOwnedForMoreThanOneYear(int totalMonths)
        {
            return totalMonths > MonthsInYear;
        }

        private int GetNumberOfMonthsBeyondFirstYear(int totalMonths, int maxOwnershipMonths)
        {
            var numberOfMonthsBeyondYear = totalMonths % MonthsInYear;
            return IsFullYearOwnership(totalMonths, maxOwnershipMonths) ? MonthsInYear : numberOfMonthsBeyondYear;
        }

        private bool IsFullYearOwnership(int totalMonths, int maxOwnershipMonths)
        {
            return totalMonths < maxOwnershipMonths;
        }

        private DateTime AdjustFinanicalYearEnd(DateTime financialYearEnd)
        {
            return financialYearEnd.AddDays(MonthAdjustmentDays);
        }

        private int CalculateTotalsMonthsElapsed(DateTime date)
        {
            return date.Month + date.Year * MonthsInYear;
        }
    }
}