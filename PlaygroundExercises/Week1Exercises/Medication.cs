using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises
{
    public record Medication(
        string Name,
        DateOnly ExpiryDate,
        DateOnly? OpenedOn,
        int? DaysValidAfterOpening)
    {
        public DateOnly EffectiveExpiryDate =>
            OpenedOn is { } opened && DaysValidAfterOpening is { } days
                ? (opened.AddDays(days) < ExpiryDate ? opened.AddDays(days) : ExpiryDate)
            : ExpiryDate;
    }
}
