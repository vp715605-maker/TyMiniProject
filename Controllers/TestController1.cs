using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TyMiniProject.Data;
using TyMiniProject.Models;

namespace TyMiniProject.Controllers
{
   
    public class TestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public TestController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Create Profile (only if not exists)
        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            // Check if profile already exists for this user
            var existingProfile = _context.Profiles.SingleOrDefault(p => p.UserId == user.Id);
            if (existingProfile != null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // POST: Create Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(Profile model)
        {
            Console.WriteLine("POST called"); // check console/log
            try
            {
                var user = await _userManager.GetUserAsync(User);

                // Prevent duplicate
                var existingProfile = _context.Profiles.SingleOrDefault(p => p.UserId == user.Id);
                if (existingProfile != null)
                {
                    TempData["Error"] = "You already have a profile!";
                    return RedirectToAction("ProfileDetails");
                }

                if (ModelState.IsValid)
                {
                    model.UserId = user.Id;

                    _context.Profiles.Add(model);
                    _context.SaveChanges();

                    TempData["Success"] = "Profile Successfully Created.";
                    return RedirectToAction("ProfileDetails");
                }

                ModelState.AddModelError("", "Invalid input, please check again.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        // Show current user's profile
        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var data = _context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

            if (data == null)
            {
                return RedirectToAction("CreateProfile");
            }

            return View(data);
        }

        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> ProfileEdit()
        {
            var user = await _userManager.GetUserAsync(User);
            var data = _context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

            if (data == null)
            {
                return RedirectToAction("CreateProfile");
            }

            return View(data);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileEdit(Profile model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var data = _context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

                if (data == null)
                {
                    return RedirectToAction("CreateProfile");
                }

                if (ModelState.IsValid)
                {
                    // Update fields (photo removed)
                    data.FullName = model.FullName;
                    data.Dob = model.Dob;
                    data.AdmissionYear = model.AdmissionYear;
                    data.Branch = model.Branch;
                    data.Caste = model.Caste;
                    data.Email = model.Email;
                    data.FeeCategory = model.FeeCategory;
                    data.PRN = model.PRN;
                    data.PhoneNumber = model.PhoneNumber;

                    _context.Profiles.Update(data);
                    _context.SaveChanges();

                    TempData["Success"] = "Profile Updated Successfully.";
                    return RedirectToAction("ProfileDetails");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
