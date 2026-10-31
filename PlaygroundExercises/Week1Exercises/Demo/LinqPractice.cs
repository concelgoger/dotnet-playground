namespace Week1Exercises.Demo
{
    public static class LinqPractice
    {
        private static void Print<T>(string title, IEnumerable<T> items)
        {
            Console.WriteLine($"{title}:");
            foreach (var item in items)
                Console.WriteLine($"  {item}");
        }
        public static void Run()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            FilterAndProject(CreateMedicines(today), today);
            SortAndGroup(CreateMedicines(today), today);
            Materialize(CreateMedicines(today), today);
        }
        private static List<Medication> CreateMedicines(DateOnly today) =>
        [
            new("Парацетамол", today.AddDays(60), today.AddDays(-7), 30),
            new("Парацетамол", today.AddDays(200), null, null),
            new("Ибупрофен", today.AddDays(90), null, null),
            new("Нексиум", today.AddDays(25), null, null),
            new("Аспирин", today.AddDays(-5), null, null),
            new("Активированный уголь", today.AddDays(10), null, null)
        ];

        private static void FilterAndProject(List<Medication> medicines, DateOnly today)
        {
            /* блок 1 */
            var itemsToPayAttation = medicines
                .Where(x => MedicationRules.GetStatus(x, today) is MedicationStatus.Expired or MedicationStatus.ExpiresSoon);

            var medTuples = itemsToPayAttation.Select(x => (x.Name, x.EffectiveExpiryDate));
            Print("Tuples", medTuples);

            var anyExpired = medicines.Any(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Expired);
            Console.WriteLine($"Any expired? {anyExpired}");

            var allValid = medicines.All(x => x.EffectiveExpiryDate > today);
            Console.WriteLine($"All Valid& {allValid}");

            var qtyOk = medicines.Count(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Ok);
            Console.WriteLine($"Ok items qty = {qtyOk}");
        }
        private static void SortAndGroup(List<Medication> medicines, DateOnly today)
        {
            /* блок 2 */
            var sortedList = medicines
                                .OrderBy(x => x.EffectiveExpiryDate)
                                .ThenBy(x => x.Name)
                                .ToList();

            var groupedList = medicines
                                .GroupBy(x => MedicationRules.GetStatus(x, today))
                                .Select(y => new { Status = y.Key, Count = y.Count() })
                                .ToList();
            Print("GroupedList", groupedList);
            var minEffectiveDateItem = medicines
                                .MinBy(x => x.EffectiveExpiryDate);
        }
        private static void Materialize(List<Medication> medicines, DateOnly today)
        {
            /* блок 3 */
            var expiresSoonQuery = medicines
                                    .Where(x => MedicationRules.GetStatus(x, today) == MedicationStatus.ExpiresSoon);

            var expiresSoonList = medicines
                                    .Where(x => MedicationRules.GetStatus(x, today) == MedicationStatus.ExpiresSoon)
                                    .ToList();

            medicines.Add(new("Називин", today.AddDays(24), null, null));
            medicines.Add(new("Хлоргексидин", today.AddDays(24), null, null));

            Console.WriteLine($"expiresSoonQuery count: {expiresSoonQuery.Count()}");
            Console.WriteLine($"expiresSoonList count: {expiresSoonList.Count()}");

            //cath Exception with duplicates while creating Dictionary
            try
            {
                medicines.ToDictionary(x => x.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
            }

            var byNameLookup = medicines.ToLookup(x => x.Name);

            Print("LookUp by Name", byNameLookup["Парацетамол"]);
        }
    }
}
