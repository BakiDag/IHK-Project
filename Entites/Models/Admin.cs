using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Admin 
    {
        //private Role role;


        //public Admin()
        //{

        //}

        //public Admin(string firstName, string lastName, string email, string userName, DateTime dateEntry, Role role)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    UserName = userName;
        //    DateEntry = dateEntry;
        //    this.role = role;
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
