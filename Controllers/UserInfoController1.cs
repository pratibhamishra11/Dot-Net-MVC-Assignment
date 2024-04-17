using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.InteropServices;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class UserInfoController1 : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserInfoController1(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(UserInfo ob) {

            _db.Add(ob);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        }
    }

