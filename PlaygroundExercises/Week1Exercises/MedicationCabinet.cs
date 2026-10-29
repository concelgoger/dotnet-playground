using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises
{
    public class MedicationCabinet(TimeProvider clock)
    {
        public IReadOnlyList<Medication> GetExpiringSoon(IEnumerable<Medication> items)
        {
            var today = DateOnly.FromDateTime(clock.GetUtcNow().UtcDateTime);
            return items.Where(x => MedicationRules.GetStatus(x, today) == MedicationStatus.ExpiresSoon).ToList();
        }

        public IReadOnlyList<Medication> GetExpired(IEnumerable<Medication> items)
        {
            var today = DateOnly.FromDateTime(clock.GetUtcNow().UtcDateTime);
            return items.Where(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Expired).ToList();
        }

        public IReadOnlyList<Medication> GetNotOk(IEnumerable<Medication> items)
        {
            var today = DateOnly.FromDateTime(clock.GetUtcNow().UtcDateTime);
            return items.Where(x => MedicationRules.GetStatus(x, today) == MedicationStatus.Expired ||
                                    MedicationRules.GetStatus(x, today) == MedicationStatus.ExpiresSoon).ToList();
        }
    }
}
