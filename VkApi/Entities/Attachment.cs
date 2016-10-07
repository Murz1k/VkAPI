namespace VkApi.Entities
{
    /// <summary>
    /// Медиа-вложение.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Идентификатор медиа-вложения. 
        /// </summary>
        public uint id { get; set; }
        /// <summary>
        /// Идентификатор владельца медиа-вложения.
        /// </summary>
        public int owner_id { get; set; }
        /// <summary>
        /// Дата добавления в формате unixtime.
        /// </summary>
        public uint date { get; set; }
    }
}
