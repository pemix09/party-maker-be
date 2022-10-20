using System.Text.Json;
using Bogus;

namespace DataFaker;
using Core.Models;

public class AppUserFakeDataFactory
{
    private Faker<AppUser> AppUserFaker;

    public AppUserFakeDataFactory()
    {
        AppUserFaker = new Faker<AppUser>()
            .RuleFor(user => user.Email, fake => fake.Person.Email)
            .RuleFor(user => user.UserName, fake => fake.Person.UserName)
            .RuleFor(user => user.PhoneNumber, fake => fake.Person.Phone)
            .RuleFor(user => user.Avatar, fake => fake.Person.Avatar);
    }
    public void Generate()
    {
        foreach (var user in AppUserFaker.GenerateForever())
        {
            var text = JsonSerializer.Serialize(user);
            Console.WriteLine(text);
        }
    }
    
}