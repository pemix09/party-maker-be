using Bogus;

namespace DataFaker;
using Core.Models;

public static class FakeDataFactory
{
    public static IEnumerable<AppUser> GenerateFakeUsers(int count)
    {
        Faker<AppUser> appUserFaker = new Faker<AppUser>(locale: "pl")
        .RuleFor(user => user.Email, fake => fake.Person.Email)
        .RuleFor(user => user.UserName, fake => fake.Person.UserName)
        .RuleFor(user => user.PhoneNumber, fake => fake.Person.Phone)
        .RuleFor(user => user.Avatar, fake => fake.Person.Avatar);

        List<AppUser> fakeUsersList = new List<AppUser>();
        
        for (int i = 0; i < count; i++)
        {
            fakeUsersList.Add(appUserFaker.Generate());
        }

        return fakeUsersList;
    }
    
}