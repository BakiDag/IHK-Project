using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.InstructorRepository
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorAsyncById(int id);
        Task<Instructor> GetInstructorAsyncByEmail(string _mail);
        Task<Instructor> CreateInstructorAsync(Instructor instructor);
        Task<Instructor> DeleteInstructorAsync(int id);
        Task<Instructor> DeleteInstructorByEmail(string _email);
        Task<Instructor> UpdateInstructor(Instructor instructor);
        bool InstructorExists(string email);
        bool InstructorLoginValid(string email, string password);


    }
}
