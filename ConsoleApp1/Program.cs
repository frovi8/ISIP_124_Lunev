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

            ReadOperations(names, amounts);
            RunMenu(names, amounts);
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

        static void ReadOperations(string[] names, double[] amounts)
        {
            Console.WriteLine("\nВводите траты по шаблону: Название услуги или товара;Количество денег");
            Console.WriteLine("Пример: Влажные салфетки \"Лента\";235\n");

            for (int i = 0; i < names.Length; i++)
            {
                bool isValid = false;

                while (!isValid)
                {
                    Console.Write($"Операция {i + 1}/{names.Length}: ");
                    string line = Console.ReadLine();

                    int separatorIndex = line.IndexOf(';');

                    if (separatorIndex < 0)
                    {
                        Console.WriteLine("Не найден разделитель ';'. Повторите ввод.");
                        continue;
                    }

                    string name = line.Substring(0, separatorIndex).Trim();
                    string amountPart = line.Substring(separatorIndex + 1).Trim();

                    double amount;
                    isValid = double.TryParse(amountPart, out amount) && name.Length > 0 && amount >= 0;

                    if (!isValid)
                    {
                        Console.WriteLine("Некорректная запись. Проверьте название и сумму (сумма >= 0).");
                        continue;
                    }

                    names[i] = name;
                    amounts[i] = amount;
                }
            }
        }
         static void RunMenu(string[] names, double[] amounts)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== МЕНЮ =====");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
                Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Выход из программы.");
                        break;
                    default:
                        Console.WriteLine("Неизвестный пункт меню. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}