using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UrlProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrlProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UrlProfile ob)
        {
            _context.Add(ob);
            _context.SaveChanges();
            return RedirectToAction("SuccessPage","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = await _context.UrlProfile.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UrlProfile obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _context.SaveChanges();
                return RedirectToAction("SuccessPage", "Home");
            }
            return View(obj);
        }
        public IActionResult Delete(int? Id, UrlProfile obj)
        {
            var user = _context.UrlProfile.FirstOrDefault(c => c.Id == obj.Id);
            _context.UrlProfile.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("SuccessPage", "Home");
        }
    

}
}
