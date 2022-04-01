using Domain.Models;

namespace DataAccessEfCore.UnitOfWork
{
    public class HelperClass
    {
        public HelperClass()
        {
            
        }
        public HelperClass(int _ID, string _Email,string _Password,string _UserName,string _FirstName, string _LastName, DateTime _DateEntry, Role _Role, string _Token)
        {
            ID = _ID;
            Email = _Email;
            Password = _Password;
            UserName = _UserName;
            FirstName = _FirstName;
            LastName = _LastName;
            DateEntry = _DateEntry;
            Role = _Role;
            Token = _Token;
        }
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEntry { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}