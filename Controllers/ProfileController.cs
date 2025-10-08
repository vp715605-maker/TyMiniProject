using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TyMiniProject.Data;
using TyMiniProject.Models;

namespace TyMiniProject.Controllers
{
    [Authorize] // ensure only logged-in users can access
    public class ProfileController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<Users> userManager;

        public ProfileController(AppDbContext context, IWebHostEnvironment env, UserManager<Users> userManager)
        {
            this.context = context;
            this.env = env;
            this.userManager = userManager;
        }

        // GET: Create Profile (only if not exists)
        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            var user = await userManager.GetUserAsync(User);

            // Check if profile already exists for this user
            var existingProfile = context.Profiles.SingleOrDefault(p => p.UserId == user.Id);
            if (existingProfile != null)
            {
                return RedirectToAction("ProfileDetails");
            }

            return View();
        }

        // POST: Create Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(Profile model)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);

                // Prevent duplicate
                var existingProfile = context.Profiles.SingleOrDefault(p => p.UserId == user.Id);
                if (existingProfile != null)
                {
                    TempData["Error"] = "You already have a profile!";
                    return RedirectToAction("ProfileDetails");
                }

                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadImage(model);

                    model.UserId = user.Id;
                    model.ImageUrl = uniqueFileName;

                    context.Profiles.Add(model);
                    context.SaveChanges();

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
            var user = await userManager.GetUserAsync(User);
            var data = context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

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
            var user = await userManager.GetUserAsync(User);
            var data = context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

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
                var user = await userManager.GetUserAsync(User);
                var data = context.Profiles.SingleOrDefault(e => e.UserId == user.Id);

                if (data == null)
                {
                    return RedirectToAction("CreateProfile");
                }

                if (ModelState.IsValid)
                {
                    string uniqueFileName = data.ImageUrl;

                    if (model.ImageFile != null)
                    {
                        // Delete old image
                        if (!string.IsNullOrEmpty(data.ImageUrl))
                        {
                            string oldPath = Path.Combine(env.WebRootPath, "StudentPhoto", data.ImageUrl);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }

                        // Upload new one
                        uniqueFileName = UploadImage(model);
                    }

                    // Update fields
                    data.FullName = model.FullName;
                    data.Dob = model.Dob;
                    data.AdmissionYear = model.AdmissionYear;
                    data.Branch = model.Branch;
                    data.Caste = model.Caste;
                    data.Email = model.Email;
                    data.FeeCategory = model.FeeCategory;
                    data.PRN = model.PRN;
                    data.PhoneNumber = model.PhoneNumber;
                    data.ImageUrl = uniqueFileName;

                    context.Profiles.Update(data);
                    context.SaveChanges();

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

        // Handle image upload
        private string UploadImage(Profile model)
        {
            string uniqueFileName = string.Empty;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(env.WebRootPath, "StudentPhoto");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
