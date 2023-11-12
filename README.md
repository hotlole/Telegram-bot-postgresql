# Telegram-bot-postgresql
Чат бот для адаптации новых сотрудников на предприятии "Пивоварня Кожевниково"


использованные пакеты:
Microsoft.EntityFrameworkCore.Tools

Newtonsoft.Json

Npgsql.EntityFrameworkCore.PostgreSQL

Telegram.Bot версия 16.0.2 

Telegram.Bot.Extensions.Polling


для управления бд используем pgAdmin4 

там вводим SQL запрос, для создания нужной нам таблицы, в неё будем сохранять пользователей, после регистрации
CREATE TABLE Users
(
    Id serial PRIMARY KEY,
    Name varchar(255),
    FirstName varchar(255),
    LastName varchar(255),
    PhoneNumber varchar(255),
    Email varchar(255),
    Age varchar(255)
);
