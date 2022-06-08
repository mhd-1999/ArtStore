using ArtStore.Models;
using ArtStore.Services;
using ArtStore.Web.Areas.Admin.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Areas.Admin.Controllers
{
    public class ArtManagementController : Controller
    {
        private readonly IArtServices _artServices;
        private readonly ICategoryServices _categoryServices;

        public ArtManagementController(IArtServices artServices, ICategoryServices categoryServices)
        {
            _artServices = artServices;
            _categoryServices = categoryServices;
        }

        // GET: Admin/ArtManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string search, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (search != null)
            {
                pageIndex = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;

            Expression<Func<Art, bool>> filter = null;

            if (!String.IsNullOrEmpty(search))
            {
                filter = c => c.Name.Contains(search);
            }

            Func<IQueryable<Art>, IOrderedQueryable<Art>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = n => n.OrderByDescending(c => c.Name);
                    break;
                default:
                    orderBy = n => n.OrderBy(p => p.Name);
                    break;
            }

            var arts = await _artServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(arts);
        }

        // GET: Admin/ArtManagement/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryServices.GetAll(), "Id","Name");
            var artViewModel = new ArtViewModel();
            return View(artViewModel);
        }

        // POST: Admin/ArtManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ArtViewModel artViewModel, HttpPostedFileBase uploadImage)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.ModelState = errors;
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (uploadImage != null)
                {
                    fileName = Path.GetFileName(uploadImage.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Assets/images"), uploadImage.FileName);
                    uploadImage.SaveAs(folderPath);
                }
                artViewModel.Image = fileName;
                var art = new Art
                {
                    Name = artViewModel.Name,
                    Image = artViewModel.Image,
                    Description = artViewModel.Description,
                    Price = artViewModel.Price,
                    SalePercent = artViewModel.SalePercent,
                    CategoryId = artViewModel.CategoryId
                };

                var result = _artServices.Add(art);
                if (result > 0)
                {
                    TempData["Message"] = "Tạo thành công.";
                }
                else
                {
                    TempData["Message"] = "Tạo thất bại. Thử lại sao nhé!";
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryServices.GetAll(), "Id", "Name", artViewModel.CategoryId);
            return View(artViewModel);
        }

        // GET: Admin/ArtManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Art art = _artServices.GetById((int)id);
            if (art == null)
            {
                return HttpNotFound();
            }

            var artViewModel = new ArtViewModel()
            {
                Id = art.Id,
                Name = art.Name,
                Description = art.Description,
                Image = art.Image,
                Price = art.Price,
                SalePercent = art.SalePercent,
                CategoryId = art.CategoryId
            };

            ViewBag.CategoryId = new SelectList(_categoryServices.GetAll(), "Id", "Name", artViewModel.CategoryId);
            return View(artViewModel);
        }

        // POST: Admin/ArtManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ArtViewModel artViewModel)
        {
            if (ModelState.IsValid)
            {

                var art = _artServices.GetById(artViewModel.Id);
                if(art == null)
                {
                    return HttpNotFound();
                }

                art.Name = artViewModel.Name;
                art.Image = artViewModel.Image;
                art.Description = artViewModel.Description;
                art.Price = artViewModel.Price;
                art.SalePercent = artViewModel.SalePercent;
                art.CategoryId = artViewModel.CategoryId;

                var result = _artServices.Update(art);
                if (result)
                {
                    TempData["Message"] = "Cập nhật thành công.";
                }
                else
                {
                    TempData["Message"] = "Cập nhật thất bại.";
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryServices.GetAll(), "Id", "Name", artViewModel.CategoryId);
            return View(artViewModel);
        }

        // POST: Admin/ArtManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var result = _artServices.Delete(id);
            if (result)
            {
                TempData["Message"] = "Xóa thành công.";
            }
            else
            {
                TempData["Message"] = "Xóa thất bại.";
            }
            return RedirectToAction("Index");
        }
    }
}
