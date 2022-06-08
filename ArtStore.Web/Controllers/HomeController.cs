using ArtStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtServices _artServices;

        public HomeController(IArtServices artServices)
        {
            _artServices = artServices;
        }

        public ActionResult Index()
        {
            var arts = _artServices.GetAll();
            return View(arts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}