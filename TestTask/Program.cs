using System;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Очищаем консоль
                Console.Clear();

                try
                {
                    // Выводим немного информации о программе, а также как работать с ней
                    Console.WriteLine("О программе:");
                    Console.WriteLine("Программа скачивает HTML-страницу посредством HTTP-запроса, \n" +
                        "сохраняет загруженный файл на жесткий диск компьютера, подсчитывает количество уникальных \n" +
                        "слов на странице и выводит статистику по словам.\n");
                    Console.WriteLine("Начало работы программы:");
                    Console.WriteLine(@"Введите адрес русскоязычного сайта, например: http://www.simbirsoft.com и нажмите клавишу <ENTER>:");

                    // Сохраняем полный адрес сайта с http 
                    Console.Write(@"http://");
                    string url = @"http://" + Console.ReadLine();

                    if (url == "http://")
                    {
                        WordHtml wh = new WordHtml();
                        
                        Console.WriteLine("\nХотите сохранить результат в базу данных? В случае положительного ответа \n" +
                                           "введите слово <да> и нажмите на <ENTER>, или введите любое слово и нажмите <ENTER>");
                        Console.Write("-> ");

                        if (Console.ReadLine().ToLower() == "да")
                        {
                            wh.SaveToBase();
                        }
                    }
                    else
                    {
                        WordHtml wh = new WordHtml(url, "data.txt");
                        
                        Console.WriteLine("\nХотите сохранить результат в базу данных? В случае положительного ответа \n" +
                                    "введите слово <да> и нажмите на <ENTER>, или введите любое слово и нажмите <ENTER>");
                        Console.Write("-> ");

                        if (Console.ReadLine().ToLower() == "да")
                        {
                            wh.SaveToBase();
                        }
                    }
                }
                catch
                {
                    // Выводим информацию в случае исключения
                    Console.WriteLine(@"Сайт не найден либо некоректно введен адрес сайта, формат ввода имеет вид: http:\\адрес сайта");
                    Console.WriteLine("Нажмите на любую клавишу для продолжения работы приложения");
                    Console.ReadKey();
                }

                // Спрашиваем пользователя хотим ли выйти из программы или нет
                Console.WriteLine("\nЕсли Вы хотите выйти из программы введите слово <да>, \n" +
                    "либо введите любое слово и нажмите на <ЕNTER> для продолжения работы программы");
                Console.Write("-> ");

                if (Console.ReadLine().ToLower() == "да")
                {
                    break;
                }

            } while (true);
        }

    }
}
