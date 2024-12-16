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
                    if (apiResult != null)
                    {
                        Session["UserName"] = apiResult.UserName;
                        Session["UserType"] = apiResult.UserType;


                        if (apiResult.UserType == "Admin")
                        {
                            TempData["Message"] = $"Admin Login{Session["UserName"]}";
                            return RedirectToAction("Login", "Auth");
                        }
                        else if (apiResult.UserType == "Seller")
                        {
                            TempData["Message"] = $"Seller Login{Session["UserName"]}";
                            return RedirectToAction("Login", "Auth");
                        }

                        else if (apiResult.UserType == "Buyer")
                        {
                            TempData["Message"] = $"Buyer Login{Session["UserName"]}";
                            return RedirectToAction("Login", "Auth");
                        }
                    }


                    return View();
                }
                else
                {
                    TempData["err"] = "Invalid username or password.";
                    return View();
                }
            }
        }
    }

    public class AuthResponse
    {
        public string Message { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }

    }
}





