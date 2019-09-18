using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Models.Data;
using DocumentManagement.ModelViews.Document;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManagement.Services;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace DocumentManagement.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        #region Private Propertise
        private readonly DocumentManagementContext _db;
        IDataProtector _protector;
        private readonly IHostingEnvironment _env;
        public DocumentController(DocumentManagementContext db, IDataProtectionProvider provider, IHostingEnvironment env)
        {
            _db = db;
            _protector = provider.CreateProtector(GetType().FullName);
            _env = env;
        }
        #endregion

        #region Cabinet
        public IActionResult Cabinet()
        {
            return View();
        }
        public JsonResult GetCabinetList(int skip, int take, int? type, string selectedId, DateTime? startDate, DateTime? endDate)
        {
            var list = new List<Cabinet>();
            var count = 0;
            if (!string.IsNullOrEmpty(selectedId))
            {
                Cabinet cabinet;
                foreach (var id in selectedId.Split(','))
                {
                    int _id = Convert.ToInt32(id);
                    cabinet = _db.Cabinet.Find(_id);
                    if (cabinet.Id > 0)
                    {
                        list.Add(cabinet);
                    }
                }
                count = list.Count();
                list = list.OrderByDescending(o => o.CreatedDate)
                            .Skip(skip).Take(take)
                            .ToList();
            }
            else if (startDate != null && endDate != null)
            {
                count = _db.Cabinet.Where(a => startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date).Count();
                list = _db.Cabinet.Where(a => startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date)
                        .OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
            }
            else
            {
                list = _db.Cabinet.OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
                count = _db.Cabinet.Count();
            }
            var viewList = list.Join(_db.UserInformation, d => d.CreatedBy, u => u.Id, (d, u) => new {
                EncryptedId = _protector.Protect(d.Id.ToString()),
                d.Name,
                Status = d.Status == 1 ? "Active" : "Deleted",
                CreatedBy = u.Name,
                d.CreatedDate
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public JsonResult GetCabinet(string text, DateTime? startDate, DateTime? endDate)
        {
            var list = new SelectList("");
            if (!string.IsNullOrEmpty(text))
            {
                if (startDate != null && endDate != null)
                {
                    list = new SelectList(_db.Cabinet.Where(a => startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date && a.Name.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take
                        (50)
                        .Select(a => new { a.Name, a.Id }), "Id", "Name");
                }
                else
                {
                    list = new SelectList(_db.Cabinet.Where(a => a.Name.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.Name, a.Id }), "Id", "Name");
                }
            }
            return Json(list);
        }
        public IActionResult CabinetDetails(string q)
        {
            int id = Convert.ToInt32(_protector.Unprotect(q));
            var cabinet = _db.Cabinet.Find(id);
            CabinetModelView model = new CabinetModelView
            {
                EncryptedId = q,
                CabinetName = cabinet.Name,
                Status = cabinet.Status,
                CreatedBy = _db.UserInformation.Find(cabinet.CreatedBy).Name,
                CreatedDate = cabinet.CreatedDate,
            };
            return View(model);
        }

        public IActionResult CabinetCreate(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                int cabinetId = Convert.ToInt32(_protector.Unprotect(q));
                var cabinet = _db.Cabinet.Find(cabinetId);
                CabinetModelView model = new CabinetModelView
                {
                    CountId = cabinet.Id,
                    CabinetName = cabinet.Name,
                    EncryptedId = q,
                    Status = cabinet.Status,
                    CreatedBy = _db.UserInformation.Find(cabinet.CreatedBy).Name,
                    CreatedDate = cabinet.CreatedDate
                };
                return View(model);
            }
            return View();
        }
        public JsonResult CabinetSave(CabinetModelView model)
        {
            try
            {
                Cabinet cabinet;
                if (!string.IsNullOrEmpty(model.EncryptedId))
                {
                    int id = Convert.ToInt32(_protector.Unprotect(model.EncryptedId));
                    cabinet = _db.Cabinet.Find(id);
                    cabinet.UpdatedBy = User.GetUserId();
                    cabinet.UpdatedDate = DateTime.Now;
                    _db.Entry(cabinet).State = EntityState.Modified;
                }
                else
                {
                    cabinet = new Cabinet
                    {
                        Name = model.CabinetName,
                        Status = 1,
                        CreatedBy = User.GetUserId(),
                        CreatedDate = DateTime.Now
                    };
                    _db.Cabinet.Add(cabinet);
                }
                _db.SaveChanges();
                F1 f1;
                F2 f2;
                F3 f3;
                if (model.F1Folder != null)
                {
                    foreach (var f1List in model.F1Folder)
                    {
                        f1 = new F1
                        {
                            CabinetId = cabinet.Id,
                            Name = f1List.Name,
                            Status = 1,
                            CreatedBy = User.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        _db.F1.Add(f1);
                        _db.SaveChanges();
                        if (f1List.F2Folder != null)
                        {
                            foreach (var f2List in f1List.F2Folder)
                            {
                                f2 = new F2
                                {
                                    CabinetId = cabinet.Id,
                                    F1id = f1.Id,
                                    Name = f2List.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F2.Add(f2);
                                _db.SaveChanges();
                                if (f2List.F3Folder != null)
                                {
                                    foreach (var f3List in f2List.F3Folder)
                                    {
                                        f3 = new F3
                                        {
                                            CabinetId = cabinet.Id,
                                            F1id = f1.Id,
                                            F2id = f2.Id,
                                            Name = f3List.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(f3);
                                        _db.SaveChanges();
                                    }
                                }
                            }
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
        public PartialViewResult CabinetEdit(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _CabinetEdit(string id)
        {
            int cabinetId = Convert.ToInt32(_protector.Unprotect(id));
            var cabinet = _db.Cabinet.Find(cabinetId);
            CabinetModelView model = new CabinetModelView
            {
                EncryptedId = id,
                CabinetName = cabinet.Name
            };
            if (_db.F1.Any(a => a.CabinetId == cabinetId && a.Status == 1))
            {
                var f1List = _db.F1.Where(a => a.CabinetId == cabinetId && a.Status == 1).ToList();
                model.F1List = new List<F1ModelView>();
                foreach (var f1 in f1List)
                {
                    var f1Item = new F1ModelView
                    {
                        F1Id = _protector.Protect(f1.Id.ToString()),
                        Name = f1.Name
                    };
                    if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                    {
                        var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                        f1Item.F2List = new List<F2ModelView>();
                        foreach (var f2 in f2List)
                        {
                            var f2Item = new F2ModelView
                            {
                                F2Id = _protector.Protect(f2.Id.ToString()),
                                Name = f2.Name
                            };
                            if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                            {
                                var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                f2Item.F3List = new List<F3ModelView>();
                                foreach (var f3 in f3List)
                                {
                                    var f3Item = new F3ModelView
                                    {
                                        F3Id = _protector.Protect(f3.Id.ToString()),
                                        Name = f3.Name
                                    };
                                    f2Item.F3List.Add(f3Item);
                                }
                            }
                            f1Item.F2List.Add(f2Item);
                        }
                    }
                    model.F1List.Add(f1Item);
                }
            }
            return PartialView(model);
        }

        public PartialViewResult CabinetRename(string id)
        {
            int cabinetId = Convert.ToInt32(_protector.Unprotect(id));
            ViewBag.Id = cabinetId;
            return PartialView();
        }
        public PartialViewResult _CabinetRename(int id)
        {
            ViewBag.Name = _db.Cabinet.Find(id).Name;
            return PartialView();
        }
        public JsonResult CabinetNameUpdate(string name, int id)
        {
            try
            {
                var cabinet = _db.Cabinet.Find(id);
                cabinet.Name = name;
                cabinet.UpdatedBy = User.GetUserId();
                cabinet.UpdatedDate = DateTime.Now;
                _db.Entry(cabinet).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch(Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public PartialViewResult CabinetDeleteAlert(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _CabinetDeleteAlert(string id)
        {
            int cabinetId = Convert.ToInt32(_protector.Unprotect(id));
            var fileList = _db.Files.Where(a => a.CabinetId == cabinetId && a.Status == true).ToList();
            return PartialView(fileList);
        }
        public JsonResult CabinetDelete(string id, string selectIds, string unSelectIds)
        {
            try
            {
                int cabinetId = Convert.ToInt32(_protector.Unprotect(id));
                var cabinet = _db.Cabinet.Find(cabinetId);
                cabinet.Status = 0;
                cabinet.UpdatedBy = User.GetUserId();
                cabinet.UpdatedDate = DateTime.Now;
                _db.Entry(cabinet).State = EntityState.Modified;
                _db.SaveChanges();

                //f1 folder deactive
                if (_db.F1.Any(a => a.CabinetId == cabinetId && a.Status == 1))
                {
                    var f1List = _db.F1.Where(a => a.CabinetId == cabinetId && a.Status == 1).ToList();
                    foreach (var f1 in f1List)
                    {
                        f1.Status = 0;
                        f1.UpdatedBy = User.GetUserId();
                        f1.UpdatedDate = DateTime.Now;
                        _db.Entry(f1).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                //f2 deactive
                if (_db.F2.Any(a => a.CabinetId == cabinetId && a.Status == 1))
                {
                    var f2List = _db.F2.Where(a => a.CabinetId == cabinetId && a.Status == 1).ToList();
                    foreach (var f2 in f2List)
                    {
                        f2.Status = 0;
                        f2.UpdatedBy = User.GetUserId();
                        f2.UpdatedDate = DateTime.Now;
                        _db.Entry(f2).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                //f3 deactive
                if (_db.F3.Any(a => a.CabinetId == cabinetId && a.Status == 1))
                {
                    var f3List = _db.F3.Where(a => a.CabinetId == cabinetId && a.Status == 1).ToList();
                    foreach (var f3 in f3List)
                    {
                        f3.Status = 0;
                        f3.UpdatedBy = User.GetUserId();
                        f3.UpdatedDate = DateTime.Now;
                        _db.Entry(f3).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                //un select files to generic folder
                if (!string.IsNullOrEmpty(unSelectIds))
                {
                    Files file;
                    foreach (var fId in unSelectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 1;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                //inactive selected files
                if (!string.IsNullOrEmpty(selectIds))
                {
                    Files file;
                    foreach (var fId in selectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 2;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json("success");
            }
            catch (Exception)
            {
                return Json("error");
            }
        }

        #endregion

        #region F1
        public IActionResult F1Add(string q)
        {
            long id = Convert.ToInt32(_protector.Unprotect(q));
            var f1 = _db.F1.Find(id);
            F1ModelView model = new F1ModelView
            {
                F1Id = q,
                CabinetId = _protector.Protect(f1.CabinetId.ToString()),
                CabinetName = _db.Cabinet.Find(f1.CabinetId).Name,
                Name = f1.Name,
                Status = f1.Status,
                CreatedBy = _db.UserInformation.Find(f1.CreatedBy).Name,
                CreatedDate = f1.CreatedDate,
                C_countId = f1.CabinetId,
                F1_CountId = f1.Id
            };
            return View(model);
        }
        public JsonResult FolderSave(F1ModelView model)
        {
            try
            {
                long f1Id = Convert.ToInt64(_protector.Unprotect(model.F1Id));
                F1 f1 = _db.F1.Find(f1Id);
                f1.UpdatedBy = User.GetUserId();
                f1.UpdatedDate = DateTime.Now;
                _db.Entry(f1).State = EntityState.Modified;
                _db.SaveChanges();

                F2 f2;
                F3 f3;
                if (model.F2Folder != null)
                {
                    foreach (var f2List in model.F2Folder)
                    {
                        f2 = new F2
                        {
                            CabinetId = f1.CabinetId,
                            F1id = f1.Id,
                            Name = f2List.Name,
                            Status = 1,
                            CreatedBy = User.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        _db.F2.Add(f2);
                        _db.SaveChanges();
                        if (f2List.F3Folder != null)
                        {
                            foreach (var f3List in f2List.F3Folder)
                            {
                                f3 = new F3
                                {
                                    CabinetId = f1.CabinetId,
                                    F1id = f1.Id,
                                    F2id = f2.Id,
                                    Name = f3List.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F3.Add(f3);
                                _db.SaveChanges();
                            }
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
        public IActionResult F1Details(string q)
        {
            long id = Convert.ToInt32(_protector.Unprotect(q));
            var f1 = _db.F1.Find(id);
            F1ModelView model = new F1ModelView
            {
                F1Id = q,
                CabinetId = _protector.Protect(f1.CabinetId.ToString()),
                CabinetName = _db.Cabinet.Find(f1.CabinetId).Name,
                Name = f1.Name,
                Status = f1.Status,
                CreatedBy = _db.UserInformation.Find(f1.CreatedBy).Name,
                CreatedDate = f1.CreatedDate,
            };
            return View(model);
        }
        public PartialViewResult F1List(string q)
        {
            int cabinetId = Convert.ToInt32(_protector.Unprotect(q));
            var list = _db.F1.Where(a => a.CabinetId == cabinetId && a.Status == 1).ToList();
            var viewList = list.Join(_db.UserInformation, f1 => f1.CreatedBy, u => u.Id, (f1, u) => new F1ModelView
            {
                F1Id = _protector.Protect(f1.Id.ToString()),
                Name = f1.Name,
                Status = f1.Status,
                CreatedBy = u.Name,
                CreatedDate = f1.CreatedDate
            }).ToList();
            return PartialView(viewList);
        }
        public JsonResult F1NameUpdate(string name, string id)
        {
            try
            {
                long f1Id = Convert.ToInt64(_protector.Unprotect(id));
                var f1 = _db.F1.Find(f1Id);
                f1.Name = name;
                f1.UpdatedBy = User.GetUserId();
                f1.UpdatedDate = DateTime.Now;
                _db.Entry(f1).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public PartialViewResult F1Edit(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _F1Edit(string id)
        {
            long f1Id = Convert.ToInt64(_protector.Unprotect(id));
            var f1 = _db.F1.Find(f1Id);
            F1ModelView model = new F1ModelView();
            model.F1Id = id;
            model.Name = f1.Name;
            F2ModelView f2Item;
            F3ModelView f3Item;
            if (_db.F2.Any(a => a.F1id == f1.Id))
            {
                var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                model.F2List = new List<F2ModelView>();
                foreach (var f2 in f2List)
                {
                    f2Item = new F2ModelView();
                    f2Item.F2Id = _protector.Protect(f2.Id.ToString());
                    f2Item.Name = f2.Name;
                    if (_db.F3.Any(a => a.F2id == f2.Id))
                    {
                        var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                        f2Item.F3List = new List<F3ModelView>();
                        foreach (var f3 in f3List)
                        {
                            f3Item = new F3ModelView();
                            f3Item.F3Id = _protector.Protect(f3.Id.ToString());
                            f3Item.Name = f3.Name;
                            f2Item.F3List.Add(f3Item);
                        }
                    }
                    model.F2List.Add(f2Item);
                }
            }
            return PartialView(model);
        }
        public PartialViewResult F1DeleteAlert(string id, bool? isPartial, bool? isMultiple, string encryptId)
        {
            ViewBag.Id = id;
            ViewBag.IsPartial = isPartial;
            ViewBag.IsMultiple = isMultiple;
            ViewBag.EncryptId = encryptId;
            return PartialView();
        }
        public PartialViewResult _F1DeleteAlert(string id, bool? isMultiple)
        {
            var list = new List<Files>();
            long docId = 0;
            if (isMultiple == true)
            {
                bool isFile = false;
                if(!string.IsNullOrEmpty(id))
                {
                    foreach(var doc in id.Split(","))
                    {
                        isFile = long.TryParse(doc, out long number);
                        if(isFile == false)
                        {
                            docId = Convert.ToInt64(_protector.Unprotect(doc));
                            list.AddRange(_db.Files.Where(a => a.F1id == docId && a.Status == true).ToList());
                        }
                        else
                        {
                            docId = Convert.ToInt64(doc);
                            list.Add(_db.Files.FirstOrDefault(a => a.Id == docId));
                        }
                    }
                }
            }
            else
            {
                docId = Convert.ToInt64(_protector.Unprotect(id));
                list = _db.Files.Where(a => a.F1id == docId && a.Status == true).ToList();
            }
            return PartialView(list);
        }
        public JsonResult F1Delete(string id, bool? isMultiple,string encryptId, string selectIds, string unSelectIds)
        {
            try
            {
                long docId = 0;
                if(isMultiple == true)
                {
                    bool isFile = false;
                    if (!string.IsNullOrEmpty(id))
                    {
                        foreach (var doc in id.Split(","))
                        {
                            isFile = long.TryParse(doc, out long number);
                            if (isFile == false)
                            {
                                docId = Convert.ToInt64(_protector.Unprotect(doc));
                                var f1 = _db.F1.Find(docId);
                                f1.Status = 0;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();

                                //folder deactive
                                if (_db.F2.Any(a => a.F1id == docId && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.F1id == docId && a.Status == 1).ToList();
                                    foreach (var f2 in f2List)
                                    {
                                        f2.Status = 0;
                                        f2.UpdatedBy = User.GetUserId();
                                        f2.UpdatedDate = DateTime.Now;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //sub folder deactive
                                if (_db.F3.Any(a => a.F1id == docId && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F1id == docId && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        f3.Status = 0;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    docId = Convert.ToInt64(_protector.Unprotect(id));
                    var f1 = _db.F1.Find(docId);
                    encryptId = _protector.Protect(f1.CabinetId.ToString());
                    f1.Status = 0;
                    f1.UpdatedBy = User.GetUserId();
                    f1.UpdatedDate = DateTime.Now;
                    _db.Entry(f1).State = EntityState.Modified;
                    _db.SaveChanges();

                    //folder deactive
                    if (_db.F2.Any(a => a.F1id == docId && a.Status == 1))
                    {
                        var f2List = _db.F2.Where(a => a.F1id == docId && a.Status == 1).ToList();
                        foreach (var f2 in f2List)
                        {
                            f2.Status = 0;
                            f2.UpdatedBy = User.GetUserId();
                            f2.UpdatedDate = DateTime.Now;
                            _db.Entry(f2).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }

                    //sub folder deactive
                    if (_db.F3.Any(a => a.F1id == docId && a.Status == 1))
                    {
                        var f3List = _db.F3.Where(a => a.F1id == docId && a.Status == 1).ToList();
                        foreach (var f3 in f3List)
                        {
                            f3.Status = 0;
                            f3.UpdatedBy = User.GetUserId();
                            f3.UpdatedDate = DateTime.Now;
                            _db.Entry(f3).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }
                }

                //un select files to generic folder
                if (!string.IsNullOrEmpty(unSelectIds))
                {
                    Files file;
                    foreach (var fId in unSelectIds.Split(','))
                    {
                        docId = Convert.ToInt64(fId);
                        file = _db.Files.Find(docId);
                        file.Status = false;
                        file.DeleteType = 1;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                //inactive selected files
                if (!string.IsNullOrEmpty(selectIds))
                {
                    Files file;
                    foreach (var fId in selectIds.Split(','))
                    {
                        docId = Convert.ToInt64(fId);
                        file = _db.Files.Find(docId);
                        file.Status = false;
                        file.DeleteType = 2;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json(new { cabinetId = encryptId });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        public PartialViewResult F1Rename(string id)
        {
            long f1Id = Convert.ToInt64(_protector.Unprotect(id));
            ViewBag.Id = f1Id;
            ViewBag.Cid = _db.F1.Find(f1Id).CabinetId;
            return PartialView();
        }
        public PartialViewResult _F1Rename(long id)
        {
            ViewBag.Name = _db.F1.Find(id).Name;
            return PartialView();
        }
        public JsonResult F1RenameSave(string name, long id)
        {
            try
            {
                var f1 = _db.F1.Find(id);
                f1.Name = name;
                f1.UpdatedBy = User.GetUserId();
                f1.UpdatedDate = DateTime.Now;
                _db.Entry(f1).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { cabinetId = _protector.Protect(f1.CabinetId.ToString()) });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }

        #endregion

        #region F2
        public IActionResult F2Add(string q)
        {
            long id = Convert.ToInt64(_protector.Unprotect(q));
            var f2 = _db.F2.Find(id);
            F2ModelView model = new F2ModelView
            {
                F2Id = q,
                F1Id = _protector.Protect(f2.F1id.ToString()),
                F1Name = _db.F1.Find(f2.F1id).Name,
                CabinetId = _protector.Protect(f2.CabinetId.ToString()),
                CabinetName = _db.Cabinet.Find(f2.CabinetId).Name,
                Name = f2.Name,
                Status = f2.Status,
                CreatedBy = _db.UserInformation.Find(f2.CreatedBy).Name,
                CreatedDate = f2.CreatedDate,
                C_countId = f2.CabinetId,
                F1_CountId = f2.F1id,
                F2_CountId = f2.Id
            };
            return View(model);
        }
        public IActionResult F2Details(string q)
        {
            long id = Convert.ToInt64(_protector.Unprotect(q));
            var f2 = _db.F2.Find(id);
            F2ModelView model = new F2ModelView
            {
                F2Id = q,
                F1Id = _protector.Protect(f2.F1id.ToString()),
                F1Name = _db.F1.Find(f2.F1id).Name,
                CabinetId = _protector.Protect(f2.CabinetId.ToString()),
                CabinetName = _db.Cabinet.Find(f2.CabinetId).Name,
                Name = f2.Name,
                Status = f2.Status,
                CreatedBy = _db.UserInformation.Find(f2.CreatedBy).Name,
                CreatedDate = f2.CreatedDate,
            };
            return View(model);
        }
        public PartialViewResult F2List(string q)
        {
            long f1Id = Convert.ToInt64(_protector.Unprotect(q));
            var list = _db.F2.Where(a => a.F1id == f1Id && a.Status == 1).ToList();
            var viewList = list.Join(_db.UserInformation, f2 => f2.CreatedBy, u => u.Id, (f2, u) => new F2ModelView
            {
                F2Id = _protector.Protect(f2.Id.ToString()),
                Name = f2.Name,
                Status = f2.Status,
                CreatedBy = u.Name,
                CreatedDate = f2.CreatedDate
            }).ToList();
            return PartialView(viewList);
        }
        public JsonResult SubFolderSave(F2ModelView model)
        {
            try
            {
                long f2Id = Convert.ToInt64(_protector.Unprotect(model.F2Id));
                F2 f2 = _db.F2.Find(f2Id);
                f2.UpdatedBy = User.GetUserId();
                f2.UpdatedDate = DateTime.Now;
                _db.Entry(f2).State = EntityState.Modified;
                _db.SaveChanges();
                F3 f3;
                if (model.F3Folder != null)
                {
                    foreach (var f3List in model.F3Folder)
                    {
                        f3 = new F3();
                        f3.CabinetId = f2.CabinetId;
                        f3.F1id = f2.F1id;
                        f3.F2id = f2.Id;
                        f3.Name = f3List.Name;
                        f3.Status = 1;
                        f3.CreatedBy = User.GetUserId();
                        f3.CreatedDate = DateTime.Now;
                        _db.F3.Add(f3);
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public JsonResult F2NameUpdate(string name, string id)
        {
            try
            {
                long f2Id = Convert.ToInt64(_protector.Unprotect(id));
                var f2 = _db.F2.Find(f2Id);
                f2.Name = name;
                f2.UpdatedBy = User.GetUserId();
                f2.UpdatedDate = DateTime.Now;
                _db.Entry(f2).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public PartialViewResult F2Edit(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _F2Edit(string id)
        {
            long f2Id = Convert.ToInt64(_protector.Unprotect(id));
            var f2 = _db.F2.Find(f2Id);
            F2ModelView model = new F2ModelView();
            model.F2Id = id;
            model.Name = f2.Name;
            F3ModelView f3Item;
            if (_db.F3.Any(a => a.F2id == f2.Id))
            {
                var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                model.F3List = new List<F3ModelView>();
                foreach (var f3 in f3List)
                {
                    f3Item = new F3ModelView();
                    f3Item.F3Id = _protector.Protect(f3.Id.ToString());
                    f3Item.Name = f3.Name;
                    model.F3List.Add(f3Item);
                }
            }
            return PartialView(model);
        }
        public PartialViewResult F2Rename(string id)
        {
            long f2Id = Convert.ToInt64(_protector.Unprotect(id));
            var f2 = _db.F2.Find(f2Id);
            ViewBag.Id = f2Id;
            ViewBag.Cid = f2.CabinetId;
            ViewBag.F1id = f2.F1id;
            return PartialView();
        }
        public PartialViewResult _F2Rename(long id)
        {
            ViewBag.Name = _db.F2.Find(id).Name;
            return PartialView();
        }
        public JsonResult F2RenameSave(string name, long id)
        {
            try
            {
                var f2 = _db.F2.Find(id);
                f2.Name = name;
                f2.UpdatedBy = User.GetUserId();
                f2.UpdatedDate = DateTime.Now;
                _db.Entry(f2).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { f1Id = _protector.Protect(f2.F1id.ToString()) });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        public PartialViewResult F2DeleteAlert(string id, int? type,bool? isMultiple, string encryptId)
        {
            ViewBag.Id = id;
            ViewBag.Type = type;
            ViewBag.IsMultiple = isMultiple;
            ViewBag.EncryptId = encryptId;
            return PartialView();
        }
        public PartialViewResult _F2DeleteAlert(string id, bool? isMultiple)
        {
            var list = new List<Files>();
            long docId = 0;
            if (isMultiple == true)
            {
                bool isFile = false;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var doc in id.Split(","))
                    {
                        isFile = long.TryParse(doc, out long number);
                        if (isFile == false)
                        {
                            docId = Convert.ToInt64(_protector.Unprotect(doc));
                            list.AddRange(_db.Files.Where(a => a.F2id == docId && a.Status == true).ToList());
                        }
                        else
                        {
                            docId = Convert.ToInt64(doc);
                            list.Add(_db.Files.FirstOrDefault(a => a.Id == docId));
                        }
                    }
                }
            }
            else
            {
                docId = Convert.ToInt64(_protector.Unprotect(id));
                list = _db.Files.Where(a => a.F2id == docId && a.Status == true).ToList();
            }
            return PartialView(list);
        }
        public JsonResult F2Delete(string id, bool? isMultiple, string encryptId, string selectIds, string unSelectIds, int? type)
        {
            try
            {
                long docId = 0;
                if (isMultiple == true)
                {
                    bool isFile = false;
                    if (!string.IsNullOrEmpty(id))
                    {
                        foreach (var doc in id.Split(","))
                        {
                            isFile = long.TryParse(doc, out long number);
                            if (isFile == false)
                            {
                                docId = Convert.ToInt64(_protector.Unprotect(doc));
                                var f2 = _db.F2.Find(docId);
                                f2.Status = 0;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();

                                //sub folder deactive
                                if (_db.F3.Any(a => a.F2id == docId && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F2id == docId && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        f3.Status = 0;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    docId = Convert.ToInt64(_protector.Unprotect(id));
                    var f2 = _db.F2.Find(docId);
                    if(type == 1)
                    {
                        encryptId = _protector.Protect(f2.CabinetId.ToString());
                    }
                    else if(type == 2)
                    {
                        encryptId = _protector.Protect(f2.F1id.ToString());
                    }
                    else
                    {
                        encryptId = _protector.Protect(f2.F1id.ToString());
                    }

                    //encryptId = f2.CabinetId;
                    f2.Status = 0;
                    f2.UpdatedBy = User.GetUserId();
                    f2.UpdatedDate = DateTime.Now;
                    _db.Entry(f2).State = EntityState.Modified;
                    _db.SaveChanges();

                    //sub folder deactive
                    if (_db.F3.Any(a => a.F2id == docId && a.Status == 1))
                    {
                        var f3List = _db.F3.Where(a => a.F2id == docId && a.Status == 1).ToList();
                        foreach (var f3 in f3List)
                        {
                            f3.Status = 0;
                            f3.UpdatedBy = User.GetUserId();
                            f3.UpdatedDate = DateTime.Now;
                            _db.Entry(f3).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }
                }
                
                //un select files to generic folder
                if (!string.IsNullOrEmpty(unSelectIds))
                {
                    Files file;
                    foreach (var fId in unSelectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 1;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                //inactive selected files
                if (!string.IsNullOrEmpty(selectIds))
                {
                    Files file;
                    foreach (var fId in selectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 2;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json(new { encryptId, type });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        #endregion

        #region F3
        public IActionResult F3Details(string q)
        {
            long id = Convert.ToInt64(_protector.Unprotect(q));
            var f3 = _db.F3.Find(id);
            F3ModelView model = new F3ModelView
            {
                F3Id = q,
                F1Id = _protector.Protect(f3.F1id.ToString()),
                F1Name = _db.F1.Find(f3.F1id).Name,
                F2Id = _protector.Protect(f3.F2id.ToString()),
                F2Name = _db.F2.Find(f3.F2id).Name,
                CabinetId = _protector.Protect(f3.CabinetId.ToString()),
                CabinetName = _db.Cabinet.Find(f3.CabinetId).Name,
                Name = f3.Name,
                Status = f3.Status,
                CreatedBy = _db.UserInformation.Find(f3.CreatedBy).Name,
                CreatedDate = f3.CreatedDate,
            };
            return View(model);
        }
        public PartialViewResult F3List(string q)
        {
            long f2Id = Convert.ToInt32(_protector.Unprotect(q));
            var list = _db.F3.Where(a => a.F2id == f2Id && a.Status == 1).ToList();
            var viewList = list.Join(_db.UserInformation, f3 => f3.CreatedBy, u => u.Id, (f3, u) => new F3ModelView
            {
                F3Id = _protector.Protect(f3.Id.ToString()),
                Name = f3.Name,
                Status = f3.Status,
                CreatedBy = u.Name,
                CreatedDate = f3.CreatedDate
            }).ToList();
            return PartialView(viewList);
        }
        public JsonResult F3NameUpdate(string name, string id)
        {
            try
            {
                long f3Id = Convert.ToInt64(_protector.Unprotect(id));
                var f3 = _db.F3.Find(f3Id);
                f3.Name = name;
                f3.UpdatedBy = User.GetUserId();
                f3.UpdatedDate = DateTime.Now;
                _db.Entry(f3).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json("error");
            }
            return Json("success");
        }
        public PartialViewResult F3DeleteAlert(string id, int? type, bool? isMultiple, string encryptId)
        {
            ViewBag.Id = id;
            ViewBag.Type = type;
            ViewBag.IsMultiple = isMultiple;
            ViewBag.EncryptId = encryptId;
            return PartialView();
        }
        public PartialViewResult _F3DeleteAlert(string id, bool? isMultiple)
        {
            var list = new List<Files>();
            long docId = 0;
            if (isMultiple == true)
            {
                bool isFile = false;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var doc in id.Split(","))
                    {
                        isFile = long.TryParse(doc, out long number);
                        if (isFile == false)
                        {
                            docId = Convert.ToInt64(_protector.Unprotect(doc));
                            list.AddRange(_db.Files.Where(a => a.F3id == docId && a.Status == true).ToList());
                        }
                        else
                        {
                            docId = Convert.ToInt64(doc);
                            list.Add(_db.Files.FirstOrDefault(a => a.Id == docId));
                        }
                    }
                }
            }
            else
            {
                docId = Convert.ToInt64(_protector.Unprotect(id));
                list = _db.Files.Where(a => a.F3id == docId && a.Status == true).ToList();
            }
            return PartialView(list);
        }
        public JsonResult F3Delete(string id, bool? isMultiple, string encryptId, string selectIds, string unSelectIds, int? type)
        {
            try
            {
                long docId = 0;
                if (isMultiple == true)
                {
                    bool isFile = false;
                    if (!string.IsNullOrEmpty(id))
                    {
                        foreach (var doc in id.Split(","))
                        {
                            isFile = long.TryParse(doc, out long number);
                            if (isFile == false)
                            {
                                docId = Convert.ToInt64(_protector.Unprotect(id));
                                var f3 = _db.F3.Find(docId);
                                f3.Status = 0;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    docId = Convert.ToInt64(_protector.Unprotect(id));
                    var f3 = _db.F3.Find(docId);
                    if (type == 1)
                    {
                        encryptId = _protector.Protect(f3.CabinetId.ToString());
                    }
                    else if (type == 2)
                    {
                        encryptId = _protector.Protect(f3.F1id.ToString());
                    }
                    else if(type == 3)
                    {
                        encryptId = _protector.Protect(f3.F2id.ToString());
                    }
                    else
                    {
                        encryptId = _protector.Protect(f3.F2id.ToString());
                    }
                    f3.Status = 0;
                    f3.UpdatedBy = User.GetUserId();
                    f3.UpdatedDate = DateTime.Now;
                    _db.Entry(f3).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                //un select files to generic folder
                if (!string.IsNullOrEmpty(unSelectIds))
                {
                    Files file;
                    foreach (var fId in unSelectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 1;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                //inactive selected files
                if (!string.IsNullOrEmpty(selectIds))
                {
                    Files file;
                    foreach (var fId in selectIds.Split(','))
                    {
                        var fileId = Convert.ToInt64(fId);
                        file = _db.Files.Find(fileId);
                        file.Status = false;
                        file.DeleteType = 2;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json(new { encryptId, type });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        public PartialViewResult F3Rename(string id)
        {
            long f3Id = Convert.ToInt64(_protector.Unprotect(id));
            var f3 = _db.F3.Find(f3Id);
            ViewBag.Id = f3Id;
            ViewBag.Cid = f3.CabinetId;
            ViewBag.F1id = f3.F1id;
            ViewBag.F2id = f3.F2id;
            return PartialView();
        }
        public PartialViewResult _F3Rename(long id)
        {
            ViewBag.Name = _db.F3.Find(id).Name;
            return PartialView();
        }
        public JsonResult F3RenameSave(string name, long id)
        {
            try
            {
                var f3 = _db.F3.Find(id);
                f3.Name = name;
                f3.UpdatedBy = User.GetUserId();
                f3.UpdatedDate = DateTime.Now;
                _db.Entry(f3).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { f2Id = _protector.Protect(f3.F2id.ToString()) });
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        #endregion

        #region File
        public PartialViewResult FileList(string cabinetId, string f1Id, string f2Id, string f3Id, int type)
        {
            var list = new List<Files>();
            long id = 0;
            if (type == 1) //cabinet file list
            {
                int _cabinetId = Convert.ToInt32(_protector.Unprotect(cabinetId));
                list = _db.Files.Where(a => a.CabinetId == _cabinetId && a.Type == type && a.Status == true).ToList();
            }
            else if (type == 2)
            {
                id = Convert.ToInt64(_protector.Unprotect(f1Id));
                list = _db.Files.Where(a => a.F1id == id && a.Type == type && a.Status == true).ToList();
            }
            else if (type == 3)
            {
                id = Convert.ToInt64(_protector.Unprotect(f2Id));
                list = _db.Files.Where(a => a.F2id == id && a.Type == type && a.Status == true).ToList();
            }
            else if (type == 4)
            {
                id = Convert.ToInt64(_protector.Unprotect(f3Id));
                list = _db.Files.Where(a => a.F3id == id && a.Type == type && a.Status == true).ToList();
            }
            return PartialView(list);
        }
        public async Task<JsonResult> FileSave(string pId, int type, long? fileId)
        {
            FileAuditLogModelView fileLog = new FileAuditLogModelView();
            bool operationStatus = true;
            string msg = "";
            try
            {
                Files aFile = new Files();
                string fileName = "";
                string extn = "";
                int fType = 0;
                string originalfn = "";
                Random rand = new Random((int)DateTime.Now.Ticks);
                //if file replace
                if (fileId > 0)
                {
                    var oldFile = _db.Files.FirstOrDefault(a => a.Id == fileId);
                    oldFile.Status = false;
                    oldFile.IsArchive = true;
                    oldFile.UpdatedBy = User.GetUserId();
                    oldFile.UpdatedDate = DateTime.Now;
                    _db.Entry(oldFile).State = EntityState.Modified;

                    var file = Request.Form.Files[0];
                    originalfn = Path.GetFileName(file.FileName);
                    extn = Path.GetExtension(file.FileName);
                    //check type
                    if (extn == ".gif" || extn == ".png" || extn == ".jpg" || extn == ".jpeg")
                    {
                        //image
                        fType = 1;
                    }
                    else if (extn == ".pdf")
                    {
                        //pdf
                        fType = 2;
                    }
                    else if (extn == ".doc" || extn == ".docx")
                    {
                        //document
                        fType = 3;
                    }
                    else if (extn == ".txt")
                    {
                        //text
                        fType = 4;
                    }
                    else if (extn == ".ppt" || extn == ".pptm" || extn == ".pptx")
                    {
                        //powerpoint
                        fType = 5;
                    }
                    else if (extn == ".xlsx" || extn == ".xlsm" || extn == ".xltx" || extn == ".xltm")
                    {
                        //excel
                        fType = 6;
                    }
                    fileName = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{extn}";
                    aFile = new Files
                    {
                        CabinetId = oldFile.CabinetId,
                        F1id = oldFile.F1id,
                        F2id = oldFile.F2id,
                        F3id = oldFile.F3id,
                        Type = oldFile.Type,
                        Name = fileName,
                        OriginalName = originalfn,
                        Size = file.Length,
                        Status = true,
                        FileType = fType,
                        ListCount = oldFile.ListCount,
                        IsArchive = true,
                        CreatedBy = User.GetUserId(),
                        CreatedDate = DateTime.Now
                    };
                    _db.Files.Add(aFile);
                    _db.SaveChanges();

                    var path = $"{_env.WebRootPath}\\Files\\";
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
                }
                else
                {
                    F1 f1 = new F1();
                    F2 f2 = new F2();
                    F3 f3 = new F3();
                    int _cabinetId = 0;
                    int count = 0;
                    long id = 0;

                    if (type == 1)
                    {
                        _cabinetId = Convert.ToInt32(_protector.Unprotect(pId));
                        if (_db.Files.Any(a => a.CabinetId == _cabinetId && a.Type == type))
                        {
                            count = _db.Files.Where(a => a.CabinetId == _cabinetId && a.Type == type).Max(a => a.ListCount);
                        }
                    }
                    else if (type == 2)
                    {
                        id = Convert.ToInt64(_protector.Unprotect(pId));
                        f1 = _db.F1.Find(id);
                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Type == type))
                        {
                            count = _db.Files.Where(a => a.F1id == f1.Id && a.Type == type).Max(a => a.ListCount);
                        }
                    }
                    else if (type == 3)
                    {
                        id = Convert.ToInt64(_protector.Unprotect(pId));
                        f2 = _db.F2.Find(id);
                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Type == type))
                        {
                            count = _db.Files.Where(a => a.F2id == f2.Id && a.Type == type).Max(a => a.ListCount);
                        }
                    }
                    else if (type == 4)
                    {
                        id = Convert.ToInt64(_protector.Unprotect(pId));
                        f3 = _db.F3.Find(id);
                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Type == type))
                        {
                            count = _db.Files.Where(a => a.F3id == f3.Id && a.Type == type).Max(a => a.ListCount);
                        }
                    }
                    var files = Request.Form.Files;
                    if (files.Count() > 0)
                    {
                        foreach (var file in files)
                        {
                            count++;
                            fType = 0;
                            originalfn = Path.GetFileName(file.FileName);
                            extn = Path.GetExtension(file.FileName);
                            //check fType
                            if (extn == ".gif" || extn == ".png" || extn == ".jpg" || extn == ".jpeg")
                            {
                                //image
                                fType = 1;
                            }
                            else if (extn == ".pdf")
                            {
                                //pdf
                                fType = 2;
                            }
                            else if (extn == ".doc" || extn == ".docx")
                            {
                                //document
                                fType = 3;
                            }
                            else if (extn == ".txt")
                            {
                                //text
                                fType = 4;
                            }
                            else if (extn == ".ppt" || extn == ".pptm" || extn == ".pptx")
                            {
                                //powerpoint
                                fType = 5;
                            }
                            else if (extn == ".xlsx" || extn == ".xlsm" || extn == ".xltx" || extn == ".xltm")
                            {
                                //excel
                                fType = 6;
                            }
                            if (_db.Files.Any())
                            {
                                fileName = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{extn}";
                            }
                            else
                            {
                                fileName = $"{rand.Next()}{extn}";
                            }
                            aFile = new Files();
                            if (type == 1)
                            {
                                aFile.CabinetId = _cabinetId;
                                msg = $"{originalfn} has been successfully upload to {_db.Cabinet.Find(_cabinetId).Name}\\";
                            }
                            else if (type == 2)
                            {
                                aFile.CabinetId = f1.CabinetId;
                                aFile.F1id = f1.Id;
                                msg = $"{originalfn} has been successfully upload to {_db.Cabinet.Find(f1.CabinetId).Name}\\{f1.Name}\\";
                            }
                            else if (type == 3)
                            {
                                aFile.CabinetId = f2.CabinetId;
                                aFile.F1id = f2.F1id;
                                aFile.F2id = f2.Id;
                                msg = $"{originalfn} has been successfully upload to {_db.Cabinet.Find(f2.CabinetId).Name}\\{_db.F1.Find(f2.F1id).Name}\\{f2.Name}\\";
                            }
                            else if (type == 4)
                            {
                                aFile.CabinetId = f3.CabinetId;
                                aFile.F1id = f3.F1id;
                                aFile.F2id = f3.F2id;
                                aFile.F3id = f3.Id;
                                msg = $"{originalfn} has been successfully upload to {_db.Cabinet.Find(f3.CabinetId).Name}\\{_db.F1.Find(f3.F1id).Name}\\{_db.F2.Find(f3.F2id).Name}\\{f3.Name}\\";
                            }
                            aFile.Size = file.Length;
                            aFile.Type = type;
                            aFile.Name = fileName;
                            aFile.OriginalName = originalfn;
                            aFile.Status = true;
                            aFile.FileType = fType;
                            aFile.ListCount = count;
                            aFile.CreatedBy = User.GetUserId();
                            aFile.CreatedDate = DateTime.Now;
                            _db.Files.Add(aFile);
                            _db.SaveChanges();

                            var path = $"{_env.WebRootPath}\\Files\\";
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
                            //storage update
                            await Task.Run(() => StorageUpdate(aFile.Size));

                            //file log save
                            fileLog = new FileAuditLogModelView
                            {
                                FileId = aFile.Id,
                                FileName = aFile.OriginalName,
                                Operation = "Upload",
                                Status = true,
                                Message = msg
                            };
                            await Task.Run(() => FileAuditLogSave(fileLog));
                        }
                    }
                }
            }
            catch (Exception)
            {
                operationStatus = false;
            }
            if(operationStatus == true)
            {
                return Json("success");
            }
            else
            {
                if(type == 1)
                {
                    msg = "File upload was failed to cabinet";
                }
                else if(type == 2)
                {
                    msg = "File upload was failed to f1";
                }
                else if(type == 3)
                {
                    msg = "File upload was failed to f2";
                }
                else if(type == 4)
                {
                    msg = "File upload was failed to f3";
                }
                fileLog = new FileAuditLogModelView
                {
                    Operation = "Upload",
                    Status = false,
                    Message = msg,
                    FileName = ""
                };
                await Task.Run(() => FileAuditLogSave(fileLog));
                return Json("error");
            }
        }

        public PartialViewResult FileArchive(long id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _FileArchive(long id)
        {
            var list = new List<Files>();
            var file = _db.Files.Find(id);
            if (file.Type == 1)
            {
                list = _db.Files.Where(a => a.CabinetId == file.CabinetId && a.Type == file.Type && a.ListCount == file.ListCount && a.Status == false).ToList();
            }
            else if (file.Type == 2)
            {
                list = _db.Files.Where(a => a.F1id == file.F1id && a.Type == file.Type && a.ListCount == file.ListCount && a.Status == false).ToList();
            }
            else if (file.Type == 3)
            {
                list = _db.Files.Where(a => a.F2id == file.F2id && a.Type == file.Type && a.ListCount == file.ListCount && a.Status == false).ToList();
            }
            else if (file.Type == 4)
            {
                list = _db.Files.Where(a => a.F3id == file.F3id && a.Type == file.Type && a.ListCount == file.ListCount && a.Status == false).ToList();
            }
            return PartialView(list);
        }
        public JsonResult FileStatusChange(long? fileId, string ids, int? status, long? activeId, bool? isKeepDocument)
        {
            var file = new Files();
            try
            {
                if(fileId > 0)
                {
                    file = _db.Files.FirstOrDefault(a => a.Id == fileId);
                    if (status == 2) // file delete
                    {
                        file.Status = false;
                        if (isKeepDocument == true)
                        {
                            file.DeleteType = 1; // recycle Folder
                        }
                        else
                        {
                            file.DeleteType = 2; //archive Folder
                        }
                    }
                    else if (status == 3) // file replace
                    {
                        var activeFile = _db.Files.FirstOrDefault(a => a.Id == activeId);
                        activeFile.Status = false;
                        activeFile.IsArchive = true;
                        activeFile.UpdatedBy = User.GetUserId();
                        activeFile.UpdatedDate = DateTime.Now;
                        _db.Entry(activeFile).State = EntityState.Modified;

                        file.Status = true;
                    }
                    file.UpdatedBy = User.GetUserId();
                    file.UpdatedDate = DateTime.Now;
                    _db.Entry(file).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else if(!string.IsNullOrEmpty(ids))
                {
                    foreach(var id in ids.Split(","))
                    {
                        fileId = Convert.ToInt64(id);
                        file = _db.Files.FirstOrDefault(a => a.Id == fileId);
                        file.Status = false;
                        if (isKeepDocument == true)
                        {
                            file.DeleteType = 1; // recycle Folder
                        }
                        else
                        {
                            file.DeleteType = 2; //archive Folder
                        }
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json("success");
            }
            catch (Exception)
            {
                return Json("error");
            }
        }

        public PartialViewResult FileRename(long id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult _FileRename(long id)
        {
            ViewBag.Name = _db.Files.Find(id).OriginalName;
            return PartialView();
        }
        public async Task<JsonResult> FileRenameSave(string name, long id)
        {
            try
            {
                string prevName = "";
                var file = _db.Files.Find(id);
                prevName = file.OriginalName;
                file.OriginalName = name;
                file.UpdatedBy = User.GetUserId();
                file.UpdatedDate = DateTime.Now;
                _db.Entry(file).State = EntityState.Modified;
                _db.SaveChanges();

                FileAuditLogModelView fileLog = new FileAuditLogModelView
                {
                    FileId = file.Id,
                    FileName = file.OriginalName,
                    Operation = "Rename",
                    Status = true,
                    Message = $"File has been successfully rename from {prevName} to {name}"
                };
                await Task.Run(() => FileAuditLogSave(fileLog));

                if (file.Type == 1)
                {
                    return Json(new { cabinetId = _protector.Protect(file.CabinetId.ToString()), type = file.Type });
                }
                else if(file.Type == 2)
                {
                    return Json(new { f1Id = _protector.Protect(file.F1id.ToString()), type = file.Type });
                }
                else if(file.Type == 3)
                {
                    return Json(new { f2Id = _protector.Protect(file.F2id.ToString()), type = file.Type });
                }
                else if (file.Type == 4)
                {
                    return Json(new { f3Id = _protector.Protect(file.F3id.ToString()), type = file.Type });
                }
                else
                {
                    return Json("error");
                }
            }
            catch (Exception)
            {
                FileAuditLogModelView fileLog = new FileAuditLogModelView
                {
                    FileName = "",
                    Operation = "Rename",
                    Status = false,
                    Message = "File rename was unsuccessful"
                };
                await Task.Run(() => FileAuditLogSave(fileLog));
                return Json("error");
            }
        }
        public async Task<FileResult> DownloadFile(long fileId)
        {
            var file = _db.Files.Find(fileId);
            var path = $"{_env.WebRootPath}\\Files\\";
            string fullPath = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            string fileName = file.OriginalName;

            //recent file log save
            await Task.Run(() => RecentFileLog(fileId));

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public async Task<FileStreamResult> FileView(string q)
        {
            long fileId = Convert.ToInt64(_protector.Unprotect(q));
            var file = _db.Files.Find(fileId);
            var path = $"{_env.WebRootPath}\\Files\\";
            string fullPath = Path.Combine("wwwroot/Files/", file.Name);
            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            var extn = Path.GetExtension(file.Name);
            //recent file log save
            await Task.Run(() => RecentFileLog(fileId));

            if (file.FileType == 1)
            {
                if(extn == ".jpeg" || extn == ".jpg")
                {
                    return File(fs, "image/jpeg");
                }
                else if(extn == ".gif")
                {
                    return File(fs, "image/gif");
                }
                else
                {
                    return File(fs, "image/png");
                }
            }
            else if(file.FileType == 2)
            {
                return File(fs, "application/pdf");
            }
            else if(file.FileType == 4)
            {
                return File(fs, "text/plain");
            }
            else
            {
                return File(fs, System.Net.Mime.MediaTypeNames.Application.Octet, file.OriginalName);
            }
        }
        //folder and files copy/move
        public async Task<JsonResult> FileCopy(long? fileId, string folderId, string documents, bool isCopy, long destFolderId, int fType, int type, string encryptedId)
        {
            FileAuditLogModelView fileLog;
            try
            {
                int count = 1;
                Random rand = new Random((int)DateTime.Now.Ticks);
                //****************************file copy/move ********************************
                // ftype = destination folder type
                if (fileId > 0)
                {
                    var file = _db.Files.Find(fileId);
                    var newFile = new Files();
                    //**********cabinet************
                    if (fType == 1)
                    {
                        if(file.CabinetId == destFolderId && file.Type == fType)
                        {
                            return Json("same");
                        }
                        if (_db.Files.Any(a => a.CabinetId == destFolderId && a.Type == fType))
                        {
                            count = _db.Files.Where(a => a.CabinetId == destFolderId && a.Type == fType).Max(a => a.ListCount);
                        }
                        newFile.CabinetId = (int)destFolderId;
                    }
                    //**********Folder 1************
                    else if (fType == 2)
                    {
                        var f1 = _db.F1.Find(destFolderId);
                        if (file.CabinetId == f1.CabinetId && file.F1id == f1.Id && file.Type == fType)
                        {
                            return Json("same");
                        }
                        if (_db.Files.Any(a => a.F1id == destFolderId && a.Type == fType))
                        {
                            count = _db.Files.Where(a => a.F1id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                        }
                        newFile.CabinetId = f1.CabinetId;
                        newFile.F1id = f1.Id;
                    }
                    //**********Folder 2************
                    else if (fType == 3)
                    {
                        var f2 = _db.F2.Find(destFolderId);
                        if (file.CabinetId == f2.CabinetId && file.F1id == f2.F1id && file.F2id == f2.Id && file.Type == fType)
                        {
                            return Json("same");
                        }
                        if (_db.Files.Any(a => a.F2id == destFolderId && a.Type == fType))
                        {
                            count = _db.Files.Where(a => a.F2id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                        }
                        newFile.CabinetId = f2.CabinetId;
                        newFile.F1id = f2.F1id;
                        newFile.F2id = f2.Id;
                    }
                    //**********Folder 3************
                    else if (fType == 4)
                    {
                        var f3 = _db.F3.Find(destFolderId);
                        if (file.CabinetId == f3.CabinetId && file.F1id == f3.F1id && file.F2id == f3.F2id && file.F3id == f3.Id && file.Type == fType)
                        {
                            return Json("same");
                        }
                        if (_db.Files.Any(a => a.F3id == destFolderId && a.Type == fType))
                        {
                            count = _db.Files.Where(a => a.F3id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                        }
                        newFile.CabinetId = f3.CabinetId;
                        newFile.F1id = f3.F1id;
                        newFile.F2id = f3.F2id;
                        newFile.F3id = f3.Id;
                    }
                    count++;
                    if (isCopy)
                    {
                        newFile.CreatedBy = User.GetUserId();
                        newFile.CreatedDate = DateTime.Now;
                        newFile.Type = fType;
                        newFile.Size = file.Size;
                        newFile.ListCount = count;
                        newFile.FileType = file.FileType;
                        newFile.Status = true;
                        newFile.OriginalName = file.OriginalName;
                        newFile.Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}";

                        _db.Files.Add(newFile);
                        _db.SaveChanges();

                        var path = $"{_env.WebRootPath}\\Files\\";
                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                        System.IO.File.Copy(sourcFile, destFile, true);

                        //storage update
                        await Task.Run(() => StorageUpdate(newFile.Size));

                        //file log save
                        fileLog = new FileAuditLogModelView
                        {
                            FileId = newFile.Id,
                            FileName = newFile.OriginalName,
                            Operation = "Copy",
                            Status = true
                        };
                        switch (fType)
                        {
                            case 1:
                                fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\";
                                break;
                            case 2:
                                fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\";
                                break;
                            case 3:
                                fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\";
                                break;
                            case 4:
                                fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\";
                                break;
                        }
                        await Task.Run(() => FileAuditLogSave(fileLog));

                        return Json("success");
                    }
                    else
                    {
                        file.CabinetId = newFile.CabinetId;
                        file.F1id = newFile.F1id;
                        file.F2id = newFile.F2id;
                        file.F3id = newFile.F3id;
                        file.Type = fType;
                        file.ListCount = count;
                        file.IsArchive = null;
                        file.UpdatedBy = User.GetUserId();
                        file.UpdatedDate = DateTime.Now;
                        _db.Entry(file).State = EntityState.Modified;
                        _db.SaveChanges();

                        fileLog = new FileAuditLogModelView
                        {
                            FileId = file.Id,
                            FileName = file.OriginalName,
                            Operation = "Move",
                            Status = true
                        };
                        switch (fType)
                        {
                            case 1:
                                fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\";
                                break;
                            case 2:
                                fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\";
                                break;
                            case 3:
                                fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\";
                                break;
                            case 4:
                                fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\";
                                break;
                        }
                        await Task.Run(() => FileAuditLogSave(fileLog));
                    }
                }
                //***********************Folder copy/move *************************************
                else if(!string.IsNullOrEmpty(folderId))
                {
                    long fId = Convert.ToInt64(_protector.Unprotect(folderId));
                    bool copyPossible = true;
                    if (isCopy == true) // copy
                    {
                        if (type == 1) // F1 folder
                        {
                            var f1 = _db.F1.Find(fId);
                            if (fType == 1) // copy to cabinet
                            {
                                if(f1.CabinetId == destFolderId)
                                {
                                    return Json("same");
                                }
                                var newF1 = new F1
                                {
                                    CabinetId = (int)destFolderId,
                                    Name = f1.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F1.Add(newF1);
                                _db.SaveChanges();

                                // f2 folder copy to new f1
                                if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                    foreach(var f2 in f2List)
                                    {
                                        var newF2 = new F2
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        if(_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                            foreach(var f3 in f3List)
                                            {
                                                var newF3 = new F3
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Name = f3.Name,
                                                    Status = 1,
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };
                                                _db.F3.Add(newF3);
                                                _db.SaveChanges();

                                                // f3 files copy 
                                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                    count = 1;
                                                    foreach (var file in fileList)
                                                    {
                                                        var newFile = new Files
                                                        {
                                                            CabinetId = newF3.CabinetId,
                                                            F1id = newF3.F1id,
                                                            F2id = newF3.F2id,
                                                            F3id = newF3.Id,
                                                            Type = 4,
                                                            ListCount = count,
                                                            FileType = file.FileType,
                                                            Status = true,
                                                            Size = file.Size,
                                                            OriginalName = file.OriginalName,
                                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                            CreatedBy = User.GetUserId(),
                                                            CreatedDate = DateTime.Now
                                                        };

                                                        _db.Files.Add(newFile);
                                                        _db.SaveChanges();

                                                        var path = $"{_env.WebRootPath}\\Files\\";
                                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                        System.IO.File.Copy(sourcFile, destFile, true);
                                                        count++;

                                                        //storage update
                                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                                        fileLog = new FileAuditLogModelView
                                                        {
                                                            FileId = newFile.Id,
                                                            FileName = newFile.OriginalName,
                                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\",
                                                            Operation = "Copy",
                                                            Status = true
                                                        };
                                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                                    }
                                                }

                                            }
                                        }
                                        // f2 files copy 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Type = 3,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };
                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);
                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));

                                                fileLog = new FileAuditLogModelView
                                                {
                                                    FileId = newFile.Id,
                                                    FileName = newFile.OriginalName,
                                                    Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\",
                                                    Operation = "Copy",
                                                    Status = true
                                                };
                                                await Task.Run(() => FileAuditLogSave(fileLog));
                                            }
                                        }
                                    }
                                }

                                // f1 files copy 
                                if(_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                {
                                    var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Type = 2,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            Size = file.Size,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);
                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if(fType == 2) // copy to f1 folder
                            {
                                //check copy restriction 
                                if(_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                    foreach(var f2 in f2List)
                                    {
                                        if(_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            copyPossible = false;
                                            break;
                                        }
                                    }
                                }
                                if(copyPossible)
                                {
                                    var destFolder = _db.F1.Find(destFolderId);
                                    var newF2 = new F2
                                    {
                                        CabinetId = destFolder.CabinetId,
                                        F1id = destFolder.Id,
                                        Name = f1.Name,
                                        Status = 1,
                                        CreatedBy = User.GetUserId(),
                                        CreatedDate = DateTime.Now
                                    };
                                    _db.F2.Add(newF2);
                                    _db.SaveChanges();

                                    var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                    foreach (var f2 in f2List)
                                    {
                                        var newF3 = new F3
                                        {
                                            CabinetId = newF2.CabinetId,
                                            F1id = newF2.F1id,
                                            F2id = newF2.Id,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f2 files copy to new f3
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF3.CabinetId,
                                                    F1id = newF3.F1id,
                                                    F2id = newF3.F2id,
                                                    F3id = newF3.Id,
                                                    Type = 4,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));

                                                fileLog = new FileAuditLogModelView
                                                {
                                                    FileId = newFile.Id,
                                                    FileName = newFile.OriginalName,
                                                    Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\",
                                                    Operation = "Copy",
                                                    Status = true
                                                };
                                                await Task.Run(() => FileAuditLogSave(fileLog));
                                            }
                                        }
                                    }

                                    // f1 files copy to new f2
                                    if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                    {
                                        var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                        count = 1;
                                        foreach (var file in fileList)
                                        {
                                            var newFile = new Files
                                            {
                                                CabinetId = newF2.CabinetId,
                                                F1id = newF2.F1id,
                                                F2id = newF2.Id,
                                                Type = 3,
                                                ListCount = count,
                                                FileType = file.FileType,
                                                Status = true,
                                                Size = file.Size,
                                                OriginalName = file.OriginalName,
                                                Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                CreatedBy = User.GetUserId(),
                                                CreatedDate = DateTime.Now
                                            };

                                            _db.Files.Add(newFile);
                                            _db.SaveChanges();

                                            var path = $"{_env.WebRootPath}\\Files\\";
                                            string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                            string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                            System.IO.File.Copy(sourcFile, destFile, true);

                                            count++;

                                            //storage update
                                            await Task.Run(() => StorageUpdate(newFile.Size));

                                            fileLog = new FileAuditLogModelView
                                            {
                                                FileId = newFile.Id,
                                                FileName = newFile.OriginalName,
                                                Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\",
                                                Operation = "Copy",
                                                Status = true
                                            };
                                            await Task.Run(() => FileAuditLogSave(fileLog));
                                        }
                                    }
                                }
                                else
                                {
                                    return Json("NotPossible");
                                }
                            }
                            else if(fType == 3) // copy to f2 folder
                            {
                                //check copy restriction
                                if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    copyPossible = false;
                                }
                                if(copyPossible)
                                {
                                    var destFolder = _db.F2.Find(destFolderId);
                                    var newF3 = new F3
                                    {
                                        CabinetId = destFolder.CabinetId,
                                        F1id = destFolder.F1id,
                                        F2id = destFolder.Id,
                                        Name = f1.Name,
                                        Status = 1,
                                        CreatedBy = User.GetUserId(),
                                        CreatedDate = DateTime.Now
                                    };
                                    _db.F3.Add(newF3);
                                    _db.SaveChanges();

                                    // f2 files copy to new f3
                                    if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                    {
                                        var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                        count = 1;
                                        foreach (var file in fileList)
                                        {
                                            var newFile = new Files
                                            {
                                                CabinetId = newF3.CabinetId,
                                                F1id = newF3.F1id,
                                                F2id = newF3.F2id,
                                                F3id = newF3.Id,
                                                Type = 4,
                                                ListCount = count,
                                                FileType = file.FileType,
                                                Status = true,
                                                Size = file.Size,
                                                OriginalName = file.OriginalName,
                                                Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                CreatedBy = User.GetUserId(),
                                                CreatedDate = DateTime.Now
                                            };

                                            _db.Files.Add(newFile);
                                            _db.SaveChanges();

                                            var path = $"{_env.WebRootPath}\\Files\\";
                                            string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                            string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                            System.IO.File.Copy(sourcFile, destFile, true);

                                            count++;

                                            //storage update
                                            await Task.Run(() => StorageUpdate(newFile.Size));

                                            fileLog = new FileAuditLogModelView
                                            {
                                                FileId = newFile.Id,
                                                FileName = newFile.OriginalName,
                                                Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\",
                                                Operation = "Copy",
                                                Status = true
                                            };
                                            await Task.Run(() => FileAuditLogSave(fileLog));
                                        }
                                    }
                                }
                                else
                                {
                                    return Json("NotPossible");
                                }
                            }
                            else if(fType == 4)
                            {
                                return Json("NotPossible");
                            }
                        }
                        else if(type == 2) // f2 folder
                        {
                            var f2 = _db.F2.Find(fId);
                            if(fType == 1) // copy to cabinet
                            {
                                var newF1 = new F1
                                {
                                    CabinetId = (int)destFolderId,
                                    Name = f2.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F1.Add(newF1);
                                _db.SaveChanges();

                                // f3 folder copy to new f1
                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        var newF2 = new F2
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Type = 3,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));

                                                fileLog = new FileAuditLogModelView
                                                {
                                                    FileId = newFile.Id,
                                                    FileName = newFile.OriginalName,
                                                    Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\",
                                                    Operation = "Copy",
                                                    Status = true
                                                };
                                                await Task.Run(() => FileAuditLogSave(fileLog));
                                            }
                                        }
                                    }
                                }

                                // f2 files copy 
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Type = 2,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);

                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if(fType == 2) // copy to f1
                            {
                                if(f2.F1id == destFolderId)
                                {
                                    return Json("same");
                                }
                                var destFolder = _db.F1.Find(destFolderId);
                                var newF2 = new F2
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.Id,
                                    Name = f2.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F2.Add(newF2);
                                _db.SaveChanges();

                                // f2 files copy 
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF2.CabinetId,
                                            F1id = newF2.F1id,
                                            F2id = newF2.Id,
                                            Type = 3,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);

                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if(fType == 3) // copy to f2
                            {
                                //check copy restriction
                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                {
                                    return Json("NotPossible");
                                }
                                var destFolder = _db.F2.Find(destFolderId);
                                var newF3 = new F3
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.F1id,
                                    F2id = destFolder.Id,
                                    Name = f2.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F3.Add(newF3);
                                _db.SaveChanges();

                                // f2 files copy to new f3
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF3.CabinetId,
                                            F1id = newF3.F1id,
                                            F2id = newF3.F2id,
                                            F3id = newF3.Id,
                                            Type = 4,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            Size = file.Size,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);

                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                
                            }
                            else if(fType == 4)
                            {
                                return Json("NotPossible");
                            }
                        }
                        else if(type == 3) // f3 folder
                        {
                            var f3 = _db.F3.Find(fId);
                            if(fType == 1) // copy to cabinet
                            {
                                var newF1 = new F1
                                {
                                    CabinetId = (int)destFolderId,
                                    Name = f3.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F1.Add(newF1);
                                _db.SaveChanges();

                                // f3 files copy 
                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                {
                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Type = 2,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            Size = file.Size,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);

                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }

                            }
                            else if(fType == 2) // copy to f1
                            {
                                var destFolder = _db.F1.Find(destFolderId);
                                var newF2 = new F2
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.Id,
                                    Name = f3.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F2.Add(newF2);
                                _db.SaveChanges();

                                // f3 files copy 
                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                {
                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {
                                        var newFile = new Files
                                        {
                                            CabinetId = newF2.CabinetId,
                                            F1id = newF2.F1id,
                                            F2id = newF2.Id,
                                            Type = 3,
                                            ListCount = count,
                                            FileType = file.FileType,
                                            Status = true,
                                            Size = file.Size,
                                            OriginalName = file.OriginalName,
                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };

                                        _db.Files.Add(newFile);
                                        _db.SaveChanges();

                                        var path = $"{_env.WebRootPath}\\Files\\";
                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                        System.IO.File.Copy(sourcFile, destFile, true);

                                        count++;

                                        //storage update
                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = newFile.Id,
                                            FileName = newFile.OriginalName,
                                            Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\",
                                            Operation = "Copy",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if(fType == 3) // copy to f2
                            {
                                if(f3.F2id == destFolderId)
                                {
                                    return Json("same");
                                }
                                else
                                {
                                    var destFolder = _db.F2.Find(destFolderId);
                                    var newF3 = new F3
                                    {
                                        CabinetId = destFolder.CabinetId,
                                        F1id = destFolder.F1id,
                                        F2id = destFolder.Id,
                                        Name = f3.Name,
                                        Status = 1,
                                        CreatedBy = User.GetUserId(),
                                        CreatedDate = DateTime.Now
                                    };
                                    _db.F3.Add(newF3);
                                    _db.SaveChanges();

                                    // f3 files copy 
                                    if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                    {
                                        var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                        count = 1;
                                        foreach (var file in fileList)
                                        {
                                            var newFile = new Files
                                            {
                                                CabinetId = newF3.CabinetId,
                                                F1id = newF3.F1id,
                                                F2id = newF3.F2id,
                                                F3id = newF3.Id,
                                                Type = 4,
                                                ListCount = count,
                                                FileType = file.FileType,
                                                Status = true,
                                                Size = file.Size,
                                                OriginalName = file.OriginalName,
                                                Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                CreatedBy = User.GetUserId(),
                                                CreatedDate = DateTime.Now
                                            };

                                            _db.Files.Add(newFile);
                                            _db.SaveChanges();

                                            var path = $"{_env.WebRootPath}\\Files\\";
                                            string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                            string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                            System.IO.File.Copy(sourcFile, destFile, true);

                                            count++;

                                            //storage update
                                            await Task.Run(() => StorageUpdate(newFile.Size));

                                            fileLog = new FileAuditLogModelView
                                            {
                                                FileId = newFile.Id,
                                                FileName = newFile.OriginalName,
                                                Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\",
                                                Operation = "Copy",
                                                Status = true
                                            };
                                            await Task.Run(() => FileAuditLogSave(fileLog));
                                        }
                                    }
                                }
                            }
                            else if(fType == 4) // copy to f3
                            {
                                return Json("NotPossible");
                            }
                        }
                        return Json("success");
                    }
                    else
                    {
                        //move
                        if (type == 1) // F1 folder
                        {
                            var f1 = _db.F1.Find(fId);
                            if (fType == 1) // move to cabinet
                            {
                                if (f1.CabinetId == destFolderId)
                                {
                                    return Json("same");
                                }
                                f1.CabinetId = (int)destFolderId;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;

                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();

                                // f2 folder move to new f1
                                if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                    foreach (var f2 in f2List)
                                    {
                                        f2.CabinetId = f1.CabinetId;
                                        f2.UpdatedBy = User.GetUserId();
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                            foreach (var f3 in f3List)
                                            {
                                                f3.CabinetId = f2.CabinetId;
                                                _db.Entry(f3).State = EntityState.Modified;
                                                _db.SaveChanges();

                                                // f3 files copy 
                                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                    foreach (var file in fileList)
                                                    {
                                                        file.CabinetId = f3.CabinetId;
                                                        file.UpdatedBy = User.GetUserId();
                                                        file.UpdatedDate = DateTime.Now;
                                                        _db.Entry(file).State = EntityState.Modified;
                                                        _db.SaveChanges();

                                                        fileLog = new FileAuditLogModelView
                                                        {
                                                            FileId = file.Id,
                                                            FileName = file.OriginalName,
                                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\",
                                                            Operation = "Move",
                                                            Status = true
                                                        };
                                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                                    }
                                                }
                                            }
                                        }

                                        // f2 files copy 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = f2.CabinetId;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();

                                                fileLog = new FileAuditLogModelView
                                                {
                                                    FileId = file.Id,
                                                    FileName = file.OriginalName,
                                                    Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\",
                                                    Operation = "Move",
                                                    Status = true
                                                };
                                                await Task.Run(() => FileAuditLogSave(fileLog));
                                            }
                                        }
                                    }
                                }

                                // f1 files move 
                                if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                {
                                    var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                    foreach (var file in fileList)
                                    {
                                        file.CabinetId = f1.CabinetId;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if (fType == 2) // move to f1 folder
                            {
                                //check move restriction 
                                if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    var f2Lists = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                    foreach (var f2 in f2Lists)
                                    {
                                        if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            return Json("NotPossible");
                                        }
                                    }
                                }
                                var destFolder = _db.F1.Find(destFolderId);
                                var newF2 = new F2
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.Id,
                                    Name = f1.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F2.Add(newF2);
                                _db.SaveChanges();

                                var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                foreach (var f2 in f2List)
                                {
                                    var newF3 = new F3
                                    {
                                        CabinetId = newF2.CabinetId,
                                        F1id = newF2.F1id,
                                        F2id = newF2.Id,
                                        Name = f2.Name,
                                        Status = 1,
                                        CreatedBy = User.GetUserId(),
                                        CreatedDate = DateTime.Now
                                    };
                                    _db.F3.Add(newF3);
                                    _db.SaveChanges();

                                    // f2 files copy to new f3
                                    if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                    {
                                        var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                        count = 0;
                                        foreach (var file in fileList)
                                        {
                                            count++;
                                            file.CabinetId = newF3.CabinetId;
                                            file.F1id = newF3.F1id;
                                            file.F2id = newF3.F2id;
                                            file.F3id = newF3.Id;
                                            file.Type = 4;
                                            file.ListCount = count;
                                            file.UpdatedBy = User.GetUserId();
                                            file.UpdatedDate = DateTime.Now;
                                            _db.Entry(file).State = EntityState.Modified;
                                            _db.SaveChanges();

                                            fileLog = new FileAuditLogModelView
                                            {
                                                FileId = file.Id,
                                                FileName = file.OriginalName,
                                                Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\",
                                                Operation = "Move",
                                                Status = true
                                            };
                                            await Task.Run(() => FileAuditLogSave(fileLog));
                                        }
                                    }
                                    f2.Status = 0;
                                    _db.Entry(f2).State = EntityState.Modified;
                                    _db.SaveChanges();
                                }

                                // f1 files copy to new f2
                                if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                {
                                    var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                    count = 0;
                                    foreach (var file in fileList)
                                    {
                                        count++;
                                        file.CabinetId = newF2.CabinetId;
                                        file.F1id = newF2.F1id;
                                        file.F2id = newF2.Id;
                                        file.F3id = null;
                                        file.Type = 3;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f1.Status = 0;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 3) // move to f2 folder
                            {
                                //check move restriction
                                if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                {
                                    return Json("NotPossible");
                                }
                                var destFolder = _db.F2.Find(destFolderId);
                                var newF3 = new F3
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.F1id,
                                    F2id = destFolder.Id,
                                    Name = f1.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F3.Add(newF3);
                                _db.SaveChanges();

                                // f2 files move to new f3
                                if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                {
                                    var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                    count = 0;
                                    foreach (var file in fileList)
                                    {
                                        count++;
                                        file.CabinetId = newF3.CabinetId;
                                        file.F1id = newF3.F1id;
                                        file.F2id = newF3.F2id;
                                        file.F3id = newF3.Id;
                                        file.Type = 4;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f1.Status = 0;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 4)
                            {
                                return Json("NotPossible");
                            }
                        }
                        else if (type == 2) // f2 folder
                        {
                            var f2 = _db.F2.Find(fId);
                            if (fType == 1) // copy to cabinet
                            {
                                var newF1 = new F1
                                {
                                    CabinetId = (int)destFolderId,
                                    Name = f2.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F1.Add(newF1);
                                _db.SaveChanges();

                                // f3 folder copy to new f1
                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        var newF2 = new F2
                                        {
                                            CabinetId = newF1.CabinetId,
                                            F1id = newF1.Id,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 0;
                                            foreach (var file in fileList)
                                            {
                                                count++;
                                                file.CabinetId = newF2.CabinetId;
                                                file.F1id = newF2.F1id;
                                                file.F2id = newF2.Id;
                                                file.F3id = null;
                                                file.Type = 3;
                                                file.ListCount = count;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();

                                                fileLog = new FileAuditLogModelView
                                                {
                                                    FileId = file.Id,
                                                    FileName = file.OriginalName,
                                                    Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\",
                                                    Operation = "Move",
                                                    Status = true
                                                };
                                                await Task.Run(() => FileAuditLogSave(fileLog));
                                            }
                                        }
                                        f3.Status = 0;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                // f2 files copy 
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    count = 0;
                                    foreach (var file in fileList)
                                    {
                                        count++;
                                        file.CabinetId = newF1.CabinetId;
                                        file.F1id = newF1.Id;
                                        file.F2id = null;
                                        file.F3id = null;
                                        file.Type = 2;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f2.Status = 0;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 2) // copy to f1
                            {
                                if (f2.F1id == destFolderId)
                                {
                                    return Json("same");
                                }
                                var destFolder = _db.F1.Find(destFolderId);
                                f2.CabinetId = destFolder.CabinetId;
                                f2.F1id = destFolder.Id;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();         

                                // f2 files move 
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    foreach (var file in fileList)
                                    {
                                        file.CabinetId = f2.CabinetId;
                                        file.F1id = f2.F1id;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id)}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                            }
                            else if (fType == 3) // copy to f2
                            {
                                //check copy restriction
                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                {
                                    return Json("NotPossible");
                                }
                                var destFolder = _db.F2.Find(destFolderId);
                                var newF3 = new F3
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.F1id,
                                    F2id = destFolder.Id,
                                    Name = f2.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F3.Add(newF3);
                                _db.SaveChanges();

                                // f2 files copy to new f3
                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                {
                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                    count = 0;
                                    foreach (var file in fileList)
                                    {
                                        count++;
                                        file.CabinetId = newF3.CabinetId;
                                        file.F1id = newF3.F1id;
                                        file.F2id = newF3.F2id;
                                        file.F3id = newF3.Id;
                                        file.Type = 4;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id)}\\{_db.F3.Find(file.F3id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f2.Status = 0;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 4)
                            {
                                return Json("NotPossible");
                            }
                        }
                        else if (type == 3) // f3 folder
                        {
                            var f3 = _db.F3.Find(fId);
                            if (fType == 1) // move to cabinet
                            {
                                var newF1 = new F1
                                {
                                    CabinetId = (int)destFolderId,
                                    Name = f3.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F1.Add(newF1);
                                _db.SaveChanges();

                                // f3 files copy 
                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                {
                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                    count = 1;
                                    foreach (var file in fileList)
                                    {

                                        file.CabinetId = newF1.CabinetId;
                                        file.F1id = newF1.Id;
                                        file.F2id = null;
                                        file.F3id = null;
                                        file.Type = 2;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        count++;

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f3.Status = 0;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 2) // move to f1
                            {
                                var destFolder = _db.F1.Find(destFolderId);
                                var newF2 = new F2
                                {
                                    CabinetId = destFolder.CabinetId,
                                    F1id = destFolder.Id,
                                    Name = f3.Name,
                                    Status = 1,
                                    CreatedBy = User.GetUserId(),
                                    CreatedDate = DateTime.Now
                                };
                                _db.F2.Add(newF2);
                                _db.SaveChanges();

                                // f3 files copy 
                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                {
                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                    count = 0;
                                    foreach (var file in fileList)
                                    {
                                        count++;
                                        file.CabinetId = newF2.CabinetId;
                                        file.F1id = newF2.F1id;
                                        file.F2id = newF2.Id;
                                        file.F3id = null;
                                        file.Type = 3;
                                        file.ListCount = count;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        fileLog = new FileAuditLogModelView
                                        {
                                            FileId = file.Id,
                                            FileName = file.OriginalName,
                                            Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\",
                                            Operation = "Move",
                                            Status = true
                                        };
                                        await Task.Run(() => FileAuditLogSave(fileLog));
                                    }
                                }
                                f3.Status = 0;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else if (fType == 3) // move to f2
                            {
                                if (f3.F2id == destFolderId)
                                {
                                    return Json("same");
                                }
                                else
                                {
                                    var destFolder = _db.F2.Find(destFolderId);
                                    f3.CabinetId = destFolder.CabinetId;
                                    f3.F1id = destFolder.F1id;
                                    f3.F2id = destFolder.Id;
                                    f3.UpdatedBy = User.GetUserId();
                                    f3.UpdatedDate = DateTime.Now;
                                    _db.Entry(f3).State = EntityState.Modified;
                                    _db.SaveChanges();

                                    // f3 files copy 
                                    if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                    {
                                        var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                        count = 1;
                                        foreach (var file in fileList)
                                        {
                                            file.CabinetId = f3.CabinetId;
                                            file.F1id = f3.F1id;
                                            file.F2id = f3.F2id;
                                            file.UpdatedBy = User.GetUserId();
                                            file.UpdatedDate = DateTime.Now;
                                            _db.Entry(file).State = EntityState.Modified;
                                            _db.SaveChanges();

                                            fileLog = new FileAuditLogModelView
                                            {
                                                FileId = file.Id,
                                                FileName = file.OriginalName,
                                                Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\",
                                                Operation = "Move",
                                                Status = true
                                            };
                                            await Task.Run(() => FileAuditLogSave(fileLog));
                                        }
                                    }
                                }
                            }
                            else if (fType == 4) // copy to f3
                            {
                                return Json("NotPossible");
                            }
                        }
                    }
                }
                //**************************files & folder copy/move ****************************
                else if(!string.IsNullOrEmpty(documents))
                {
                    bool isFile = false;
                    long id = 0;
                    //check copy/move restriction
                    foreach (var doc in documents.Split(","))
                    {
                        isFile = long.TryParse(doc, out long number);
                        if (isFile == false) // folder
                        {
                            id = Convert.ToInt64(_protector.Unprotect(doc));
                            if (type == 1) // f1 folder
                            {
                                var f1 = _db.F1.Find(id);
                                if (fType == 1) // copy to cabinet
                                {
                                    if (f1.CabinetId == destFolderId)
                                    {
                                        return Json("same");
                                    }
                                }
                                else if(fType == 2) //copy to f1 folder
                                {
                                    if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                    {
                                        var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                        foreach (var f2 in f2List)
                                        {
                                            if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                            {
                                                return Json("NotPossible");
                                            }
                                        }
                                    }
                                }
                                else if(fType == 3) //copy to f2 folder
                                {
                                    if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                    {
                                        return Json("NotPossible");
                                    }
                                }
                                else if(fType == 4) //copy to f3 folder
                                {
                                    return Json("NotPossible");
                                }
                            }
                            else if(type == 2) // f2 folder
                            {
                                var f2 = _db.F2.Find(id);
                                if(fType == 2) // copy to f1 folder
                                {
                                    if (f2.F1id == destFolderId)
                                    {
                                        return Json("same");
                                    }
                                }
                                else if(fType == 3) // copy to f2 folder
                                {
                                    if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                    {
                                        return Json("NotPossible");
                                    }
                                }
                                else if(fType == 4)
                                {
                                    return Json("NotPossible");
                                }
                            }
                            else if(type == 3) //f3 folder
                            {
                                if(fType == 3) // copy to f2
                                {
                                    var f3 = _db.F3.Find(id);
                                    if (f3.F2id == destFolderId)
                                    {
                                        return Json("same");
                                    }
                                }
                                else if(fType == 4) // copy to f3
                                {
                                    return Json("NotPossible");
                                }
                            }
                        }
                    }
                    //copy to destination folder
                    foreach (var doc in documents.Split(","))
                    {
                        isFile = long.TryParse(doc, out long number);
                        if(isCopy == true) //copy
                        {
                            if (isFile == false) // folder
                            {
                                id = Convert.ToInt64(_protector.Unprotect(doc));
                                if (type == 1) // f1 folder
                                {
                                    var f1 = _db.F1.Find(id);
                                    if (fType == 1) // copy to cabinet
                                    {
                                        var newF1 = new F1
                                        {
                                            CabinetId = (int)destFolderId,
                                            Name = f1.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F1.Add(newF1);
                                        _db.SaveChanges();

                                        // f2 folder copy to new f1
                                        if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                        {
                                            var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                            foreach (var f2 in f2List)
                                            {
                                                var newF2 = new F2
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Name = f2.Name,
                                                    Status = 1,
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };
                                                _db.F2.Add(newF2);
                                                _db.SaveChanges();

                                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                                {
                                                    var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                                    foreach (var f3 in f3List)
                                                    {
                                                        var newF3 = new F3
                                                        {
                                                            CabinetId = newF2.CabinetId,
                                                            F1id = newF2.F1id,
                                                            F2id = newF2.Id,
                                                            Name = f3.Name,
                                                            Status = 1,
                                                            CreatedBy = User.GetUserId(),
                                                            CreatedDate = DateTime.Now
                                                        };
                                                        _db.F3.Add(newF3);
                                                        _db.SaveChanges();

                                                        // f3 files copy 
                                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                        {
                                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                            count = 1;
                                                            foreach (var file in fileList)
                                                            {
                                                                var newFile = new Files
                                                                {
                                                                    CabinetId = newF3.CabinetId,
                                                                    F1id = newF3.F1id,
                                                                    F2id = newF3.F2id,
                                                                    F3id = newF3.Id,
                                                                    Type = 4,
                                                                    ListCount = count,
                                                                    FileType = file.FileType,
                                                                    Status = true,
                                                                    Size = file.Size,
                                                                    OriginalName = file.OriginalName,
                                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                                    CreatedBy = User.GetUserId(),
                                                                    CreatedDate = DateTime.Now
                                                                };

                                                                _db.Files.Add(newFile);
                                                                _db.SaveChanges();

                                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                                count++;

                                                                //storage update
                                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                                            }
                                                        }
                                                    }
                                                }

                                                // f2 files copy 
                                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                                    count = 1;
                                                    foreach (var file in fileList)
                                                    {
                                                        var newFile = new Files
                                                        {
                                                            CabinetId = newF2.CabinetId,
                                                            F1id = newF2.F1id,
                                                            F2id = newF2.Id,
                                                            Type = 3,
                                                            ListCount = count,
                                                            FileType = file.FileType,
                                                            Status = true,
                                                            Size = file.Size,
                                                            OriginalName = file.OriginalName,
                                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                            CreatedBy = User.GetUserId(),
                                                            CreatedDate = DateTime.Now
                                                        };

                                                        _db.Files.Add(newFile);
                                                        _db.SaveChanges();

                                                        var path = $"{_env.WebRootPath}\\Files\\";
                                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                        System.IO.File.Copy(sourcFile, destFile, true);

                                                        count++;

                                                        //storage update
                                                        await Task.Run(() => StorageUpdate(newFile.Size));

                                                    }
                                                }
                                            }
                                        }

                                        // f1 files copy 
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Type = 2,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }


                                    }
                                    else if (fType == 2) //copy to f1 folder
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        var newF2 = new F2
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.Id,
                                            Name = f1.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                        foreach (var f2 in f2List)
                                        {
                                            var newF3 = new F3
                                            {
                                                CabinetId = newF2.CabinetId,
                                                F1id = newF2.F1id,
                                                F2id = newF2.Id,
                                                Name = f2.Name,
                                                Status = 1,
                                                CreatedBy = User.GetUserId(),
                                                CreatedDate = DateTime.Now
                                            };
                                            _db.F3.Add(newF3);
                                            _db.SaveChanges();

                                            // f2 files copy to new f3
                                            if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                            {
                                                var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                                count = 1;
                                                foreach (var file in fileList)
                                                {
                                                    var newFile = new Files
                                                    {
                                                        CabinetId = newF3.CabinetId,
                                                        F1id = newF3.F1id,
                                                        F2id = newF3.F2id,
                                                        F3id = newF3.Id,
                                                        Type = 4,
                                                        ListCount = count,
                                                        FileType = file.FileType,
                                                        Status = true,
                                                        Size = file.Size,
                                                        OriginalName = file.OriginalName,
                                                        Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                        CreatedBy = User.GetUserId(),
                                                        CreatedDate = DateTime.Now
                                                    };

                                                    _db.Files.Add(newFile);
                                                    _db.SaveChanges();

                                                    var path = $"{_env.WebRootPath}\\Files\\";
                                                    string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                    string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                    System.IO.File.Copy(sourcFile, destFile, true);

                                                    count++;

                                                    //storage update
                                                    await Task.Run(() => StorageUpdate(newFile.Size));
                                                }
                                            }
                                        }

                                        // f1 files copy to new f2
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Type = 3,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                    else if (fType == 3) //copy to f2 folder
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        var newF3 = new F3
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.F1id,
                                            F2id = destFolder.Id,
                                            Name = f1.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f2 files copy to new f3
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF3.CabinetId,
                                                    F1id = newF3.F1id,
                                                    F2id = newF3.F2id,
                                                    F3id = newF3.Id,
                                                    Type = 4,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                }
                                else if (type == 2) // f2 folder
                                {
                                    var f2 = _db.F2.Find(id);
                                    if(fType == 1) //copy to cabinet
                                    {
                                        var newF1 = new F1
                                        {
                                            CabinetId = (int)destFolderId,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F1.Add(newF1);
                                        _db.SaveChanges();

                                        // f3 folder copy to new f1
                                        if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                            foreach (var f3 in f3List)
                                            {
                                                var newF2 = new F2
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Name = f3.Name,
                                                    Status = 1,
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };
                                                _db.F2.Add(newF2);
                                                _db.SaveChanges();

                                                // f3 files copy 
                                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                    count = 1;
                                                    foreach (var file in fileList)
                                                    {
                                                        var newFile = new Files
                                                        {
                                                            CabinetId = newF2.CabinetId,
                                                            F1id = newF2.F1id,
                                                            F2id = newF2.Id,
                                                            Type = 3,
                                                            ListCount = count,
                                                            FileType = file.FileType,
                                                            Status = true,
                                                            Size = file.Size,
                                                            OriginalName = file.OriginalName,
                                                            Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                            CreatedBy = User.GetUserId(),
                                                            CreatedDate = DateTime.Now
                                                        };

                                                        _db.Files.Add(newFile);
                                                        _db.SaveChanges();

                                                        var path = $"{_env.WebRootPath}\\Files\\";
                                                        string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                        string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                        System.IO.File.Copy(sourcFile, destFile, true);

                                                        count++;

                                                        //storage update
                                                        await Task.Run(() => StorageUpdate(newFile.Size));
                                                    }
                                                }
                                            }
                                        }

                                        // f2 files copy 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Type = 2,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                    else if (fType == 2) // copy to f1 folder
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        var newF2 = new F2
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.Id,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        // f2 files copy 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Type = 3,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                    else if (fType == 3) // copy to f2 folder
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        var newF3 = new F3
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.F1id,
                                            F2id = destFolder.Id,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f2 files copy to new f3
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF3.CabinetId,
                                                    F1id = newF3.F1id,
                                                    F2id = newF3.F2id,
                                                    F3id = newF3.Id,
                                                    Type = 4,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                }
                                else if (type == 3) //f3 folder
                                {
                                    var f3 = _db.F3.Find(id);
                                    if (fType == 1) // copy to cabinet
                                    {
                                        var newF1 = new F1
                                        {
                                            CabinetId = (int)destFolderId,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F1.Add(newF1);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Type = 2,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                    else if(fType == 2) //copy to f1
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        var newF2 = new F2
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.Id,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF2.CabinetId,
                                                    F1id = newF2.F1id,
                                                    F2id = newF2.Id,
                                                    Type = 3,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                    else if (fType == 3) // copy to f2
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        var newF3 = new F3
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.F1id,
                                            F2id = destFolder.Id,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                var newFile = new Files
                                                {
                                                    CabinetId = newF3.CabinetId,
                                                    F1id = newF3.F1id,
                                                    F2id = newF3.F2id,
                                                    F3id = newF3.Id,
                                                    Type = 4,
                                                    ListCount = count,
                                                    FileType = file.FileType,
                                                    Status = true,
                                                    Size = file.Size,
                                                    OriginalName = file.OriginalName,
                                                    Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}",
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };

                                                _db.Files.Add(newFile);
                                                _db.SaveChanges();

                                                var path = $"{_env.WebRootPath}\\Files\\";
                                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                                System.IO.File.Copy(sourcFile, destFile, true);

                                                count++;

                                                //storage update
                                                await Task.Run(() => StorageUpdate(newFile.Size));
                                            }
                                        }
                                    }
                                }
                            }
                            else 
                            {
                                fileId = Convert.ToInt64(doc);
                                var file = _db.Files.Find(fileId);
                                var newFile = new Files();
                                //**********cabinet************
                                if (fType == 1)
                                {
                                    if (file.CabinetId == destFolderId && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.CabinetId == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.CabinetId == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    newFile.CabinetId = (int)destFolderId;
                                }
                                //**********Folder 1************
                                else if (fType == 2)
                                {
                                    var f1 = _db.F1.Find(destFolderId);
                                    if (file.CabinetId == f1.CabinetId && file.F1id == f1.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F1id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F1id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    newFile.CabinetId = f1.CabinetId;
                                    newFile.F1id = f1.Id;
                                }
                                //**********Folder 2************
                                else if (fType == 3)
                                {
                                    var f2 = _db.F2.Find(destFolderId);
                                    if (file.CabinetId == f2.CabinetId && file.F1id == f2.F1id && file.F2id == f2.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F2id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F2id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    newFile.CabinetId = f2.CabinetId;
                                    newFile.F1id = f2.F1id;
                                    newFile.F2id = f2.Id;
                                }
                                //**********Folder 3************
                                else if (fType == 4)
                                {
                                    var f3 = _db.F3.Find(destFolderId);
                                    if (file.CabinetId == f3.CabinetId && file.F1id == f3.F1id && file.F2id == f3.F2id && file.F3id == f3.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F3id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F3id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    newFile.CabinetId = f3.CabinetId;
                                    newFile.F1id = f3.F1id;
                                    newFile.F2id = f3.F2id;
                                    newFile.F3id = f3.Id;
                                }
                                count++;
                                newFile.CreatedBy = User.GetUserId();
                                newFile.CreatedDate = DateTime.Now;
                                newFile.Type = fType;
                                newFile.ListCount = count;
                                newFile.FileType = file.FileType;
                                newFile.Status = true;
                                newFile.Size = file.Size;
                                newFile.OriginalName = file.OriginalName;
                                newFile.Name = $"{rand.Next()}{_db.Files.Max(a => a.Id)}{Path.GetExtension(file.Name)}";

                                _db.Files.Add(newFile);
                                _db.SaveChanges();

                                var path = $"{_env.WebRootPath}\\Files\\";
                                string sourcFile = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
                                string destFile = Path.Combine($"{_env.WebRootPath}\\Files\\", newFile.Name);
                                System.IO.File.Copy(sourcFile, destFile, true);

                                //storage update
                                await Task.Run(() => StorageUpdate(newFile.Size));

                                fileLog = new FileAuditLogModelView
                                {
                                    FileId = newFile.Id,
                                    FileName = newFile.OriginalName,
                                    Operation = "Copy",
                                    Status = true
                                };
                                switch (fType)
                                {
                                    case 1:
                                        fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\";
                                        break;
                                    case 2:
                                        fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\";
                                        break;
                                    case 3:
                                        fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\";
                                        break;
                                    case 4:
                                        fileLog.Message = $"{newFile.OriginalName} has been successfully copy to {_db.Cabinet.Find(newFile.CabinetId).Name}\\{_db.F1.Find(newFile.F1id).Name}\\{_db.F2.Find(newFile.F2id).Name}\\{_db.F3.Find(newFile.F3id).Name}\\";
                                        break;
                                }
                                await Task.Run(() => FileAuditLogSave(fileLog));
                            }
                        }
                        //move
                        else
                        {
                            if(isFile == false)
                            {
                                id = Convert.ToInt64(_protector.Unprotect(doc));
                                if (type == 1) // f1 folder
                                {
                                    var f1 = _db.F1.Find(id);
                                    if (fType == 1) // move to cabinet
                                    {
                                        f1.CabinetId = (int)destFolderId;
                                        f1.UpdatedBy = User.GetUserId();
                                        f1.UpdatedDate = DateTime.Now;

                                        _db.Entry(f1).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        // f2 folder move to new f1
                                        if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                                        {
                                            var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                            foreach (var f2 in f2List)
                                            {
                                                f2.CabinetId = f1.CabinetId;
                                                f2.UpdatedBy = User.GetUserId();
                                                _db.Entry(f2).State = EntityState.Modified;
                                                _db.SaveChanges();

                                                if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                                {
                                                    var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                                    foreach (var f3 in f3List)
                                                    {
                                                        f3.CabinetId = f2.CabinetId;
                                                        _db.Entry(f3).State = EntityState.Modified;
                                                        _db.SaveChanges();

                                                        // f3 files copy 
                                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                        {
                                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                            foreach (var file in fileList)
                                                            {
                                                                file.CabinetId = f3.CabinetId;
                                                                file.UpdatedBy = User.GetUserId();
                                                                file.UpdatedDate = DateTime.Now;
                                                                _db.Entry(file).State = EntityState.Modified;
                                                                _db.SaveChanges();
                                                            }
                                                        }

                                                    }
                                                }

                                                // f2 files copy 
                                                if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                                    foreach (var file in fileList)
                                                    {
                                                        file.CabinetId = f2.CabinetId;
                                                        file.UpdatedBy = User.GetUserId();
                                                        file.UpdatedDate = DateTime.Now;
                                                        _db.Entry(file).State = EntityState.Modified;
                                                        _db.SaveChanges();
                                                    }
                                                }
                                            }
                                        }

                                        // f1 files move 
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = f1.CabinetId;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }
                                    }
                                    else if (fType == 2) //move to f1 folder
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        var newF2 = new F2
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.Id,
                                            Name = f1.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        var f2List = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).ToList();
                                        foreach (var f2 in f2List)
                                        {
                                            var newF3 = new F3
                                            {
                                                CabinetId = newF2.CabinetId,
                                                F1id = newF2.F1id,
                                                F2id = newF2.Id,
                                                Name = f2.Name,
                                                Status = 1,
                                                CreatedBy = User.GetUserId(),
                                                CreatedDate = DateTime.Now
                                            };
                                            _db.F3.Add(newF3);
                                            _db.SaveChanges();

                                            // f2 files move to new f3
                                            if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                            {
                                                var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                                count = 1;
                                                foreach (var file in fileList)
                                                {
                                                    file.CabinetId = newF3.CabinetId;
                                                    file.F1id = newF3.F1id;
                                                    file.F2id = newF3.F2id;
                                                    file.F3id = newF3.Id;
                                                    file.Type = 4;
                                                    file.ListCount = count;
                                                    file.UpdatedBy = User.GetUserId();
                                                    file.UpdatedDate = DateTime.Now;
                                                    _db.Entry(file).State = EntityState.Modified;
                                                    _db.SaveChanges();
                                                    count++;
                                                }
                                            }
                                            f2.Status = 0;
                                            _db.Entry(f2).State = EntityState.Modified;
                                            _db.SaveChanges();
                                        }

                                        // f1 files move to new f2
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {

                                                file.CabinetId = newF2.CabinetId;
                                                file.F1id = newF2.F1id;
                                                file.F2id = newF2.Id;
                                                file.Type = 3;
                                                file.ListCount = count;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                                count++;
                                            }
                                        }
                                        f1.Status = 0;
                                        _db.Entry(f1).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else if (fType == 3) //move to f2 folder
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        var newF3 = new F3
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.F1id,
                                            F2id = destFolder.Id,
                                            Name = f1.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f2 files move to new f3
                                        if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true && a.Type == 2))
                                        {
                                            var fileList = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true && a.Type == 2).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = newF3.CabinetId;
                                                file.F1id = newF3.F1id;
                                                file.F2id = newF3.F2id;
                                                file.F3id = newF3.Id;
                                                file.Type = 4;
                                                file.ListCount = count;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                                count++;
                                            }
                                        }
                                        f1.Status = 0;
                                        _db.Entry(f1).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                                else if (type == 2) // f2 folder
                                {
                                    var f2 = _db.F2.Find(id);
                                    if(fType == 1) //move to cabinet
                                    {
                                        var newF1 = new F1
                                        {
                                            CabinetId = (int)destFolderId,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F1.Add(newF1);
                                        _db.SaveChanges();

                                        // f3 folder move to new f1
                                        if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                                        {
                                            var f3List = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).ToList();
                                            foreach (var f3 in f3List)
                                            {
                                                var newF2 = new F2
                                                {
                                                    CabinetId = newF1.CabinetId,
                                                    F1id = newF1.Id,
                                                    Name = f3.Name,
                                                    Status = 1,
                                                    CreatedBy = User.GetUserId(),
                                                    CreatedDate = DateTime.Now
                                                };
                                                _db.F2.Add(newF2);
                                                _db.SaveChanges();

                                                // f3 files move 
                                                if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                                {
                                                    var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                                    count = 1;
                                                    foreach (var file in fileList)
                                                    {
                                                        file.CabinetId = newF2.CabinetId;
                                                        file.F1id = newF2.F1id;
                                                        file.F2id = newF2.Id;
                                                        file.Type = 3;
                                                        file.ListCount = count;
                                                        file.UpdatedBy = User.GetUserId();
                                                        file.UpdatedDate = DateTime.Now;
                                                        _db.Entry(file).State = EntityState.Modified;
                                                        _db.SaveChanges();
                                                        count++;
                                                    }
                                                }
                                                f3.Status = 0;
                                                _db.Entry(f3).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }

                                        // f2 files move 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = newF1.CabinetId;
                                                file.F1id = newF1.Id;
                                                file.Type = 2;
                                                file.ListCount = count;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                                count++;
                                            }
                                        }
                                        f2.Status = 0;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else if (fType == 2) // move to f1 folder
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        f2.CabinetId = destFolder.CabinetId;
                                        f2.F1id = destFolder.Id;
                                        f2.UpdatedBy = User.GetUserId();
                                        f2.UpdatedDate = DateTime.Now;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        // f2 files copy 
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            foreach (var file in fileList)
                                            {

                                                file.CabinetId = f2.CabinetId;
                                                file.F1id = f2.F1id;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }

                                    }
                                    else if (fType == 3) // move to f2 folder
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        var newF3 = new F3
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.F1id,
                                            F2id = destFolder.Id,
                                            Name = f2.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F3.Add(newF3);
                                        _db.SaveChanges();

                                        // f2 files copy to new f3
                                        if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true && a.Type == 3))
                                        {
                                            var fileList = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true && a.Type == 3).ToList();
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = newF3.CabinetId;
                                                file.F1id = newF3.F1id;
                                                file.F2id = newF3.F2id;
                                                file.F3id = newF3.Id;
                                                file.Type = 4;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }
                                        f2.Status = 0;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                                else if (type == 3) //f3 folder
                                {
                                    var f3 = _db.F3.Find(id);
                                    if (fType == 1) // move to cabinet
                                    {
                                        var newF1 = new F1
                                        {
                                            CabinetId = (int)destFolderId,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F1.Add(newF1);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            foreach (var file in fileList)
                                            {

                                                file.CabinetId = newF1.CabinetId;
                                                file.F1id = newF1.Id;
                                                file.Type = 2;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }
                                        f3.Status = 0;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else if(fType == 2) // move to f1
                                    {
                                        var destFolder = _db.F1.Find(destFolderId);
                                        var newF2 = new F2
                                        {
                                            CabinetId = destFolder.CabinetId,
                                            F1id = destFolder.Id,
                                            Name = f3.Name,
                                            Status = 1,
                                            CreatedBy = User.GetUserId(),
                                            CreatedDate = DateTime.Now
                                        };
                                        _db.F2.Add(newF2);
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = newF2.CabinetId;
                                                file.F1id = newF2.F1id;
                                                file.F2id = newF2.Id;
                                                file.Type = 3;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }
                                        f3.Status = 0;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else if (fType == 3) // move to f2
                                    {
                                        var destFolder = _db.F2.Find(destFolderId);
                                        f3.CabinetId = destFolder.CabinetId;
                                        f3.F1id = destFolder.F1id;
                                        f3.F2id = destFolder.Id;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();

                                        // f3 files copy 
                                        if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true && a.Type == 4))
                                        {
                                            var fileList = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true && a.Type == 4).ToList();
                                            count = 1;
                                            foreach (var file in fileList)
                                            {
                                                file.CabinetId = f3.CabinetId;
                                                file.F1id = f3.F1id;
                                                file.F2id = f3.F2id;
                                                file.UpdatedBy = User.GetUserId();
                                                file.UpdatedDate = DateTime.Now;
                                                _db.Entry(file).State = EntityState.Modified;
                                                _db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                fileId = Convert.ToInt64(doc);
                                var file = _db.Files.Find(fileId);
                                //**********cabinet************
                                if (fType == 1)
                                {
                                    if (file.CabinetId == destFolderId && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.CabinetId == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.CabinetId == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    file.CabinetId = (int)destFolderId;
                                    file.F1id = null;
                                    file.F2id = null;
                                    file.F3id = null;
                                }
                                //**********Folder 1************
                                else if (fType == 2)
                                {
                                    var f1 = _db.F1.Find(destFolderId);
                                    if (file.CabinetId == f1.CabinetId && file.F1id == f1.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F1id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F1id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    file.CabinetId = f1.CabinetId;
                                    file.F1id = f1.Id;
                                    file.F2id = null;
                                    file.F3id = null;
                                }
                                //**********Folder 2************
                                else if (fType == 3)
                                {
                                    var f2 = _db.F2.Find(destFolderId);
                                    if (file.CabinetId == f2.CabinetId && file.F1id == f2.F1id && file.F2id == f2.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F2id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F2id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    file.CabinetId = f2.CabinetId;
                                    file.F1id = f2.F1id;
                                    file.F2id = f2.Id;
                                    file.F3id = null;
                                }
                                //**********Folder 3************
                                else if (fType == 4)
                                {
                                    var f3 = _db.F3.Find(destFolderId);
                                    if (file.CabinetId == f3.CabinetId && file.F1id == f3.F1id && file.F2id == f3.F2id && file.F3id == f3.Id && file.Type == fType)
                                    {
                                        return Json("same");
                                    }
                                    if (_db.Files.Any(a => a.F3id == destFolderId && a.Type == fType))
                                    {
                                        count = _db.Files.Where(a => a.F3id == destFolderId && a.Type == fType).Max(a => a.ListCount);
                                    }
                                    file.CabinetId = f3.CabinetId;
                                    file.F1id = f3.F1id;
                                    file.F2id = f3.F2id;
                                    file.F3id = f3.Id;
                                }
                                count++;
                                file.Type = fType;
                                file.ListCount = count;
                                file.IsArchive = null;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();

                                fileLog = new FileAuditLogModelView
                                {
                                    FileId = file.Id,
                                    FileName = file.OriginalName,
                                    Operation = "move",
                                    Status = true
                                };
                                switch (fType)
                                {
                                    case 1:
                                        fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\";
                                        break;
                                    case 2:
                                        fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\";
                                        break;
                                    case 3:
                                        fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\";
                                        break;
                                    case 4:
                                        fileLog.Message = $"{file.OriginalName} has been successfully move to {_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\";
                                        break;
                                }
                                await Task.Run(() => FileAuditLogSave(fileLog));

                            }
                        }
                    }
                }
                return Json(new { encryptedId, type });
            }
            catch (Exception)
            {
                if(fileId > 0)
                {
                    fileLog = new FileAuditLogModelView
                    {
                        FileName = "",
                        Operation = isCopy == true ? "copy" : "move",
                        Message = isCopy == true ? "File Copy was failed" : "File move was failed",
                        Status = false
                    };
                    await Task.Run(() => FileAuditLogSave(fileLog));
                }
                return Json("error");
            }
        }
        #endregion

        #region Generic files
        public IActionResult Generic()
        {
            return View();
        }
        public JsonResult GetGenericFiles(int skip, int take, int? type, string selectedId, DateTime? startDate, DateTime? endDate)
        {
            var list = new List<Files>();
            var count = 0;
            if (!string.IsNullOrEmpty(selectedId))
            {
                Files file;
                foreach (var id in selectedId.Split(','))
                {
                    long _id = Convert.ToInt64(id);
                    file = _db.Files.Find(_id);
                    if (file.Id > 0)
                    {
                        list.Add(file);
                    }
                }
                count = list.Count();
                list = list.OrderByDescending(o => o.CreatedDate)
                            .Skip(skip).Take(take)
                            .ToList();
            }
            else if (startDate != null && endDate != null)
            {
                count = _db.Files.Where(a => a.Status == false && a.DeleteType == 1 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date).Count();
                list = _db.Files.Where(a => a.Status == false && a.DeleteType == 1 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date)
                        .OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
            }
            else
            {
                list = _db.Files.Where(a => a.Status == false && a.DeleteType == 1).OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
                count = _db.Files.Where(a => a.Status == false && a.DeleteType == 1).Count();
            }
            var viewList = list.Join(_db.UserInformation, f => f.CreatedBy, u => u.Id, (f, u) => new {
                f, CreatedBy = u.Name
            }).Join(_db.UserInformation, files => files.f.UpdatedBy, u => u.Id, (files, u) => new {
                files.f.Id,
                files.f.OriginalName,
                files.f.IsArchive,
                files.CreatedBy,
                files.f.CreatedDate,
                UpdatedBy = u.Name,
                files.f.UpdatedDate
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public JsonResult GetGenericList(string text, DateTime? startDate, DateTime? endDate)
        {
            var list = new SelectList("");
            if (!string.IsNullOrEmpty(text))
            {
                if (startDate != null && endDate != null)
                {
                    list = new SelectList(_db.Files.Where(a => a.Status == false && a.DeleteType == 1 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date && a.Name.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.OriginalName, a.Id }), "Id", "OriginalName");
                }
                else
                {
                    list = new SelectList(_db.Files.Where(a => a.Status == false && a.DeleteType == 1 && a.OriginalName.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.OriginalName, a.Id }), "Id", "OriginalName");
                }
            }
            return Json(list);
        }
        #endregion

        #region Recycle Bin
        public IActionResult Recycle()
        {
            return View();
        }
        public PartialViewResult RecycleList(bool? isArchive)
        {
            var list = new List<CabinetTileModelView>();

            if(isArchive == true)
            {
                list = _db.Recycle.Where(a => a.CreatedBy == User.GetUserId() && a.IsPermanent == true).Select(s => new CabinetTileModelView
                {
                    Id = s.Id,
                    IsFile = s.IsFile,
                    FolderId = s.IsFile == false ? _protector.Protect(s.OwnerId.ToString()) : null,
                    FileId = s.IsFile == true ? s.OwnerId : 0,
                    FileType = s.Type,
                    FolderType = s.Type,
                    Name = s.Name
                }).ToList();
            }
            else
            {
                list = _db.Recycle.Where(a => a.CreatedBy == User.GetUserId() && a.IsPermanent == false).Select(s => new CabinetTileModelView
                {
                    Id = s.Id,
                    IsFile = s.IsFile,
                    FolderId = s.IsFile == false ? _protector.Protect(s.OwnerId.ToString()) : null,
                    FileId = s.IsFile == true ? s.OwnerId : 0,
                    FileType = s.Type,
                    FolderType = s.Type,
                    Name = s.Name
                }).ToList();
            }
            return PartialView(list);
        }
        public JsonResult RecycleDelete(int resId)
        {
            bool isFile = false;
            try
            {
                var recycle = _db.Recycle.Find(resId);
                recycle.IsPermanent = true;
                _db.Entry(recycle).State = EntityState.Modified;
                _db.SaveChanges();

                if(recycle.IsFile == true)
                {
                    isFile = true;
                    var file = _db.Files.Find(recycle.OwnerId);
                    FileAuditLogModelView fileLog = new FileAuditLogModelView
                    {
                        FileId = file.Id,
                        FileName = file.OriginalName,
                        Operation = "Delete",
                        Status = true,
                        Message = $"{file.OriginalName} has been successfully delete from recycle bin"
                    };
                }
            }
            catch(Exception)
            {
                if(isFile == true)
                {
                    FileAuditLogModelView fileLog = new FileAuditLogModelView
                    {
                        FileName = "",
                        Operation = "Delete",
                        Status = false,
                        Message = "File delete was unsuccessful from recycle bin"
                    };
                }
                return Json("error");
            }
            return Json("success");
        }
        public JsonResult GetRecycleFiles(int skip, int take, int? type, string selectedId, DateTime? startDate, DateTime? endDate)
        {
            var list = new List<Files>();
            var count = 0;
            if (!string.IsNullOrEmpty(selectedId))
            {
                Files file;
                foreach (var id in selectedId.Split(','))
                {
                    long _id = Convert.ToInt64(id);
                    file = _db.Files.Find(_id);
                    if (file.Id > 0)
                    {
                        list.Add(file);
                    }
                }
                count = list.Count();
                list = list.OrderByDescending(o => o.CreatedDate)
                            .Skip(skip).Take(take)
                            .ToList();
            }
            else if (startDate != null && endDate != null)
            {
                count = _db.Files.Where(a => a.Status == false && a.DeleteType == 2 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date).Count();
                list = _db.Files.Where(a => a.Status == false && a.DeleteType == 2 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date)
                        .OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
            }
            else
            {
                list = _db.Files.Where(a => a.Status == false && a.DeleteType == 2).OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
                count = _db.Files.Where(a => a.Status == false && a.DeleteType == 2).Count();
            }
            var viewList = list.Join(_db.UserInformation, f => f.CreatedBy, u => u.Id, (f, u) => new {
                f,
                CreatedBy = u.Name
            }).Join(_db.UserInformation, files => files.f.UpdatedBy, u => u.Id, (files, u) => new {
                files.f.Id,
                files.f.OriginalName,
                files.f.IsArchive,
                files.CreatedBy,
                files.f.CreatedDate,
                UpdatedBy = u.Name,
                files.f.UpdatedDate
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public JsonResult GetRecyleList(string text, DateTime? startDate, DateTime? endDate)
        {
            var list = new SelectList("");
            if (!string.IsNullOrEmpty(text))
            {
                if (startDate != null && endDate != null)
                {
                    list = new SelectList(_db.Files.Where(a => a.Status == false && a.DeleteType == 2 && startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date && a.Name.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.OriginalName, a.Id }), "Id", "OriginalName");
                }
                else
                {
                    list = new SelectList(_db.Files.Where(a => a.Status == false && a.DeleteType == 2 && a.OriginalName.ToLower().Contains(text))
                        .OrderByDescending(o => o.CreatedDate)
                        .Take(50)
                        .Select(a => new { a.OriginalName, a.Id }), "Id", "OriginalName");
                }
            }
            return Json(list);
        }
        #endregion

        #region Cabinet tile
        public IActionResult CabinetTile()
        {
            return View();
        }
        public PartialViewResult CabinetList()
        {
            var list = new List<CabinetModelView>();
            list = _db.Cabinet.Where(a => a.Status == 1).Select(s => new CabinetModelView{
                EncryptedId = _protector.Protect(s.Id.ToString()),
                CabinetName = s.Name,
                CountId = s.Id
            }).ToList();
            return PartialView(list);
        }
        public async Task<IActionResult> CabinetTileDetails(string q)
        {
            int cabinetId = Convert.ToInt32(_protector.Unprotect(q));
            var cabinet = _db.Cabinet.Find(cabinetId);
            CabinetModelView model = new CabinetModelView
            {
                EncryptedId = q,
                CabinetName = cabinet.Name,
                CountId = cabinet.Id
            };
            //frequent folder log save
            await Task.Run(() => FrequentFolderLog(cabinetId, 1));

            return View(model);
        }
        public PartialViewResult CabinetDocList(string cabinetId)
        {
            var list = new List<CabinetTileModelView>();
            int _cabinetId = Convert.ToInt32(_protector.Unprotect(cabinetId));
            //*******************cabinet folder add********************
            if (_db.F1.Any(a => a.CabinetId == _cabinetId && a.Status == 1))
            {
                var f1List = _db.F1.Where(a => a.CabinetId == _cabinetId && a.Status == 1).Select(s => new CabinetTileModelView
                {
                    FolderId = _protector.Protect(s.Id.ToString()),
                    Name = s.Name,
                    IsFile = false,
                    Id = s.Id
                }).ToList();
                if(f1List.Any())
                {
                    list.AddRange(f1List);
                }
            }
            //*******************cabinet files add********************
            if(_db.Files.Any(a => a.CabinetId == _cabinetId && a.Status == true && a.Type == 1))
            {
                var fileList = _db.Files.Where(a => a.CabinetId == _cabinetId && a.Status == true && a.Type == 1).Select(s => new CabinetTileModelView
                {
                    EncryptFildId = _protector.Protect(s.Id.ToString()),
                    FileId = s.Id,
                    Name = s.OriginalName,
                    IsFile = true,
                    FileType = s.FileType
                }).ToList();
                if(fileList.Any())
                {
                    list.AddRange(fileList);
                }
            }
            return PartialView(list.OrderBy(a => a.IsFile).ToList());
        }
        #endregion

        #region F1 Tile
        public PartialViewResult F1DocList(string f1Id)
        {
            var list = new List<CabinetTileModelView>();
            int _f1Id = Convert.ToInt32(_protector.Unprotect(f1Id));
            //*******************f1 folder********************
            if (_db.F2.Any(a => a.F1id == _f1Id && a.Status == 1))
            {
                var f1List = _db.F2.Where(a => a.F1id == _f1Id && a.Status == 1).Select(s => new CabinetTileModelView
                {
                    FolderId = _protector.Protect(s.Id.ToString()),
                    Name = s.Name,
                    IsFile = false,
                    Id = s.Id
                }).ToList();
                if (f1List.Any())
                {
                    list.AddRange(f1List);
                }
            }
            //*******************f1 files********************
            if (_db.Files.Any(a => a.F1id == _f1Id && a.Status == true && a.Type == 2))
            {
                var fileList = _db.Files.Where(a => a.F1id == _f1Id && a.Status == true && a.Type == 2).Select(s => new CabinetTileModelView
                {
                    EncryptFildId = _protector.Protect(s.Id.ToString()),
                    FileId = s.Id,
                    Name = s.OriginalName,
                    IsFile = true,
                    FileType = s.FileType
                }).ToList();
                if (fileList.Any())
                {
                    list.AddRange(fileList);
                }
            }
            return PartialView(list.OrderBy(a => a.IsFile).ToList());
        }
        public async Task<IActionResult> F1TileDetails(string q)
        {
            long f1Id = Convert.ToInt64(_protector.Unprotect(q));
            var f1 = _db.F1.Find(f1Id);
            F1ModelView model = new F1ModelView
            {
                F1Id = q,
                CabinetId = _protector.Protect(f1.CabinetId.ToString()),
                Name = f1.Name,
                CabinetName = _db.Cabinet.FirstOrDefault(a => a.Id == f1.CabinetId).Name,
                C_countId = f1.CabinetId,
                F1_CountId = f1.Id
            };
            //frequent folder log save
            await Task.Run(() => FrequentFolderLog(f1Id, 2));

            return View(model);
        }
        #endregion

        #region F2 Tile
        public PartialViewResult F2DocList(string f2Id)
        {
            var list = new List<CabinetTileModelView>();
            int _f2Id = Convert.ToInt32(_protector.Unprotect(f2Id));
            //*******************f1 folder********************
            if (_db.F3.Any(a => a.F2id == _f2Id && a.Status == 1))
            {
                var f1List = _db.F3.Where(a => a.F2id == _f2Id && a.Status == 1).Select(s => new CabinetTileModelView
                {
                    FolderId = _protector.Protect(s.Id.ToString()),
                    Name = s.Name,
                    IsFile = false,
                    Id = s.Id
                }).ToList();
                if (f1List.Any())
                {
                    list.AddRange(f1List);
                }
            }
            //*******************f1 files********************
            if (_db.Files.Any(a => a.F2id == _f2Id && a.Status == true && a.Type == 3))
            {
                var fileList = _db.Files.Where(a => a.F2id == _f2Id && a.Status == true && a.Type == 3).Select(s => new CabinetTileModelView
                {
                    EncryptFildId = _protector.Protect(s.Id.ToString()),
                    FileId = s.Id,
                    Name = s.OriginalName,
                    IsFile = true,
                    FileType = s.FileType
                }).ToList();
                if (fileList.Any())
                {
                    list.AddRange(fileList);
                }
            }
            return PartialView(list.OrderBy(a => a.IsFile).ToList());
        }
        public async Task<IActionResult> F2TileDetails(string q)
        {
            long f2Id = Convert.ToInt64(_protector.Unprotect(q));
            var f2 = _db.F2.Find(f2Id);
            F2ModelView model = new F2ModelView
            {
                F1Id = _protector.Protect(f2.F1id.ToString()),
                F2Id = q,
                CabinetId = _protector.Protect(f2.CabinetId.ToString()),
                Name = f2.Name,
                CabinetName = _db.Cabinet.FirstOrDefault(a => a.Id == f2.CabinetId).Name,
                F1Name = _db.F1.FirstOrDefault(a => a.Id == f2.F1id).Name,
                C_countId = f2.CabinetId,
                F1_CountId = f2.F1id,
                F2_CountId = f2.Id
            };

            //frequent folder log save
            await Task.Run(() => FrequentFolderLog(f2Id, 3));

            return View(model);
        }
        #endregion

        #region F3 Tile
        public PartialViewResult F3DocList(string f3Id)
        {
            var list = new List<CabinetTileModelView>();
            int _f3Id = Convert.ToInt32(_protector.Unprotect(f3Id));
            //*******************f1 files********************
            if (_db.Files.Any(a => a.F3id == _f3Id && a.Status == true && a.Type == 4))
            {
                list = _db.Files.Where(a => a.F3id == _f3Id && a.Status == true && a.Type == 4).Select(s => new CabinetTileModelView
                {
                    EncryptFildId = _protector.Protect(s.Id.ToString()),
                    FileId = s.Id,
                    Name = s.OriginalName,
                    IsFile = true,
                    FileType = s.FileType
                }).ToList();
            }
            return PartialView(list);
        }
        public async Task<IActionResult> F3TileDetails(string q)
        {
            long f3Id = Convert.ToInt64(_protector.Unprotect(q));
            var f3 = _db.F3.Find(f3Id);
            F3ModelView model = new F3ModelView
            {
                F1Id = _protector.Protect(f3.F1id.ToString()),
                F2Id = _protector.Protect(f3.F2id.ToString()),
                F3Id = q,
                CabinetId = _protector.Protect(f3.CabinetId.ToString()),
                Name = f3.Name,
                CabinetName = _db.Cabinet.FirstOrDefault(a => a.Id == f3.CabinetId).Name,
                F1Name = _db.F1.FirstOrDefault(a => a.Id == f3.F1id).Name,
                F2Name = _db.F2.FirstOrDefault(a => a.Id == f3.F2id).Name,
                C_countId = f3.CabinetId,
                F1_CountId = f3.F1id,
                F2_CountId = f3.F2id,
                F3_CountId = f3.Id
            };

            //frequent folder log save
            await Task.Run(() => FrequentFolderLog(f3Id, 4));

            return View(model);
        }
        #endregion

        public IActionResult DragAndDrop()
        {
            return View();
        }

        #region Tree view
        public IActionResult TreeView()
        {
            return View();
        }
        public JsonResult Cabinets()
        {
            var list = _db.Cabinet.Where(a => a.Status == 1).Select(s => new
            {
                s.Id,
                EncryptedId = _protector.Protect(s.Id.ToString()),
                s.Name,
                Type = 1,
                spriteCssClass = "rootfolder",
                Items = _db.F1.Where(a => a.CabinetId == s.Id && a.Status == 1).Select(f1 => new
                    {
                        f1.Id,
                        EncryptedId = _protector.Protect(f1.Id.ToString()),
                        f1.Name,
                        Type = 2,
                        spriteCssClass = "folder",
                        Items = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).Select(f2 => new {
                            f2.Id,
                            EncryptedId = _protector.Protect(f2.Id.ToString()),
                            f2.Name,
                            Type = 3,
                            spriteCssClass = "folder",
                            Items = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).Select(f3 => new {
                                f3.Id,
                                EncryptedId = _protector.Protect(f3.Id.ToString()),
                                f3.Name,
                                Type = 4,
                                spriteCssClass = "folder"
                            }).ToList()
                        }).ToList()
                }).ToList(),
            }).ToList();
            return Json(list);
        }

        public PartialViewResult CabinetTreeView(long? fileId, string folderId, string documents, bool isCopy, int type, string encryptedId)
        {
            ViewBag.FileId = fileId;
            ViewBag.FolderId = folderId;
            ViewBag.IsCopy = isCopy;
            ViewBag.Type = type;
            ViewBag.EncryptId = encryptedId;
            ViewBag.Documents = documents;
            return PartialView();
        }
        #endregion

        #region Treeview with files
        public IActionResult TreeviewWithFiles()
        {
            return View();
        }
        public JsonResult CabinetWithFiles()
        {
            var list = _db.Cabinet.Where(a => a.Status == 1).Select(s => new HierarchicalDataModel
            {
                Id = s.Id,
                Name = s.Name,
                spriteCssClass = "rootfolder",
                Items = _db.F1.Where(a => a.CabinetId == s.Id && a.Status == 1).Select(f1 => new HierarchicalDataModel
                {
                    Id = f1.Id,
                    Name = f1.Name,
                    spriteCssClass = "folder",
                    Items = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).Select(f2 => new HierarchicalDataModel
                    {
                        Id = f2.Id,
                        Name = f2.Name,
                        spriteCssClass = "folder",
                        Items = _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).Select(f3 => new HierarchicalDataModel
                        {
                            Id = f3.Id,
                            Name = f3.Name,
                            spriteCssClass = "folder",
                            Items = _db.Files.Where(a => a.F3id == f3.Id && a.Type == 4 && a.Status == true).Select(file => new HierarchicalDataModel
                            {
                                Id = file.Id,
                                Name = file.OriginalName,
                                imageUrl = file.FileType == 1 ? "../../images/icon/image-icon.png" : file.FileType == 2 ? "../../images/icon/pdf.ico" : file.FileType == 3 ? "../../images/icon/word-icon.png" : file.FileType == 4 ? "../../images/icon/text-icon.ico" : file.FileType == 5 ? "../../images/icon/powerpoint-icon.png" : "../../images/icon/excel-icon.png"
                            }).ToList()
                        }).ToList(),
                    }).ToList(),
                }).ToList(),
            }).ToList();

            if(list.Any())
            {
                foreach(var c in list)
                {
                    if(c.Items.Any())
                    {
                        foreach (var f1 in c.Items)
                        {

                            if (f1.Items.Any())
                            {
                                foreach (var f2 in f1.Items)
                                {
                                    if (_db.Files.Any(a => a.F2id == f2.Id && a.Type == 3 && a.Status == true))
                                    {
                                        var item = _db.Files.Where(a => a.F2id == f2.Id && a.Type == 3 && a.Status == true).Select(file => new HierarchicalDataModel
                                        {
                                            Id = file.Id,
                                            Name = file.OriginalName,
                                            imageUrl = file.FileType == 1 ? "../../images/icon/image-icon.png" : file.FileType == 2 ? "../../images/icon/pdf.ico" : file.FileType == 3 ? "../../images/icon/word-icon.png" : file.FileType == 4 ? "../../images/icon/text-icon.ico" : file.FileType == 5 ? "../../images/icon/powerpoint-icon.png" : "../../images/icon/excel-icon.png"
                                        });
                                        f2.Items.AddRange(item);
                                    }
                                }
                            }
                            if (_db.Files.Any(a => a.F1id == f1.Id && a.Type == 2 && a.Status == true))
                            {
                                var item = _db.Files.Where(a => a.F1id == f1.Id && a.Type == 2 && a.Status == true).Select(file => new HierarchicalDataModel
                                {
                                    Id = file.Id,
                                    Name = file.OriginalName,
                                    imageUrl = file.FileType == 1 ? "../../images/icon/image-icon.png" : file.FileType == 2 ? "../../images/icon/pdf.ico" : file.FileType == 3 ? "../../images/icon/word-icon.png" : file.FileType == 4 ? "../../images/icon/text-icon.ico" : file.FileType == 5 ? "../../images/icon/powerpoint-icon.png" : "../../images/icon/excel-icon.png"
                                });
                                f1.Items.AddRange(item);
                            }
                        }
                    }
                    if (_db.Files.Any(a => a.CabinetId == c.Id && a.Type == 1 && a.Status == true))
                    {
                        var item = _db.Files.Where(a => a.CabinetId == c.Id && a.Type == 1 && a.Status == true).Select(file => new HierarchicalDataModel
                        {
                            Id = file.Id,
                            Name = file.OriginalName,
                            imageUrl = file.FileType == 1 ? "../../images/icon/image-icon.png" : file.FileType == 2 ? "../../images/icon/pdf.ico" : file.FileType == 3 ? "../../images/icon/word-icon.png" : file.FileType == 4 ? "../../images/icon/text-icon.ico" : file.FileType == 5 ? "../../images/icon/powerpoint-icon.png" : "../../images/icon/excel-icon.png"
                        });
                        c.Items.AddRange(item);
                    }
                }
            }
            return Json(list);
        }
        #endregion

        #region Favourite
        public JsonResult FavouriteSave(long? fileId, string folderId, int? folderType, long? favId)
        {
            try
            {
                //************un-favourite************
                if(favId > 0)
                {
                    var favourite = _db.Favourite.FirstOrDefault(a => a.Id == favId);
                    _db.Entry(favourite).State = EntityState.Deleted;
                }
                //***********file add to favourite*************
                else if(fileId > 0)
                {
                    if (_db.Favourite.Any(a => a.UserId == User.GetUserId() && a.IsFile == true && a.FavouriteId == fileId))
                    {
                        return Json("exist");
                    }
                    var favourite = new Favourite
                    {
                        UserId = User.GetUserId(),
                        FavouriteId = (long)fileId,
                        IsFile = true,
                        Status = true,
                        CreatedDate = DateTime.Now
                    };
                    _db.Favourite.Add(favourite);
                }
                //***********folder add to favourite*************
                else
                {
                    long fId = Convert.ToInt64(_protector.Unprotect(folderId));
                    if (_db.Favourite.Any(a => a.UserId == User.GetUserId() && a.IsFile == false && a.FavouriteId == fId && a.FolderType == folderType))
                    {
                        return Json("exist");
                    }
                    var favourite = new Favourite
                    {
                        UserId = User.GetUserId(),
                        FavouriteId = fId,
                        IsFile = false,
                        Status = true,
                        FolderType = folderType,
                        CreatedDate = DateTime.Now
                    };
                    _db.Favourite.Add(favourite);
                }
                _db.SaveChanges();
                return Json("success");
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
        public IActionResult Favourite()
        {
            return View();
        }
        public PartialViewResult FavouriteList()
        {
            var list = new List<CabinetTileModelView>();
            if(_db.Favourite.Any(a => a.UserId == User.GetUserId() && a.Status == true))
            {
               var favList = _db.Favourite.Where(a => a.Status == true && a.UserId == User.GetUserId()).ToList();
                CabinetTileModelView model;
                foreach (var fav in favList)
                {
                    model = new CabinetTileModelView();
                    if (fav.IsFile == true)
                    {
                        var file = _db.Files.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == true);
                        if(file != null)
                        {
                            model.Id = fav.Id;
                            model.EncryptFildId = _protector.Protect(file.Id.ToString());
                            model.IsFile = true;
                            model.FileId = file.Id;
                            model.Name = file.OriginalName;
                            model.FileType = file.FileType;
                            list.Add(model);
                        }
                    }
                    else
                    {
                        if (fav.FolderType == 1)
                        {
                            var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (cabinet != null)
                            {
                                model.Id = fav.Id;
                                model.IsFile = false;
                                model.FolderId = _protector.Protect(cabinet.Id.ToString());
                                model.FolderType = fav.FolderType;
                                model.Name = cabinet.Name;
                                list.Add(model);
                            }
                        }
                        else if (fav.FolderType == 2)
                        {
                            var folder = _db.F1.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if(folder != null)
                            {
                                model.Id = fav.Id;
                                model.IsFile = false;
                                model.FolderId = _protector.Protect(folder.Id.ToString());
                                model.FolderType = fav.FolderType;
                                model.Name = folder.Name;
                                list.Add(model);
                            }
                        }
                        else if(fav.FolderType == 3)
                        {
                            var folder = _db.F2.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (folder != null)
                            {
                                model.Id = fav.Id;
                                model.IsFile = false;
                                model.FolderId = _protector.Protect(folder.Id.ToString());
                                model.FolderType = fav.FolderType;
                                model.Name = folder.Name;
                                list.Add(model);
                            }
                        }
                        else if(fav.FolderType == 4)
                        {
                            var folder = _db.F3.FirstOrDefault( a => a.Id == fav.FavouriteId && a.Status == 1);
                            if(folder != null)
                            {
                                model.Id = fav.Id;
                                model.IsFile = false;
                                model.FolderId = _protector.Protect(folder.Id.ToString());
                                model.FolderType = fav.FolderType;
                                model.Name = folder.Name;
                                list.Add(model);
                            }
                        }
                    }
                }
            }
            return PartialView(list.OrderBy(a => a.IsFile).ToList());
        }
        public JsonResult GetFavouriteList()
        {
            var list = new List<CabinetTileModelView>();
            if (_db.Favourite.Any(a => a.UserId == User.GetUserId() && a.Status == true))
            {
                var favList = _db.Favourite.Where(a => a.Status == true && a.UserId == User.GetUserId()).ToList();
                CabinetTileModelView model;
                foreach (var fav in favList)
                {
                    model = new CabinetTileModelView();
                    if (fav.IsFile == true)
                    {
                        var file = _db.Files.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == true);
                        if (file != null)
                        {
                            model.Name = file.OriginalName;
                            model.IsFile = true;
                            //list.Add(model);
                        }
                    }
                    else
                    {
                        if(fav.FolderType == 1)
                        {
                            var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (cabinet != null)
                            {
                                model.Name = cabinet.Name;
                                model.IsFile = false;
                                model.FolderType = 1;
                                list.Add(model);
                            }
                        }
                        else if (fav.FolderType == 2)
                        {
                            var folder = _db.F1.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (folder != null)
                            {
                                model.Name = folder.Name;
                                model.IsFile = false;
                                model.FolderType = 2;
                                list.Add(model);
                            }
                        }
                        else if (fav.FolderType == 3)
                        {
                            var folder = _db.F2.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (folder != null)
                            {
                                model.Name = folder.Name;
                                model.IsFile = false;
                                model.FolderType = 3;
                                list.Add(model);
                            }
                        }
                        else if (fav.FolderType == 4)
                        {
                            var folder = _db.F3.FirstOrDefault(a => a.Id == fav.FavouriteId && a.Status == 1);
                            if (folder != null)
                            {
                                model.Name = folder.Name;
                                model.IsFile = false;
                                model.FolderType = 4;
                                list.Add(model);
                            }
                        }
                    }
                }
            }
            return Json(list.OrderBy(a => a.IsFile).Select(s => new { s.Name, Type = s.FolderType }).ToList());
        }

        #endregion

        #region Properties
        public PartialViewResult Properties(long? fileId, string folderId, int? type, bool? isRecycleBin)
        {
            ViewBag.FileId = fileId;
            ViewBag.FolderId = folderId;
            ViewBag.Type = type;
            ViewBag.IsRecycleBin = isRecycleBin;
            return PartialView();
        }
        public PartialViewResult _Properties(long? fileId, string folderId, int? type, bool? isRecycleBin)
        {
            var properties = new PropertiesModelView();
            decimal size = 0;
            if (fileId > 0)
            {
                var file = _db.Files.Find(fileId);
                //************type*******************
                switch (file.FileType)
                {
                    case 1:
                        properties.Type = "Image";
                        break;
                    case 2:
                        properties.Type = "pdf";
                        break;
                    case 3:
                        properties.Type = "Document";
                        break;
                    case 4:
                        properties.Type = "Text Document";
                        break;
                    case 5:
                        properties.Type = "Powerpoint";
                        break;
                    case 6:
                        properties.Type = "Excel";
                        break;
                }
                //**************location********************
                switch (file.Type)
                {
                    case 1:
                        properties.Location = $"{_db.Cabinet.Find(file.CabinetId).Name}\\";
                        break;
                    case 2:
                        properties.Location = $"{_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\";
                        break;
                    case 3:
                        properties.Location = $"{_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\";
                        break;
                    case 4:
                        properties.Location = $"{_db.Cabinet.Find(file.CabinetId).Name}\\{_db.F1.Find(file.F1id).Name}\\{_db.F2.Find(file.F2id).Name}\\{_db.F3.Find(file.F3id).Name}\\";
                        break;
                }
                //**************Size*****************
                //******kb*************
                size = (decimal)file.Size / 1024;
                properties.Size = $"{size.ToString("N2")} KB ({file.Size.ToString("N0")} bytes)";
                //**********Mb*********
                if (size > 1024)
                {
                    size = size / 1024;
                    properties.Size = $"{size.ToString("N2")} MB ({file.Size.ToString("N0")} bytes)";

                    //**********GB*********
                    if (size > 1024)
                    {
                        size = size / 1024;
                        properties.Size = $"{size.ToString("N2")} GB ({file.Size.ToString("N0")} bytes)";

                        //**********TB*********
                        if (size > 1024)
                        {
                            size = size / 1024;
                            properties.Size = $"{size.ToString("N2")} TB ({file.Size.ToString("N0")} bytes)";

                            //**********PB*********
                            if (size > 1024)
                            {
                                size = size / 1024;
                                properties.Size = $"{size.ToString("N2")} PB ({file.Size.ToString("N0")} bytes)";
                            }
                        }
                    }
                }
                properties.CreatedBy = _db.UserInformation.Find(file.CreatedBy).Name;
                properties.CreatedDate = file.CreatedDate;
                properties.IsFile = true;
                properties.Name = file.OriginalName;
            }
            else if (!string.IsNullOrEmpty(folderId))
            {
                int fileCount = 0;
                int folderCount = 0;
                long fileSize = 0;
                long id = Convert.ToInt64(_protector.Unprotect(folderId));
                switch(type)
                {

                    case 1: // cabinet
                        var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == id);
                        properties.Name = cabinet.Name;
                        properties.Type = "Cabinet";
                        properties.Location = $"\\";
                        if(isRecycleBin == true)
                        {
                            if (_db.Files.Any(a => a.CabinetId == cabinet.Id))
                            {
                                fileSize = _db.Files.Where(a => a.CabinetId == cabinet.Id).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.CabinetId == cabinet.Id).Count();
                            }
                            //f1 count
                            if (_db.F1.Any(a => a.CabinetId == cabinet.Id))
                            {
                                folderCount = _db.F1.Where(a => a.CabinetId == cabinet.Id).Count();
                            }
                            //f2 count
                            if (_db.F2.Any(a => a.CabinetId == cabinet.Id))
                            {
                                folderCount = folderCount + _db.F2.Where(a => a.CabinetId == cabinet.Id).Count();
                            }
                            //f3 count
                            if (_db.F3.Any(a => a.CabinetId == cabinet.Id))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.CabinetId == cabinet.Id).Count();
                            }
                        }
                        else
                        {
                            if (_db.Files.Any(a => a.CabinetId == cabinet.Id && a.Status == true))
                            {
                                fileSize = _db.Files.Where(a => a.CabinetId == cabinet.Id && a.Status == true).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.CabinetId == cabinet.Id && a.Status == true).Count();
                            }
                            //f1 count
                            if (_db.F1.Any(a => a.CabinetId == cabinet.Id && a.Status == 1))
                            {
                                folderCount = _db.F1.Where(a => a.CabinetId == cabinet.Id && a.Status == 1).Count();
                            }
                            //f2 count
                            if (_db.F2.Any(a => a.CabinetId == cabinet.Id && a.Status == 1))
                            {
                                folderCount = folderCount + _db.F2.Where(a => a.CabinetId == cabinet.Id && a.Status == 1).Count();
                            }
                            //f3 count
                            if (_db.F3.Any(a => a.CabinetId == cabinet.Id && a.Status == 1))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.CabinetId == cabinet.Id && a.Status == 1).Count();
                            }
                        }
                        properties.CreatedBy = _db.UserInformation.Find(cabinet.CreatedBy).Name;
                        properties.CreatedDate = cabinet.CreatedDate;
                        break;
                    case 2: //f1 folder
                        var f1 = _db.F1.Find(id);
                        properties.Name = f1.Name;
                        properties.Type = "Folder";
                        properties.Location = $"{_db.Cabinet.Find(f1.CabinetId).Name}\\";
                        if(isRecycleBin == true)
                        {
                            if (_db.Files.Any(a => a.F1id == f1.Id))
                            {
                                fileSize = _db.Files.Where(a => a.F1id == f1.Id).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F1id == f1.Id).Count();
                            }
                            if (_db.F2.Any(a => a.F1id == f1.Id))
                            {
                                folderCount = _db.F2.Where(a => a.F1id == f1.Id).Count();
                            }
                            if (_db.F3.Any(a => a.F1id == f1.Id))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.F1id == f1.Id).Count();
                            }
                        }
                        else
                        {
                            if (_db.Files.Any(a => a.F1id == f1.Id && a.Status == true))
                            {
                                fileSize = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F1id == f1.Id && a.Status == true).Count();
                            }
                            if (_db.F2.Any(a => a.F1id == f1.Id && a.Status == 1))
                            {
                                folderCount = _db.F2.Where(a => a.F1id == f1.Id && a.Status == 1).Count();
                            }
                            if (_db.F3.Any(a => a.F1id == f1.Id && a.Status == 1))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.F1id == f1.Id && a.Status == 1).Count();
                            }
                        }
                        properties.CreatedBy = _db.UserInformation.Find(f1.CreatedBy).Name;
                        properties.CreatedDate = f1.CreatedDate;
                        break;
                    case 3: // f2 folder
                        var f2 = _db.F2.Find(id);
                        properties.Name = f2.Name;
                        properties.Type = "Folder";
                        properties.Location = $"{_db.Cabinet.Find(f2.CabinetId).Name}\\{_db.F1.Find(f2.F1id).Name}\\";
                        if(isRecycleBin == true)
                        {
                            if (_db.Files.Any(a => a.F2id == f2.Id))
                            {
                                fileSize = _db.Files.Where(a => a.F2id == f2.Id).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F2id == f2.Id).Count();
                            }
                            if (_db.F3.Any(a => a.F2id == f2.Id))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.F2id == f2.Id).Count();
                            }
                        }
                        else
                        {
                            if (_db.Files.Any(a => a.F2id == f2.Id && a.Status == true))
                            {
                                fileSize = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F2id == f2.Id && a.Status == true).Count();
                            }
                            if (_db.F3.Any(a => a.F2id == f2.Id && a.Status == 1))
                            {
                                folderCount = folderCount + _db.F3.Where(a => a.F2id == f2.Id && a.Status == 1).Count();
                            }
                        }
                        properties.CreatedBy = _db.UserInformation.Find(f2.CreatedBy).Name;
                        properties.CreatedDate = f2.CreatedDate;
                        break;
                    case 4: // f3 folder
                        var f3 = _db.F3.Find(id);
                        properties.Name = f3.Name;
                        properties.Type = "Folder";
                        properties.Location = $"{_db.Cabinet.Find(f3.CabinetId).Name}\\{_db.F1.Find(f3.F1id).Name}\\{_db.F2.Find(f3.F2id).Name}\\";
                        if(isRecycleBin == true)
                        {
                            if (_db.Files.Any(a => a.F3id == f3.Id))
                            {
                                fileSize = _db.Files.Where(a => a.F3id == f3.Id).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F3id == f3.Id).Count();
                            }
                        }
                        else
                        {
                            if (_db.Files.Any(a => a.F3id == f3.Id && a.Status == true))
                            {
                                fileSize = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true).Sum(a => a.Size);
                                fileCount = _db.Files.Where(a => a.F3id == f3.Id && a.Status == true).Count();
                            }
                        }
                        properties.CreatedBy = _db.UserInformation.Find(f3.CreatedBy).Name;
                        properties.CreatedDate = f3.CreatedDate;
                        break;
                }
                //**************Size*****************
                //***************kb*************
                size = (decimal)fileSize / 1024;
                properties.Size = $"{size.ToString("N2")} KB ({fileSize.ToString("N0")} bytes)";
                //**********Mb*********
                if (size > 1024)
                {
                    size = size / 1024;
                    properties.Size = $"{size.ToString("N2")} MB ({fileSize.ToString("N0")} bytes)";

                    //**********GB*********
                    if (size > 1024)
                    {
                        size = size / 1024;
                        properties.Size = $"{size.ToString("N2")} GB ({fileSize.ToString("N0")} bytes)";

                        //**********TB*********
                        if (size > 1024)
                        {
                            size = size / 1024;
                            properties.Size = $"{size.ToString("N2")} TB ({fileSize.ToString("N0")} bytes)";

                            //**********PB*********
                            if (size > 1024)
                            {
                                size = size / 1024;
                                properties.Size = $"{size.ToString("N2")} PB ({fileSize.ToString("N0")} bytes)";
                            }
                        }
                    }
                }
                //**************contains******************
                properties.Contains = $"{fileCount} files, {folderCount} folders";
                properties.IsFile = false;
            }
            return PartialView(properties);
        }
        #endregion

        #region SearchBox
        public JsonResult GetSearchResult(string text)
        {
            var list = new SelectList("");
            var selectListItem = new List<SelectListItem>();
            //************cabinet****************
            if(!string.IsNullOrEmpty(text))
            {
                if (_db.Cabinet.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    var cabinetList = _db.Cabinet.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SelectListItem { Value = $"{s.Id},1", Text = s.Name });
                    selectListItem.AddRange(cabinetList);
                }
                //************f1********************
                if (_db.F1.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    var f1List = _db.F1.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SelectListItem { Value = $"{s.Id},2", Text = s.Name });
                    selectListItem.AddRange(f1List);
                }
                //***************f2****************
                if (_db.F2.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    var f2List = _db.F2.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SelectListItem { Value = $"{s.Id},3", Text = s.Name });
                    selectListItem.AddRange(f2List);
                }
                //***************f3****************
                if (_db.F3.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    var f3List = _db.F3.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SelectListItem { Value = $"{s.Id},4", Text = s.Name });
                    selectListItem.AddRange(f3List);
                }
                //***************files****************
                if (_db.Files.Any(a => a.Status == true && a.OriginalName.ToLower().Contains(text.ToLower())))
                {
                    var files = _db.Files.Where(a => a.Status == true && a.OriginalName.ToLower().Contains(text.ToLower())).ToList().Select(s => new SelectListItem { Value = $"{s.Id},5", Text = s.OriginalName });
                    selectListItem.AddRange(files);
                }
                list = new SelectList(selectListItem.GroupBy(g => g.Text).Select(s => new { Value = s.FirstOrDefault().Text, Text = $"{s.FirstOrDefault().Text} ({s.Count()})"  }).Take(50), "Value", "Text");
            }
            return Json(list);
        }
        public PartialViewResult SearchResultInfo(string text)
        {
            var list = new List<SearchResultModelView>();
            List<SearchResultModelView> result;
            if (!string.IsNullOrEmpty(text))
            {
                if (_db.Cabinet.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    result = _db.Cabinet.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SearchResultModelView
                    {
                        Name = s.Name,
                        Type = 1,
                        IsFile = false,
                        Location = "\\",
                    }).ToList();
                    list.AddRange(result);
                }
                //************f1********************
                if (_db.F1.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    result = _db.F1.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SearchResultModelView
                    {
                        Name = s.Name,
                        ProtectedId = _protector.Protect(s.CabinetId.ToString()),
                        Type = 2,
                        IsFile = false,
                        Location = $"{_db.Cabinet.Find(s.CabinetId).Name}\\"
                    }).ToList();
                    list.AddRange(result);
                }
                //***************f2****************
                if (_db.F2.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    result = _db.F2.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SearchResultModelView
                    {
                        Name = s.Name,
                        ProtectedId = _protector.Protect(s.F1id.ToString()),
                        Type = 3,
                        IsFile = false,
                        Location = $"{_db.Cabinet.Find(s.CabinetId).Name}\\{_db.F1.Find(s.F1id).Name}\\"
                    }).ToList();
                    list.AddRange(result);
                }
                //***************f3****************
                if (_db.F3.Any(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())))
                {
                    result = _db.F3.Where(a => a.Status == 1 && a.Name.ToLower().Contains(text.ToLower())).ToList().Select(s => new SearchResultModelView
                    {
                        Name = s.Name,
                        ProtectedId = _protector.Protect(s.F2id.ToString()),
                        Type = 4,
                        IsFile = false,
                        Location = $"{_db.Cabinet.Find(s.CabinetId).Name}\\{_db.F1.Find(s.F1id).Name}\\{_db.F2.Find(s.F2id).Name}\\"
                    }).ToList();
                    list.AddRange(result);
                }
                //***************files****************
                if (_db.Files.Any(a => a.Status == true && a.OriginalName.ToLower().Contains(text.ToLower())))
                {
                    result = _db.Files.Where(a => a.Status == true && a.OriginalName.ToLower().Contains(text.ToLower())).ToList().Select(s => new SearchResultModelView
                    {
                        Name = s.OriginalName,
                        ProtectedId = s.Type == 1 ? _protector.Protect(s.CabinetId.ToString()) : s.Type == 2 ? _protector.Protect(s.F1id.ToString()) : s.Type == 3 ? _protector.Protect(s.F2id.ToString()) : _protector.Protect(s.F3id.ToString()),
                        Type = s.Type,
                        FileType = s.FileType,
                        IsFile = true,
                        Location = s.Type == 1 ? $"{_db.Cabinet.Find(s.CabinetId).Name}\\" : s.Type == 2 ? $"{_db.Cabinet.Find(s.CabinetId).Name}\\{_db.F1.Find(s.F1id).Name}\\" : s.Type == 3 ? $"{_db.Cabinet.Find(s.CabinetId).Name}\\{_db.F1.Find(s.F1id).Name}\\{_db.F2.Find(s.F2id).Name}\\" : $"{_db.Cabinet.Find(s.CabinetId).Name}\\{_db.F1.Find(s.F1id).Name}\\{_db.F2.Find(s.F2id).Name}\\{_db.F3.Find(s.F3id).Name}\\"
                    }).ToList();
                    list.AddRange(result);
                }
            }
            return PartialView(list);
        }
        #endregion

        #region Document Delete
        public async Task<JsonResult> DocumentDelete(string pId, string pIds, long? fileId, int type, bool isPermanent)
        {
            FileAuditLogModelView fileLog;
            try
            {
                // pid = folder/cabinet protected id
                // pIds = folder/cabinet/files id
                // ispermanent = true => this is archive.
                Recycle recycle;
                long id = 0;
                if (!string.IsNullOrEmpty(pId))
                {
                    recycle = new Recycle();
                    id = Convert.ToInt64(_protector.Unprotect(pId));
                    if (type == 1) // cabinet delete
                    {
                        var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == id);
                        cabinet.Status = 0;
                        cabinet.UpdatedBy = User.GetUserId();
                        cabinet.UpdatedDate = DateTime.Now;
                        _db.Entry(cabinet).State = EntityState.Modified;
                        _db.SaveChanges();

                        recycle.Name = cabinet.Name;

                        //f1 folder deactive
                        if (_db.F1.Any(a => a.CabinetId == id && a.Status == 1))
                        {
                            var f1List = _db.F1.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                            foreach (var f1 in f1List)
                            {
                                f1.Status = 0;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //f2 deactive
                        if (_db.F2.Any(a => a.CabinetId == id && a.Status == 1))
                        {
                            var f2List = _db.F2.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                            foreach (var f2 in f2List)
                            {
                                f2.Status = 0;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //f3 deactive
                        if (_db.F3.Any(a => a.CabinetId == id && a.Status == 1))
                        {
                            var f3List = _db.F3.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 0;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //cabinet files deactive
                        if (_db.Files.Any(a => a.CabinetId == id && a.Status == true))
                        {
                            var files = _db.Files.Where(a => a.CabinetId == id && a.Status == true).ToList();
                            foreach (var file in files)
                            {
                                file.Status = false;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 2) // f1 delete
                    {
                        var f1 = _db.F1.Find(id);
                        f1.Status = 0;
                        f1.UpdatedBy = User.GetUserId();
                        f1.UpdatedDate = DateTime.Now;
                        _db.Entry(f1).State = EntityState.Modified;
                        _db.SaveChanges();

                        recycle.Name = f1.Name;

                        //folder deactive
                        if (_db.F2.Any(a => a.F1id == id && a.Status == 1))
                        {
                            var f2List = _db.F2.Where(a => a.F1id == id && a.Status == 1).ToList();
                            foreach (var f2 in f2List)
                            {
                                f2.Status = 0;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //sub folder deactive
                        if (_db.F3.Any(a => a.F1id == id && a.Status == 1))
                        {
                            var f3List = _db.F3.Where(a => a.F1id == id && a.Status == 1).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 0;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //f1 files deactive
                        if (_db.Files.Any(a => a.F1id == id && a.Status == true))
                        {
                            var files = _db.Files.Where(a => a.F1id == id && a.Status == true).ToList();
                            foreach (var file in files)
                            {
                                file.Status = false;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 3) // f2 delete
                    {
                        var f2 = _db.F2.Find(id);
                        recycle.Name = f2.Name;
                        f2.Status = 0;
                        f2.UpdatedBy = User.GetUserId();
                        f2.UpdatedDate = DateTime.Now;
                        _db.Entry(f2).State = EntityState.Modified;
                        _db.SaveChanges();

                        //sub folder deactive
                        if (_db.F3.Any(a => a.F2id == id && a.Status == 1))
                        {
                            var f3List = _db.F3.Where(a => a.F2id == id && a.Status == 1).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 0;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //f2 files deactive
                        if (_db.Files.Any(a => a.F2id == id && a.Status == true))
                        {
                            var files = _db.Files.Where(a => a.F2id == id && a.Status == true).ToList();
                            foreach (var file in files)
                            {
                                file.Status = false;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 4)
                    {
                        var f3 = _db.F3.Find(id);
                        recycle.Name = f3.Name;
                        f3.Status = 0;
                        f3.UpdatedBy = User.GetUserId();
                        f3.UpdatedDate = DateTime.Now;
                        _db.Entry(f3).State = EntityState.Modified;
                        _db.SaveChanges();

                        //f3 files deactive
                        if (_db.Files.Any(a => a.F3id == id && a.Status == true))
                        {
                            var files = _db.Files.Where(a => a.F3id == id && a.Status == true).ToList();
                            foreach (var file in files)
                            {
                                file.Status = false;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return Json("error");
                    }
                    recycle.OwnerId = id;
                    recycle.Type = type;
                    recycle.IsFile = false;
                    recycle.IsPermanent = isPermanent;
                    recycle.CreatedBy = User.GetUserId();
                    recycle.CreatedDate = DateTime.Now;
                    _db.Recycle.Add(recycle);
                    _db.SaveChanges();
                }
                else if(fileId > 0)
                {
                    recycle = new Recycle();
                    var file = _db.Files.Find(fileId);
                    file.Status = false;
                    file.UpdatedBy = User.GetUserId();
                    file.UpdatedDate = DateTime.Now;
                    _db.Entry(file).State = EntityState.Modified;
                    _db.SaveChanges();

                    fileLog = new FileAuditLogModelView
                    {
                        FileId = file.Id,
                        FileName = file.OriginalName,
                        Operation = "Delete",
                        Status = true,
                        Message = $"{file.OriginalName} has been successfully deleted"
                    };
                    await Task.Run(() => FileAuditLogSave(fileLog));

                    recycle.Name = file.OriginalName;
                    recycle.OwnerId = (long)fileId;
                    recycle.Type = file.FileType;
                    recycle.IsFile = true;
                    recycle.IsPermanent = isPermanent;
                    recycle.CreatedBy = User.GetUserId();
                    recycle.CreatedDate = DateTime.Now;
                    _db.Recycle.Add(recycle);
                    _db.SaveChanges();
                }
                else if(!string.IsNullOrEmpty(pIds))
                {
                    bool isFile = false;
                    foreach (var sId in pIds.Split(","))
                    {
                        isFile = long.TryParse(sId, out long number);
                        recycle = new Recycle();
                        if (isFile == true)
                        {
                            fileId = Convert.ToInt64(sId);
                            var file = _db.Files.Find(fileId);
                            file.Status = false;
                            file.UpdatedBy = User.GetUserId();
                            file.UpdatedDate = DateTime.Now;
                            _db.Entry(file).State = EntityState.Modified;
                            _db.SaveChanges();

                            fileLog = new FileAuditLogModelView
                            {
                                FileId = file.Id,
                                FileName = file.OriginalName,
                                Operation = "Delete",
                                Status = true,
                                Message = $"{file.OriginalName} has been successfully deleted"
                            };
                            await Task.Run(() => FileAuditLogSave(fileLog));

                            recycle.Name = file.OriginalName;
                            recycle.OwnerId = (long)fileId;
                            recycle.Type = file.FileType;
                            recycle.IsFile = true;
                            recycle.IsPermanent = isPermanent;
                            recycle.CreatedBy = User.GetUserId();
                            recycle.CreatedDate = DateTime.Now;
                            _db.Recycle.Add(recycle);
                            _db.SaveChanges();
                        }
                        else
                        {
                            id = Convert.ToInt64(_protector.Unprotect(sId));
                            if (type == 1) // cabinet delete
                            {
                                var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == id);
                                cabinet.Status = 0;
                                cabinet.UpdatedBy = User.GetUserId();
                                cabinet.UpdatedDate = DateTime.Now;
                                _db.Entry(cabinet).State = EntityState.Modified;
                                _db.SaveChanges();

                                recycle.Name = cabinet.Name;

                                //f1 folder deactive
                                if (_db.F1.Any(a => a.CabinetId == id && a.Status == 1))
                                {
                                    var f1List = _db.F1.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                                    foreach (var f1 in f1List)
                                    {
                                        f1.Status = 0;
                                        f1.UpdatedBy = User.GetUserId();
                                        f1.UpdatedDate = DateTime.Now;
                                        _db.Entry(f1).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //f2 deactive
                                if (_db.F2.Any(a => a.CabinetId == id && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                                    foreach (var f2 in f2List)
                                    {
                                        f2.Status = 0;
                                        f2.UpdatedBy = User.GetUserId();
                                        f2.UpdatedDate = DateTime.Now;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //f3 deactive
                                if (_db.F3.Any(a => a.CabinetId == id && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.CabinetId == id && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        f3.Status = 0;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //cabinet files deactive
                                if (_db.Files.Any(a => a.CabinetId == id && a.Status == true))
                                {
                                    var files = _db.Files.Where(a => a.CabinetId == id && a.Status == true).ToList();
                                    foreach (var file in files)
                                    {
                                        file.Status = false;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                            else if (type == 2) // f1 delete
                            {
                                var f1 = _db.F1.Find(id);
                                f1.Status = 0;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();

                                recycle.Name = f1.Name;

                                //folder deactive
                                if (_db.F2.Any(a => a.F1id == id && a.Status == 1))
                                {
                                    var f2List = _db.F2.Where(a => a.F1id == id && a.Status == 1).ToList();
                                    foreach (var f2 in f2List)
                                    {
                                        f2.Status = 0;
                                        f2.UpdatedBy = User.GetUserId();
                                        f2.UpdatedDate = DateTime.Now;
                                        _db.Entry(f2).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //sub folder deactive
                                if (_db.F3.Any(a => a.F1id == id && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F1id == id && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        f3.Status = 0;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //f1 files deactive
                                if (_db.Files.Any(a => a.F1id == id && a.Status == true))
                                {
                                    var files = _db.Files.Where(a => a.F1id == id && a.Status == true).ToList();
                                    foreach (var file in files)
                                    {
                                        file.Status = false;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                            else if (type == 3) // f2 delete
                            {
                                var f2 = _db.F2.Find(id);
                                recycle.Name = f2.Name;
                                f2.Status = 0;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();

                                //sub folder deactive
                                if (_db.F3.Any(a => a.F2id == id && a.Status == 1))
                                {
                                    var f3List = _db.F3.Where(a => a.F2id == id && a.Status == 1).ToList();
                                    foreach (var f3 in f3List)
                                    {
                                        f3.Status = 0;
                                        f3.UpdatedBy = User.GetUserId();
                                        f3.UpdatedDate = DateTime.Now;
                                        _db.Entry(f3).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }

                                //f2 files deactive
                                if (_db.Files.Any(a => a.F2id == id && a.Status == true))
                                {
                                    var files = _db.Files.Where(a => a.F2id == id && a.Status == true).ToList();
                                    foreach (var file in files)
                                    {
                                        file.Status = false;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                            else if (type == 4)
                            {
                                var f3 = _db.F3.Find(id);
                                recycle.Name = f3.Name;
                                f3.Status = 0;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();

                                //f3 files deactive
                                if (_db.Files.Any(a => a.F3id == id && a.Status == true))
                                {
                                    var files = _db.Files.Where(a => a.F3id == id && a.Status == true).ToList();
                                    foreach (var file in files)
                                    {
                                        file.Status = false;
                                        file.UpdatedBy = User.GetUserId();
                                        file.UpdatedDate = DateTime.Now;
                                        _db.Entry(file).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                return Json("error");
                            }
                            recycle.OwnerId = id;
                            recycle.Type = type;
                            recycle.IsFile = false;
                            recycle.IsPermanent = isPermanent;
                            recycle.CreatedBy = User.GetUserId();
                            recycle.CreatedDate = DateTime.Now;
                            _db.Recycle.Add(recycle);
                            _db.SaveChanges();
                        }
                    }
                }
                else
                {
                    return Json("error");
                }
            }
            catch(Exception)
            {
                if(fileId > 0)
                {
                    fileLog = new FileAuditLogModelView
                    {
                        FileName = "",
                        Operation = "Delete",
                        Status = false,
                        Message = "File delete was unsuccessful"
                    };
                    await Task.Run(() => FileAuditLogSave(fileLog));
                }
                return Json("error");
            }
            return Json("success");
        }
        #endregion

        #region Document Restore
        public async Task<JsonResult> DocumentRestore(int resId)
        {
            bool isFile = false;
            try
            {
                var recycle = _db.Recycle.Find(resId);
                if(recycle.IsFile == true)
                {
                    isFile = true;
                    var file = _db.Files.Find(recycle.OwnerId);
                    file.Status = true;
                    file.UpdatedBy = User.GetUserId();
                    file.UpdatedDate = DateTime.Now;
                    _db.Entry(file).State = EntityState.Modified;
                    _db.SaveChanges();

                    FileAuditLogModelView fileLog = new FileAuditLogModelView
                    {
                        FileId = file.Id,
                        FileName = file.OriginalName,
                        Operation = "Restore",
                        Status = true,
                        Message = $"{file.OriginalName} has been successfully restored"
                    };
                    await Task.Run(() => FileAuditLogSave(fileLog));

                    switch (file.Type)
                    {
                        case 1:
                            //check cabinet is active
                            if(_db.Cabinet.Find(file.CabinetId).Status == 0)
                            {
                                var cabinet = _db.Cabinet.Find(file.CabinetId);
                                cabinet.Status = 1;
                                cabinet.UpdatedBy = User.GetUserId();
                                cabinet.UpdatedDate = DateTime.Now;
                                _db.Entry(cabinet).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            break;
                        case 2:
                            //check cabinet is active
                            if (_db.Cabinet.Find(file.CabinetId).Status == 0)
                            {
                                var cabinet = _db.Cabinet.Find(file.CabinetId);
                                cabinet.Status = 1;
                                cabinet.UpdatedBy = User.GetUserId();
                                cabinet.UpdatedDate = DateTime.Now;
                                _db.Entry(cabinet).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f1 is active
                            if (_db.F1.Find(file.F1id).Status == 0)
                            {
                                var f1 = _db.F1.Find(file.F1id);
                                f1.Status = 1;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            break;
                        case 3:
                            //check cabinet is active
                            if (_db.Cabinet.Find(file.CabinetId).Status == 0)
                            {
                                var cabinet = _db.Cabinet.Find(file.CabinetId);
                                cabinet.Status = 1;
                                cabinet.UpdatedBy = User.GetUserId();
                                cabinet.UpdatedDate = DateTime.Now;
                                _db.Entry(cabinet).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f1 is active
                            if (_db.F1.Find(file.F1id).Status == 0)
                            {
                                var f1 = _db.F1.Find(file.F1id);
                                f1.Status = 1;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f2 is active
                            if (_db.F2.Find(file.F2id).Status == 0)
                            {
                                var f2 = _db.F2.Find(file.F2id);
                                f2.Status = 1;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            break;
                        case 4:
                            //check cabinet is active
                            if (_db.Cabinet.Find(file.CabinetId).Status == 0)
                            {
                                var cabinet = _db.Cabinet.Find(file.CabinetId);
                                cabinet.Status = 1;
                                cabinet.UpdatedBy = User.GetUserId();
                                cabinet.UpdatedDate = DateTime.Now;
                                _db.Entry(cabinet).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f1 is active
                            if (_db.F1.Find(file.F1id).Status == 0)
                            {
                                var f1 = _db.F1.Find(file.F1id);
                                f1.Status = 1;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f2 is active
                            if (_db.F2.Find(file.F2id).Status == 0)
                            {
                                var f2 = _db.F2.Find(file.F2id);
                                f2.Status = 1;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            //check f3 is active
                            if (_db.F3.Find(file.F3id).Status == 0)
                            {
                                var f3 = _db.F3.Find(file.F3id);
                                f3.Status = 1;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                            break;
                    }
                }
                else
                {
                    long id = recycle.OwnerId;
                    int type = recycle.Type;
                    if (type == 1) // cabinet active
                    {
                        var cabinet = _db.Cabinet.FirstOrDefault(a => a.Id == id);
                        if(cabinet.Status == 0)
                        {
                            cabinet.Status = 1;
                            cabinet.UpdatedBy = User.GetUserId();
                            cabinet.UpdatedDate = DateTime.Now;
                            _db.Entry(cabinet).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        //f1 folder active
                        if (_db.F1.Any(a => a.CabinetId == id && a.Status == 0))
                        {
                            var f1List = _db.F1.Where(a => a.CabinetId == id && a.Status == 0).ToList();
                            foreach (var f1 in f1List)
                            {
                                f1.Status = 1;
                                f1.UpdatedBy = User.GetUserId();
                                f1.UpdatedDate = DateTime.Now;
                                _db.Entry(f1).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                        //f2 active
                        if (_db.F2.Any(a => a.CabinetId == id && a.Status == 0))
                        {
                            var f2List = _db.F2.Where(a => a.CabinetId == id && a.Status == 0).ToList();
                            foreach (var f2 in f2List)
                            {
                                f2.Status = 1;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                        //f3 active
                        if (_db.F3.Any(a => a.CabinetId == id && a.Status == 0))
                        {
                            var f3List = _db.F3.Where(a => a.CabinetId == id && a.Status == 0).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 1;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }

                        //cabinet files active
                        if (_db.Files.Any(a => a.CabinetId == id && a.Status == false))
                        {
                            var files = _db.Files.Where(a => a.CabinetId == id && a.Status == false).ToList();
                            foreach (var file in files)
                            {
                                file.Status = true;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 2) // f1 delete
                    {
                        var f1 = _db.F1.Find(id);
                        if(f1.Status == 0)
                        {
                            f1.Status = 1;
                            f1.UpdatedBy = User.GetUserId();
                            f1.UpdatedDate = DateTime.Now;
                            _db.Entry(f1).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        //folder active
                        if (_db.F2.Any(a => a.F1id == id && a.Status == 0))
                        {
                            var f2List = _db.F2.Where(a => a.F1id == id && a.Status == 0).ToList();
                            foreach (var f2 in f2List)
                            {
                                f2.Status = 1;
                                f2.UpdatedBy = User.GetUserId();
                                f2.UpdatedDate = DateTime.Now;
                                _db.Entry(f2).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                        //sub folder active
                        if (_db.F3.Any(a => a.F1id == id && a.Status == 0))
                        {
                            var f3List = _db.F3.Where(a => a.F1id == id && a.Status == 0).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 1;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                        //f1 files active
                        if (_db.Files.Any(a => a.F1id == id && a.Status == false))
                        {
                            var files = _db.Files.Where(a => a.F1id == id && a.Status == false).ToList();
                            foreach (var file in files)
                            {
                                file.Status = true;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 3) // f2 active
                    {
                        var f2 = _db.F2.Find(id);
                        if(f2.Status == 0)
                        {
                            f2.Status = 1;
                            f2.UpdatedBy = User.GetUserId();
                            f2.UpdatedDate = DateTime.Now;
                            _db.Entry(f2).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        //sub folder active
                        if (_db.F3.Any(a => a.F2id == id && a.Status == 0))
                        {
                            var f3List = _db.F3.Where(a => a.F2id == id && a.Status == 0).ToList();
                            foreach (var f3 in f3List)
                            {
                                f3.Status = 1;
                                f3.UpdatedBy = User.GetUserId();
                                f3.UpdatedDate = DateTime.Now;
                                _db.Entry(f3).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                        //f2 files active
                        if (_db.Files.Any(a => a.F2id == id && a.Status == false))
                        {
                            var files = _db.Files.Where(a => a.F2id == id && a.Status == false).ToList();
                            foreach (var file in files)
                            {
                                file.Status = true;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else if (type == 4)
                    {
                        var f3 = _db.F3.Find(id);
                        if(f3.Status == 0)
                        {
                            f3.Status = 1;
                            f3.UpdatedBy = User.GetUserId();
                            f3.UpdatedDate = DateTime.Now;
                            _db.Entry(f3).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        //f3 files active
                        if (_db.Files.Any(a => a.F3id == id && a.Status == false))
                        {
                            var files = _db.Files.Where(a => a.F3id == id && a.Status == false).ToList();
                            foreach (var file in files)
                            {
                                file.Status = true;
                                file.UpdatedBy = User.GetUserId();
                                file.UpdatedDate = DateTime.Now;
                                _db.Entry(file).State = EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return Json("error");
                    }
                }
                _db.Entry(recycle).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch(Exception)
            {
                if(isFile == true)
                {
                    FileAuditLogModelView fileLog = new FileAuditLogModelView
                    {
                        FileName = "",
                        Operation = "Restore",
                        Status = false,
                        Message = $"File restore was unsuccessful"
                    };
                    await Task.Run(() => FileAuditLogSave(fileLog));
                }
                return Json("error");
            }
            return Json("success");
        }
        #endregion

        #region Archive
        public IActionResult Archive()
        {
            return View();
        }
        #endregion

        #region Files History
        public IActionResult FileHistory()
        {
            return View();
        }
        public JsonResult GetFileHistory(int skip, int take, int? type, string selectedId, DateTime? startDate, DateTime? endDate)
        {
            var list = new List<FileAuditLog>();
            var count = 0;
            if (startDate != null && endDate != null)
            {
                count = _db.FileAuditLog.Where(a => startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date).Count();
                list = _db.FileAuditLog.Where(a => startDate.Value.Date <= a.CreatedDate.Date && endDate.Value.Date >= a.CreatedDate.Date)
                        .OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
            }
            else
            {
                list = _db.FileAuditLog.OrderByDescending(o => o.CreatedDate)
                        .Skip(skip).Take(take)
                        .ToList();
                count = _db.FileAuditLog.Count();
            }
            var viewList = list.Join(_db.UserInformation, h => h.CreatedBy, u => u.Id, (h, u) => new {
                h.FileName,
                h.Message,
                h.Operation,
                Status = h.Status == true ? "Success" : "Error",
                CreatedBy = u.Name,
                h.CreatedDate
            }).ToList();
            return Json(new { total = count, data = viewList });
        }
        public PartialViewResult FileHistoryPartial(long id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public PartialViewResult FileHistoryList(long id)
        {
            var list = new List<FileAuditLogModelView>();
            if(id > 0)
            {
                ViewBag.Name = _db.Files.Find(id).OriginalName;
                list = _db.FileAuditLog.Where(a => a.FileId == id).Join(_db.UserInformation, h => h.CreatedBy, u => u.Id, (h, u) => new FileAuditLogModelView
                {
                    Message = h.Message,
                    Operation = h.Operation,
                    Status = h.Status,
                    CreatedBy = u.Name,
                    CreatedDate = h.CreatedDate,
                }).ToList();
            }
            return PartialView(list);
        }
        #endregion

        #region Storage Info
        [NonAction]
        public void StorageUpdate(long bytes)
        {
            var storage = _db.Storage.FirstOrDefault(a => a.UserId == User.GetUserId());
            if(storage != null)
            {
                storage.Used = storage.Used + bytes;
                _db.Entry(storage).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public JsonResult GetStorageInfo()
        {
            try
            {
                string storageInfo = string.Empty;
                decimal percentUsed = 0;
                var storage = _db.Storage.FirstOrDefault(a => a.UserId == User.GetUserId());
                storageInfo = $"{FormatBytes(storage.Used)} of {FormatBytes(storage.Allowed)} used";
                percentUsed = (decimal) storage.Used / storage.Allowed;
                percentUsed = percentUsed * 100;
                return Json(new { percentUsed, storageInfo, used = FormatBytes(storage.Used), allocated = FormatBytes(storage.Allowed), available = FormatBytes(storage.Allowed - storage.Used)});
            }
            catch(Exception)
            {
                return Json("Error");
            }
        }
        public PartialViewResult StorageInfo()
        {
            return PartialView();
        }
        public PartialViewResult _StorageInfo()
        {
            long documents = _db.Files.Where(a => a.Status == true && a.CreatedBy == User.GetUserId() && a.FileType > 1).Sum(s => s.Size);
            long image = _db.Files.Where(a => a.Status == true && a.CreatedBy == User.GetUserId() && a.FileType == 1).Sum(s => s.Size);
            var storage = _db.Storage.FirstOrDefault(a => a.UserId == User.GetUserId());
            StorageModelView model = new StorageModelView
            {
                Allocated = storage != null ? FormatBytes(storage.Allowed) : "0MB",
                Used = storage != null ? FormatBytes(storage.Used) : "0MB",
                Available = storage != null ? FormatBytes(storage.Allowed - storage.Used) : "0MB",
                Documents = FormatBytes(documents),
                Image = FormatBytes(image),
                Audio = "0MB",
                Video = "0MB",
                Files = "0MB"
            };
            return PartialView(model);
        }
        #endregion

        #region Bytes convert
        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }
            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }
        #endregion

        #region Spreadsheet editor
        public IActionResult SpreadSheetEditor()
        {
            return View();
        }
        public JsonResult GetSpreadsheetFile(long fileId)
        {
            var file = _db.Files.Find(fileId);
            var path = $"{_env.WebRootPath}\\Files\\";
            string fullPath = Path.Combine($"{_env.WebRootPath}\\Files\\", file.Name);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            FileContentResult robj;
            var bytesdata = File(fileBytes.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "myFileName.xlsx");
            robj = bytesdata;
            ViewBag.FileContent = robj.FileContents;
            return Json(robj);
        }
        #endregion

        #region Document Editor
        public IActionResult DocEditor()
        {
            return View();
        }
        #endregion

        #region Frequent folder log
        [NonAction]
        public void FrequentFolderLog(long folderId, int type)
        {
            try
            {
                FrequentFolder fFolder;
                if(_db.FrequentFolder.Any(a => a.Type == type && a.FolderId == folderId))
                {
                    fFolder = _db.FrequentFolder.FirstOrDefault(a => a.Type == type && a.FolderId == folderId);
                    fFolder.Count = fFolder.Count + 1;
                    _db.Entry(fFolder).State = EntityState.Modified;
                }
                else
                {
                    fFolder = new FrequentFolder
                    {
                        FolderId = folderId,
                        Type = type,
                        Count = 1
                    };
                    _db.FrequentFolder.Add(fFolder);
                }
                _db.SaveChanges();
            }
            catch(Exception)
            {

            }
        }
        public PartialViewResult FrequentFolderList()
        {
            var list = new List<CabinetTileModelView>();
            list = _db.FrequentFolder.OrderByDescending(o => o.IsPined).ThenByDescending(o => o.Count).Take(10).Select(s => new CabinetTileModelView {
                Id = s.Id,
                IsPined = s.IsPined,
                FolderId = _protector.Protect(s.FolderId.ToString()),
                FolderType = s.Type,
                Name = s.Type == 1 ? _db.Cabinet.FirstOrDefault(a => a.Id == s.FolderId).Name : s.Type == 2 ? _db.F1.FirstOrDefault(f1 => f1.Id == s.FolderId).Name : s.Type == 3 ? _db.F2.FirstOrDefault(f2 => f2.Id == s.FolderId).Name : s.Type == 4 ? _db.F3.FirstOrDefault(f3 => f3.Id == s.FolderId).Name : "",
                Status = s.Type == 1 ? _db.Cabinet.FirstOrDefault(a => a.Id == s.FolderId).Status : s.Type == 2 ? _db.F1.FirstOrDefault(f1 => f1.Id == s.FolderId).Status : s.Type == 3 ? _db.F2.FirstOrDefault(f2 => f2.Id == s.FolderId).Status : s.Type == 4 ? _db.F3.FirstOrDefault(f3 => f3.Id == s.FolderId).Status : 0
            }).ToList();
            return PartialView(list.Where(a => a.Status == 1).ToList());
        }
        public JsonResult FrequentFolderDelete(long id)
        {
            try
            {
                var folder = _db.FrequentFolder.Find(id);
                _db.Entry(folder).State = EntityState.Deleted;
                _db.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("error");
            }
        }
        public JsonResult PinFolderSave(long freqFolderId)
        {
            try
            { 
                //frequent folder isPined status change
                var freqFolder = _db.FrequentFolder.Find(freqFolderId);
                freqFolder.IsPined = freqFolder.IsPined == true ? false : true;
                _db.Entry(freqFolder).State = EntityState.Modified;
                _db.SaveChanges();
                return Json("success");
            }
            catch(Exception)
            {
                return Json("error");
            }
        }
        #endregion

        #region Recent file log
        [NonAction]
        public void RecentFileLog(long fileId)
        {
            //this method works only file view/download.
            try
            {
                RecentFile rFile;
                //recent file max 10
                if (_db.RecentFile.Count() == 10)
                {
                    if(_db.RecentFile.Any(a => a.FileId == fileId))
                    {
                        rFile = _db.RecentFile.FirstOrDefault(a => a.FileId == fileId);
                        rFile.Date = DateTime.Now;
                    }
                    else
                    {
                        rFile = _db.RecentFile.OrderBy(a => a.Date).FirstOrDefault();
                        rFile.FileId = fileId;
                        rFile.Date = DateTime.Now;
                    }
                    _db.Entry(rFile).State = EntityState.Modified;
                }
                else
                {
                    if (_db.RecentFile.Any(a => a.FileId == fileId))
                    {
                        rFile = _db.RecentFile.FirstOrDefault(a => a.FileId == fileId);
                        rFile.Date = DateTime.Now;
                        _db.Entry(rFile).State = EntityState.Modified;
                    }
                    else
                    {
                        rFile = new RecentFile
                        {
                            FileId = fileId,
                            Date = DateTime.Now
                        };
                        _db.RecentFile.Add(rFile);
                    }
                }
                _db.SaveChanges();
            }
            catch(Exception)
            {

            }
        }
        public PartialViewResult RecentFileList()
        {
            var list = new List<RecentFileModelView>();
            list = _db.RecentFile.OrderByDescending(o => o.Date).Join(_db.Files.Where(a => a.Status == true), rf => rf.FileId, f => f.Id, (rf, f) => new RecentFileModelView
            {
                Id = rf.Id,
                ProtectedId = f.Type == 1 ?  _protector.Protect(f.CabinetId.ToString()) : f.Type == 2 ? _protector.Protect(f.F1id.ToString()) : f.Type == 3 ? _protector.Protect(f.F2id.ToString()) : f.Type == 4 ? _protector.Protect(f.F3id.ToString()) : "",
                Name = f.OriginalName,
                Type = f.Type,
                FileType = f.FileType,
                Location = f.Type == 1 ? $"{_db.Cabinet.FirstOrDefault(c => c.Id == f.CabinetId).Name}\\" : f.Type == 2 ? $"{_db.Cabinet.FirstOrDefault(c => c.Id == f.CabinetId).Name}\\{_db.F1.FirstOrDefault(f1 => f1.Id == f.F1id).Name}\\" : f.Type == 3 ? $"{_db.Cabinet.FirstOrDefault(c => c.Id == f.CabinetId).Name}\\{_db.F1.FirstOrDefault(f1 => f1.Id == f.F1id).Name}\\{_db.F2.FirstOrDefault(f2 => f2.Id == f.F2id).Name}\\" : f.Type == 4 ? $"{_db.Cabinet.FirstOrDefault(c => c.Id == f.CabinetId).Name}\\{_db.F1.FirstOrDefault(f1 => f1.Id == f.F1id).Name}\\{_db.F2.FirstOrDefault(f2 => f2.Id == f.F2id).Name}\\{_db.F3.FirstOrDefault(f3 => f3.Id == f.F3id).Name}\\" : ""
            }).ToList();
            return PartialView(list);
        }
        public JsonResult RecentFileDelete(int id)
        {
            try
            {
                var recentFile = _db.RecentFile.Find(id);
                _db.Entry(recentFile).State = EntityState.Deleted;
                _db.SaveChanges();
                return Json("success");
            }
            catch(Exception)
            {
                return Json("error");
            }
        }
        #endregion

        #region File audit log
        [NonAction]
        public void FileAuditLogSave(FileAuditLogModelView fileLog)
        {
            try
            {
                var log = new FileAuditLog
                {
                    FileId = fileLog.FileId,
                    FileName = fileLog.FileName,
                    Message = fileLog.Message,
                    Operation = fileLog.Operation,
                    Status = fileLog.Status,
                    CreatedBy = User.GetUserId(),
                    CreatedDate = DateTime.Now
                };
                _db.FileAuditLog.Add(log);
                _db.SaveChanges();
            }
            catch(Exception)
            {

            }
        }
        #endregion
    }
}