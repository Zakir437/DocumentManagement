using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Models.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManagement.ModelViews.HR;
using Microsoft.EntityFrameworkCore;
using DocumentManagement.Services;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

namespace DocumentManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HRController : Controller
    {
        #region Private Propertise
        private readonly DocumentManagementContext _db;
        IDataProtector _protector;
        IHostingEnvironment _env;
        readonly DateTime now = DateTimeOffset.Now.ToOffset(new TimeSpan(6, 0, 0)).DateTime;
        public HRController(DocumentManagementContext db, IDataProtectionProvider provider, IHostingEnvironment env)
        {
            _db = db;
            _protector = provider.CreateProtector(GetType().FullName);
            _env = env;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetUser(int skip, int take, string selectedId, DateTime? startDate, DateTime? endDate)
        {
            var list = new List<UserInformation>();
            var count = 0;
            if (!string.IsNullOrEmpty(selectedId))
            {
                UserInformation user;
                foreach (var id in selectedId.Split(','))
                {
                    int userId = Convert.ToInt32(id);
                    user = _db.UserInformation.Find(userId);
                    if (user.Id > 0)
                    {
                        list.Add(user);
                    }
                }
                count = list.Count();
                list = list.OrderByDescending(o => o.CreatedDate)
                            .Skip(skip).Take(take)
                            .ToList();
            }
            else
            {
                list = _db.UserInformation.OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
                count = _db.UserInformation.Count();
            }
            var viewList = list.Join(_db.UserLogin, u => u.Id, ul => ul.UserId, (u, ul) => new
            {
                EncryptedId = _protector.Protect(u.Id.ToString()),
                u.Name,
                ul.UserName,
                Status = u.Status == true ? "Active" : "Deleted",
                u.CreatedDate,
                u.Image
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public JsonResult GetUserForMulti(string text)
        {
            var list = new SelectList("");
            if (!string.IsNullOrEmpty(text))
            {
                list = new SelectList(_db.UserLogin.Where(a => a.UserName.ToLower().Contains(text))
                    .Take(50)
                    .Select(a => new { a.UserName, a.UserId }), "UserId", "UserName");
            }
            return Json(list);
        }
        public JsonResult UserUpdate(UserModelView model, bool? isInfoUpdate, bool? isPassUpdate)
        {
            try
            {
                int userId = Convert.ToInt32(_protector.Unprotect(model.EncryptedId));
                var user = _db.UserInformation.Find(userId);
                if (isInfoUpdate == true)
                {
                    user.MobileNumber = model.MobileNumber;
                    user.DateOfBirth = model.DOB;
                    user.Role = model.RoleId;
                }
                else if (isPassUpdate == true)
                {
                    var login = _db.UserLogin.FirstOrDefault(a => a.UserId == userId);
                    login.Password = model.Password;
                    _db.Entry(login).State = EntityState.Modified;
                }
                else
                {
                    user.Name = model.Name;
                }
                user.UpdatedBy = User.GetUserId();
                user.UpdatedDate = now;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public JsonResult UserStatusChange(string id)
        {
            try
            {
                int userId = Convert.ToInt32(_protector.Unprotect(id));
                var user = _db.UserInformation.Find(userId);
                user.Status = false;
                user.UpdatedBy = User.GetUserId();
                user.UpdatedDate = now;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public IActionResult UserDetails(string q, bool? isEdit)
        {
            ViewBag.IsEdit = isEdit;
            int userId = Convert.ToInt32(_protector.Unprotect(q));
            var user = _db.UserInformation.Find(userId);
            var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserId == userId);
            UserModelView model = new UserModelView
            {
                EncryptedId = q,
                Name = user.Name,
                Username = userLogin.UserName,
                Status = user.Status == true ? "Active" : "Deleted",
                Image = user.Image,
                CreatedDate = user.CreatedDate,
                MobileNumber = user.MobileNumber,
                DOB = user.DateOfBirth,
                Role = _db.RoleList.Find(user.Role).RoleName
            };
            return View(model);
        }
        public JsonResult UserImageUpdate(IFormFile file, string userId)
        {
            try
            {
                if (file.Length > 0)
                {
                    int id = Convert.ToInt32(_protector.Unprotect(userId));
                    var user = _db.UserInformation.Find(id);
                    string fileName = "";
                    fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{id.ToString("000")}{Path.GetExtension(file.FileName)}";
                    //save original file
                    var path = $"{_env.WebRootPath}\\images\\User\\Original\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = $"{path}{$@"\{fileName}"}";
                    using (FileStream fs = System.IO.File.Create(path))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    user.Image = fileName;
                    user.UpdatedBy = User.GetUserId();
                    user.UpdatedDate = DateTime.Now;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            catch(Exception)
            {
                return Json("error");
            }
            return Json("success");
        }

        public PartialViewResult UserEdit(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _UserEdit(string id)
        {
            int userId = Convert.ToInt32(_protector.Unprotect(id));
            ViewBag.Name = _db.UserInformation.Find(userId).Name;
            return PartialView();
        }
        public PartialViewResult UserInfoEdit(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _UserInfoEdit(string id)
        {
            int userId = Convert.ToInt32(_protector.Unprotect(id));
            var user = _db.UserInformation.Find(userId);
            var userLogin = _db.UserLogin.FirstOrDefault(a => a.UserId == userId);
            UserModelView model = new UserModelView
            {
                EncryptedId = id,
                MobileNumber = user.MobileNumber,
                DOB = user.DateOfBirth != null ? user.DateOfBirth : null,
                RoleId = user.Role
            };
            return PartialView(model);
        }
        public PartialViewResult ChangePassword(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _ChangePassword()
        {
            return PartialView();
        }
        public JsonResult GetRole()
        {
            var roleList = new SelectList(_db.RoleList.Where(a => a.Status == true).Select(s => new { s.Id, s.RoleName }), "Id", "RoleName");
            return Json(roleList);
        }
        public JsonResult GetUserLog(int skip, int take, string id)
        {
            int userId = Convert.ToInt32(_protector.Unprotect(id));
            var list = new List<DocumentLog>();
            var count = 0;
            list = _db.DocumentLog.Where(a => a.CreatedBy == userId).OrderByDescending(o => o.CreatedDate)
                    .Skip(skip).Take(take)
                    .ToList();
            var viewList = list.Select(s => new
            {
                s.TableName,
                s.CreatedDate,
                s.Status
            }).ToList();
            count = _db.DocumentLog.Where(a => a.CreatedBy == userId).Count();
            return Json(new { total = count, data = viewList });
        }
    }
}