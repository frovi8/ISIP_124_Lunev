using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int count = ReadOperationsCount();

            string[] names = new string[count];
            double[] amounts = new double[count];
        }

        static int ReadOperationsCount()
        {
            int count;
            bool isValid;

            do
            {
                Console.Write("Введите количество операций (от 2 до 40): ");
                string input = Console.ReadLine();

                isValid = int.TryParse(input, out count) && count >= 2 && count <= 40;

                if (!isValid)
                {
                    Console.WriteLine("Некорректное значение. Введите целое число от 2 до 40.");
                }
            }
            while (!isValid);

            return count;
        }
    }
}