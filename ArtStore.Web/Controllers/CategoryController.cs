using ArtStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryServices _categoryServices;
        private IArtServices _artServices;

        public CategoryController(ICategoryServices categoryServices, IArtServices artServices)
        {
            _categoryServices = categoryServices;
            _artServices = artServices;
        }

        // GET: Category
        public ActionResult GetAllCategory()
        {
            var categories = _categoryServices.GetAll();
            return PartialView("_ListCategories", categories);
        }
        public ActionResult Details(int cateId)
        {
            var cate = _categoryServices.GetById(cateId);
            if (cate == null)
            {
                return HttpNotFound();
            }
            var arts = _artServices.GetArtByCategory(cate.Id);
            ViewBag.Name = cate.Name;
            return View("_ListArts", arts);
        }
    }
}