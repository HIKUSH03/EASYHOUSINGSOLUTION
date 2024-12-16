using System;
using System.Net.Http;
using System.Web.Mvc;
using EasyHousingClient.Models;
using Newtonsoft.Json;


namespace EasyHousingClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _authApiUrl = "http://localhost:54057/api/auth/login";
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        //Handle login request
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill in all required fields.";
                return View();
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_authApiUrl);

                //make a POST request to the API
                var response = client.PostAsJsonAsync("", loginViewModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    //DeSerialize the JSON response
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;

                    //Define a temporary class to hold API response
                    var apiResult = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);


                    if (apiResult.UserType == "Admin")
                        return RedirectToAction("Index", "Admin");
                    else if (apiResult.UserType == "Seller")
                        return RedirectToAction("Index", "Seller");
                    else if (apiResult.UserType == "buyer")
                        return RedirectToAction("Index", "Buyer");
                    else
                        ViewBag.ErrorMessage = "Invalid user";
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View();
                }
            }
        }
    }

    public class AuthResponse
    {
        public string Message { get; set; }
        public string UserType { get; set; }

    }
}





