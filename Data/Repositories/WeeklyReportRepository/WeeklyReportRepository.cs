using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataAccessEfCore.DataContext;

namespace DataAccessEfCore.Repositories.WeeklyReportRepository
{
    public class WeeklyReportRepository : IWeeklyReportRepository
    {
        private readonly WochenberichtDBContext _dbContext;
       

        #region ctor
        public WeeklyReportRepository(WochenberichtDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region create one
        
        public async Task<WeeklyReport> CreateWeeklyReportAsync(WeeklyReport weeklyReport)
        {
            var result = _dbContext.WeeklyReports.FirstOrDefaultAsync(x => x.ID == weeklyReport.ID).Result !=null;
            if (result == false)
            {
                _dbContext.WeeklyReports.Add(weeklyReport);
                return weeklyReport;
            }
            //_dbContext.Dispose();
            weeklyReport = null;
            return weeklyReport;
            //return weeklyReport;
        }
       
        

        #endregion

        #region getOneByID           

        public async Task<WeeklyReport?> GetWeeklyReportAsyncById(int id)
        {
            var OneReport = await _dbContext.WeeklyReports.FirstOrDefaultAsync(weeklyReport => weeklyReport.ID == id);
            return OneReport;
        }

        #endregion
        #region getOneByMonday           

        public async Task<WeeklyReport?> GetWeeklyReportAsyncByDateFrom(DateTime _monday)
        {
            var OneReport =  _dbContext.WeeklyReports.FirstOrDefault(weeklyReport => weeklyReport.DateFrom.Date == _monday.Date);
            
            
            return OneReport;
        }

        #endregion

        #region getAll

        public async Task<List<WeeklyReport>> GetAllWeeklyReportAsync()
        {
            return await _dbContext.WeeklyReports.ToListAsync();
        }
        
        #endregion

        #region deleteOne
                
        public async Task<WeeklyReport> DeleteWeeklyReportAsync(int id)
        {
            var report = _dbContext.WeeklyReports.Where(x => x.ID == id)
                                                    .FirstOrDefault();
            if(report != null)
            {
                _dbContext.WeeklyReports.Remove(report);                
                return report;
            }
            return report;
        }

        #endregion

        


    }
}
