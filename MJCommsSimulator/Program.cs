using System;

namespace MJCommsSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            MegaJolt mj = new MegaJolt("COM3");
            mj.Run();

            Console.ReadLine();
        }
    }
}
