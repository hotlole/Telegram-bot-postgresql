using Microsoft.EntityFrameworkCore;
using Monkey_tg_bot_v2.DB;
using Telegram.Bot;
using Telegram.Bot.Types;

public class UserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(DbContextOptions<ApplicationContext> dbContextOptions)
    {
        _context = new ApplicationContext(dbContextOptions);
    }

    public static void RegisterUser(string name, string firstName, string lastName, string phoneNumber, string email, string age)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=1111");

        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var user = new Monkey_tg_bot_v2.DB.User
            {
                Name = name,
                FirstName = firstName,
                LastName = lastName, // Установите значение по умолчанию, если lastName равно null
                PhoneNumber = phoneNumber,
                Email = email , // Установите значение по умолчанию, если email равно null
                Age = age
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }

    


}