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


        [HttpGet]
        public ActionResult SearchPropertyByRegion()
        {
            return View(new List<Property>());
        }

        [HttpPost]
        public async Task<ActionResult> SearchPropertyByRegion(string region)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:54057/region?region=" + region);
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