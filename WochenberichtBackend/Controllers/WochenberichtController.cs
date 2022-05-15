
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
using FluentDateTime;

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
                DateTime ThisWeekMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime today = DateTime.Today;
                ThisWeekMonday = ThisWeekMonday.Date;
                _weeklyReport.DateFrom = _weeklyReport.DateFrom.Date;
                _weeklyReport.DateTo = _weeklyReport.DateTo.Date;
                //Vorbereiten fuer Wochen Datum Unabhaengig erstellen
                //if(_weeklyReport.DateFrom.DayOfWeek == DayOfWeek.Monday)
                //{
                //    var ItsMonday = true;
                //    return Ok("Its Monday");
                //}
                //else
                //{
                //    return BadRequest("Date From ist nicht monday");
                //}

                if (_weeklyReport.DateFrom != ThisWeekMonday || _weeklyReport.DateTo != ThisWeekMonday.AddDays(4))
                {
                    return BadRequest("Datum falsch");
                }
                
                var thisWeekReportExist = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(ThisWeekMonday);
                
                if(thisWeekReportExist.Result == null)
                {
                    _weeklyReport.DateFrom = ThisWeekMonday;
                }
                else
                {
                    return BadRequest("Wochenbericht existiert bereits");
                }   
                
                var weeklyReport = new WeeklyReport
                {
                    InstructorID = _weeklyReport.InstructorID,
                    CalenderWeek = _weeklyReport.CalenderWeek,
                    DateFrom = _weeklyReport.DateFrom,
                    DateTo = _weeklyReport.DateFrom.AddDays(4),
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
                
                //var getReportID = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(weeklyReport.DateFrom).Result.ID;
                
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

                    return BadRequest("Wochenbericht mit Positionen nicht gespeichert werden " + weeklyReport);
                }
                else
                {
                    var getReport = unitOfWork.WeeklyReportRepository.GetWeeklyReportAsyncByDateFrom(weeklyReport.DateFrom);

                    var countPositionDays = 0;
                    var FivePositionsSaved = 0;
                    

                    while (getReport.Result.DateFrom < getReport.Result.DateTo)                    
                    {
                        var notes = new Note();                        
                        notes.InstructorID = weeklyReport.InstructorID;
                        
                        var weeklyReportPosition = new WeeklyReportPosition();
                        weeklyReportPosition.WeeklyReportID = getReport.Result.ID;
                        weeklyReportPosition.ApprenticeID = Appr.ID;
                        
                        
                        weeklyReportPosition.DailyReport = "";
                        weeklyReportPosition.DailyHours = 0;
                        if(countPositionDays == 0)
                        {
                            weeklyReportPosition.Date = weeklyReport.DateFrom;
                        }
                        else
                        {
                            weeklyReportPosition.Date = weeklyReport.DateFrom.AddDays(countPositionDays);
                        }
                        countPositionDays++;
                        var createReportPosition = await unitOfWork.WeeklyReportPositionRepository.CreateWeeklyReportPositionAsync(weeklyReportPosition);
                        
                        bool created;
                        if(createReportPosition != null)
                        {
                            created = true;
                            //var savePosition = await unitOfWork.SaveAsync();
                        }
                        else
                        {
                            var removeReport = unitOfWork.WeeklyReportRepository.DeleteWeeklyReportAsync(getReport.Result.ID) != null;
                            if(removeReport == true)
                            {
                                return BadRequest("Problems at creating Position. Report deleted");
                            }
                        }
                        
                        if (created = true)
                        {
                            var savePosition = await unitOfWork.SaveAsync();
                            var getPosition = await unitOfWork.WeeklyReportPositionRepository.GetWeeklyReportPositionAsyncById(createReportPosition.ID);
                            
                            notes.WeeklyReportPositionsID= getPosition.ID;
                            
                            var createNote = unitOfWork.NotesRepository.CreateNoteAsync(notes);
                            var saveNote = await unitOfWork.SaveAsync();                            
                            weeklyReportPosition.NoteID = createNote.Result.ID;
                            
                            // get last 5 notes and update position note ID
                            // position ID and Instructor ID needed for searching note with new get method

                            if (savePosition == true)
                            {
                                FivePositionsSaved++;
                            }
                            else
                            {
                                var removePosition = await unitOfWork.WeeklyReportPositionRepository.DeletetWeeklyReportPositionAsync(getPosition.ID);
                                var removeNote = await unitOfWork.NotesRepository.DeleteNoteAsyncByID(removePosition.NoteID);
                                return BadRequest("Check if Position and note deleted "+weeklyReportPosition);
                            }
                            if (FivePositionsSaved == 5)
                            {
                                var saveLastPos = unitOfWork.SaveAsync() != null;
                                if (saveLastPos == false)
                                    return BadRequest(weeklyReportPosition);
                                return StatusCode(201, weeklyReportPosition);
                            }
                        }
                    }
                    
                    return StatusCode(201, "Weekly Report Created\r\n" + weeklyReport);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #endregion
        
        

       

        
    }

}
