using BlitzFlug.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlitzFlug.Controllers
{
    public class PlanesController : Controller
    {
        public ActionResult Index()
        {
            var plane = new Plane();
            List<Plane> planes = plane.GetAllPlanes().ToList();

            return View(planes);
        }
    }
}
