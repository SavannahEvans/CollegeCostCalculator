using System;
using NUnit.Framework;

namespace CollegeCostCalculator.Tests
{
    public class CostCalculatorTests
    {
        private CostCalculator calculator;

        [OneTimeSetUp]
        public void init()
        {
            calculator = new CostCalculator();
        }

        [TestCase("Arizona State University", 11224, 11224 + 11841)]    // All values
        [TestCase("Agnes Scott College", 54007, 54007 + 12449)]         // Unknown character
        [TestCase("Amherst College", 70260, 70260)]                     // Missing out-of-state
        [TestCase("Clemson University", 36058, 36058 +9595)]            // Missing in-state
        public void TestAllCases(string name, Decimal cost, Decimal costPlus)
        {
            Assert.AreEqual(cost, calculator.GetCost(name, false));
            Assert.AreEqual(costPlus, calculator.GetCost(name, true));
        }
    }
}