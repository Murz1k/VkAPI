namespace VkApi.Entities
{
    /// <summary>
    /// Аудиозапись.
    /// </summary>
    public class Audio : Attachment
    {
        /// <summary>
        /// Исполнитель.
        /// </summary>
        public string artist { get; set; }
        /// <summary>
        /// Название композиции.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Длительность аудиозаписи в секундах.
        /// </summary>
        public uint duration { get; set; }
        /// <summary>
        /// Ссылка на mp3.
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Идентификатор текста аудиозаписи (если доступно).
        /// </summary>
        public uint lyrics_id { get; set; }
        /// <summary>
        /// Идентификатор альбома, в котором находится аудиозапись (если присвоен).
        /// </summary>
        public uint album_id { get; set; }
        /// <summary>
        /// Идентификатор жанра из списка аудио жанров.
        /// </summary>
        public uint genre_id { get; set; }
        /// <summary>
        /// 1, если включена опция «Не выводить при поиске». Если опция отключена, поле не возвращается.
        /// </summary>
        public uint no_search { get; set; }
    }
}
