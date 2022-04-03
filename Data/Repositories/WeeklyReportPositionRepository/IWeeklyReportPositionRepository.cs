using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccessEfCore.Repositories.WeeklyReportPositionRepository
{
    public interface IWeeklyReportPositionRepository
    {
        Task<List<WeeklyReportPosition>> GetAllWeeklyReportPositionAsync();
        Task<List<int>> GetAllReportPositionIDAsync();
        Task<WeeklyReportPosition> GetWeeklyReportPositionAsyncById(int id);
        Task<WeeklyReportPosition> CreateWeeklyReportPositionAsync(WeeklyReportPosition weeklyReportPosition);
        Task<WeeklyReportPosition> DeletetWeeklyReportPositionAsync(int id);
        Task<int> GetIDCount();
        
    }
}
