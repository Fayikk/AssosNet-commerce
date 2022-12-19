using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if (!userManager.Users.Any())
            {
                var user = new AppUser{
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address{
                        FirstName = "Bob",
                        LastName = "Bobinity",
                        Street = "9 th street",
                        City = "New york",
                        State = "NY",
                        ZipCode="21100"
                    }
                };

                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }   
    }
}