using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin> GetAdminAsyncById(int id);
        Task<Admin> GetAdminAsyncByEmail(string _mail);
        Task<Admin> CreateAdminAsync(Admin admin);
        Task<Admin> DeleteAdminAsync(int id);
        Task<Admin> DeleteAdminByEmail(string _email);
        Task<Admin> UpdateAdmin(Admin admin);


        //bool LoginVerify(string email, string password);
        bool AdminExists(string email);
        bool AdminLoginValid(string email,string password);

        //#region alternativetasks
        //Task<IEnumerable<Admin>> GetAllAdminsAsync();

        //void AddAdmin(Admin admin);
        //void DeleteAdmin(int id);
        //Task<bool> SaveAsync();
        //#endregion 


    }
}
