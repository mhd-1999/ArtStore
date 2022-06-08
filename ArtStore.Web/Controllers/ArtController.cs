using ArtStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Controllers
{
    public class ArtController : Controller
    {
        private IArtServices _artServices;

        public ArtController(IArtServices artServices)
        {
            _artServices = artServices;
        }
        public ActionResult AllProduct(string search)
        {
            
            var arts = _artServices.GetAll();
            if (!String.IsNullOrEmpty(search))
            {
                arts=arts.Where(s=>s.Name.Contains(search));
            }
            return View(arts);
        }
        // GET: Art
        public ActionResult Details(int artId)
        {
            var art = _artServices.GetById(artId);
            if (art == null)
            {
                return HttpNotFound();
            }
            return View(art);
        }
      

    }
}