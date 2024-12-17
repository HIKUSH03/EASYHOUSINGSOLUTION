using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EHSDataAccessLayer.Entity;
using Newtonsoft.Json;

namespace EasyHousingClient.Controllers
{
    public class BuyerController : BaseController
    {
        // get all properties
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
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
                var response = await httpClient.GetAsync("http://localhost:54057/" + id);
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
                var response = await httpClient.GetAsync("http://localhost:54057/");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var properties = JsonConvert.DeserializeObject<List<Property>>(jsonString);
                    return View(properties);
                }
                return View();
            }
            //return View(new List<Property>());
        }

        [HttpPost]
        public async Task<ActionResult> SearchPropertyByRegion(string region)
        {
            var url = "http://localhost:54057/";
            if(region != string.Empty)
                url += "region?region=" + region;
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
    }
}