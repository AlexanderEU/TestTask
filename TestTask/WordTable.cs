namespace TestTask
{
    /// <summary>
    /// Определяем модель которая описывает используемые в базе данные
    /// </summary>
    class WordTable
    {
        public int Id { get; set; } // идентификатор целое число
        public string Word { get; set; } // Слово
        public int Count { get; set; } // Сколько раз встречается слово
    }
}
