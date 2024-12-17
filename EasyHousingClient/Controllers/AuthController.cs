using System;
using System.Net.Http;
using System.Web.Mvc;
using EasyHousingClient.Models;
using EHSWebAPI.DTOs;
using Newtonsoft.Json;


namespace EasyHousingClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _authApiUrl = "http://localhost:54057/api/auth/login";
        private readonly string _RegisterBuyerApiUrl = "http://localhost:54057/api/auth/registerBuyer";
        private readonly string _RegisterSellerApiUrl = "http://localhost:54057/api/auth/registerSeller";
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
                            return RedirectToAction("Login", "Auth");
                        }

                        else if (apiResult.UserType == "Seller")
                        {
                            return RedirectToAction("Index", "Seller");
                        }

                        else if (apiResult.UserType == "Buyer")
                        {

                            return RedirectToAction("Index", "Buyer");
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

        // GET: Buyer Register
        public ActionResult BuyerRegister()
        {
            return View();
        }

        // POST: Buyer Register
        [HttpPost]


        public ActionResult BuyerRegister(RegisterBuyerDto registerBuyerDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill in all required fields properly.";
                return View(registerBuyerDto); // Return the form with validation messages
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_RegisterBuyerApiUrl);

                // Make a synchronous POST request to the API to register the user
                var response = client.PostAsJsonAsync("", registerBuyerDto).Result;

                if (response != null)
                {

                    TempData["SuccessMessage"] = "You are successfully registered!";
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    TempData["ErrorMessage"] = "Registration failed. Please try again.";
                    return RedirectToAction("BuyerRegister", "Auth"); // Return the form with error message
                }


            }
        }




        [HttpGet]
        public ActionResult SellerRegister()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SellerRegister(RegisterSellerDto registerSellerDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill in all required fields properly.";
                return View(registerSellerDto); // Return the form with validation messages
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_RegisterSellerApiUrl);

                // Make a synchronous POST request to the API to register the user
                var response = client.PostAsJsonAsync("", registerSellerDto).Result;

                if (response != null)
                {

                    TempData["SuccessMessage"] = "You are successfully registered!";
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    TempData["ErrorMessage"] = "Registration failed. Please try again.";
                    return RedirectToAction("SellerRegister", "Auth"); // Return the form with error message
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





