using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{

    [XmlRoot("error")]
    public class Error
    {
        public Error error = null;

        private string GetError(Error error)
        {
            string text = "Неизвестная ошибка";
            switch (error.error_code)
            {
                case 1: text = "Произошла неизвестная ошибка"; break;
                case 2: text = "Приложение выключено."; break;
                case 3: text = "Передан неизвестный метод."; break;
                case 4: text = "Неверная подпись."; break;
                case 5: text = "Авторизация пользователя не удалась."; break;
                case 6: text = "Слишком много запросов в секунду."; break;
                case 7: text = "Нет прав для выполнения этого действия."; break;
                case 8: text = "Неверный запрос."; break;
                case 9: text = "Слишком много однотипных действий."; break;
                case 10: text = "Произошла внутренняя ошибка сервера."; break;
                case 11: text = "В тестовом режиме приложение должно быть выключено или пользователь должен быть залогинен."; break;
                case 14: text = "Требуется ввод кода с картинки (Captcha)."; break;
                case 15: text = "Доступ запрещён."; break;
                case 16: text = "Требуется выполнение запросов по протоколу HTTPS, т.к. пользователь включил настройку, требующую работу через безопасное соединение."; break;
                case 17: text = "Требуется валидация пользователя."; break;
                case 20: text = "Данное действие запрещено для не Standalone приложений."; break;
                case 21: text = "Данное действие разрешено только для Standalone и Open API приложений."; break;
                case 23: text = "Метод был выключен."; break;
                case 24: text = "Требуется подтверждение со стороны пользователя."; break;
                case 100: text = "Один из необходимых параметров был не передан или неверен."; break;
                case 101: text = "Неверный API ID приложения."; break;
                case 113: text = "Неверный идентификатор пользователя."; break;
                case 150: text = "Неверный timestamp."; break;
                case 200: text = "Доступ к альбому запрещён."; break;
                case 201: text = "Доступ к аудио запрещён."; break;
                case 203: text = "Доступ к группе запрещён."; break;
                case 212: text = "Нет доступа к комментариям записи"; break;
                case 213: text = "Нет доступа к комментированию записи."; break;
                case 300: text = "Альбом переполнен."; break;
                case 500: text = "Действие запрещено. Вы должны включить переводы голосов в настройках приложения."; break;
                case 600: text = "Нет прав на выполнение данных операций с рекламным кабинетом."; break;
                case 603: text = "Произошла ошибка в работе с рекламным кабинетом."; break;
            }
            return text;
        }
        public Error()
        {

        }
        public Error(string text)
        {
            XmlSerializer xmlSerializer1 = new XmlSerializer(typeof(Error));
            try
            {
                error = (Error)xmlSerializer1.Deserialize(new StringReader(text));
                error_code = error.error_code;
            }
            catch(Exception)
            {
                error_msg = GetError(error);
            }
        }
        [XmlElement("error_code")]
        public int error_code { get; set; }
        [XmlElement("error_msg")]
        public string error_msg { get; set; }

        [XmlElement("captcha_sid")]
        public string id { get; set; }
        [XmlElement("captcha_img")]
        public string url { get; set; }
        public string RequestUrl { get; set; }
    }
}
