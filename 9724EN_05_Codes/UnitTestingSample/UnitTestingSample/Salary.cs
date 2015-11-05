using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingSample
{
    public class Salary
    {
        public const string ARGUMENTLESSTHAN20 = "Cannot calculate salary of a person below age 20";
        public const string ARGUMENTGREATERTHAN85 = "You are too old to be valid for pension";
        string name;
        int age;

        public Salary(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        // For simplicity we define salary by age.
        public float GetSalary()
        {
            int baseSalary = 20000;

            int ageSeed = this.age - 20;

            if (ageSeed < 20)
                throw new ArgumentOutOfRangeException(ARGUMENTLESSTHAN20);

            if (this.age > 85)
                throw new ArgumentOutOfRangeException(ARGUMENTGREATERTHAN85);

            float pensionFraction = 1.0f;
            if (this.age > 60) //retired
            {
                ageSeed = 40; //(60 - 20)
                pensionFraction = 0.5f;
            }
            float actualSalary = (baseSalary * ageSeed) * pensionFraction;

            return actualSalary;
        }

        static string GetName(Salary oSalary)
        {
            return oSalary.name;
        }

    }
}
