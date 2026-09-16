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
                Console.WriteLine("2 - Добавить товар");
                Console.WriteLine("3 - Удалить товар");
                Console.WriteLine("4 - Заказать поставку товара");
                Console.WriteLine("5 - Продать товар");
                Console.WriteLine("6 - Поиск товара");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите команду: ");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        RemoveProduct();
                        break;
                    case "4":
                        OrderSupply();
                        break;
                    case "5":
                        SellProduct();
                        break;
                    case "6":
                        SearchProduct();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда, попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowAllProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст.");
                return;
            }

            foreach (Product p in products)
            {
                p.PrintInfo();
            }
        }

        static string ReadNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        static decimal ReadNonNegativeDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out value) && value >= 0)
                    return value;

                Console.WriteLine("Ошибка: введите неотрицательное число.");
            }
        }

        static int ReadNonNegativeInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && value >= 0)
                    return value;

                Console.WriteLine("Ошибка: введите целое неотрицательное число.");
            }
        }

        static Category ReadCategory()
        {
            Console.WriteLine("Выберите категорию:");
            Console.WriteLine("1 - Продукты");
            Console.WriteLine("2 - Электроника");
            Console.WriteLine("3 - Одежда");

            while (true)
            {
                Console.Write("Ваш выбор: ");
                string input = Console.ReadLine();

                if (input == "1") return Category.Продукты;
                if (input == "2") return Category.Электроника;
                if (input == "3") return Category.Одежда;

                Console.WriteLine("Ошибка: введите 1, 2 или 3.");
            }
        }

        static void AddProduct()
        {
            Console.WriteLine("Добавление нового товара");

            string name = ReadNonEmptyString("Введите название товара: ");
            decimal price = ReadNonNegativeDecimal("Введите цену товара: ");
            int quantity = ReadNonNegativeInt("Введите количество товара: ");
            Category category = ReadCategory();

            Product newProduct = new Product(name, price, quantity, category);
            products.Add(newProduct);

            Console.WriteLine("Товар добавлен. Код товара: " + newProduct.Code);
        }

        static Product FindByCode(int code)
        {
            foreach (Product p in products)
            {
                if (p.Code == code)
                    return p;
            }
            return null;
        }

        static void RemoveProduct()
        {
            Console.WriteLine("Удаление товара");
            int code = ReadNonNegativeInt("Введите код товара для удаления: ");

            Product found = FindByCode(code);
            if (found == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }

            products.Remove(found);
            Console.WriteLine("Товар \"" + found.Name + "\" удалён.");
        }

        static void OrderSupply()
        {
            Console.WriteLine("Заказ поставки товара");
            int code = ReadNonNegativeInt("Введите код товара: ");

            Product found = FindByCode(code);
            if (found == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }

            int amount = ReadNonNegativeInt("Введите количество для поставки: ");
            found.Quantity += amount;

            Console.WriteLine("Поставка оформлена. Новое количество: " + found.Quantity);
        }

        static void SellProduct()
        {
            Console.WriteLine("Продажа товара");
            int code = ReadNonNegativeInt("Введите код товара: ");

            Product found = FindByCode(code);
            if (found == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }

            if (found.Quantity == 0)
            {
                Console.WriteLine("Товара \"" + found.Name + "\" нет в наличии.");
                return;
            }

            int amount = ReadNonNegativeInt("Введите количество для продажи: ");

            if (amount > found.Quantity)
            {
                Console.WriteLine("Недостаточно товара на складе. В наличии: " + found.Quantity);
                return;
            }

            found.Quantity -= amount;
            decimal total = found.Price * amount;

            Console.WriteLine("Продано " + amount + " шт. товара \"" + found.Name + "\" на сумму " + total + " руб.");
        }

        static void SearchProduct()
        {
            Console.WriteLine("Поиск товара");
            Console.WriteLine("1 - По коду");
            Console.WriteLine("2 - По названию");
            Console.WriteLine("3 - По категории");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            List<Product> results = new List<Product>();

            if (choice == "1")
            {
                int code = ReadNonNegativeInt("Введите код: ");
                Product found = FindByCode(code);
                if (found != null)
                    results.Add(found);
            }
            else if (choice == "2")
            {
                Console.Write("Введите название (или часть названия): ");
                string name = Console.ReadLine();

                foreach (Product p in products)
                {
                    if (p.Name.ToLower().Contains(name.ToLower()))
                        results.Add(p);
                }
            }
            else if (choice == "3")
            {
                Category category = ReadCategory();

                foreach (Product p in products)
                {
                    if (p.Category == category)
                        results.Add(p);
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор");
                return;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("Ничего не найдено");
            }
            else
            {
                foreach (Product p in results)
                {
                    p.PrintInfo();
                }
            }
        }
    }
}