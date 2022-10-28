// See https://aka.ms/new-console-template for more information

using Core.Models;
using DataFaker;

foreach (AppUser user in FakeDataFactory.GenerateFakeUsers(5))
{
    Console.WriteLine(user.UserName);
}