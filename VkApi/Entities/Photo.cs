namespace VkApi.Entities
{
    /// <summary>
    /// Фотография.
    /// </summary>
    public class Photo : Attachment
    {
        /// <summary>
        /// Идентификатор альбома, в котором находится фотография. 
        /// </summary>
        public int album_id { get; set; }
        /// <summary>
        /// Идентификатор пользователя, загрузившего фото (если фотография размещена в сообществе). Для фотографий, размещенных от имени сообщества, user_id=100. 
        /// </summary>
        public uint user_id { get; set; }
        /// <summary>
        /// Текст описания фотографии.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// url копии фотографии с максимальным размером 75x75px.
        /// </summary>
        public string photo_75 { get; set; }
        /// <summary>
        /// Url копии фотографии с максимальным размером 130x130px.
        /// </summary>
        public string photo_130 { get; set; }
        /// <summary>
        /// Url копии фотографии с максимальным размером 604x604px.
        /// </summary>
        public string photo_604 { get; set; }
        /// <summary>
        /// Url копии фотографии с максимальным размером 807x807px.
        /// </summary>
        public string photo_807 { get; set; }
        /// <summary>
        /// Url копии фотографии с максимальным размером 1280x1024px.
        /// </summary>
        public string photo_1280 { get; set; }
        /// <summary>
        /// Url копии фотографии с максимальным размером 2560x2048px.
        /// </summary>
        public string photo_2560 { get; set; }
    }
}
