using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccessEfCore.Repositories.ApprenticeRepository
{
    public interface IApprenticeRepository
    {
        Task<List<Apprentice>> GetAllApprenticesAsync();
        Task<Apprentice> GetApprenticeAsyncById(int id);
        Task<Apprentice> GetApprenticeAsyncByEmail(string _mail);
        Task<Apprentice> CreateApprenticeAsync(Apprentice apprentice);
        Task<Apprentice> DeleteApprenticeAsync(int id);
        Task<Apprentice> DeleteApprenticeByEmail(string _email);
        Task<Apprentice> UpdateApprentice(Apprentice apprentice);
        bool ApprenticeExists(string email);
        bool ApprenticeLoginValid(string email, string password);


       
    }
}
