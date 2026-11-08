using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindIt.Data;
using FindIt.Models;

namespace FindIt.Controllers
{
    public class ItemsController : Controller
    {
        private readonly FindItContext _context;

        public ItemsController(FindItContext context)
        {
            _context = context;
        }

        // GET: Items (Public catalog & search)
        public async Task<IActionResult> Index(string searchString, string category)
        {
            var items = from i in _context.Items select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category))
            {
                items = items.Where(s => s.Category == category);
            }

            return View(await items.OrderByDescending(i => i.DateFound).ToListAsync());
        }

        // GET: Items/Create (Security guard logs a found item)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Category,LocationFound,StorageLocation,SecretQuestion")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Claim/5 (Student submits ownership claim)
        public async Task<IActionResult> Claim(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Claim(int id, string answer)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            // Simple verification logic rule
            item.ClaimStatus = "Pending Verification";
            _context.Update(item);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Claim submitted successfully! Please proceed to the Security Office with proof.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Verify/5 (Admin/Security approves claim)
        public async Task<IActionResult> Verify(int? id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                item.Status = "Claimed";
                item.ClaimStatus = "Approved & Retrieved";
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}