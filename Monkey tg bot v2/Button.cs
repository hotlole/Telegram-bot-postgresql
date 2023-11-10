using Telegram.Bot.Types.ReplyMarkups;

namespace Monkey_tg_bot_v2
{
    internal class Button
    {
        public static IReplyMarkup RegistretionButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> {new KeyboardButton { Text = "Офисный работник"} , new KeyboardButton{ Text = "Управляющий" }, new KeyboardButton {Text = "Назад" } }
                }
            };
        }

        public static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Регистрация" }, new KeyboardButton { Text = "FAQ" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Список сотрудников" }, new KeyboardButton { Text = "Выход" } }
                }
            };
        }

        public static IReplyMarkup GetFQAButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> {new KeyboardButton { Text = "Расписание работы"} , new KeyboardButton{ Text = "О компании " }, new KeyboardButton {Text = "Контакты руководителя" } },
                    new List<KeyboardButton> {new KeyboardButton { Text = "Контакты наставника"} , new KeyboardButton{ Text = "Другой вопрос" }, new KeyboardButton {Text = "Назад" } }
                }
            };
        }

        public static IReplyMarkup GetQuestionButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> {new KeyboardButton { Text = "Какие есть льготы?"}, new KeyboardButton { Text = "Дресскод" }, new KeyboardButton { Text = "Правила и политика компании" } },
                    new List<KeyboardButton> {new KeyboardButton { Text = "Карьерный рост"}, new KeyboardButton { Text = "Программное обеспечение" }, new KeyboardButton { Text = "Назад" } }

                }
            };
        }





    }
}


