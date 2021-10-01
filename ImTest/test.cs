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
                string[] forDict = file[id].Split();
                words_rus_eng.Add(forDict[0], forDict[1]);
                }
            } catch (Exception) {
                Console.WriteLine("[EROR] выбранный файл не содержит лекскиу, или не правильного формата");
                Console.WriteLine("Файл с лекский ввида [русское слово][пробел][английское слово]");
                startTest.reInit();
            }

            // запуск теста
            exam(words_rus_eng);
        }
        public void exam(Dictionary<string, string> words)
        {
            // начало
            Console.WriteLine("Начнём тест");
            Console.WriteLine("Начните вводить перевод русских слов на английском");
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();

            // ввод значений
            Dictionary<string, string> result = new Dictionary<string, string>();
            Console.WriteLine();
            foreach (var word in words)
            {
                Console.Write($"{word.Key} : ");
                string answer = Console.ReadLine(); // проверка на правильность ответа
                if (answer == word.Value) // запись для сводки
                {
                    result.Add($"{word.Key} - {answer}", "correct");
                } else {
                    result.Add($"{word.Key} - {answer} {word.Value}", "uncorrect");
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
