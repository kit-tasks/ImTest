using System;
using System.Collections.Generic;
using System.Linq;

namespace ImTest
{
    class test
    {
        startTest startTest = new startTest();
        public void init(string path)
        {
            // инициализация
            string[] file = System.IO.File.ReadAllLines(path);
            Dictionary<string, string> words_rus_eng = new Dictionary<string, string>();
            
            // получение рандомных значений
            var rand = new Random();
            var nums = Enumerable.Range(0, file.Length).OrderBy(n => rand.Next()).ToArray();
            try // проверка лексического файла на правильность ввода
            {
                foreach (int id in nums)
                {
                string[] forDict = file[id].Split("_");
                words_rus_eng.Add(forDict[0], forDict[1]);
                }
            } catch (Exception) {
                Console.WriteLine("[EROR] выбранный файл не содержит лекскиу, или не правильного формата");
                Console.WriteLine("Файл с лекский ввида [русское слово]_[английское слово]");
                Console.WriteLine("Пример: читать_read");
                startTest.reInit();
            }

            // запуск теста
            exam(words_rus_eng);
        }
        public void exam(Dictionary<string, string> words)
        {
            Console.WriteLine($"{words.Count} - слов в лексике");
            Console.WriteLine("Сколько слов хотите в тесте?");
            bool notGood;
            int n = 1;
            do
            {
                try
                {
                    n = int.Parse(Console.ReadLine());
                    notGood = false;
                }
                catch
                {
                    Console.WriteLine("Вы ввели не число");
                    notGood = true;
                }
            } while (notGood);
                
                
            // начало
            Console.WriteLine("Начнём тест");
            Console.WriteLine("Начните вводить перевод русских слов на английском");
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();

            // ввод значений
            Dictionary<string, string> result = new Dictionary<string, string>();
            Console.WriteLine();
            int counter = 0;
            foreach (var word in words)
            {
                counter += 1;
                Console.Write($"{word.Key} : ");
                string answer = Console.ReadLine(); // проверка на правильность ответа
                if (answer == word.Value) // запись для сводки
                {
                    result.Add($"{word.Key} - {answer}", "correct");
                } else {
                    result.Add($"{word.Key} - {answer} {word.Value}", "uncorrect");
                }   
                if (counter == n)
                {
                    break;
                }
            }

            // конец
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            foreach (string line in result.Keys)
            {
                if (result[line] == "correct")
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                } else {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine(line);
            }
            startTest.reInit();
        }
    }
}
