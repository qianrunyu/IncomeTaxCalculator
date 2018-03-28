using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment_GaryQian.BusinessLogic;
using Payment_GaryQian.Model;

namespace UnitTestProject
{
    [TestClass]
    public class PaymentUnitTest
    {
        [TestMethod]
        public void TestEemployeePayment()
        {
            EmployeeDetail employeeMember1 = new EmployeeDetail("Tom", "Ford", "92000", "9.5%", "20 Aprial - 20 May");
            ManipulateOutput test1 = new ManipulateOutput(employeeMember1);
            EmployeeOutputDetail employeePaymentOutput = test1.CalculatePayment();
            Assert.AreEqual("Tom Ford", employeePaymentOutput.Name);
            Assert.AreEqual("5861", employeePaymentOutput.NetIncome);
           
        }
        [TestMethod]
        public void TestIncomeFormula()
        {
           Assert.AreEqual( 11250,IncomeTaxFormula.GetGrossMonthlySalary(135000));
            Assert.AreEqual(1069, IncomeTaxFormula.GetMonthlySuper(11250,"9.5%"));
        }
    }
}
