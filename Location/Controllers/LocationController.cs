using Location.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Location.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            //int CompanyIdId = 1;
            LocationDbHandller DbLocation = new LocationDbHandller();
            ModelState.Clear();
            Locations model = new Locations();
            var data = DbLocation.GetLocationsList();

            List<SelectListItem> items = new List<SelectListItem>();

            var list = data.Select(p => new SelectListItem
            {
                Value = p.CountryName.ToString(),
                Text = p.CountryName.ToString()
            });
            var listcn = new SelectList("Value", "Text");
            ViewBag.CountryName = list;

            return View(data);
        }


        // GET: Default/Edit/5
        public ActionResult Edit(int id = 3)
        {
            LocationDbHandller DbLocation = new LocationDbHandller();
            var data = DbLocation.GetLocation();

            List<SelectListItem> items = new List<SelectListItem>();

            var listCountry = data.Select(p => new SelectListItem
            {
                Value = p.CountryId.ToString(),
                Text = p.CountryName.ToString()
            });
            var listcnCountry = new SelectList("Value", "Text");
            //ViewBag.CountryName = listCountry;


            var listCountryId = data.Select(p => new SelectListItem
            {
                Value = p.CountryId.ToString(),
                Text = p.CountryId.ToString()
            });
            var listcnCountryId = new SelectList("Value", "Text");
            //ViewBag.CountryID = listCountryId;


            var listState = data.Select(p => new SelectListItem
            {
                Value = p.StateId.ToString(),
                Text = p.StateName.ToString()
            });
            var listcnState = new SelectList("Value", "Text");
            //ViewBag.StateName = listState;


            var listCity = data.Select(p => new SelectListItem
            {
                Value = p.CityName.ToString(),
                Text = p.CityName.ToString()
            });
            var listcnCity = new SelectList("Value", "Text");

            ViewBag.CountryName = listCountry;
            ViewBag.StateName = listState;
            ViewBag.CityName = listCity;
            ViewBag.CountryId = listCountryId;

            return View(data);
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult Edit(Locationssave Location)
        {
            try
            {
                LocationDbHandller DbLocation = new LocationDbHandller();
                List<SelectListItem> items = new List<SelectListItem>();

                if (Location.CountryName == null || Location.CountryName == "1")
                {
                    //Location.CountryName = "India";
                    Location.CountryName = "1";
                }

                if (Location.StateName == null || Location.StateName == "0")
                {
                    //Location.StateName = "Gujarat";
                    Location.StateName = "1";
                }

                var data = DbLocation.GetLocation();
                var dataCountrytostate = DbLocation.GetStateLocationByCountry(Location);
                var dataCitytoState = DbLocation.GetCityLocationByState(Location);

                // Country List 
                var listCountry = data.Select(p => new SelectListItem
                {
                    Value = p.CountryId.ToString(),
                    Text = p.CountryName.ToString()
                });
                var listcnCountry = new SelectList("Value", "Text");
                ViewBag.CountryName = listCountry;

                // State List 
                var listState = dataCountrytostate.Select(p => new SelectListItem
                {
                    Value = p.StateId.ToString(),
                    Text = p.StateName.ToString()
                });
                var listcnState = new SelectList("Value", "Text");
                ViewBag.StateName = listState;

                // City List 
                var listCity = dataCitytoState.Select(p => new SelectListItem
                {
                    Value = p.CityName.ToString(),
                    Text = p.CityName.ToString()
                });
                var listcnCity = new SelectList("Value", "Text");
                ViewBag.CityName = listCity;

                data = dataCountrytostate;
                data = dataCitytoState;

                if (data != null)
                {
                    @ViewBag.Message = "List Added Successfully";
                }
                Thread.Sleep(800);
                return View(data);
            }
            catch
            {
                return View();
            }
        }


        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
