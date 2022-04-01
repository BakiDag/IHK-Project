using DataAccessEfCore.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly WochenberichtDBContext _dbContext;        

        #region ctor
        public AdminRepository(WochenberichtDBContext dBContext)
        {
            _dbContext = dBContext;            
        }
        #endregion

        #region getOne
        public async Task<Admin?> GetAdminAsyncById(int id)
        {
            Admin admin = new Admin();
            var admineID = await _dbContext.Admins.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync() != null;
            if (admineID == true)
            {
                var found = await _dbContext.Admins.Where(x => x.ID == id)
                                                .SingleOrDefaultAsync();
                return found;
            }            
            return admin;

        }
        #endregion

        #region createOne
        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            var result = _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == admin.Email).Result != null;
            if (result == false)
            {
                _dbContext.Admins.Add(admin);
                return admin;
            }

            admin = null;
            return admin;


        }


        #endregion

        #region GetAll
        public async Task<List<Admin>> GetAllAdminsAsync()
        {            
            return await _dbContext.Admins.ToListAsync();
        }
        #endregion

        #region Update
        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var admineID = await _dbContext.Admins.Where(x => x.ID == admin.ID)
                                                .SingleOrDefaultAsync() != null;            
            
            if (admineID == true )
            {
                var updateFoundAdmin = await _dbContext.Admins.Where(x => x.ID == admin.ID)
                                                .SingleOrDefaultAsync();
                
                if (updateFoundAdmin != null)
                {                    
                    updateFoundAdmin.FirstName = admin.FirstName;
                    updateFoundAdmin.LastName = admin.LastName;
                    updateFoundAdmin.Email = admin.Email;
                    updateFoundAdmin.UserName = admin.UserName;
                    updateFoundAdmin.Password = admin.Password;
                    updateFoundAdmin.DateEntry = admin.DateEntry;
                    updateFoundAdmin.Token = admin.Token;
                    updateFoundAdmin.Role = admin.Role;
                    updateFoundAdmin.ID = admin.ID;

                    return updateFoundAdmin;
                }                
            }
            return null;
        }
        #endregion

        #region DeleteOneByID
        public async Task<Admin> DeleteAdminAsync(int id)
        {
            var admin = _dbContext.Admins.Where(x => x.ID == id)
                                               .FirstOrDefault();
            if (admin != null)
            {
                _dbContext.Admins.Remove(admin);
                return admin;
            }
            return null;

        }
        #endregion

        #region DeleteOneByEmail
        public async Task<Admin> DeleteAdminByEmail(string _email)
        {
            var admin = _dbContext.Admins.Where(x => x.Email == _email)
                                               .FirstOrDefault();
            if (admin != null)
            {
                _dbContext.Admins.Remove(admin);
                return admin;
            }
            return null;

        }
        #endregion

        #region GetByEmail
        public async Task<Admin> GetAdminAsyncByEmail(string _mail)
        {
            Admin admin = new Admin();     
            var AdminMail = await _dbContext.Admins.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync() !=null;
            if (AdminMail == true)
            {
                var found = await _dbContext.Admins.Where(x => x.Email == _mail)
                                                .SingleOrDefaultAsync();
                return found;
            }
            return admin;
            
        }
        #endregion


        bool IAdminRepository.AdminExists(string email)
        {
            var result = _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == email).Result != null;
            //Thread.Sleep(1000); //use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return result;
        }

        bool IAdminRepository.AdminLoginValid(string email, string password)
        {
            var result = _dbContext.Admins.FirstOrDefault(u => u.Email == email & u.Password == password) != null;
            Thread.Sleep(1000);//use result or thread sleep, thread sleep worst option
            if (result == false)
                return false;
            return true;
        }


       



    }
}
