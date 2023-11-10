using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Npgsql;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Monkey_tg_bot_v2.DB;

namespace Monkey_tg_bot_v2
{
   
    internal class Program
    {
        enum RegistrationState
        {
            None,
            Name,
            FirstName,
            LastName,
            PhoneNumber,
            Email,
            Age
        }

        private static string token_bot { get; } = "6589506173:AAE3w_g_2QtRvuOpgWas5dG26tcjsw1ncp4";
        private static TelegramBotClient client;
        static RegistrationState registrationState = RegistrationState.None;


        static void Main(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=1111");

            var userRepository = new UserRepository(optionsBuilder.Options);

            client = new TelegramBotClient(token_bot);
            client.StartReceiving();
            client.OnMessage += (sender, e) => OnMessageHandler(sender, e, userRepository);
            Console.WriteLine("Bot is running. Press any key to stop.");
            Console.ReadKey(); // Ждет, пока пользователь не нажмет клавишу.


            client.StopReceiving();
        }




        private static async void OnMessageHandler(object sender, MessageEventArgs e, UserRepository userRepository)
        {
            string userName = null;
            string firstName = null;
            string lastName = null;
            string phoneNumber = null;
            string userEmail = null;
            string age = null;

            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {msg.Text} Время: {DateTime.Now} Id: {msg.Chat.Id}");

                switch (msg.Text)
                {
                    case "/start":
 
                        await client.SendTextMessageAsync(msg.Chat.Id, "Рады видеть новых людей в нашей компании, для начала вам нужно пройти регистрацию ");
                        // Устанавливаем состояние в ожидание имени
                        Program.registrationState = RegistrationState.Name;
                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваше имя:");
                        break;
                        


                    case "FAQ":
                        await client.SendTextMessageAsync(msg.Chat.Id,
                            "Здесь находятся ответы на самые часто задаваемые вопросы, выберите действие по кнопкам ниже",
                            replyMarkup: Button.GetFQAButtons());

                        break;
                    case "Список сотрудников":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Гордынский Егор Денисович +79609040237  Бухгалтер\r\n" +
                            "Борисов Захар Александрович +79644281197 Младший менеджер по продажам\r\n" +
                            "Артёмов Игорь Иванович +79511201564 Старший разработчик программного обеспечения\r\n" +
                            "Белоусов Александр Геннадьевич +79133060833 Офисный работник\r\n" +
                            "Александров Егор Дмитревич +79039560297 Офисный работник\r\n" +
                            "Львова Татьяна Александровна +79652281297 Офисный работник\r\n" +
                            "Иванова Кристина Михайловна +79516201593 Администратор\r\n" +
                            "Мозговой Даниил Юрьевич +796050630237 Разработчик программного обеспечения\r\n ");

                        break;


                    case "Расписание работы":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Расписание");
                        var PictureSchedule = await client.SendPhotoAsync(chatId: msg.Chat.Id, photo: "https://i.pinimg.com/originals/90/e3/45/90e34580b77691140800f8f35894b1b3.jpg");

                        break;
                    case "О компании":
                        await client.SendTextMessageAsync(msg.Chat.Id, "" +
                            "Пивоварня Кожевниково - это современное предприятие по производству пива и безалкогольных напитков, сетью торговых домов в различных регионах России." +
                            "Мы активно ведем тотальную реконструкцию всего производства," +
                            " применяя новейшие изыскания в области пивоварения, используем множество новаторских решений." +
                            " Нами разработаны оригинальные рецептуры и технологии брожения медовухи, бражки, коктейлей и кваса." +
                            "Практически все сотрудники предприятия продолжают обучаться и совершенствовать своё ремесло.\r\n");
                        var Picturelogo = await client.SendPhotoAsync(chatId: msg.Chat.Id, photo: "https://i.ytimg.com/vi/R4WcnXFEt5Q/maxresdefault.jpg");
                        break;
                    case "Контакты руководителя":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Гришаков Иван Вечеславович +79133313759");
                        var PictureBoss = await client.SendPhotoAsync(chatId: msg.Chat.Id, photo: "https://sun9-67.userapi.com/impg/qCNro2g70tniMgEDBtC7YsyUrA7bQgxE0rvMeg/db25_zgMHUM.jpg?size=1552x2160&quality=95&sign=b4270c5302f89e3a5c8cffbd2514be8f&type=album");
                        break;
                    case "Контакты наставника":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Косторев Данила Алексеевич +79835950730");
                        //var PictureMentor = await client.SendPhotoAsync(chatId: msg.Chat.Id, photo: "https://sun9-14.userapi.com/c637618/v637618132/1212/5rblZ_RWGU8.jpg");
                        break;
                    case "Другой вопрос":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Выберерите вопрос по кнопкам ниже", replyMarkup: Button.GetQuestionButtons());
                        break;
                    case "Какие есть хмельные льготы?":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Заработная плата: Компания может предоставлять конкурентоспособную заработную плату, бонусы и премии в зависимости от результатов работы.\n" +
                            "\nГибкий график: Пивоварня может предоставлять возможность выбора сотрудникам гибкого графика работы.\n" +
                            "\nПрофессиональное развитие: Компания может поддерживать обучение и повышение квалификации своих сотрудников, предоставляя возможности для профессионального роста.\n" +
                            "\nПитание и продукция: Сотрудники могут получать льготы на продукцию пивоварни или бесплатное питание на рабочем месте.\n" +
                            "\nКорпоративные мероприятия: Организация корпоративных мероприятий, вечеринок и других событий для создания командного духа.\n" +
                            "\nМатериальная помощь: В случае неотложных финансовых трудностей, компания может предоставлять материальную помощь своим сотрудникам.\n" +
                            "\nОтпуск и отгулы: Предоставление оплачиваемых отпусков и дополнительных отгулов.\n" +
                            "\nРабочая среда: Создание комфортной и безопасной рабочей среды, включая соблюдение стандартов охраны труда.\n" +
                            "\nСкидки на продукцию: Сотрудники могут получать скидки на продукцию пивоварни для себя и своих семей.\n");
                        break;
                    case "Дресскод":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Бизнес-класс: Офисные сотрудники могут носить бизнес-классический стиль.\n" +
                            "\nЭто включает в себя костюмы для мужчин и костюмы или брючные костюмы для женщин.\n" +
                            "\nКостюмы должны быть аккуратными и сочетаться с соответствующими аксессуарами.\n" +
                            "\nРабочая одежда: В зависимости от конкретной должности, некоторые сотрудники могут требовать специализированной рабочей одежды, такой как белые халаты или униформа.\n" +
                            "\n Она должна быть чистой и ухоженной.\n" +
                            "\nДеловой кэжуал: В некоторых офисах можно разрешить стиль делового кэжуал, который подразумевает более свободный стиль одежды, но все равно профессиональный.\n" +
                            "\nЭто могут быть хорошо подогнанные брюки и рубашки для мужчин, а для женщин - блузки и юбки.\n" +
                            "\nАксессуары и обувь: Сотрудники должны выбирать профессиональные обувь и аксессуары, соответствующие общему стилю офиса.\n" +
                            "\nИндивидуальность и брендинг: Возможно, компания позволяет носить аксессуары или элементы одежды, которые подчеркивают бренд пивоварни, такие как футболки с логотипом компании.\n" +
                            "\nСоблюдение стандартов безопасности: Если в офисе есть особые требования безопасности (например, в зонах производства), сотрудники должны соблюдать соответствующие правила и носить предписанную одежду.");
                        break;
                    /*case "Регистрация":
                        //await client.SendTextMessageAsync(msg.Chat.Id, "выберите в каком подразделении вы состоите", replyMarkup: Button.RegistretionButtons());

                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваше имя:");

                        var userResponse = new UserResponse();

                        // Ожидаем ввод имени
                        var userNameMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var userName = userNameMessage.Text; //чтобы передавать в базу данных делаем это,иначе не сможем преобраховать типы


                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите вашу фамилию:");
                        var firstNameMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var firstName = firstNameMessage.Text;

                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваше отчество:");
                        var lastNameMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var lastName = lastNameMessage.Text;

                        await client.SendTextMessageAsync(msg.Chat.Id, " Введите ваш номер телефона:");
                        var phoneNumberMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var phoneNumber = phoneNumberMessage.Text;

                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваш email:");
                        var userEmailMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var userEmail = userEmailMessage.Text;

                        await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваш возраст:");
                        var ageMessage = await userResponse.GetUserResponseAsync(client, msg.Chat.Id);
                        var age = ageMessage.Text;
                        // Добавление пользователя в базу данных
                        UserRepository.RegisterUser(userName, firstName, lastName, phoneNumber, userEmail, age);
                        break;*/
                    default:
                        //await client.SendTextMessageAsync(msg.Chat.Id, "Выберите команду", replyMarkup: Button.GetButtons());
                        if (registrationState != RegistrationState.None)
                        {
                            // Если ожидается ввод какого-либо параметра, то сохраняем этот параметр и переключаем состояние
                            switch (registrationState)
                            {
                                case RegistrationState.Name:
                                    userName = msg.Text;
                                    registrationState = RegistrationState.FirstName;
                                    await client.SendTextMessageAsync(msg.Chat.Id, " Напишите вашу фамилию:");
                                    break;
                                case RegistrationState.FirstName:
                                    firstName = msg.Text;
                                    registrationState = RegistrationState.LastName;
                                    await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваше отчество:");
                                    break;
                                case RegistrationState.LastName:
                                    lastName = msg.Text;
                                    registrationState = RegistrationState.PhoneNumber;
                                    await client.SendTextMessageAsync(msg.Chat.Id, " Введите ваш номер телефона:");
                                    break;
                                case RegistrationState.PhoneNumber:
                                    phoneNumber = msg.Text;
                                    registrationState = RegistrationState.Email;
                                    await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваш email:");
                                    break;
                                case RegistrationState.Email:
                                    userEmail = msg.Text;
                                    registrationState = RegistrationState.Age;
                                    await client.SendTextMessageAsync(msg.Chat.Id, " Напишите ваш возраст:");
                                    break;
                                case RegistrationState.Age:
                                    age = msg.Text;
                                    // Добавление пользователя в базу данных
                                    UserRepository.RegisterUser(userName, firstName, lastName, phoneNumber, userEmail, age);
                                    await client.SendTextMessageAsync(msg.Chat.Id, "Регистрация завершена. Спасибо!");
                                    registrationState = RegistrationState.None; // Сбрасываем состояние
                                    break;
                            }
                        }
                        break;
                }
            }

        }



    }
}




