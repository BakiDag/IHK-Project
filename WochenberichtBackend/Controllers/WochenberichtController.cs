
using DataAccessEfCore.Repositories;
using DataAccessEfCore.Repositories.WeeklyReportRepository;
using DataAccessEfCore.UnitOfWork;
using Domain.Models;
using Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using WochenberichtManagement.Models.BindingModel;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Wochenbericht.Models;
using DataAccessEfCore.DataContext;
using WochenberichtWebApp.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System.Threading;

namespace Wochenbericht.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/wochenbericht")]
    public class WochenberichtController : ControllerBase
    {
        #region Variables for Dependency Injection
        private readonly IUnitOfWork unitOfWork;
        private readonly JWTConfig _jWTConfig;
        #endregion

        #region ctor
        public WochenberichtController(IUnitOfWork _unitOfWork, IOptions<JWTConfig> jwtConfig)
        {
            this.unitOfWork = _unitOfWork;
            _jWTConfig = jwtConfig.Value;

        }
        #endregion

        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            var ReportList = await unitOfWork.WeeklyReportRepository.GetAllWeeklyReportAsync();
            if (ReportList == null)
                return NotFound();
            return Ok(ReportList);
        }
        [HttpGet("GetAllReportPositions")]
        public async Task<IActionResult> GetAllReportPositions()
        {
            var ReportPositions = await unitOfWork.WeeklyReportRepository.GetAllWeeklyReportAsync();
            if (ReportPositions == null)
                return NotFound();
            return Ok(ReportPositions);
        }


        
        #region Createone

        #region CreateWeeklyReport
        [AllowAnonymous]
        [HttpPost("CreateWeeklyReport")]
        public async Task<IActionResult> CreateWeeklyReport(WeeklyReport _weeklyReport)
        {
            try
            {
                var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                var reportExist = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(monday) != null;
                
                if (reportExist == true)
                {
                    monday = monday.AddDays(7);
                }
                var today = DateTime.Today;
                var friday = monday.AddDays(4);
                
                var weeklyReport = new WeeklyReport
                {
                    InstructorID = _weeklyReport.InstructorID,
                    CalenderWeek = _weeklyReport.CalenderWeek,
                    DateFrom = monday,
                    DateTo = friday,
                    Page = _weeklyReport.Page,
                    ApprenticeID = _weeklyReport.ApprenticeID,
                    StatusApprentice = _weeklyReport.StatusApprentice,
                    StatusInstructor = _weeklyReport.StatusInstructor,
                    SigningApprentice = _weeklyReport.SigningApprentice,
                    SigningInstructor = _weeklyReport.SigningInstructor,
                    SigningDateInstructor = _weeklyReport.SigningDateInstructor,
                    SigningDateApprentice = _weeklyReport.SigningDateApprentice,
                    WeeklyReportPositions = _weeklyReport.WeeklyReportPositions,
                };
                var getReportID = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(monday);
                
                var Appr = await unitOfWork.ApprenticeRepository.GetApprenticeAsyncById(_weeklyReport.ApprenticeID);
                var Inst = await unitOfWork.InstrutorRepository.GetInstructorAsyncById(_weeklyReport.InstructorID);
                                
                var result = await unitOfWork.WeeklyReportRepository.CreateWeeklyReportAsync(weeklyReport) != null;
                if (result == false)
                {
                    BadRequest("Wochenbericht konnte nicht hinzugefügt werden");
                }
                var complete = await unitOfWork.SaveAsync();
                if (complete == false)
                {
                    return BadRequest("Wochenbericht mit Positionen und Notes konnte nicht gespeichert werden " + weeklyReport);
                }
                var getReport = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(monday);
                var weeklyReportPosition = new WeeklyReportPosition();
                //weeklyReportPosition.WeeklyReportID = getReport.Result.ID;
                weeklyReportPosition.ApprenticeID = Appr.ID;
                //weeklyReportPosition.NoteID = _weeklyReportPosition.NoteID;
                weeklyReportPosition.DailyReport = "";
                weeklyReportPosition.DailyHours = 0;
                weeklyReportPosition.Date = monday;

                var createReport = await unitOfWork.WeeklyReportPositionRepository.CreateWeeklyReportPositionAsync(weeklyReportPosition) != null;
                if (createReport == true)
                {
                    var savePosition = await unitOfWork.SaveAsync();
                    if (savePosition == true)
                    {
                        return StatusCode(201, weeklyReportPosition);
                    }
                }
                else
                {
                    return BadRequest(weeklyReportPosition);
                }
                return StatusCode(201, "Weekly Report Created\r\n" + weeklyReport);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region CreateReportPosition
        [HttpPost("CreateReportPosition")]
        public async Task<IActionResult> CreateReportPosition([FromBody] WeeklyReportPosition _weeklyReportPosition)
        {
            var weeklyReportPosition = new WeeklyReportPosition();
            //weeklyReportPosition.WeeklyReportID = _weeklyReportPosition.WeeklyReportID;
            weeklyReportPosition.ApprenticeID = _weeklyReportPosition.ApprenticeID;
            weeklyReportPosition.NoteID = _weeklyReportPosition.NoteID;
            weeklyReportPosition.DailyReport = _weeklyReportPosition.DailyReport;
            weeklyReportPosition.DailyHours = _weeklyReportPosition.DailyHours;
            weeklyReportPosition.Date = _weeklyReportPosition.Date;

            var result = await unitOfWork.WeeklyReportPositionRepository.CreateWeeklyReportPositionAsync(weeklyReportPosition) != null;
            if (result == true)
            {
                var complete = await unitOfWork.SaveAsync();
                if (complete == true)
                {
                    return StatusCode(201, weeklyReportPosition);
                }
            }
            return BadRequest(weeklyReportPosition);
        }
        #endregion

        #region CreateNote
        [HttpPost("CreateNote")]
        public async Task<IActionResult> CreateNote([FromBody] Note _note)
        {
            Note note = new Note();
            //note.WeeklyReportPositionsID = _note.WeeklyReportPositionsID;
            note.InstructorID = _note.InstructorID;
            note.Comment = _note.Comment;

            var added = await unitOfWork.NotesRepository.CreateNoteAsync(note) != null;
            if (added == true)
            {
                var complete = await unitOfWork.SaveAsync();
                if (complete == true)
                {
                    return StatusCode(201, note);
                }
            }
            return BadRequest(_note);
        }
        #endregion
        #endregion
        
        

       

        
    }

}
