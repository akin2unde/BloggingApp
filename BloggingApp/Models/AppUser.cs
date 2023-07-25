using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        [Key]
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public static void SeedAdminUser(BloggingDBContext context,SignInManager<AppUser> _loginManager)
        {
            context.Database.EnsureCreated();
            if(context.AppUsers.FirstOrDefault(f=>f.MailAddress== "otawise@gmail.com")==null)
            {
                var user = new AppUser
                {
                    Name = "Otawise",
                    MailAddress = "otawise@gmail.com",
                    Password = Util.HashPasword("otawise"),
                    IsAdmin = true,
                    Id = Guid.NewGuid(),
                    UserName = "Otawise",
                    Email = "otawise@gmail.com",
                    PasswordHash= Util.HashPasword("otawise")
                };
            context.Users.Add(user);
            context.SaveChanges();
             //_loginManager.SignInAsync(new AppUser { UserName = user.MailAddress, Email = user.MailAddress, PasswordHash = user.Password }, isPersistent: false);
            }
         

        }

    }
}
