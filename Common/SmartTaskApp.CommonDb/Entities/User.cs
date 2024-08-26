using Microsoft.AspNetCore.Identity;

namespace SmartTaskApp.CommonDb.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }


        public User(string email, string firstName, string lastName, DateTime dateOfBirth)
        {
            Email = email;
            UserName = email; 
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public User() { } 
    }
}
