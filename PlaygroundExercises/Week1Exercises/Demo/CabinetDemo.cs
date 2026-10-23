using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises.Demo
{
    public class CabinetDemo
    {
        public static void Run()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            List<Medication> a = [new Medication("Парацетамол", today.AddDays(60), today.AddDays(-7), 30),
                                        new Medication("Ибупрофен", today.AddDays(90), null, null),
                                        new Medication("Нексиум", today.AddDays(25), null, null)];
            // список a через collection expression: [ ... ]
            List<Medication> b = [new Medication("Парацетамол", today.AddDays(200), null, null)];

            // all через spread: [..a, ..b]
            List<Medication> aSpread_b = [.. a, .. b];


            var cabinet = new MedicationCabinet(TimeProvider.System);
            var expiresItems = cabinet.GetExpiringSoon(aSpread_b);

            foreach (var item in expiresItems)
            {
                Console.WriteLine(item);
            }
        }
    }
}
