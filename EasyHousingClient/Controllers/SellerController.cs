using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using EHSDataAccessLayer.Entity;

namespace EasyHousingClient.Controllers
{
    public class SellerController : BaseController
    {

        // GET: Seller
        private readonly HttpClient _httpClient = new HttpClient(); // HttpClient instance for API calls

        // GET: Seller
        public async Task<ActionResult> Index()
        {
            try
            {
                // Validate that UserName exists in session
                if (Session["UserName"] == null)
                {
                    TempData["ErrorMessage"] = "User not logged in.";
                    return RedirectToAction("Login", "Auth");
                }

                // Step 1: Call API to fetch SellerId
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

                // Step 2: Call API to fetch list of sellers
                var sellers = await GetFromApi<List<Seller>>("api/Seller");
                return View(sellers);
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View(new List<Seller>());
            }
        }


        // GET: Seller/Details/5
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

        // GET: Seller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Seller seller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Call API to create a seller
                    var response = await PostToApi("api/Seller", seller);
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to Index page after successful creation
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to create seller.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the seller: " + ex.Message;
            }

            return View(seller);
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
                    // Call API to update seller details
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

        // GET: Seller/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // Call API to get seller by id for deletion confirmation
                var seller = await GetFromApi<Seller>($"api/Seller/{id}");
                if (seller == null)
                {
                    return HttpNotFound();
                }
                return View(seller);
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
                var response = await _httpClient.DeleteAsync($"api/Seller/{id}");
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
    }
}