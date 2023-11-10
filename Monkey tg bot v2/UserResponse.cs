using Telegram.Bot.Types;
using Telegram.Bot;

namespace Monkey_tg_bot_v2
{
    public class UserResponse
    {
        // Функция для ожидания ответа от пользователя
        public async Task<Message> GetUserResponseAsync(ITelegramBotClient client, long chatId)
        {
            var message = await client.SendTextMessageAsync(chatId, "Ожидаем ваш ответ...");
            var response = await WaitForUserResponse(client, chatId);
            return response;
        }

        // Функция для ожидания сообщения от пользователя
        public async Task<Message> WaitForUserResponse(ITelegramBotClient client, long chatId)
        {
            var offset = 0;
            while (true)
            {
                var updates = await client.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    if (update.Message != null && update.Message.Chat.Id == chatId)
                    {
                        return update.Message;
                    }

                    offset = update.Id + 1;
                }

                await Task.Delay(-1); // Подождать секунду перед следующей проверкой
            }
        }
    }
}
