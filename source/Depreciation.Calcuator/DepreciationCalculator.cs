﻿using System;

namespace Depreciation.Calculator
{
    public class DepreciationCalculator
    {
        private static int MonthAdjustmentDays = 2;
        private static int Months = 12;

        public double GetStraightLineAmount(DepreciationCalculatorInputModel inputModel)
        {
            var maxStraightLineAmountPerMonth = ((inputModel.AssetCost - inputModel.SalvageValue)/inputModel.UsefulLifeInYears)/12;
            var monthsOwned = CalculateMonthsOwned(inputModel);
            return maxStraightLineAmountPerMonth*monthsOwned;
        }

        private int CalculateMonthsOwned(DepreciationCalculatorInputModel inputModel)
        {
            var endDate = AdjustFinanicalYearEnd(inputModel);
            var startDate = inputModel.PurchaseDate;
            var months = GetMonthsOwnedForCurrentFinancialYear(endDate, startDate);
            return months;
        }

        private int GetMonthsOwnedForCurrentFinancialYear(DateTime endDate, DateTime startDate)
        {
            var totalMonths = CalculateMonths(endDate) - CalculateMonths(startDate);
            var result = CalculateMonthsOfOwnershipForFinancialYear(totalMonths);
            return result;
        }

        private int CalculateMonthsOfOwnershipForFinancialYear(int totalMonths)
        {
            return HasOwnedForMoreThanOneYear(totalMonths) ? GetNumberOfMonthsBeyondYear(totalMonths) : totalMonths;
        }

        private bool HasOwnedForMoreThanOneYear(int totalMonths)
        {
            return GetNumberOfMonthsBeyondYear(totalMonths) != 0;
        }

        private static int GetNumberOfMonthsBeyondYear(int totalMonths)
        {
            return totalMonths % Months;
        }

        private DateTime AdjustFinanicalYearEnd(DepreciationCalculatorInputModel inputModel)
        {
            return inputModel.FinancialYearEnd.AddDays(MonthAdjustmentDays);
        }

        private int CalculateMonths(DateTime date)
        {
            return date.Month + date.Year * Months;
        }
    }
}