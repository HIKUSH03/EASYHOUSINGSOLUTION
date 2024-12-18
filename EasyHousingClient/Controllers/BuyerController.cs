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
    public class BuyerController : SecurityController
    {
        // get all properties
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/property");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
                    //return View(properties.Where(x => x.IsActive == true).ToList());
                    return View(properties);
                }
                return View();
            }
        }


        // see individual property
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/property/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var property = JsonConvert.DeserializeObject<Property>(jsonString);
                    return View(property);
                }
                return View();
            }
        }

        // get seller details
        [HttpGet]
        public async Task<ActionResult> SellerDetails(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/Seller/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var seller = JsonConvert.DeserializeObject<Seller>(jsonString);
                    return View(seller);
                }
                return View();
            }
        }

        // sorted by price
        [HttpGet]
        public async Task<ActionResult> SortedByPrice()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/buyers/propertybyprice");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
                    return View(properties);
                }
                return View();
            }
        }

        // search property by region
        [HttpGet]
        public async Task<ActionResult> SearchPropertyByRegion()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/property");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
                    return View(properties);
                }
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> SearchPropertyByRegion(string region)
        {
            // http://localhost:54057/api/property/region?region=pune

            var url = "http://localhost:54057/api/property";
            if (region != string.Empty)
                url += "/region?region=" + region;
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
                    return View(properties);
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> BuyerProfile()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/api/buyers/buyer/" + Session["UserName"]);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    int buyerId = Convert.ToInt32(jsonString);
                    response = await httpClient.GetAsync("http://localhost:54057/api/buyers/" + buyerId);
                    var buyerObj = await response.Content.ReadAsStringAsync();
                    var buyer = JsonConvert.DeserializeObject<Buyer>(buyerObj);
                    return View(buyer);
                }
                return View();
            }
        }

        // post request to update the buyer profile
        [HttpPost]
        public async Task<ActionResult> BuyerProfile(Buyer buyer)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(buyer) , System.Text.Encoding.UTF8 , "application/json");
                var response = await httpClient.PutAsync("http://localhost:54057/api/buyers/updateProfile", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Profile Updated Successfully";
                    return RedirectToAction("BuyerProfile");
                }
                TempData["Message"] = "Can not Updated!";
                return View();
            }
        }
    }
}