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

        public IActionResult GetPlane(Plane plane) 
        {
            return View(plane.GetPlaneByFlight());
        }

        public IActionResult GetPlaneOrder(OrderedTicket orderedTicket)
        {
            var plane = new Plane();
            plane.Id = orderedTicket.Id;

            return RedirectToAction("GetPlane", plane);
        }
    }
}
