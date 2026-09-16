using System;
using System.Collections.Generic;

namespace ShopInventory
{
    enum Category
    {
        Продукты,
        Электроника,
        Одежда
    }

    class Product
    {
        static int nextCode = 1;

        public int Code;
        public string Name;
        public decimal Price;
        public int Quantity;
        public Category Category;

        public Product(string name, decimal price, int quantity, Category category)
        {
            Code = nextCode;
            nextCode++;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public void PrintInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Код: " + Code);
            Console.WriteLine("Название: " + Name);
            Console.WriteLine("Цена: " + Price + " руб.");
            Console.WriteLine("Количество: " + Quantity);
            Console.WriteLine("В наличии: " + (Quantity > 0 ? "да" : "нет"));
            Console.WriteLine("Категория: " + Category);
            Console.WriteLine();
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; 

            FillTestData();
            RunMenu();
        }

        static void FillTestData()
        {
            products.Add(new Product("Хлеб", 45.50m, 30, Category.Продукты));
            products.Add(new Product("Молоко", 89.90m, 20, Category.Продукты));
            products.Add(new Product("Наушники", 1500.00m, 10, Category.Электроника));
            products.Add(new Product("Футболка", 799.00m, 15, Category.Одежда));
            products.Add(new Product("Смартфон", 25990.00m, 5, Category.Электроника));
        }

        static void RunMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Учёт товаров в магазине");
                Console.WriteLine("1 - Показать все товары");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите команду: ");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда, попробуйте снова");
                        break;
                }
            }
        }

        static void ShowAllProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст");
                return;
            }

            foreach (Product p in products)
            {
                p.PrintInfo();
            }
        }
    }
}