using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AnimeInfo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr, ApplicationDbContext context)
        {
            _userManager = userMngr ?? throw new ArgumentNullException(nameof(userMngr));
            _roleManager = roleMngr ?? throw new ArgumentNullException(nameof(roleMngr));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
            }

            var model = new UserVM
            {
                Users = users,
                Roles = await _roleManager.Roles.ToListAsync()
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new AdminCreateUserViewModel());
        }

        // New method to handle user creation
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    SignUpdate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.IsAdmin)
                    {
                        // Check if Admin role exists, create if it doesn't
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)

            //was running into a issue where deleting the user would cuase a foreign key restraint error. So in order to fix this, the users comments, and blogs needed to be deleted first. then the user. This stumped me for a bit
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // First, delete all comments by this user
                var userComments = await _context.Comments
                    .Where(c => c.CommentAuthor.Id == id)
                    .ToListAsync();

                if (userComments.Any())
                {
                    _context.Comments.RemoveRange(userComments);
                    await _context.SaveChangesAsync();
                }

                // Also handle any blogs by this user
                var userBlogs = await _context.Blogs
                    .Where(b => b.BlogAuthor.Id == id)
                    .ToListAsync();

                if (userBlogs.Any())
                {
                    _context.Blogs.RemoveRange(userBlogs);
                    await _context.SaveChangesAsync();
                }

                // Now delete the user
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["Message"] = "Admin role does not exist. Create it first.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}