namespace ПОП_лаб1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt16(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                int threadNumber = i+1;
                Thread thread = new Thread(() => CalculateSum(threadNumber));
                thread.Start();
            }

            Thread controller = new Thread(TimeController);
            controller.Start();
        }
        static void CalculateSum(int threadNumber)
        {
            long sum = 0;
            while (!canStop)
            {
                sum++;
            }
            Console.WriteLine("Thread " + threadNumber + ": sum - " + sum);
        }

        static bool canStop = false;

        static void TimeController()
        {
            Thread.Sleep(50 * 1000);
            canStop = true;
        }
    }
}
