using System;
using System.Linq;
using System.Threading;

namespace ПОП_лаб1
{
    internal class Program
    {
        static int threadCount;
        static int[] times;
        static bool[] canStop;


        static void Main(string[] args)
        {

            Console.Write("Введіть кількість потоків: ");
            threadCount = Convert.ToInt32(Console.ReadLine());

            times = new int[threadCount];
            canStop = new bool[threadCount];
            Thread[] threads = new Thread[threadCount];

            Console.WriteLine($"Введіть час роботи {threadCount} потоків в секундах: ");
            string[] line = Console.ReadLine().Split();
            int[] indexes = new int[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                times[i] = Convert.ToInt32(line[i]) * 1000;
                indexes[i] = i;
            }

            for (int i = 0; i < threadCount - 1; i++)
            {
                for (int j = 0; j < threadCount - i - 1; j++)
                {
                    if (times[j] > times[j + 1])
                    {
                        int tempTime = times[j];
                        times[j] = times[j + 1];
                        times[j + 1] = tempTime;

                        int tempIndex = indexes[j];
                        indexes[j] = indexes[j + 1];
                        indexes[j + 1] = tempIndex;
                    }
                }
            }

            for (int i = 0; i < threadCount; i++)
            {
                int index = i;
                threads[i] = new Thread(() => CalculateSum(indexes[index], times[index]));
                threads[i].Start();
            }

            Thread stopper = new Thread(() => TimeController(indexes));
            stopper.Start();
        }

        static void CalculateSum(int threadNumber, int time)
        {
            long sum = 0;
            while (!canStop[threadNumber])
            {
                sum++;
            }
            Console.WriteLine($"Thread {threadNumber+1}: sum = {sum}, time: {time/1000} сек");
        }

        static void TimeController(int[] sortedIndexes)
        {
            int prevTime = 0;

            for (int i = 0; i < threadCount; i++)
            {
                int index = sortedIndexes[i];
                int currentTime = times[i];

                int sleepTime = currentTime - prevTime;

                Thread.Sleep(sleepTime);
                canStop[index] = true;

                prevTime = currentTime;
            }
        }
    }
}
