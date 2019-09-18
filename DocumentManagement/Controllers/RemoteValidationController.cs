using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Models.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class RemoteValidationController : Controller
    {
        #region Private Propertise
        private readonly DocumentManagementContext _db;
        IDataProtector _protector;
        public RemoteValidationController(DocumentManagementContext db, IDataProtectionProvider provider)
        {
            _db = db;
            _protector = provider.CreateProtector(GetType().FullName);
        }
        #endregion
        public JsonResult UserEmailExist(long? id, string email)
        {
            bool exists = _db.UserLogin.Any(m => (id.HasValue) ? (m.Id != id && m.UserName == email) : (m.UserName == email));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }
        public JsonResult CabinetNameExist(int? id, string name)
        {
            bool exists = _db.Cabinet.Any(m => (id.HasValue) ? (m.Id != id && m.Name == name) : (m.Name == name));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }
        public JsonResult F1NameExist(long? id, int cId, string name)
        {
            bool exists = _db.F1.Any(m => (id.HasValue) ? (m.Id != id && m.CabinetId == cId && m.Name == name) : ( m.CabinetId == cId && m.Name == name));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }
        public JsonResult F2NameExist(long? id, long f1Id, int cId, string name)
        {
            bool exists = _db.F2.Any(m => (id.HasValue) ? (m.Id != id && m.CabinetId == cId && m.F1id == f1Id && m.Name == name) : (m.CabinetId == cId && m.F1id == f1Id && m.Name == name));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }
        public JsonResult F3NameExist(long? id, long f2Id, long f1Id, int cId, string name)
        {
            bool exists = _db.F3.Any(m => (id.HasValue) ? (m.Id != id && m.CabinetId == cId && m.F1id == f1Id && m.F2id == f2Id && m.Name == name) : (m.CabinetId == cId && m.F1id == f1Id && m.F2id == f2Id && m.Name == name));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }
        public JsonResult RoleNameExist(int? id, string name)
        {
            bool exists = _db.RoleList.Any(m => (id.HasValue) ? (m.Id != id && m.RoleName == name) : (m.RoleName == name));
            if (exists) { return Json(false); }
            else { return Json(true); }
        }


    }
}