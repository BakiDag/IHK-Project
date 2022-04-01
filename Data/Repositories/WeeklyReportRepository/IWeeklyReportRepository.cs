using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DataAccessEfCore.Repositories.WeeklyReportRepository
{
    public interface IWeeklyReportRepository //move
    {
        #region getAll
        
        Task<List<WeeklyReport>> GetAllWeeklyReportAsync();
        
        #endregion

        #region getOneById
        
        Task<WeeklyReport> GetWeeklyReportAsyncById(int id);
        Task<WeeklyReport> GetWeeklyReportAsyncByDateFrom(DateTime _monday);

        #endregion

        #region CreateOne

        Task<WeeklyReport> CreateWeeklyReportAsync(WeeklyReport weeklyReport);
        
        #endregion

        #region DeleteOne
        
        Task<WeeklyReport> DeleteWeeklyReportAsync(int id);

        #endregion
        


    }

   
}
