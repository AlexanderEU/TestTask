using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace TestTask
{
    /// <summary>
    /// Основной класс для работы с html
    /// </summary>
    class WordHtml
    {
        private string Url { get; set; } // адрес сайта
        private string FileName { get; set; } // имя файла

        /// <summary>
        /// Конструктор по умолчанию
        /// адрес сайта = www.simbirsoft.com
        /// имя файла = data.txt
        /// </summary>
        public WordHtml()
        {
            Url = "http://www.simbirsoft.com";
            FileName = "data.txt";
        }

        /// <summary>
        /// Конструктор, задает адрес сайта, и имя файла
        /// </summary>
        /// <param name="url">адрес сайта</param>
        /// <param name="fileName">имя файла</param>
        public WordHtml(string url, string fileName)
        {
            Url = url;
            FileName = fileName;
        }

        /// <summary>
        /// Печает в консоль результат анализа html
        /// </summary>
        public void PrintResultToConsole()
        {
            SaveToHtml();
            foreach (var pair in WordsCounts())
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Сохраняет результат анализа в html
        /// </summary>
        public void SaveToBase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    foreach (var pair in WordsCounts())
                    {
                        WordTable wt = new WordTable { 
                            Word = pair.Key,
                            Count = pair.Value
                        };
                        db.WordTables.Add(wt);                       
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка сохранения данных...");
                    Console.WriteLine(ex.Message);
                }
            }

        }

        /// <summary>
        /// Сохраняет html страницу в текстовый файл
        /// </summary>
        private void SaveToHtml()
        {
            // Используем класс WebClient для загрузки страницы сайта
            using (WebClient wc = new WebClient())
            {
                // Сохраняем страницу в текстовый файл
                wc.DownloadFile(Url, FileName);
            }
        }

        /// <summary>
        /// Количество слов и сколько каждое слово встречается
        /// </summary>
        /// <returns>Словарь ключ - слово, значение - количество</returns>
        Dictionary<string, int> WordsCounts()
        {
            // Получить текст файла
            string txt = File.ReadAllText(FileName);

            // Использование регулярных выражений для замены символов
            // это не буквы
            Regex regExp = new Regex("[^а-яА-Я]");
            txt = regExp.Replace(txt, " ");

            // Разделить текст на слова, игнорируем пробелы
            string[] words = txt.ToUpper().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Используйте LINQ для получения уникальных слов
            var wordQuery =
                (from string word in words
                 orderby word
                 select word).Distinct();

            string[] result = wordQuery.ToArray();
            
            // Временный словарь для хранения результата
            Dictionary<string, int> buffer = new Dictionary<string, int>(); 
            int count;
            for (int i = 0; i < result.Length; i++)
            {
                count = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    if (result[i] == words[j])
                    {
                        count++;
                    }
                }
               
                buffer.Add(result[i], count);
            }
            return buffer;
        }

    }
}
