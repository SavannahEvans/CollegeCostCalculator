using System;
using System.Globalization;

namespace CollegeCostCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                if (args.Length == 1)
                {
                    CostCalculator calculator = new CostCalculator();
                    Console.WriteLine(calculator.GetCost(args[0]).ToString("C2"));
                }
                else if (args.Length > 1)
                {
                    CostCalculator calculator = new CostCalculator();
                    bool room = bool.Parse(args[1]);
                    Console.WriteLine(calculator.GetCost(args[0], room).ToString("C2"));
                }
                else
                {
                    Console.Error.WriteLine("Error: College name is required.");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}
