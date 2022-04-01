using DataAccessEfCore.UnitOfWork;
using Domain.Models;
using Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WochenberichtManagement.Models.BindingModel;
using WochenberichtWebApp.Models;

namespace WochenberichtWebApp.Controllers
{
    [ApiController]
    [Route("api/userManagment")]
    public class UserManagementController : ControllerBase
    {
        #region Variables for Dependency Injection
        private readonly IUnitOfWork unitOfWork;
        private readonly JWTConfig _jWTConfig;
        #endregion
        
        #region ctor
        public UserManagementController(IUnitOfWork _unitOfWork, IOptions<JWTConfig> jwtConfig)
        {
            this.unitOfWork = _unitOfWork;
            _jWTConfig = jwtConfig.Value;

        }
        #endregion


        #region Login

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region login seperate
                    var adminRegistered = unitOfWork.Adminrepository.AdminExists(model.Email);
                    if (adminRegistered)
                    {
                        var AdminResult = unitOfWork.Adminrepository.AdminLoginValid(model.Email, model.Password);
                        if (AdminResult == true)
                        {
                            var appUser = unitOfWork.Adminrepository.GetAdminAsyncByEmail(model.Email).Result;
                            appUser.Token = GenerateAdminToken(appUser, appUser.Role.ToString());
                            var result2d = await unitOfWork.SaveAsync();
                            if (result2d == true)
                            {
                                var user = new UserDTO
                                {
                                    Email = model.Email,
                                    UserName = model.Email,
                                    FirstName = appUser.FirstName,
                                    LastName = appUser.LastName,
                                    Role = appUser.Role.ToString(),
                                    Token = appUser.Token,
                                };
                                user.Token = GenerateAdminToken(appUser, appUser.Role.ToString());
                                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "user logged in", user));
                            }
                            return BadRequest("Token konnte nicht erstellt werden");
                        }
                        return BadRequest("Passwort falsch");
                    }
                    var InstructorRegistered = unitOfWork.InstrutorRepository.InstructorExists(model.Email);
                    if (InstructorRegistered)
                    {
                        var InstResult = unitOfWork.InstrutorRepository.InstructorLoginValid(model.Email, model.Password);
                        if (InstResult == true)
                        {
                            var appUser = unitOfWork.InstrutorRepository.GetInstructorAsyncByEmail(model.Email).Result;
                            appUser.Token = GenerateInstructorToken(appUser, appUser.Role.ToString());
                            var result2d = await unitOfWork.SaveAsync();
                            if (result2d == true)
                            {
                                var user = new UserDTO
                                {
                                    Email = model.Email,
                                    UserName = model.Email,
                                    FirstName = appUser.FirstName,
                                    LastName = appUser.LastName,
                                    Role = appUser.Role.ToString(),
                                    Token = appUser.Token,
                                };
                                user.Token = GenerateInstructorToken(appUser, appUser.Role.ToString());
                                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                            }
                        }
                        return BadRequest("Passwort falsch");
                    }
                    var ApprenticeRegistered = unitOfWork.ApprenticeRepository.ApprenticeExists(model.Email);
                    if (ApprenticeRegistered)
                    {
                        var ApprResult = unitOfWork.ApprenticeRepository.ApprenticeLoginValid(model.Email, model.Password);
                        if (ApprResult == true)
                        {
                            var appUser = unitOfWork.ApprenticeRepository.GetApprenticeAsyncByEmail(model.Email).Result;
                            appUser.Token = GenerateApprenticeToken(appUser, appUser.Role.ToString());
                            var result2d = await unitOfWork.SaveAsync();
                            if (result2d == true)
                            {
                                var user = new UserDTO
                                {
                                    Email = model.Email,
                                    UserName = model.Email,
                                    FirstName = appUser.FirstName,
                                    LastName = appUser.LastName,
                                    Role = appUser.Role.ToString(),
                                    Token = appUser.Token,
                                };
                                user.Token = GenerateApprenticeToken(appUser, appUser.Role.ToString());
                                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                            }
                        }
                        return BadRequest("Passwort falsch");

                        #endregion
                    }
                    return NotFound("Email Adresse ist nicht registriert");
                }
                return BadRequest("Model State invalid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion
        #region Logout
        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<object> Logout([FromBody] LoginModel model)
        {
            try
            {
                var appUser = unitOfWork.Adminrepository.GetAdminAsyncByEmail(model.Email).Result;
                                
                    var user = new UserDTO
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        Role = appUser.Role.ToString(),
                        Token = appUser.Token,
                    };
                
                user.Token = ExpireAdminToken(appUser, appUser.Role.ToString());
                    var tokendel = await unitOfWork.Update(model.Email); //checken wegen update method in admin repo
                var complete = unitOfWork.SaveAsync().Result;
                if (complete == false)
                    return "Token konnte nicht geloescht werden";
                if (tokendel == true)
                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User konnte nicht ausgeloogt werden", model.Email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region deleteContact
        [AllowAnonymous]
        [HttpPost("deleteContact")]
        public async Task<IActionResult> deleteContact([FromBody] string _mail)
        {
            var user = unitOfWork.deleteContact(_mail).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _mail);
        }
        #endregion
        #region GetAllEmployee
        //[Authorize]
        [HttpGet("GetAllEmployee")]
        public async Task<object> GetAllEmployee()
        {
            var EmployeeList = await unitOfWork.GetAllEmployee();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", EmployeeList));
        }
        #endregion
        #region SearchAllEmployeeByEmail
        [AllowAnonymous]
        [HttpGet("GetEmployeeAsyncByEmail")]
        public async Task<object> GetEmployeeAsyncByEmail([FromBody] string _mail)
        {
            var employee = await unitOfWork.GetEmployeeAsyncByEmail(_mail);
            if (employee.GetType() == typeof(string))
                return NotFound();
            return employee;
        }

        #endregion

        #region Admin
        #region CreateAdmin
        [AllowAnonymous]
        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] Admin _admin)
        {
            var admin = new Admin
            {
                Email = _admin.Email,
                Password = _admin.Password,
                UserName = _admin.Email,
                FirstName = _admin.FirstName,
                LastName = _admin.LastName,
                DateEntry = _admin.DateEntry,
                Role = (Domain.Models.Role.Admin),
                Token = _admin.Token,
            };
            var result = await unitOfWork.Adminrepository.CreateAdminAsync(admin);
            if (result == null)
            {
                return BadRequest("Benutzer konnte nicht erstellt werden. \n\rEmail Adresse bereits vergeben\n\r" + _admin.Email);
            }
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
            {
                return BadRequest("Daten konnten nicht gespeichert werden");
            }
            return StatusCode(201, "Benutzer: \n\r" + _admin.Email + "\n\rerfolgreich erstellt");

        }
        #endregion
        #region GetAdminByMail
        [AllowAnonymous]
        [HttpGet("GetAdminByMail")]
        public async Task<IActionResult> GetAdminByMail([FromBody] string _mail)
        {

            var admin = await unitOfWork.Adminrepository.GetAdminAsyncByEmail(_mail);
            if (admin.Email == null)
                return NotFound("Mail Doesnt exists");
            return Ok(admin);
        }
        #endregion
        #region GetAllAdmins
        [AllowAnonymous]
        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {

            var adminList = await unitOfWork.Adminrepository.GetAllAdminsAsync();
            if (adminList == null)
                return NotFound("Admins Doesnt exists");
            return Ok(adminList);
        }

        #endregion
        #region DeleteAdminAsync
        [AllowAnonymous]
        [HttpPost("DeleteAdminbyID")]
        public async Task<IActionResult> DeleteAdminbyID([FromBody] int _id)
        {
            var user = unitOfWork.Adminrepository.DeleteAdminAsync(_id).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _id);
        }
        #endregion
        #region DeleteAdminbyEmail
        [AllowAnonymous]
        [HttpPost("DeleteAdminbyEmail")]
        public async Task<IActionResult> DeleteAdminbyEmail([FromBody] string _email)
        {
            var user = unitOfWork.Adminrepository.DeleteAdminByEmail(_email).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _email);
        }
        #endregion
        #region UpdateAdmin
        [AllowAnonymous]
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody]  Admin admin)
        {
            var user = unitOfWork.Adminrepository.UpdateAdmin(admin).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Update Durchgeführt für: " + user.Email);
        }
        #endregion
        #endregion

        #region Instructor
        #region CreateInstructor
        [AllowAnonymous]
        [HttpPost("CreateInstructor")]
        public async Task<IActionResult> CreateInstructor([FromBody] Instructor _instructor)
        {
            var instructor = new Instructor
            {
                Email = _instructor.Email,
                Password = _instructor.Password,
                UserName = _instructor.Email,
                FirstName = _instructor.FirstName,
                LastName = _instructor.LastName,
                DateEntry = _instructor.DateEntry,
                Role = (Domain.Models.Role.Admin),
                Token = _instructor.Token,
            };
            var result = await unitOfWork.InstrutorRepository.CreateInstructorAsync(instructor);
            if (result == null)
            {
                return BadRequest("Benutzer konnte nicht erstellt werden. \n\rEmail Adresse bereits vergeben\n\r" + _instructor.Email);
            }
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
            {
                return BadRequest("Daten konnten nicht gespeichert werden");
            }
            return StatusCode(201, "Benutzer: \n\r" + _instructor.Email + "\n\rerfolgreich erstellt");

        }
        #endregion
        #region GetAdminByMail
        [AllowAnonymous]
        [HttpGet("GetInstructorByMail")]
        public async Task<IActionResult> GetInstructorByMail([FromBody] string _mail)
        {

            var instructor = await unitOfWork.InstrutorRepository.GetInstructorAsyncByEmail(_mail);
            if (instructor.Email == null)
                return NotFound("Mail Doesnt exists");
            return Ok(instructor);
        }
        #endregion
        #region GetAllInstructors
        [AllowAnonymous]
        [HttpGet("GetAllInstructors")]
        public async Task<IActionResult> GetAllInstructors()
        {

            var instructorList = await unitOfWork.InstrutorRepository.GetAllInstructorsAsync();
            if (instructorList == null)
                return NotFound("Instructor Doesnt exists");
            return Ok(instructorList);
        }

        #endregion
        #region DeleteInstroctorAsync
        [AllowAnonymous]
        [HttpPost("DeleteInstructorID")]
        public async Task<IActionResult> DeleteInstructorID([FromBody] int _id)
        {
            var user = unitOfWork.InstrutorRepository.DeleteInstructorAsync(_id).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _id);
        }
        #endregion
        #region DeleteInstroctorbyEmail
        [AllowAnonymous]
        [HttpPost("DeleteInstructorbyEmail")]
        public async Task<IActionResult> DeleteInstructorbyEmail([FromBody] string _email)
        {
            var user = unitOfWork.InstrutorRepository.DeleteInstructorByEmail(_email).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _email);
        }
        #endregion
        #region UpdateInstructor
        [AllowAnonymous]
        [HttpPut("UpdateInstructor")]
        public async Task<IActionResult> UpdateInstructor([FromBody] Instructor instructor)
        {
            var user = unitOfWork.InstrutorRepository.UpdateInstructor(instructor).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Update Durchgeführt für: " + user.Email);
        }
        #endregion
        #endregion

        #region Apprentice
        #region CreateApprentice
        [AllowAnonymous]
        [HttpPost("CreateApprentice")]
        public async Task<IActionResult> CreateApprentice([FromBody] Apprentice _apprentice)
        {
            var apprentice = new Apprentice
            {
                Email = _apprentice.Email,
                Password = _apprentice.Password,
                UserName = _apprentice.Email,
                FirstName = _apprentice.FirstName,
                LastName = _apprentice.LastName,
                DateEntry = _apprentice.DateEntry,
                Role = (Domain.Models.Role.Admin),
                Token = _apprentice.Token,
            };
            var result = await unitOfWork.ApprenticeRepository.CreateApprenticeAsync(apprentice);
            if (result == null)
            {
                return BadRequest("Benutzer konnte nicht erstellt werden. \n\rEmail Adresse bereits vergeben\n\r" + _apprentice.Email);
            }
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
            {
                return BadRequest("Daten konnten nicht gespeichert werden");
            }
            return StatusCode(201, "Benutzer: \n\r" + apprentice.Email + "\n\rerfolgreich erstellt");

        }
        #endregion
        #region GetAdminByMail
        [AllowAnonymous]
        [HttpGet("GetApprenticeByMail")]
        public async Task<IActionResult> GetApprenticeByMail([FromBody] string _mail)
        {

            var apprentice = await unitOfWork.ApprenticeRepository.GetApprenticeAsyncByEmail(_mail);
            if (apprentice.Email == null)
                return NotFound("Mail Doesnt exists");
            return Ok(apprentice);
        }
        #endregion
        #region GetAllApprentices
        [AllowAnonymous]
        [HttpGet("GetAllApprentices")]
        public async Task<IActionResult> GetAllApprentices()
        {

            var apprenticeList = await unitOfWork.ApprenticeRepository.GetAllApprenticesAsync();
            if (apprenticeList == null)
                return NotFound("Instructor Doesnt exists");
            return Ok(apprenticeList);
        }

        #endregion
        #region DeleteApprenticeID
        [AllowAnonymous]
        [HttpPost("DeleteApprenticeID")]
        public async Task<IActionResult> DeleteApprenticeID([FromBody] int _id)
        {
            var user = unitOfWork.ApprenticeRepository.DeleteApprenticeAsync(_id).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _id);
        }
        #endregion
        #region DeleteApprenticebyEmail
        [AllowAnonymous]
        [HttpPost("DeleteApprenticebyEmail")]
        public async Task<IActionResult> DeleteApprenticebyEmail([FromBody] string _email)
        {
            var user = unitOfWork.ApprenticeRepository.DeleteApprenticeByEmail(_email).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Nutzer geloescht " + _email);
        }
        #endregion
        #region UpdateApprentice
        [AllowAnonymous]
        [HttpPut("UpdateApprentice")]
        public async Task<IActionResult> UpdateApprentice([FromBody] Apprentice apprentice)
        {
            var user = unitOfWork.ApprenticeRepository.UpdateApprentice(apprentice).Result;
            if (user == null)
                return BadRequest("User existiert nicht");
            var complete = await unitOfWork.SaveAsync();
            if (complete == false)
                return BadRequest("Nutzer konnte nicht geloescht werden");
            return Ok("Update Durchgeführt für: " + user.Email);
        }
        #endregion
        #endregion

        #region GereateToken for Admin
        private string GenerateAdminToken(Admin user, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);
            return jwttokenHandler.WriteToken(token);
        }
        #endregion

        #region GenerateToken for Instructor
        private string GenerateInstructorToken(Instructor user, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);
            return jwttokenHandler.WriteToken(token);
        }
        #endregion
        #region GenerateToken for Apprentice
        private string GenerateApprenticeToken(Apprentice user, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);
            return jwttokenHandler.WriteToken(token);
        }
        #endregion

        #region Expire Token for Admin
        private string ExpireAdminToken(Admin admin, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, admin.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, admin.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddSeconds(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);

            admin.Token = "";
            var user = unitOfWork.Adminrepository.UpdateAdmin(admin).Result;
            if (user == null)
                return "User existiert nicht";
            return jwttokenHandler.WriteToken(token) + "Update Durchgeführt für: " + user.Email;
        }
        #endregion

        #region Expire Token for Instructor
        private string ExpireInstructorToken(Instructor user, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddSeconds(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);
            return jwttokenHandler.WriteToken(token);
        }
        #endregion

        #region Expire Token for Apprentice
        private string ExpireApprenticeToken(Apprentice user, string role)
        {
            var jwttokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, role ),
                }),
                Expires = DateTime.UtcNow.AddSeconds(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwttokenHandler.CreateToken(tokenDescriptor);
            return jwttokenHandler.WriteToken(token);
        }
        #endregion
    }
}
