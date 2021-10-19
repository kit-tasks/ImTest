using System;
using System.Collections.Generic;
using System.Linq;

namespace ImTest
{
    class Test
    {
        StartTest startTest = new StartTest();
        private Dictionary<string, string> Init(string path, Dictionary<string, string>words_rus_eng)
        {
            // инициализация
            string[] file = System.IO.File.ReadAllLines(path);
            
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
                // запуск теста
            } catch (Exception) {
                Console.WriteLine("[EROR] выбранный файл не содержит лекскиу, или не правильного формата");
                Console.WriteLine("Файл с лекский ввида [русское слово]_[английское слово]");
                Console.WriteLine("Пример: читать_read");
            }
            return words_rus_eng;
        }
        private void Exam(Dictionary<string, string> words)
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
            startTest = new StartTest();
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
        }
        private void End()
        {
            startTest.ReInit();
        }

        public void MainTest(string Path)
        {
            Dictionary <string, string> words = new Dictionary<string, string>();
            Dictionary<string, string> init = Init(Path, words);
            if (init.Count != 0)
            {
                Exam(init);
            }
            End();
        }
    }
}
