using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultilEcommer.Data;

namespace MultilEcommer.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public DbManageController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _dbContext = dbContext;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }


        // GET: DbManage
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa thành công" : "Không xóa được";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cập nhật Database thành công";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach(var role in roleNames)
            {
                var roleName = (string)role.GetRawConstantValue();
                var roleFound = await _RoleManager.FindByNameAsync(roleName);
                if(roleFound == null)
                {
                    await _RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //admin, pass=admin123, mail: admin@example.com
            var userAdmin = await _UserManager.FindByNameAsync("admin");
            if(userAdmin == null)
            {
                userAdmin = new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await _UserManager.CreateAsync(userAdmin, "admin123");
                await _UserManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }
            StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");
        }
    }
}
