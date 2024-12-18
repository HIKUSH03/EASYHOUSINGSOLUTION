using EasyHousingClient.Models;
using EHSDataAccessLayer.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyHousingClient.Controllers
{
    public class AdminController : BaseController
    {
        //private readonly string _adminApiUrl = "http://localhost:54057/";


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
                var lk = $"region?region={region}";
                var query = $"api/property/{lk}";

                var properties = await GetFromApi<List<Property>>(query);

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
        public async Task<ActionResult> ViewPropertiesByOwner(int sellerId)
        {
            if (sellerId <= 0)
            {
                ModelState.AddModelError("", "Please enter a valid Owner Id.");
                return View(new List<Property>());
            }

            try
            {
                // Encode the sellerId as a string for URL encoding (though for integers it's often unnecessary)
                var query = $"api/property/seller/{sellerId}";

                // Fetch properties from API
                var properties = await GetFromApi<List<Property>>(query);

                if (properties == null || properties.Count == 0)
                {
                    ModelState.AddModelError("", "No properties found for the entered Owner Id.");
                }

                // Pass the sellerId back to the view. Adjust naming if needed (e.g., ViewBag.SellerId instead of PropertyId).
                ViewBag.SellerId = sellerId;
                return View(properties);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error retrieving properties: {ex.Message}");
                return View(new List<Property>());
            }
        }

        


        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var properties = await GetFromApi<List<Property>>("api/property");

            return View(properties);
            
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // Call API to get seller by id for deletion confirmation
                var property = await GetFromApi<Property>($"api/property/{id}");
                if (property == null)
                {
                    return HttpNotFound();
                }
                return View(property);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving seller for deletion: " + ex.Message;
                return View();
            }
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Call API to delete seller
                var response = await _httpClient.DeleteAsync($"api/property/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete seller.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the seller: " + ex.Message;
            }

            return View();
        }

        //[HttpGet]

        //public async Task<ActionResult> IsVerified(int id)
        //{
        //    return View(id);
        //}


        //[HttpPost, ActionName("IsVerified")]
        //[ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<ActionResult> IsVerified(int id)
        {
            var query = $"api/property/verify/{id}";

            var properties = await PostToApi<bool>(query, true);
            return RedirectToAction("Details");
        }

        //[HttpGet]

        //public async Task<ActionResult> UnVerified(int id)
        //{
        //    return View(id);
        //}


        //[HttpPost, ActionName("UnVerified")]
        //[ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<ActionResult> UnVerified(int id)
        {
            var query = $"api/property/{id}/verify";

            var properties = await PostToApi<bool>(query, false);
            return RedirectToAction("Details");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var queary = $"api/property/{id}";
                var response = await httpClient.GetAsync("http://localhost:54057/"+queary);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var property = JsonConvert.DeserializeObject<Property>(jsonString);
                    return View(property);
                }
                return View();
            }
        }
    }
}
