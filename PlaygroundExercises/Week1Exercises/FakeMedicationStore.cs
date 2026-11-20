using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Exercises;

public class FakeMedicationStore
{
    private readonly List<Medication> _items = [];

    public async Task AddAsync(Medication medication, CancellationToken ct = default)
    {
        Console.WriteLine("AddAsync: до await");
        await Task.Delay(1000, ct);
        Console.WriteLine("AddAsync: после await");
        _items.Add(medication);
    }

    public async Task<IReadOnlyList<Medication>> GetAllAsync(CancellationToken ct = default)
    {
        await Task.Delay(1000, ct);
        return _items.ToList();
    }
}
