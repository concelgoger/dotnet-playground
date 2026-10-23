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
    }
}
