using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Week1Exercises.Demo;

public class AsyncDemo
{
    public static async Task RunAsync()
    {
        await Block1();
    }

    public static async Task Block1()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        Console.WriteLine(Environment.CurrentManagedThreadId);

        FakeMedicationStore store = new FakeMedicationStore();
        await store.AddAsync(new Medication("Ибупрофен", today.AddDays(60), today.AddDays(-7), 30));
        await store.AddAsync(new Medication("Парацетамол", today.AddDays(200), null, null));

        var medicineList = await store.GetAllAsync();
        foreach (var item in medicineList)
            Console.WriteLine(item);

        stopWatch.Stop();

        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);

        Console.WriteLine(Environment.CurrentManagedThreadId);

    }
}
