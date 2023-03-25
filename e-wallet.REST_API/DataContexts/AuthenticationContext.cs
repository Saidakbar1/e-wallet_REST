using e_wallet.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_wallet.REST_API.DataContexts
{
    public class AuthenticationContext : IdentityUserContext<User, Guid>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            User identifiedUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                Identified = false
            };
            passwordHasher.HashPassword(identifiedUser, "Admin*123");

            User unidentifiedUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                Identified = false
            };
            passwordHasher.HashPassword(unidentifiedUser, "Admin*123");

            builder.Entity<User>().HasData(unidentifiedUser);
            builder.Entity<User>().HasData(identifiedUser);
        }
    }
}