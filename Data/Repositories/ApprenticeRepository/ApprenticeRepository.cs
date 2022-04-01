using DataAccessEfCore.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.ApprenticeRepository
{
    public class ApprenticeRepository : IApprenticeRepository
    {
        private readonly WochenberichtDBContext _dbContext;

        #region ctor
        public ApprenticeRepository(WochenberichtDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        
        #endregion

        #region getOne
        public async Task<Apprentice?> GetApprenticeAsyncById(int id)
        {
            Apprentice apprentice = new Apprentice();
            var apprenticeID = await _dbContext.Apprentices.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync() != null;
            if (apprenticeID == true)
            {
                var found = await _dbContext.Apprentices.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync();
                return found;
            }
            return apprentice;

        }
        #endregion

        #region createOne
        public async Task<Apprentice> CreateApprenticeAsync(Apprentice apprentice)
        {
            var result = _dbContext.Apprentices.FirstOrDefaultAsync(x => x.Email == apprentice.Email).Result != null;
            if (result == false)
            {
                _dbContext.Apprentices.Add(apprentice);
                return apprentice;
            }

            apprentice = null;
            return apprentice;


        }


        #endregion

        #region GetAll
        public async Task<List<Apprentice>> GetAllApprenticesAsync()
        {
            return await _dbContext.Apprentices.ToListAsync();
        }
        #endregion

        #region Update
        public async Task<Apprentice> UpdateApprentice(Apprentice apprentice)
        {
            var apprenticeeID = await _dbContext.Apprentices.Where(x => x.ID == apprentice.ID)
                                                .SingleOrDefaultAsync() != null;

            if (apprenticeeID == true)
            {
                var updateFoundApprentice = await _dbContext.Apprentices.Where(x => x.ID == apprentice.ID)
                                                .SingleOrDefaultAsync();

                if (updateFoundApprentice != null)
                {
                    updateFoundApprentice.FirstName = apprentice.FirstName;
                    updateFoundApprentice.LastName = apprentice.LastName;
                    updateFoundApprentice.Email = apprentice.Email;
                    updateFoundApprentice.UserName = apprentice.UserName;
                    updateFoundApprentice.Password = apprentice.Password;
                    updateFoundApprentice.DateEntry = apprentice.DateEntry;
                    updateFoundApprentice.Token = apprentice.Token;
                    updateFoundApprentice.Role = apprentice.Role;
                    updateFoundApprentice.ID = apprentice.ID;

                    return updateFoundApprentice;
                }
            }
            return null;
        }
        #endregion

        #region DeleteOne
        public async Task<Apprentice> DeleteApprenticeAsync(int id)
        {
            var apprentice = _dbContext.Apprentices.Where(x => x.ID == id)
                                               .FirstOrDefault();
            if (apprentice != null)
            {
                _dbContext.Apprentices.Remove(apprentice);
                return apprentice;
            }
            return null;

        }
        #endregion
        #region DeleteOneByEmail
        public async Task<Apprentice> DeleteApprenticeByEmail(string _email)
        {
            var apprentice = _dbContext.Apprentices.Where(x => x.Email == _email)
                                               .FirstOrDefault();
            if (apprentice != null)
            {
                _dbContext.Apprentices.Remove(apprentice);
                return apprentice;
            }
            return null;

        }
        #endregion

        #region GetByEmail
        public async Task<Apprentice> GetApprenticeAsyncByEmail(string _mail)
        {
            Apprentice apprentice = new Apprentice();
            var ApprenticeMail = await _dbContext.Apprentices.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync() != null;
            if (ApprenticeMail == true)
            {
                var found = await _dbContext.Apprentices.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync();
                return found;
            }
            return apprentice;

        }
        #endregion


        bool IApprenticeRepository.ApprenticeExists(string email)
        {
            var result = _dbContext.Apprentices.FirstOrDefaultAsync(x => x.Email == email).Result != null;
            //Thread.Sleep(1000); //use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return result;
        }

        bool IApprenticeRepository.ApprenticeLoginValid(string email, string password)
        {
            var result = _dbContext.Apprentices.FirstOrDefault(u => u.Email == email & u.Password == password) != null;
            Thread.Sleep(1000);//use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return true;
        }

    }
}
