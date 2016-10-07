using System.Collections.Generic;

namespace VkApi.Entities
{    /*
geo	информация о местоположении , содержит поля:
type — тип места; 
строка
coordinates — координаты места;
place — описание места (если оно добавлено), объект с полями:
id — идентификатор места (если назначено);
              положительное число
title — название места (если назначено);
              строка
latitude — географическая широта;
longitude — географическая долгота;
created — дата создания (если назначено);
icon — url изображения-иконки;
country — название страны;
             строка
city — название города;
             строка

emoji	содержатся ли в сообщении emoji-смайлы.
флаг, может принимать значения 1 или 0
important	является ли сообщение важным.
флаг, может принимать значения 1 или 0
deleted	удалено ли сообщение.
флаг, может принимать значения 1 или 0

chat_active	идентификаторы авторов последних сообщений беседы.
список строк, разделенных запятыми
push_settings	настройки уведомлений для беседы, если они есть.
sound и disabled_until
action	поле передано, если это служебное сообщение
строка, может быть chat_photo_update или chat_photo_remove, а с версии 5.14 еще и chat_create, chat_title_update, chat_invite_user, chat_kick_user
action_mid	идентификатор пользователя (если > 0) или email (если < 0), которого пригласили или исключили
число, для служебных сообщений с action равным chat_invite_user или chat_kick_user
action_email	email, который пригласили или исключили
строка, для служебных сообщений с action равным chat_invite_user или chat_kick_user и отрицательным action_mid
action_text	название беседы
строка, для служебных сообщений с action равным chat_create или chat_title_update
         */
     /// <summary>
     /// Личное сообщение.
     /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификатор сообщения (не возвращается для пересланных сообщений).
        /// </summary>
        public uint id { get; set; }
        /// <summary>
        ///  Идентификатор пользователя, в диалоге с которым находится сообщение.
        /// </summary>
        public uint user_id { get; set; }
        /// <summary>
        /// Идентификатор автора сообщения.
        /// </summary>
        public uint from_id { get; set; }
        /// <summary>
        /// Дата отправки сообщения в формате unixtime.
        /// </summary>
        public uint date { get; set; }
        /// <summary>
        /// Статус сообщения(0 — не прочитано, 1 — прочитано, не возвращается для пересланных сообщений).
        /// </summary>
        public bool read_state { get; set; }
        /// <summary>
        /// Тип сообщения(0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений).
        /// </summary>
        public bool _out { get; set; }
        /// <summary>
        /// Заголовок сообщения или беседы.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// Массив медиа-вложений(см.Описание формата медиа-вложений).
        /// </summary>
        public IEnumerable<Attachment> attachments { get; set; }
        /// <summary>
        ///  Идентификатор беседы.
        /// </summary>
        public uint chat_id { get; set; }
        /// <summary>
        /// Количество участников беседы.
        /// </summary>
        public uint users_count { get; set; }
        /// <summary>
        /// Массив пересланных сообщений(если есть).
        /// </summary>
        public IEnumerable<Message> fwd_messages { get; set; }
        /// <summary>
        /// Идентификатор создателя беседы.
        /// </summary>
        public uint admin_id { get; set; }
        /// <summary>
        /// Url копии фотографии беседы шириной 50px.
        /// </summary>
        public string photo_50 { get; set; }
        /// <summary>
        /// Url копии фотографии беседы шириной 100px.
        /// </summary>
        public string photo_100 { get; set; }
        /// <summary>
        /// Url копии фотографии беседы шириной 200px.
        /// </summary>
        public string photo_200 { get; set; }
        /// <summary>
        /// Идентификатор, используемый при отправке сообщения.Возвращается только для исходящих сообщений.
        /// </summary>
        public string random_id { get; set; }
    }
}
