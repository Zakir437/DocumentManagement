using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Models.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DocumentManagement.ModelViews.Account;
using System.IO;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using DocumentManagement.Services;

namespace DocumentManagement.Controllers
{
    public class AccountController : Controller
    {
        #region Private Propertise
        private readonly DocumentManagementContext _db;
        private readonly DateTime _now;
        IDataProtector _protector;
        IHostingEnvironment _env;
        public AccountController(DocumentManagementContext db, IDataProtectionProvider provider, IHostingEnvironment env)
        {
            _db = db;
            _protector = provider.CreateProtector(GetType().FullName);
            _env = env;
            _now = DateTime.UtcNow;
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(ReturnUrl);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView model, string ReturnUrl)
        {
            try
            {
                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();
                if (ModelState.IsValid)
                {
                    if (_db.Database.GetService<IRelationalDatabaseCreator>().Exists())
                    {
                        if (_db.UserLogin.Where(m => m.UserName == model.UserName && m.Password == model.Password).Any())
                        {
                            var login = _db.UserLogin.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
                            if (login.IsConfirmed == true)
                            {
                                var user = _db.UserInformation.FirstOrDefault(x => x.Id == login.UserId);
                                if (user.Role > 0)
                                {

                                    await UserSignIn(login, user, ReturnUrl, model.RememberMe);
                                    //var Roles = _db.RoleList.Where(x => x.Id == user.Role).FirstOrDefault();
                                    //var identity = new ClaimsIdentity(new[] {
                                    //   new Claim(ClaimTypes.Name, $"{user?.Name}"),
                                    //   new Claim(ClaimTypes.NameIdentifier, login?.UserName),
                                    //   new Claim(ClaimTypes.Sid, login?.UserId.ToString()),
                                    //   new Claim(ClaimTypes.Role, Roles?.RoleName),
                                    //   new Claim(ClaimTypes.OtherPhone, Roles?.Id.ToString())
                                    //   }, CookieAuthenticationDefaults.AuthenticationScheme);

                                    //var principal = new ClaimsPrincipal(identity);

                                    //await HttpContext.SignInAsync(
                                    //    CookieAuthenticationDefaults.AuthenticationScheme,
                                    //    principal, new AuthenticationProperties { IsPersistent = model.RememberMe });



                                    //return RedirectToLocal(ReturnUrl);
                                    return RedirectToAction("OAuth", "Account");

                                   if (login.IsLoginBefore == false || login.IsOtpenable == true)
                                   {
                                       return RedirectToAction("ConfirmOTP", "Account", new
                                       {
                                           q = _protector.Protect(login.UserName.ToString()),
                                           p = _protector.Protect(login.Password.ToString()),
                                           IsLoginBefore = login.IsLoginBefore ? "true" : "false",
                                           RememberMe = model.RememberMe ? "true" : "false",
                                           ReturnUrl
                                       });
                                   }
                                   else
                                   {
                                       /*var Roles = _db.RoleList.Where(x => x.Id == user.Role).FirstOrDefault();
                                       var identity = new ClaimsIdentity(new[] {
                                       new Claim(ClaimTypes.Name, $"{user?.Name}"),
                                       new Claim(ClaimTypes.NameIdentifier, login?.UserName),
                                       new Claim(ClaimTypes.Sid, login?.UserId.ToString()),
                                       new Claim(ClaimTypes.Role, Roles?.RoleName),
                                       new Claim(ClaimTypes.OtherPhone, Roles?.Id.ToString())
                                       }, CookieAuthenticationDefaults.AuthenticationScheme);

                                       var principal = new ClaimsPrincipal(identity);

                                       await HttpContext.SignInAsync(
                                           CookieAuthenticationDefaults.AuthenticationScheme,
                                           principal, new AuthenticationProperties { IsPersistent = model.RememberMe });*/

                                    await UserSignIn(login, user, ReturnUrl, model.RememberMe);
                                        //return RedirectToLocal(ReturnUrl);
                                        return RedirectToAction("OAuth", "Account");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Role not set! Please contact your administrator.");
                                    return View(model);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Email has not been confirmed yet!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Email/UserName or Password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Could not connect to the Database!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email/UserName or Password Format.");
                }
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something wrong while connecting!");
            }
            return View(model);
        }
        [NonAction]//User Login Method
        private async Task<bool> UserSignIn(UserLogin login, UserInformation user, string ReturnUrl, bool RememberMe = false)
        {
            try
            {
                var Roles = _db.RoleList.Where(x => x.Id == user.Role).FirstOrDefault();
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, $"{user?.Name}"),
                new Claim(ClaimTypes.NameIdentifier, login?.UserName),
                new Claim(ClaimTypes.Sid, login?.UserId.ToString()),
                new Claim(ClaimTypes.Role, Roles?.RoleName),
                new Claim(ClaimTypes.OtherPhone, Roles?.Id.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, new AuthenticationProperties { IsPersistent = RememberMe });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Authorize]
        public IActionResult OAuth()
        {
            return View();
        }
        //Register User Login Device
        public JsonResult RegisterLoginDevice(UserLoginActivity register)
        {
            try
            {
                if (register != null)
                {
                    var deviceLog = new UserLoginActivity
                    {
                        UserId = User.GetUserId(),
                        Browser = register.Browser,
                        DeviceOs = register.DeviceOs,
                        Ipaddress = register.Ipaddress,
                        City = register.City,
                        Country = register.Country,
                        UniqueId = register.UniqueId,
                        Status = false,
                        LoginDate = _now
                    };
                    _db.UserLoginActivity.Add(deviceLog);
                    _db.SaveChanges();
                    return Json(true);
                }
            }
            catch (Exception e)
            {
                return Json(false);
            }
            return Json(false);
        }
        #endregion

        #region LogOff
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }
        #endregion

        #region Redirect to local method.
        private IActionResult RedirectToLocal(string ReturnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region SignUp
        [Authorize(Roles = "Admin")]
        public IActionResult SignUp()
        {
            return PartialView();
        }
        [Authorize(Roles = "Admin")]
        public JsonResult UserSave(SignUpModelView model)
        {
            try
            {
                UserInformation user = new UserInformation
                {
                    Name = model.Name,
                    //Role = 2, // user role
                    MobileNumber = model.MobileNumber,
                    Status = true,
                    CreatedDate = _now,
                };
                _db.UserInformation.Add(user);
                _db.SaveChanges();

                UserLogin login = new UserLogin
                {
                    UserId = user.Id,
                    UserName = model.UserName,
                    Password = model.Password,
                    IsConfirmed = false,
                    IsLoginBefore = false,
                    IsOtpenable = false
                };
                _db.UserLogin.Add(login);
                _db.SaveChanges();

                //Storage 
                Storage storage = new Storage
                {
                    Allowed = 5368709120,
                    UserId = user.Id,
                    CreatedBy = User.GetUserId(),
                    CreatedDate = _now
                };

                _db.Storage.Add(storage);
                _db.SaveChanges();

                if (login.UserName != null)
                {
                    //send confirmation email
                    string url = string.Empty;
                    string domain = $"{HttpContext.Request.Scheme}{Uri.SchemeDelimiter}{HttpContext.Request.Host}{(HttpContext.Request.Host.HasValue ? "" : ":" + HttpContext.Request.Host.Port)}";
                    url = $"{domain}/Account/ConfirmEmail?q={ _protector.Protect(login.Id.ToString())}";
                    string body = string.Empty;
                    string webRoothPath = _env.WebRootPath;
                    using (StreamReader reader = new StreamReader(webRoothPath + "\\Template\\E-mailTemplate.min.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ApplicantFullName}", user.Name)
                        .Replace("{UserName}", login.UserName)
                        .Replace("{Password}", login.Password)
                        .Replace("{Url}", url)
                        .Replace("{Year}", DateTime.UtcNow.Year.ToString());

                    string mailAddress = model.UserName;
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    mail.To.Add(new MailAddress(mailAddress));
                    mail.From = new MailAddress("admin-filekoi@gmail.com", "File-Koi", System.Text.Encoding.UTF8);
                    mail.Subject = "To Confirm E-mail ";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = body;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient
                    {
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("admin-filekoi@gmail.com", "45604560"),
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Port = 587,
                        Timeout = 18000000
                    };
                    try
                    {
                        client.Send(mail);
                    }
                    catch
                    {
                        client.Port = 25;
                        client.Timeout = 18000000;
                        try
                        {
                            client.Send(mail);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public IActionResult ConfirmEmail(string q)
        {
            try
            {
                long loginId = Convert.ToInt64(_protector.Unprotect(q));
                var userLogin = _db.UserLogin.FirstOrDefault(a => a.Id == loginId);
                if (userLogin.IsConfirmed == false)
                {
                    userLogin.IsConfirmed = true;
                    _db.Entry(userLogin).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.Message = "Your email is successfully confirmed.";
                }
                else if (userLogin.IsConfirmed == true)
                {
                    ViewBag.Message = "This account already confirmed with this email!!!";
                }
                else
                {
                    ViewBag.Message = "Confirmed error.";
                }
            }
            catch
            {
                ViewBag.Message = "Confirmed error.";
            }
            return View();
        }
        public IActionResult ChangePassword(string q)
        {

            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword()
        {
            return View();
        }
        public JsonResult SendResetLink(string email)
        {
            try
            {
                var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserName == email);
                if (userLogin != null)
                {
                    var userName = _db.UserInformation.FirstOrDefault(a => a.Id == userLogin.UserId).Name;

                    //send password reset link to email
                    string url = string.Empty;
                    string domain = $"{HttpContext.Request.Scheme}{Uri.SchemeDelimiter}{HttpContext.Request.Host}{(HttpContext.Request.Host.HasValue ? "" : ":" + HttpContext.Request.Host.Port)}";
                    url = $"{domain}/Account/SetNewPassword?q={ _protector.Protect(userLogin.Id.ToString())}";
                    string body = string.Empty;
                    string webRootPath = _env.WebRootPath;
                    using (StreamReader reader = new StreamReader(webRootPath + "\\Template\\ResetPasswordTemplate.min.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ApplicantFullName}", userName);
                    body = body.Replace("{Url}", url);
                    string mailAddress = email;
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    mail.To.Add(new MailAddress(mailAddress));
                    mail.From = new MailAddress("spicyb.developer@gmail.com", "Order Management", System.Text.Encoding.UTF8);
                    mail.Subject = "To reset new password ";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = body;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient
                    {
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("spicyb.developer@gmail.com", "45604560"),
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Port = 587,
                        Timeout = 18000000
                    };
                    try
                    {
                        client.Send(mail);
                    }
                    catch
                    {
                        client.Port = 25;
                        client.Timeout = 18000000;
                        try
                        {
                            client.Send(mail);
                        }
                        catch
                        {
                            return Json("error");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public IActionResult SetNewPassword(string q)
        {
            ViewBag.Id = q;
            return View();
        }
        public JsonResult SetPassword(string id, string password)
        {
            try
            {
                long loginId = Convert.ToInt64(_protector.Unprotect(id));
                var userLogin = _db.UserLogin.FirstOrDefault(a => a.Id == loginId);
                userLogin.Password = password;
                _db.Entry(userLogin).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        #endregion

        #region Send OTP PIN
        //[Authorize]
        public IActionResult ConfirmOTP(string q, string p, string IsLoginBefore, string RememberMe, string ReturnUrl)
        {
            if(q != null && p != null)
            {
                try
                {
                    string userName = _protector.Unprotect(q);
                    string password = _protector.Unprotect(p);
                    var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserName == userName && a.Password == password);
                    var userInformation = _db.UserInformation.FirstOrDefault(a => a.Id == userLogin.UserId);
                    if(!(userLogin.IsConfirmed || userInformation.Role > 0))
                    {
                        return View(new ConfirmOTPModel { Error = true });
                    }
                    var OTP = new ConfirmOTPModel
                    {
                        UserName = q,
                        Password = p,
                        IsLoginBefore = IsLoginBefore,
                        RememberMe = RememberMe,
                        ReturnUrl = ReturnUrl,
                        MobileNumber = userInformation.MobileNumber.Substring(0, 7),
                        Error = false
                    };
                    return View(OTP);
                }
                catch
                {
                    return View(new ConfirmOTPModel { Error = true });
                }
            }
            return View(new ConfirmOTPModel { Error = true });
        }
        public JsonResult ConfirmOTP4Digit(ConfirmOTPModel oTPModel)
        {
            if(oTPModel.UserName != null && oTPModel.Password != null && oTPModel.Last4Digit != null)
            {
                try
                {
                    string userName = _protector.Unprotect(oTPModel.UserName);
                    string password = _protector.Unprotect(oTPModel.Password);
                    var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserName == userName && a.Password == password);
                    var userInformation = _db.UserInformation.FirstOrDefault(a => a.Id == userLogin.UserId);
                    if (userInformation.MobileNumber.Substring(7) == oTPModel.Last4Digit)
                    {
                        if (userLogin.Otpcode > 99999)
                        {
                            SendSMS.DoSendSMS(new SendSMSModel
                            {
                                Message = $"{userLogin.Otpcode} is your login code. Don't reply this message.",
                                MobileNumber = $"88{userInformation.MobileNumber}"
                            });
                        }
                        else
                        {
                            Random rnd = new Random();
                            int rnd_Code = rnd.Next(100000, 999999);//6digit random number
                            SendSMS.DoSendSMS(new SendSMSModel
                            {
                                Message = $"{rnd_Code} is your login code. Don't reply this message.",
                                MobileNumber = $"88{userInformation.MobileNumber}"
                            });

                            userLogin.Otpcode = rnd_Code;
                            _db.Entry(userLogin).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        
                        return Json("success");
                    }
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
            return Json(false);
        }
        public async Task<JsonResult> OTPVerificationCodeCheck(ConfirmOTPModel oTPModel)
        {
            if (oTPModel.UserName != null && oTPModel.Password != null && oTPModel.VarificationCode > 99999)
            {
                try
                {
                    string userName = _protector.Unprotect(oTPModel.UserName);
                    string password = _protector.Unprotect(oTPModel.Password);
                    var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserName == userName && a.Password == password);
                    var userInformation = _db.UserInformation.FirstOrDefault(a => a.Id == userLogin.UserId);
                    if (userLogin.Otpcode == oTPModel.VarificationCode)
                    {
                        await UserSignIn(userLogin, userInformation, oTPModel.ReturnUrl, oTPModel.RememberMe == "true" ? true : false);
                        userLogin.IsLoginBefore = oTPModel.IsLoginBefore == "false"? true : userLogin.IsLoginBefore;//if first login
                        userLogin.Otpcode = null;
                        _db.Entry(userLogin).State = EntityState.Modified;
                        _db.SaveChanges();
                        return Json("success");
                    }
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
            return Json(false);
        }
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return PartialView();
        }
        #endregion
    }
}