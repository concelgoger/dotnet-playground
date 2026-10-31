namespace Week1Exercises
{
    public class MedicationCabinet(TimeProvider clock)
    {
        private DateOnly Today => DateOnly.FromDateTime(clock.GetLocalNow().DateTime);
        public IReadOnlyList<Medication> GetExpiringSoon(IEnumerable<Medication> items)
        {
            return GetWithStatus(items, MedicationStatus.ExpiresSoon);
        }

        public IReadOnlyList<Medication> GetExpired(IEnumerable<Medication> items)
        {
            return GetWithStatus(items, MedicationStatus.Expired);
        }

        public IReadOnlyList<Medication> GetNotOk(IEnumerable<Medication> items)
        {
            return GetWithStatus(items, MedicationStatus.ExpiresSoon, MedicationStatus.Expired);
        }

        private IReadOnlyList<Medication> GetWithStatus(IEnumerable<Medication> items, params MedicationStatus[] statuses)
        {
            return items.Where(x => statuses.Contains(MedicationRules.GetStatus(x, Today))).ToList();
        }
    }
}
