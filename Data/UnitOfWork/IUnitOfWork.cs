using DataAccessEfCore.Repositories.AdminRepository;
using DataAccessEfCore.Repositories.ApprenticeRepository;
using DataAccessEfCore.Repositories.InstructorRepository;
using DataAccessEfCore.Repositories.NotesRepository;
using DataAccessEfCore.Repositories.WeeklyReportPositionRepository;
using DataAccessEfCore.Repositories.WeeklyReportRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEfCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAdminRepository Adminrepository { get; }
        IInstructorRepository InstrutorRepository { get; }
        IApprenticeRepository ApprenticeRepository { get; }
        IWeeklyReportRepository WeeklyReportRepository { get; }
        IWeeklyReportPositionRepository WeeklyReportPositionRepository { get; }
        INotesRepository NotesRepository { get; }
        Task<object> GetEmployeeAsyncByEmail(string mail);
        Task<object> GetAllEmployee();

        Task<object> CheckEmailPassword(string _email, string _password);
        Task<bool> SaveAsync();
        //Task<bool> Dispose();
        Task<bool> Update(string _mail);
        Task<object> deleteContact(string _email);



    }
}
