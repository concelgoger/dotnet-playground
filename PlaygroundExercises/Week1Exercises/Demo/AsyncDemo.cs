using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Week1Exercises.Demo;

public class AsyncDemo
{
    public static async Task RunAsync()
    {
        await RunSequentialAsync();
        await RunWhenAllAsync();
        await RunCancellationAsync();
    }

    public static async Task RunSequentialAsync()
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

        Console.WriteLine($"RunTime {stopWatch.ElapsedMilliseconds} ms");

        Console.WriteLine(Environment.CurrentManagedThreadId);

    }

    public static async Task RunWhenAllAsync()
    {
        FakeMedicationStore store = new FakeMedicationStore();
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        await store.GetAllAsync();
        await store.GetAllAsync();
        await store.GetAllAsync();

        stopWatch.Stop();

        Console.WriteLine($"RunTime {stopWatch.ElapsedMilliseconds} ms");


        Stopwatch stopWatch1 = new Stopwatch();
        stopWatch1.Start();
        Task getStoreData1 = store.GetAllAsync();
        Task getStoreData2 = store.GetAllAsync();
        Task getStoreData3 = store.GetAllAsync();

        await Task.WhenAll(getStoreData1, getStoreData2, getStoreData3);

        stopWatch1.Stop();

        Console.WriteLine($"RunTime {stopWatch1.ElapsedMilliseconds} ms");
    }

    public static async Task RunCancellationAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var store = new FakeMedicationStore();
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
        var sw = Stopwatch.StartNew();

        Console.WriteLine("RunAsync: перед вызовом");
        Task addTask = store.AddAsync(new Medication("Ибупрофен", today.AddDays(60), today.AddDays(-7), 30), cts.Token);   // без await        
        Console.WriteLine("RunAsync: вызов вернулся");

        try
        {
            await addTask;                                // теперь ждём
            Console.WriteLine("RunAsync: задача завершена");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"RunAsync: отменено через {sw.ElapsedMilliseconds} ms");
        }
        
    }
}
