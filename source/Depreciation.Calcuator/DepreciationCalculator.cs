using System;

namespace Depreciation.Calculator
{
    public class DepreciationCalculator
    {
        private static int MonthAdjustmentDays = 2;
        private static int MonthsInYear = 12;

        public double GetStraightLineAmount(DepreciationCalculatorInputModel inputModel)
        {
            var maxStraightLineAmountPerMonth = (inputModel.AssetCost - inputModel.SalvageValue)/inputModel.UsefulLifeInYears/12;
            var monthsOwned = CalculateMonthsOwned(inputModel);
            return maxStraightLineAmountPerMonth*monthsOwned;
        }

        private int CalculateMonthsOwned(DepreciationCalculatorInputModel inputModel)
        {
            var adjustFinanicalYearEnd = AdjustFinanicalYearEnd(inputModel.FinancialYearEnd);
            var months = GetMonthsOwnedForCurrentFinancialYear(adjustFinanicalYearEnd, inputModel.PurchaseDate, inputModel.UsefulLifeInYears);
            return months;
        }

        private int GetMonthsOwnedForCurrentFinancialYear(DateTime finanicalYearEnd, DateTime purchaseDate, int usefulLifeInYears)
        {
            var maxOwnershipMonths = CalculateMaxOwnershipMonths(purchaseDate, usefulLifeInYears);
            var totalDepreceiationMonths = CalculateTotalsMonthsElapsed(finanicalYearEnd) - CalculateTotalsMonthsElapsed(purchaseDate);
            var monthsThisYear = CalculateMonthsOfOwnershipForFinancialYear(totalDepreceiationMonths, maxOwnershipMonths);
            return monthsThisYear;
        }

        private int CalculateMaxOwnershipMonths(DateTime purchaseDate, int usefulLifeInYears)
        {
            var endUsefulLife = CalculateEndUsefulLifeDate(purchaseDate, usefulLifeInYears);
            var maxOwnershipMonths = CalculateTotalsMonthsElapsed(endUsefulLife) - CalculateTotalsMonthsElapsed(purchaseDate);
            return maxOwnershipMonths;
        }

        private DateTime CalculateEndUsefulLifeDate(DateTime purchaseDate, int usefulLifeInYears)
        {
            var endUsefulLife = purchaseDate.AddYears(usefulLifeInYears);
            return endUsefulLife;
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
            if (IsFullYearOwnership(totalMonths, maxOwnershipMonths))
            {
                return MonthsInYear;
            }
            return numberOfMonthsBeyondYear;
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