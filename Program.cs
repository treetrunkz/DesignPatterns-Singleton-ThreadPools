using System;
using System.Threading;
using System.Diagnostics;
public class Pooler
{
    public int id;
    public Pooler(int _id)
    {
        this.id = _id;
    }
}
class PoolQueue
{
    public int QueueLength;
    public PoolQueue()
    {
        QueueLength = 0;
    }
    public void Create(Pooler software)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(Consume), software);
        QueueLength++;
    }
    public void Consume(Object thread)
    {
        Console.WriteLine("Thread {0} consumes {1}",
        Thread.CurrentThread.GetHashCode(),
        ((Pooler)thread).id);
        Thread.Sleep(100);
        QueueLength--;
    }
    public static void Main(String[] args)
    {
        PoolQueue obj = new PoolQueue();
        for (int i = 0; i < 100; i++) { obj.Create(new Pooler(i)); }
        Console.WriteLine("Thread Pooled @ {0}", Thread.CurrentThread.GetHashCode());
        while (obj.QueueLength != 0)
        {
            Thread.Sleep(1000);
        }
        Console.Read();
    }
}