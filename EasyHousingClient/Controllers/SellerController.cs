using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using EHSDataAccessLayer.Entity;
using Newtonsoft.Json;

namespace EasyHousingClient.Controllers
{
    public class SellerController : BaseController
    {

        // GET: Seller
        private readonly string apiBaseUrl = "http://localhost:54057/api/";
        private readonly HttpClient _httpClient = new HttpClient(); 

        // GET: Seller
        public async Task<ActionResult> Index()
        {
            try
            {
              
                if (Session["UserName"] == null)
                {
                    TempData["ErrorMessage"] = "User not logged in.";
                    return RedirectToAction("Login", "Auth");
                }

                string apiUrl = $"http://localhost:54057/api/Seller/seller/{Session["UserName"]}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response content as an integer
                    var sellerIdString = await response.Content.ReadAsStringAsync();
                    int sellerId = Convert.ToInt32(sellerIdString);

                    // Store SellerId in Session
                    Session["SellerId"] = sellerId;
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve Seller ID.";
                }

                var Properties = await GetFromApi<List<Property>>($"api/property/seller/{Session["SellerId"]}");
                return View(Properties);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View(new List<Seller>());
            }
        }


        [HttpGet]
        public ActionResult AddProperty()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddProperty(Property property)
        {
            //int sellerId = (int)Session["SellerId"];
            //property.SellerId = sellerId;
            property.IsActive = false;
            property.SellerId = (int)Session["SellerId"];

            if (!ModelState.IsValid)
            {
                return View(property);
            }

            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.PostAsJsonAsync($"property", property);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Property created successfully!";

                    return RedirectToAction("Index");
                    
                }
            }

            ViewBag.Error = "Error adding property.";
            return View(property);
        }
        public async Task<ActionResult> ViewVerifiedProperties(int id)
        {
            List<Property> properties = new List<Property>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.GetAsync($"property/seller/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    properties = JsonConvert.DeserializeObject<List<Property>>(result);
                }
                var res = properties.Where(x => x.SellerId == id && x.IsActive == true).ToList();
                return View(res);


                
            }
        }
        public async Task<ActionResult> UnViewVerifiedProperties(int id)
        {
            List<Property> properties = new List<Property>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.GetAsync($"property/seller/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    properties = JsonConvert.DeserializeObject<List<Property>>(result);
                }
                var res = properties.Where(x => x.SellerId == id && x.IsActive == false).ToList();
                return View(res);



            }
        }

        [HttpGet]
        public async Task<ActionResult> PropertyDetails(int id)
        {
            Property property = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.GetAsync($"property/{id}"); // Fetch property by ID

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    property = JsonConvert.DeserializeObject<Property>(result);

                    if (property.SellerId != (int)Session["SellerId"])
                    {
                        return new HttpUnauthorizedResult("Unauthorized access.");
                    }
                }
            }

            return View(property); 
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            Property property = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.GetAsync($"property/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    property = JsonConvert.DeserializeObject<Property>(result);
                    if (property.SellerId != (int)Session["SellerId"])
                    {
                        return new HttpUnauthorizedResult("Unauthorized access.");
                    }
                }
                else
                {
                    return HttpNotFound("Property not found.");
                }
            }

            return View(property);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.DeleteAsync($"property/{id}");
                
                return RedirectToAction("Index", "Seller");

                //if (response.IsSuccessStatusCode)
                //{
                   
                //    return RedirectToAction("Index", "Seller");
                //}
                //else
                //{
                  
                //    TempData["ErrorMessage"] = "Failed to delete the property. Please try again.";
                //    return RedirectToAction("Delete", new { id });
                //}
            }
        }






        [HttpGet]
        public async Task<ActionResult> EditProperty(int id)
        {
            Property property = null;

            using (var client = new HttpClient())
            {
              
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.GetAsync($"property/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    property = JsonConvert.DeserializeObject<Property>(result);

                    if (property.SellerId != (int)Session["SellerId"])
                    {
                        return new HttpUnauthorizedResult("Unauthorized access.");
                    }
                }
                else
                {
                    return HttpNotFound("Property not found.");
                }
            }

            return View(property); 
        }

        [HttpPost]
        public async Task<ActionResult> EditProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return View(property);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = await client.PutAsJsonAsync($"property/{property.PropertyId}", property);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); 
                }
                else
                {
                    ViewBag.Error = "Failed to update property.";
                }
            }

            return View(property);
        }














        public async Task<ActionResult> Details(int id)
        {
            try
            {
                // Call API to get a specific seller by id
                var seller = await GetFromApi<Seller>($"api/Seller/{id}");
                if (seller == null)
                {
                    return HttpNotFound();
                }
                return View(seller);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving seller details: " + ex.Message;
                return View();
            }
        }

     
       

        // GET: Seller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // Call API to get seller by id for editing
                var seller = await GetFromApi<Seller>($"api/Seller/{id}");
                if (seller == null)
                {
                    return HttpNotFound();
                }
                return View(seller);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving seller for editing: " + ex.Message;
                return View();
            }
        }

        // POST: Seller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Seller seller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await PostToApi($"api/Seller/{seller.SellerId}", seller);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to update seller.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while updating the seller: " + ex.Message;
            }

            return View(seller);
        }
    }
}