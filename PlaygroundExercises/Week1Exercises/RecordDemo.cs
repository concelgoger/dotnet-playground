using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises
{
    public class RecordDemo
    {
        public static void Run()
        {
            //OpenedOn = null, DaysValidAfterOpening = null
            var a = new Medication("Парацетамол", new DateOnly(2026, 5, 1), null, null);
            var b = new Medication("Парацетамол", new DateOnly(2026, 5, 1), null, null);

            bool equalityValue = a == b;
            bool equalityRef = ReferenceEquals(a, b);

            Console.WriteLine(a);

            var c = a with { OpenedOn = new DateOnly(2026, 10, 1) };

            Console.WriteLine(a == c);
            Console.WriteLine(c);

            //OpenedOn заполнен, DaysValidAfterOpening = null
            var d = new Medication("Парацетамол", new DateOnly(2026, 5, 1), new DateOnly(2026, 4, 1), null);
            Console.WriteLine(d);

            //Вскрыт, срок после вскрытия короче, чем до ExpiryDate
            var e = new Medication("Парацетамол", new DateOnly(2026, 5, 1), new DateOnly(2026, 4, 1), 1);
            Console.WriteLine(e);

            //Вскрыт, срок после вскрытия длиннее, чем до ExpiryDate
            var f = new Medication("Парацетамол", new DateOnly(2026, 5, 1), new DateOnly(2026, 4, 1), 90);
            Console.WriteLine(f);
        }
    }
}
