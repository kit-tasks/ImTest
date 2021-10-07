using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

// hello

namespace ImTest
{
    class StartTest
    {
        static Dictionary<string, string> OpenConfig(Dictionary<string, string> config) // доступ тип_вывода название (переменные)
        {
            // чтение, запись файла
            string[] configFile;
            configFile = System.IO.File.ReadAllLines("config.txt");
            foreach (string param in configFile)
            {
                string[] forDict = param.Split();
                config.Add(forDict[0], forDict[1]);
            }
            return config;
        }
        static void ChangeConsole(Dictionary<string, string> config) // void - без вывода
        {
            // чтение конфига
            ConsoleColor ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), config["ForegroundColor"]);
            //   тип        название            тип      класс|конверт  в       тип        словарь     раздел
            ConsoleColor BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), config["BackgroundColor"]);
            // enum - класс для перечислений

            // применение конфигураций
            Console.Title = config["Title"];
            Console.CursorVisible = bool.Parse(config["CursorVisible"]);
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Clear();
        }
        static string FindUnit(string pathOfDirectory)
        {
            // запрос нужного раздела
            DirectoryInfo dir = new DirectoryInfo(pathOfDirectory);
            Console.WriteLine("Номер файла, который хотите открыть для тестирования?");
            int counter = 1;
            Dictionary<int, string> filesForTest = new Dictionary<int, string>();

            // вылавливание ошибок

            try // проверка на конфиг
            {
                foreach (var item in dir.GetFiles())
                {
                    Console.WriteLine($"{counter}. {item.Name}");
                    filesForTest.Add(counter, item.Name);
                    counter += 1;
                }
                try // проверка запроса
                {
                    int req = Convert.ToInt32(Console.ReadLine());
                    try // проверка на наличае файла
                    {
                        string pathOfUnit = filesForTest[req];
                        return pathOfUnit;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Нет такого файла");
                        return FindUnit(pathOfDirectory);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели неверное значение");
                    return FindUnit(pathOfDirectory);
                }
            } catch (Exception) {
                Console.WriteLine("[EROR] отредактируте файл config.txt, укажите нужный путь к разделу с лескикой.");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
                return null;
            }
            
        }
        public void ReInit()
        {
            //инициализация повторного запуска

            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Запустить программу еще раз? y/n");
            string req = Console.ReadLine();
            if (req == "y" || req == "Y")
            { // запуск опять
                Main();
            } else { // закрытие
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        static void Main()
        {
            // объявление перменных
            Dictionary<string, string> config = new Dictionary<string, string>(); // - файл с настройками
            Test test = new Test(); // - ссылка на объект test
            string pathOfUnit; // путь к лексике

            //запуск методов
            config = OpenConfig(config); // чтение config.txt
            ChangeConsole(config); // изменение консосли по конфигу
            pathOfUnit = FindUnit(config["pathOfDirectory"]); // присваивание пути к лексике
            Console.WriteLine(pathOfUnit); 
            test.Init($"{config["pathOfDirectory"]}/{pathOfUnit}"); // запуск теста
        }
    }
}
