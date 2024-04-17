using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Policy;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserLogin _userLogin = new UserLogin();
            return View(_userLogin);
        }

        
        public IActionResult Index(UserLogin userLogin)
        {
            var status = _context.Users.Where(m => m.Email == userLogin.Email && m.Password == userLogin.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                //TempData["Name"] = status.Name;
                //TempData["PhoneNumber"] = status.PhoneNumber;
                //TempData["Email"] = status.Email;

                Response.Cookies.Append("Id", status.Id.ToString());
                Response.Cookies.Append("Name", status.Name);
                Response.Cookies.Append("Email", status.Email);

                return RedirectToAction("SuccessPage","Home");
            }
            return View(userLogin);

        }

        public IActionResult SuccessPage()
        {
            string Id = Request.Cookies["Id"];
            string Name = Request.Cookies["Name"];
            string Email = Request.Cookies["Email"];
            ViewData["Id"] = Id;
            ViewData["Name"] = Name;
            ViewData["Email"] = Email;
            List<UrlProfile> c = _context.UrlProfile.ToList();
            return View(c);

            //return View();
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            string Id = Request.Cookies["id"];
            var user = _context.Users.FirstOrDefault(n => n.Id.ToString() == Id);
            if(user == null) { 
            return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserInfo ob)
        {
            if (!ModelState.IsValid)
            {
                return View(ob);
            }

            _context.Update(ob);
            _context.SaveChanges();
            Response.Cookies.Delete("");
            Response.Cookies.Append("Name", ob.Name);
            Response.Cookies.Append("Email", ob.Email);

            return RedirectToAction("SuccessPage", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}





