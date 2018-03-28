using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_GaryQian.BusinessLogic
{
    /// <summary>
    /// Static class provides functions for income tax calculation
    /// </summary>
    public static class IncomeTaxFormula
    {
        /// <summary>
        /// Get monthly tax base on annual gross income
        /// </summary>
        /// <param name="annualSalary">receive annual gross income</param>
        /// <returns>monthly tax in double rype</returns>
        public static double GetMonthlyTax(double annualSalary)
        {
            if (annualSalary <= 18200)
                return 0;
            else if (annualSalary >= 18201 && annualSalary <= 37000)
                return myRound((annualSalary - 18200) * 0.19 / 12);
            else if (annualSalary >= 37001 && annualSalary <= 87000)
                return myRound((3572 + (annualSalary - 37000) * 0.325) / 12);
            else if (annualSalary >= 87001 && annualSalary <= 180000)
                return myRound((19822 + (annualSalary - 87000) * 0.37) / 12);
            else
                return myRound((54232 + (annualSalary - 180000) * 0.45) / 12);
        }
        /// <summary>
        /// Get monthly gross income base on annual gross income
        /// </summary>
        /// <param name="annualIncome"></param>
        /// <returns>monthly gross salary in double type</returns>
        public static double GetGrossMonthlySalary(double annualIncome)
        {
            return myRound(annualIncome / 12);
        }
        /// <summary>
        /// Get monthly net income base on monlty gross income and tax
        /// </summary>
        /// <param name="grossMonthly"></param>
        /// <param name="taxMontly"></param>
        /// <returns>monthly net income in double type</returns>
        public static double GetMonthlyNetIncome(double grossMonthly, double taxMontly)
        {
            return grossMonthly - taxMontly;

        }
        /// <summary>
        ///  Get monthly super base on monlty gross income and super rate
        /// </summary>
        /// <param name="grossMonthly"></param>
        /// <param name="superRateStr"></param>
        /// <returns>monthly super in double type</returns>
        public static double GetMonthlySuper(double grossMonthly, string superRateStr)
        {
            double rate = convertSuperToDouble(superRateStr);
            if (rate == -1)
                return -1;
            return myRound(grossMonthly * rate);
        }
        /// <summary>
        /// Convert super rate from string (x%) to a number
        /// return -1 if error found
        /// </summary>
        /// <param name="superRate">x%</param>
        /// <returns></returns>
        private static double convertSuperToDouble(string superRate)
        {
            double output = -1;
            if (double.TryParse(superRate.Remove(superRate.Length - 1), out output) && output > 0)
                return output / 100;
            return -1;
        }
        //rouding number
        private static int myRound(double d)
        {
            return (int)(d + 0.5);
        }
    }
}
