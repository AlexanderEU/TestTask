using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestTask
{
    /// <summary>
    /// Для взаимодействия с БД создаем новый класс наследуемый от DbContext
    /// </summary>
    class ApplicationContext : DbContext
    {
        /// <summary>
        /// Определяем набор объектов которые хранятся в БД
        /// </summary>
        public DbSet<WordTable> WordTables { get; set; } // Создаем таблицу на основании класса WordTable

        /// <summary>
        /// Конструктор, проверяет наличию БД если ее нет создаем
        /// </summary>
        public ApplicationContext()
        {
            Database.EnsureCreated(); // Создаем ДБ если ее нет
        }

        /// <summary>
        /// Для настройки подключения переопределяем метод OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Переопределяем метод конфигурации соединения с БД
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WordBase;Trusted_Connection=True;");

        }
    }
}
