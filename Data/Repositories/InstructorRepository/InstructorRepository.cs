using DataAccessEfCore.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.InstructorRepository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly WochenberichtDBContext _dbContext;
        

        #region ctor
        public InstructorRepository(WochenberichtDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region getOne
        public async Task<Instructor?> GetInstructorAsyncById(int id)
        {
            Instructor instructor = new Instructor();
            var InstructorID = await _dbContext.Instructors.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync() != null;
            if (InstructorID == true)
            {
                var found = await _dbContext.Instructors.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync();
                return found;
            }
            return instructor;

        }
        #endregion

        #region createOne
        public async Task<Instructor> CreateInstructorAsync(Instructor instructor)
        {
            var result = _dbContext.Instructors.FirstOrDefaultAsync(x => x.Email == instructor.Email).Result != null;
            if (result == false)
            {
                _dbContext.Instructors.Add(instructor);
                return instructor;
            }

            instructor = null;
            return instructor;


        }


        #endregion

        #region GetAll
        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            return await _dbContext.Instructors.ToListAsync();
        }
        #endregion

        #region Update
        public async Task<Instructor> UpdateInstructor(Instructor instructor)
        {
            var InstructoreID = await _dbContext.Instructors.Where(x => x.ID == instructor.ID)
                                                .SingleOrDefaultAsync() != null;

            if (InstructoreID == true)
            {
                var updateFoundInstructor = await _dbContext.Instructors.Where(x => x.ID == instructor.ID)
                                                .SingleOrDefaultAsync();

                if (updateFoundInstructor != null)
                {
                    updateFoundInstructor.FirstName = instructor.FirstName;
                    updateFoundInstructor.LastName = instructor.LastName;
                    updateFoundInstructor.Email = instructor.Email;
                    updateFoundInstructor.UserName = instructor.UserName;
                    updateFoundInstructor.Password = instructor.Password;
                    updateFoundInstructor.DateEntry = instructor.DateEntry;
                    updateFoundInstructor.Token = instructor.Token;
                    updateFoundInstructor.Role = instructor.Role;
                    updateFoundInstructor.ID = instructor.ID;

                    return updateFoundInstructor;
                }
            }
            return null;
        }
        #endregion

        #region DeleteOne
        public async Task<Instructor> DeleteInstructorAsync(int id)
        {
            var instructor = _dbContext.Instructors.Where(x => x.ID == id)
                                               .FirstOrDefault();
            if (instructor != null)
            {
                _dbContext.Instructors.Remove(instructor);
                return instructor;
            }
            return null;

        }
        #endregion

        #region DeleteOneByEmail
        public async Task<Instructor> DeleteInstructorByEmail(string _email)
        {
            var instructor = _dbContext.Instructors.Where(x => x.Email == _email)
                                               .FirstOrDefault();
            if (instructor != null)
            {
                _dbContext.Instructors.Remove(instructor);
                return instructor;
            }
            return null;

        }
        #endregion

        #region GetByEmail
        public async Task<Instructor> GetInstructorAsyncByEmail(string _mail)
        {
            Instructor instructor = new Instructor();
            var InstructorMail = await _dbContext.Instructors.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync() != null;
            if (InstructorMail == true)
            {
                var found = await _dbContext.Instructors.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync();
                return found;
            }
            return instructor;

        }
        #endregion


        bool IInstructorRepository.InstructorExists(string email)
        {
            var result = _dbContext.Instructors.FirstOrDefaultAsync(x => x.Email == email).Result != null;
            //Thread.Sleep(1000); //use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return result;
        }

        bool IInstructorRepository.InstructorLoginValid(string email, string password)
        {
            var result = _dbContext.Instructors.FirstOrDefault(u => u.Email == email & u.Password == password) != null;
            Thread.Sleep(1000);//use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return true;
        }


    }
}
