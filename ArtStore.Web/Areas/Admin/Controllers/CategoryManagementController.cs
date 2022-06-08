using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArtStore.Data;
using ArtStore.Models;
using ArtStore.Services;
using ArtStore.Web.Areas.Admin.ViewModels;

namespace ArtStore.Web.Areas.Admin.Controllers
{
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryManagementController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: Admin/CategoryManagement
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

            Expression<Func<Category, bool>> filter = null;

            if (!String.IsNullOrEmpty(search))
            {
                filter = c => c.Name.Contains(search);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = n => n.OrderByDescending(c => c.Name);
                    break;
                default:
                    orderBy = n => n.OrderBy(p => p.Name);
                    break;
            }

            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }

        // GET: Admin/CategoryManagement/Create
        public ActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        // POST: Admin/CategoryManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var cate = new Category
                {
                    Name = categoryViewModel.Name
                };
                var result = _categoryServices.Add(cate);
                if (result > 0)
                {
                    TempData["Message"] = "Tạo thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Tạo thất bại. Thử lại sau nhé!";
                }
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Admin/CategoryManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cate = _categoryServices.GetById((int)id);
            if (cate == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = new CategoryViewModel
            {
                Name = cate.Name
            };

            return View(categoryViewModel);
        }

        // POST: Admin/CategoryManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var cate = _categoryServices.GetById(categoryViewModel.Id);
                if (cate == null)
                {
                    return HttpNotFound();
                }

                cate.Name = categoryViewModel.Name;

                var result = _categoryServices.Update(cate);
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
            return View(categoryViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var result = _categoryServices.Delete(id);
            if (result)
            {
                TempData["Message"] = "Xóa thành công.";
            }
            else
            {
                TempData["Message"] = "Xóa thất bại!";
            }
            return RedirectToAction("Index");
        }
    }
}
