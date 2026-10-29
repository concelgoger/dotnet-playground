using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises.Demo
{
    public static class LinqPractice
    {
        public static void Run()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            List<Medication> medicineList =
            [
                new("Парацетамол", today.AddDays(60), today.AddDays(-7), 30),
                new("Парацетамол", today.AddDays(200), null, null),
                new("Ибупрофен", today.AddDays(90), null, null),
                new("Нексиум", today.AddDays(25), null, null),
                new("Аспирин", today.AddDays(-5), null, null),
                new("Активированный уголь", today.AddDays(10), null, null)
            ];


            var cabinet = new MedicationCabinet(TimeProvider.System);

            /*
             * Block 1
             */
            var itemsToPayAttation = cabinet.GetNotOk(medicineList);

            var medTuples = itemsToPayAttation.Select(x => (x.Name, x.EffectiveExpiryDate));
            foreach (var item in medTuples)
            {
                Console.WriteLine(item);
            }

            var anyExpired = medicineList.Any(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Expired);
            Console.WriteLine($"Any expired? {anyExpired}");
            var allValid = medicineList.All(x => x.EffectiveExpiryDate > today);
            Console.WriteLine($"All Valid& {allValid}");
            var qtyOk = medicineList.Count(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Ok);
            Console.WriteLine($"Ok items qty = {qtyOk}");

            /*
             * Block 2
             */
            var sortedList = medicineList.OrderBy(x => x.EffectiveExpiryDate).ThenBy(x => x.Name).ToList();
            var groupedList = medicineList.GroupBy(x => MedicationRules.GetStatus(x, today))
                                            .Select(y => new { Status = y.Key, Count = y.Count() }).ToList();
            foreach (var item in groupedList)
            {
                Console.WriteLine(item);
            }
            var minEffectiveDateItem = medicineList.MinBy(x => x.EffectiveExpiryDate);
        }
    }
}
