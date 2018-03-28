using Payment_GaryQian.Model;

namespace Payment_GaryQian.BusinessLogic
{
    public class ManipulateOutput
    {
        private EmployeeDetail importedEmployee { get; set; }
        public ManipulateOutput(EmployeeDetail input)
        {
            importedEmployee = input;
        }
        //create an employeeOuput instance base on AUS tax calcualtion rule. Error handling included.
        public EmployeeOutputDetail CalculatePayment()
        {
            double grossAnnualSalaray = -1, grossMonthlySalary = -1, monthlyIncomTax = -1, monthlyNetIncome = -1, monthlySuper = -1, payPeriodErrFormat = -1;
            string payPeriod = double.TryParse(importedEmployee.Period, out payPeriodErrFormat) ? Properties.Resources.ErrorCell : importedEmployee.Period;
            if (payPeriod!= Properties.Resources.ErrorCell && double.TryParse(importedEmployee.AnnualSalary, out grossAnnualSalaray) && grossAnnualSalaray >= 0)
            {
                grossMonthlySalary = IncomeTaxFormula.GetGrossMonthlySalary(grossAnnualSalaray);
                monthlyIncomTax = IncomeTaxFormula.GetMonthlyTax(grossAnnualSalaray);
                monthlyNetIncome = IncomeTaxFormula.GetMonthlyNetIncome(grossMonthlySalary, monthlyIncomTax);
                monthlySuper = IncomeTaxFormula.GetMonthlySuper(grossMonthlySalary, importedEmployee.SuperRate);
            }
            return new EmployeeOutputDetail(importedEmployee.FirstName, importedEmployee.LastName, payPeriod, grossMonthlySalary, monthlyIncomTax, monthlyNetIncome, monthlySuper);

        }
    }
}
