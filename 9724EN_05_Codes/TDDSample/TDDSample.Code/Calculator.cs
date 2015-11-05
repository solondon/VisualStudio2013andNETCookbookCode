using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDSample.Code
{
    public class Calculator
    {
        public double Divide(double p1, double p2)
        {
            if (p1 == 0 || p2 == 0)
                return 0;

            return p1 / p2;
        }
    }
}
