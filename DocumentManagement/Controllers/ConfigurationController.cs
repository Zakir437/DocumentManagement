using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Hubs;
using DocumentManagement.Models.Data;
using DocumentManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ConfigurationController : Controller
    {
        #region Private Propertise
        private readonly DocumentManagementContext _db;
        private readonly IDataProtector _protector;
        private readonly DateTime now = DateTimeOffset.Now.ToOffset(new TimeSpan(6, 0, 0)).DateTime;
        private readonly IHubContext<MessagingHub> _hubContext;

        public IHubContext<StronglyTypedMessagingHub, IMessagingClient> _strongChatHubContext { get; }
        public ConfigurationController(DocumentManagementContext db, 
            IDataProtectionProvider provider, IHubContext<MessagingHub> hubContext)
        {
            _db = db;
            _protector = provider.CreateProtector(GetType().FullName);
            _hubContext = hubContext;
        }
        #endregion
        
        #region Role
        public IActionResult RoleCreate()
        {
            return View();
        }
        public JsonResult RoleSave(string name, int? id)
        {
            try
            {
                RoleList role;
                if (id > 0)
                {
                    role = _db.RoleList.Find(id);
                    role.RoleName = name;
                    role.UpdatedBy = User.GetUserId();
                    role.UpdatedDate = now;
                    _db.Entry(role).State = EntityState.Modified;
                }
                else
                {
                    role = new RoleList();
                    role.RoleName = name;
                    role.Status = true;
                    role.CreatedBy = User.GetUserId();
                    role.CreatedDate = now;
                    _db.RoleList.Add(role);
                }
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public IActionResult RoleList()
        {
            return View();
        }
        public JsonResult GetRoleList(int skip, int take, string selectedId)
        {
            var list = new List<RoleList>();
            var count = 0;
            if (!string.IsNullOrEmpty(selectedId))
            {
                int roleId = 0;
                foreach (var id in selectedId.Split(','))
                {
                    roleId = Convert.ToInt32(id);
                    var role = _db.RoleList.FirstOrDefault(a => a.Id == roleId);
                    if (role != null)
                    {
                        list.Add(role);
                    }
                }
                count = list.Count();
                list = list.OrderByDescending(a => a.CreatedDate)
                        .Skip(skip).Take(take).ToList();
            }
            else
            {
                count = _db.RoleList.Where(a => a.Status != null).Count();
                list = _db.RoleList.Where(a => a.Status != null).OrderByDescending(a => a.CreatedDate)
                    .Skip(skip).Take(take).ToList();
            }
            var viewList = list.Join(_db.UserInformation, t => t.CreatedBy, u => u.Id, (t, u) => new
            {
                t.Id,
                t.RoleName,
                CreatedBy = u.Name,
                t.CreatedDate
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public JsonResult GetRole(string text)
        {
            var list = new SelectList("");
            if (!string.IsNullOrEmpty(text))
            {
                list = new SelectList(_db.RoleList.Where(a => a.Status != null && a.RoleName.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.RoleName, a.Id }), "Id", "RoleName");
            }
            return Json(list);
        }
        public JsonResult RoleStatusChange(int id)
        {
            try
            {
                var role = _db.RoleList.Find(id);
                role.Status = null;
                role.UpdatedBy = User.GetUserId();
                role.UpdatedDate = now;
                _db.Entry(role).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public PartialViewResult RoleEdit(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult RoleEditPartial(int id)
        {
            ViewBag.Name = _db.RoleList.Find(id).RoleName;
            return PartialView();
        }
        #endregion

        #region System Configuration
        public IActionResult AllSystemConfiguration()
        {
            return View();
        }
        public JsonResult SaveSystemConfiguration()
        {
            //await _hubContext.Clients.All.SendAsync("GetNotification", 1);
            return Json(true);
        }
        #endregion
    }
}