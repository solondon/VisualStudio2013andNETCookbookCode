using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstDebuggingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass tclass = new TestClass(20, 30);
            tclass.PrintMessage();
        }
        //static void Main(string[] args)
        //{
        //    Thread.CurrentThread.Name = "Main Thread";
        //    Thread t1 = new Thread(new ThreadStart(Run));
        //    t1.Name = "Thread 1";
        //    Thread t2 = new Thread(new ThreadStart(Run));
        //    t2.Name = "Thread 2";
        //    t1.Start();
        //    t2.Start();
        //    Run();
        //}
        //static void Main(string[] args)
        //{
        //    Task task_a = Task.Factory.StartNew(() => DoSomeWork(10000));
        //    Task task_b = Task.Factory.StartNew(() => DoSomeWork(5000));
        //    Task task_c = Task.Factory.StartNew(() => DoSomeWork(1000));

        //    Task.WaitAll(task_a, task_b, task_c);

        //}
        static void DoSomeWork(int time)
        {
            Task.Delay(time);
        }

        static void Run()
        {
            Console.WriteLine("hello {0}!", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
        }

    }
}
