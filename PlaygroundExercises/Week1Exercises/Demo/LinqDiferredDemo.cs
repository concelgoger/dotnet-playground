using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises.Demo
{
    /// <summary>
    /// Class to test deferred request in Linq
    /// </summary>
    public static class LinqDeferredDemo
    {
        public static void Run()
        {
            RunWhere();
            RunWhereWithList();
        }
        public static void RunWhere()
        {
            var numbers = new List<int> { 1, 2, 3 };

            var query = numbers.Where(n =>
            {
                Console.WriteLine($"проверяю {n}");
                return n > 1;
            });

            Console.WriteLine("запрос создан");
            numbers.Add(4);

            foreach (var n in query) Console.WriteLine($"получил {n}");
            foreach (var n in query) Console.WriteLine($"снова {n}");
        }

        public static void RunWhereWithList()
        {
            var numbers = new List<int> { 1, 2, 3 };

            var query = numbers.Where(n =>
            {
                Console.WriteLine($"проверяю {n}");
                return n > 1;
            }).ToList();

            Console.WriteLine("запрос создан");
            numbers.Add(4);

            foreach (var n in query) Console.WriteLine($"получил {n}");
            foreach (var n in query) Console.WriteLine($"снова {n}");
        }
    }
}
