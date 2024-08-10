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

        public DbManageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
