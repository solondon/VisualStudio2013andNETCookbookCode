using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDebuggingSample
{
    public class TestClass
    {
        public int Start { get; set; }
        public int End { get; set; }

        public TestClass(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }
        public int GetSum(int start, int end)
        {
            int sum = 0;
            for (int i = start; i <= end; i++)
            {
                sum += i;
            }
            return sum;
        }

        public string GetMessage()
        {
            int sum = this.GetSum(this.Start, this.End);
            return string.Format("Sum of all numbers between {0} and {1} is {2}", this.Start, this.End, sum);
        }
        public void PrintMessage()
        {
            string message = this.GetMessage();
            Console.WriteLine(message);
            Console.ReadKey(true);
        }

    }
}
