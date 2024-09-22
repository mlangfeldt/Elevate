using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public User()
        {

        }

        public User(string email, string firstName, string lastName, string password)
        {

            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;

        }
    }

    
}
