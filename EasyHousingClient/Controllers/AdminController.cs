using EHSDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyHousingClient.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public ActionResult ViewPropertiesByRegion()
        {
            // Return the view to allow admin to enter a region
            return View(new List<Property>());
        }

        [HttpPost]
        public async Task<ActionResult> ViewPropertiesByRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
            {
                ModelState.AddModelError("", "Please enter a valid region.");
                return View(new List<Property>());
            }

            try
            {
                // Get properties from API

                var properties = await GetFromApi<List<Property>>($"region?region={HttpUtility.UrlEncode(region)}");

                if (properties == null || properties.Count == 0)
                {
                    ModelState.AddModelError("", "No properties found for the entered region.");
                }

                ViewBag.Region = region; // Pass the region back to the view for display
                return View(properties);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error retrieving properties: {ex.Message}");
                return View(new List<Property>());
            }
        }


        [HttpGet]
        public ActionResult ViewPropertiesByOwner()
        {
            // Return the view to allow admin to enter a OwnerId
            return View(new List<Property>());
        }

        [HttpPost]
        public async Task<ActionResult> ViewPropertiesByOwner(int id)
        {
            if (id<=0)
            {
                ModelState.AddModelError("", "Please enter a valid Owner Id.");
                return View(new List<Property>());
            }

            try
            {
                // Get properties from API
                var properties = await GetFromApi<List<Property>>($"id?id={HttpUtility.UrlEncode("{id}")}");

                if (properties == null || properties.Count == 0)
                {
                    ModelState.AddModelError("", "No properties found for the entered Owner Id.");
                }

                ViewBag.PropertyId = id; // Pass the id back to the view for display
                return View(properties);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error retrieving properties: {ex.Message}");
                return View(new List<Property>());
            }
        }
    }
}
