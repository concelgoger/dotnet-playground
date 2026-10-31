namespace Week1Exercises
{
    public static class MedicationRules
    {
        public static MedicationStatus GetStatus(Medication m, DateOnly today)
        {
            int daysLeft = m.EffectiveExpiryDate.DayNumber - today.DayNumber;

            return daysLeft switch
            {
                < 0 => MedicationStatus.Expired,
                >= 0 and <= 30 => MedicationStatus.ExpiresSoon,
                > 30 => MedicationStatus.Ok

            };
        }
    }
}
