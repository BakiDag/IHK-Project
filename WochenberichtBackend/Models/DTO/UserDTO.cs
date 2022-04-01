using System;

namespace Models.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public UserDTO( string _Email, string _UserName, string _FirstName, string _LastName, string _Role)
        {
            
            Email = _Email;
            UserName = _UserName;
            FirstName = _FirstName;
            LastName = _LastName;
            Role = _Role;
        }
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEntry { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}