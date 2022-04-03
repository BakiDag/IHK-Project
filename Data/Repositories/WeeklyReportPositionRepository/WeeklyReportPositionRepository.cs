using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessEfCore.DataContext;

namespace DataAccessEfCore.Repositories.WeeklyReportPositionRepository
{
    public class WeeklyReportPositionRepository : IWeeklyReportPositionRepository
    {
        private readonly WochenberichtDBContext _dbContext;
        #region ctor
        public WeeklyReportPositionRepository(WochenberichtDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region CreateOne
        public async Task<WeeklyReportPosition> CreateWeeklyReportPositionAsync(WeeklyReportPosition weeklyReportPosition)
        {
            
            var added = _dbContext.WeeklyReportPositions.Add(weeklyReportPosition) != null;
            if(added == false)
                _dbContext.Dispose();
            return weeklyReportPosition;
        }

        #endregion
        public async Task<int> GetIDCount()
        {
            var count = _dbContext.WeeklyReportPositions.Count(t => t.ID == 1);
            return count;
        }
        #region GetOne
        public async Task<WeeklyReportPosition?> GetWeeklyReportPositionAsyncById(int id)
        {
            var OneReportPosition = await _dbContext.WeeklyReportPositions.FirstOrDefaultAsync(w => w.ID == id);
            return OneReportPosition;
        }
        #endregion

        #region GetAll

        public async Task<List<WeeklyReportPosition>> GetAllWeeklyReportPositionAsync()
        {
            return await _dbContext.WeeklyReportPositions.ToListAsync();
        }
        #endregion
        #region GetAllID
        public async Task<List<int>> GetAllReportPositionIDAsync()
        {
            return _dbContext.WeeklyReportPositions.Select(x => x.ID).ToList();

        }
        #endregion

        

        #region DeleteOne
        public async Task<WeeklyReportPosition> DeletetWeeklyReportPositionAsync(int id)
        {
            var position = _dbContext.WeeklyReportPositions.Where(x => x.ID == id)
                                                            .FirstOrDefault();
            if (position != null)
            {
                _dbContext.WeeklyReportPositions.Remove(position);                
                return position;
            }
            return position;
        }

        
        #endregion



    }
}
