using System;
using System.Text;

namespace pr3
{
    internal class Program
    {
        static string? Text()
        {
            while (true)
            {
                Console.Write("Введите текст (> 100 символов): ");
                string? text = Console.ReadLine();
                Console.WriteLine();

                if (text == "esc")
                {
                    return null;
                }

                if (text?.Length > 100)
                {
                    return text;
                }
                else
                {
                    Console.WriteLine("Количество символов должно быть > 100");
                }
            }
            // По реке плывет кораблик. Он плывет издалека. На кораблике четыре Очень храбрых моряка. У них ушки на макушке, У них длинные хвосты, И страшны им только кошки, Только кошки да коты
        }

        static void Main(string[] args)
        {
            Start();
        }

        static List<string> list = new List<string>();

        static void Start()
        {
            bool running = true;

            while (running)
            {

                Console.WriteLine("Выберите опцию: ");
                Console.WriteLine("Вввести текст - 1");
                Console.WriteLine("Просмотр истории статистики - 2");
                Console.WriteLine("Выход - 0");
                Console.WriteLine();
                Console.Write("Ваш выбор: ");

                string? input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        string checkText = Text();
                        if (checkText != null)
                        {
                            string his = Build(checkText);
                            list.Add(his);
                            Console.WriteLine(his);
                            Console.WriteLine("Нажмите любую клавишу для выхода");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                        }
                        break;
                    case "2":
                        if (list.Count != 0)
                            foreach (string his in list) { Console.WriteLine(his); }
                        else
                            Console.WriteLine("Сначала введите текст!");
                        Console.WriteLine("Нажмите любую клавишу для выхода");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод (только 1, 2 или 0)");
                        break;
                }
            }
        }
        static string Build(string text)
        {
            StringBuilder sb = new StringBuilder();
            string t = text.Substring(0, 15);
            sb.AppendLine($"{t}...");
            var (v, c) = VowelsAndConsonants(text);
            sb.AppendLine($"Количество слов: {WordCount(text)}");
            sb.AppendLine($"Самое короткое слово: {MinWord(text)}");
            sb.AppendLine($"Количество предложений: {Sentence(text)}");
            sb.AppendLine($"Количество гласных: {v}, количество согласных: {c}");
            sb.AppendLine($"Самое длинное слово: {MaxWord(text)}");
            sb.AppendLine($"Частота встречаемости букв:\n{Staristic(text)}");

            return sb.ToString();
        }

        static int WordCount(string text)
        {
            string[] splitText = Symbols.Wrods(text);

            return splitText.Length;
        }

        static string MinWord(string text)
        {
            string[] splitText = Symbols.Wrods(text);

            string min = splitText[0];

            for (int i = 1; i < splitText.Length; i++)
            {
                if (splitText[i].Length < min.Length)
                {
                    min = splitText[i];
                }
            }
            return min;
        }

        static string MaxWord(string text)
        {
            string[] splitText = Symbols.Wrods(text);

            string max = splitText[0];

            for (int i = 1; i < splitText.Length; i++)
            {
                if (splitText[i].Length > max.Length)
                {
                    max = splitText[i];
                }
            }
            return max;
        }

        static int Sentence(string text)
        {
            string[] splitText = Symbols.Sentence(text);

            return splitText.Length;
        }

        static (int countVowels, int countConsonants) VowelsAndConsonants(string text)
        {
            int countVowels = 0;
            int countConsonants = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Symbols.Vowels(text[i]) == true)
                {
                    countVowels++;
                }
                else if (char.IsLetter(text[i]) && text[i] != 'ь' && text[i] != 'ъ')
                {
                    countConsonants++;
                }
            }
            return (countVowels, countConsonants);
        }

        static string Staristic(string text)
        {
            var dictionary = new Dictionary<char, int>();

            var words = Symbols.Wrods(text.ToLower());

            var letters = Symbols.Letters(words);

            foreach (char c in letters)
            {
                if (char.IsLetter(c))
                {
                    if (dictionary.ContainsKey(c))
                    {
                        dictionary[c] += 1;
                    }
                    else
                    {
                        dictionary.Add(c, 1);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            int count = 0;

            foreach (var letter in dictionary)
            {
                count++;

                if (count < 4)
                    sb.Append($"[{letter.Key}]: {letter.Value}  \t");
                else
                {
                    sb.Append($"[{letter.Key}]: {letter.Value}  \n");
                    count = 0;
                }
            }
            return sb.ToString();
        }
    }
}