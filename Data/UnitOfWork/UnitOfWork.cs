using DataAccessEfCore.Repositories.AdminRepository;
using DataAccessEfCore.Repositories.ApprenticeRepository;
using DataAccessEfCore.Repositories.InstructorRepository;
using DataAccessEfCore.Repositories.WeeklyReportPositionRepository;
using DataAccessEfCore.Repositories.WeeklyReportRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessEfCore.DataContext;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccessEfCore.Repositories.NotesRepository;

namespace DataAccessEfCore.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly WochenberichtDBContext _dbContext;
        List<HelperClass> EmployeeList = new List<HelperClass>();
        #region ctor
        public UnitOfWork(WochenberichtDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public IAdminRepository Adminrepository =>
            new AdminRepository(_dbContext);

        IInstructorRepository IUnitOfWork.InstrutorRepository =>
            new InstructorRepository(_dbContext);

        IApprenticeRepository IUnitOfWork.ApprenticeRepository =>
            new ApprenticeRepository(_dbContext);

        IWeeklyReportRepository IUnitOfWork.WeeklyReportRepository =>
            new WeeklyReportRepository(_dbContext);

        IWeeklyReportPositionRepository IUnitOfWork.WeeklyReportPositionRepository =>
            new WeeklyReportPositionRepository(_dbContext);

        INotesRepository IUnitOfWork.NotesRepository =>
            new NotesRepository(_dbContext);

        public async Task<bool> Update(string _mail)
        {

            var adminRep = new AdminRepository(_dbContext);
            var admin = await adminRep.GetAdminAsyncByEmail(_mail);
            admin.Token = "";            
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _dbContext.SaveChangesAsync() >0;
            if(saved == false)
                _dbContext.Dispose();
            return true;
        }
        
        

        #region getall EmployeeList
        public async Task<object> GetAllEmployee()
        {
            var adminRep = new AdminRepository(_dbContext);
            var AdminList = await adminRep.GetAllAdminsAsync();

            var InstRep = new InstructorRepository(_dbContext);
            var InsList = await InstRep.GetAllInstructorsAsync();

            var appRep = new ApprenticeRepository(_dbContext);
            var apprenticeList = await appRep.GetAllApprenticesAsync();

            
            foreach (var user in AdminList)
            {
                EmployeeList.Add(new HelperClass
                {
                    ID = user.ID,
                    Email = user.Email,
                    Password = user.Password,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateEntry = user.DateEntry,
                    Role = user.Role,
                });
            }
            foreach (var user in InsList)
            {
                EmployeeList.Add(new HelperClass
                {
                    ID = user.ID,
                    Email = user.Email,
                    Password = user.Password,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateEntry = user.DateEntry,
                    Role = user.Role,
                });
            }
            foreach (var user in apprenticeList)
            {
                EmployeeList.Add(new HelperClass
                {
                    ID = user.ID,
                    Email = user.Email,
                    Password = user.Password,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateEntry = user.DateEntry,
                    Role = user.Role,
                });
            }
            
            return EmployeeList;
        }
        #endregion

        #region SearchAllEmployeeByEmail
        public async Task<object> GetEmployeeAsyncByEmail(string _mail)
        {

            var adminRep = new AdminRepository(_dbContext);
            var admin = await adminRep.GetAdminAsyncByEmail(_mail);
            if (admin.Email != null)
            {
                return admin;
            }


            var InstRep = new InstructorRepository(_dbContext);
            var instructor = InstRep.GetInstructorAsyncByEmail(_mail).Result;
            if (instructor.Email != null)
            {
                return instructor;
            }
            

            var appRep = new ApprenticeRepository(_dbContext);
            var apprentice = appRep.GetApprenticeAsyncByEmail(_mail).Result;
            if (apprentice.Email != null)
            {
                return apprentice;
            }

            return new HelperClass();
        }

        public async Task<object> CheckEmailPassword(string _email, string _password)
        {
            var user = await GetEmployeeAsyncByEmail(_email);
            //EmployeeList.FirstOrDefault(u => u.Email == _email & u.)
            // var user2 = EmployeeList.FirstOrDefault(u => u.Email == _email & u.pass == password);
            //if (user != null)
            //{
            //    if (user.Email == _email && user.Password == _password)
            //    {
            //        return user;
            //    }
            //}

            return "";
        }

        #endregion
        public async Task<object> deleteContact(string _email)
        {
            var admin = await _dbContext.Admins.Where(x => x.Email == _email)
                            .SingleOrDefaultAsync();
            if (admin != null)
            {
                _dbContext.Admins.Remove(admin);
                return admin;
            }

            var instructor = await _dbContext.Instructors.Where(x => x.Email == _email)
                .SingleOrDefaultAsync();
            if(instructor != null)
            {
                _dbContext.Instructors.Remove(instructor);
                return instructor;
            }
            var apprentice = await _dbContext.Apprentices.Where(x => x.Email == _email)
                                        .SingleOrDefaultAsync();
            if (apprentice != null)
            {
                _dbContext.Apprentices.Remove(apprentice);
                return apprentice;
            }
            
            
            return null;                
            
        }










    }
}
