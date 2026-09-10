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
                        PrintOperations(names, amounts);
                        break;
                    case "2":
                        PrintStatistics(amounts);
                        break;
                    case "3":
                        BubbleSortByPrice(names, amounts);
                        Console.WriteLine("Список отсортирован по цене (по возрастанию).");
                        PrintOperations(names, amounts);
                        break;
                        break;
                    case "4":
                        ConvertCurrency(amounts);
                        break;
                    case "5":
                        SearchByName(names, amounts);
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

        static void ConvertCurrency(double[] amounts)
                {
                    string[] currencyNames = { "USD (доллар США)", "EUR (евро)", "CNY (юань)" };
                    double[] currencyRates = { 90.0, 98.0, 12.5 };

                    Console.WriteLine("\n--- Конвертация валюты ---");
                    Console.WriteLine("1. Ввести курс вручную");
                    Console.WriteLine("2. Выбрать валюту из списка");
                    Console.Write("Выберите способ: ");
                    string method = Console.ReadLine();

                    double rate = 0;
                    bool rateChosen = false;
                    string currencyLabel = "";

                    if (method == "1")
                    {
                        bool isValid = false;
                        while (!isValid)
                        {
                            Console.Write("Введите курс (сколько рублей за 1 единицу валюты): ");
                            string input = Console.ReadLine();
                            isValid = double.TryParse(input, out rate) && rate > 0;

                            if (!isValid)
                                Console.WriteLine("Курс должен быть положительным числом.");
                        }
                        currencyLabel = "выбранной валюте";
                        rateChosen = true;
                    }
                    else if (method == "2")
                    {
                        for (int i = 0; i < currencyNames.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {currencyNames[i]} (курс: {currencyRates[i]} руб.)");
                        }

                        Console.Write("Выберите номер валюты: ");
                        string input = Console.ReadLine();
                        int index;

                        if (int.TryParse(input, out index) && index >= 1 && index <= currencyNames.Length)
                        {
                            rate = currencyRates[index - 1];
                            currencyLabel = currencyNames[index - 1];
                            rateChosen = true;
                        }
                        else
                        {
                            Console.WriteLine("Неверный номер валюты.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неизвестный способ.");
                    }

                    if (rateChosen)
                    {
                        Console.WriteLine($"\n--- Суммы в {currencyLabel} ---");
                        for (int i = 0; i < amounts.Length; i++)
                        {
                            double converted = amounts[i] / rate;
                            Console.WriteLine($"{i + 1}. {converted:0.00}");
                        }
                        Console.WriteLine("(Исходные суммы в рублях не изменены — это только отображение.)");
                    }
              
        }
        static void PrintOperations(string[] names, double[] amounts)
        {
            Console.WriteLine("\n--- Список трат ---");
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} — {amounts[i]:0.00} руб.");
            }
        }
        static void PrintStatistics(double[] amounts)
        {
            double sum = 0;
            double max = amounts[0];
            double min = amounts[0];

            for (int i = 0; i < amounts.Length; i++)
            {
                sum += amounts[i];

                if (amounts[i] > max)
                    max = amounts[i];

                if (amounts[i] < min)
                    min = amounts[i];
            }

            double average = sum / amounts.Length;

            Console.WriteLine("\n--- Статистика ---");
            Console.WriteLine($"Сумма:     {sum:0.00} руб.");
            Console.WriteLine($"Среднее:   {average:0.00} руб.");
            Console.WriteLine($"Максимум:  {max:0.00} руб.");
            Console.WriteLine($"Минимум:   {min:0.00} руб.");
        }
        static void BubbleSortByPrice(string[] names, double[] amounts)
        {
            int n = amounts.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (amounts[j] > amounts[j + 1])
                    {
                        Swap(ref amounts[j], ref amounts[j + 1]);
                        Swap(ref names[j], ref names[j + 1]);
                    }
                }
            }
        }

        static void Swap(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }

        static void Swap(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }
        static void SearchByName(string[] names, double[] amounts)
        {
            Console.Write("\nВведите название или часть названия для поиска: ");
            string query = Console.ReadLine().Trim().ToLower();

            bool found = false;

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].ToLower().Contains(query))
                {
                    if (!found)
                    {
                        Console.WriteLine("--- Результаты поиска ---");
                        found = true;
                    }
                    Console.WriteLine($"{i + 1}. {names[i]} — {amounts[i]:0.00} руб.");
                }
            }

            if (!found)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
    }
}