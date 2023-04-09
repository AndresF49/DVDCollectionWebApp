using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVDCollectionWebApp.Models;

namespace DVDCollectionWebApp
{
    public class DVDCollectionController : Controller
    {
        private readonly DVDCollectionContext _context;

        public DVDCollectionController(DVDCollectionContext context)
        {
            _context = context;
        }

        //// GET: DVDCollection
        //public async Task<IActionResult> Index()
        //{
        //      return _context.DVDs != null ? 
        //                  View(await _context.DVDs.ToListAsync()) :
        //                  Problem("Entity set 'DVDCollectionContext.DVDs'  is null.");
        //}

        public async Task<IActionResult> Index(string sortOrder, string catSearch)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CategoryCurrentFilter"] = catSearch;


            var dvds = from d in _context.DVDs
                       select d;
            if (!String.IsNullOrEmpty(catSearch))
            {
                dvds = dvds.Where(d => d.category.Contains(catSearch));
            }
            switch (sortOrder)
            {
                case "Date":
                    dvds = dvds.OrderBy(d => d.yearOfRelease);
                    break;
                case "date_desc":
                    dvds = dvds.OrderByDescending(d => d.yearOfRelease);
                    break;
                default:
                    dvds = dvds.OrderBy(d => d.title); // order by title by default
                    break;
            }

            return View(await dvds.ToListAsync());
        }

        // GET: DVDCollection/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dVD == null)
            {
                return NotFound();
            }

            return View(dVD);
        }

        // GET: DVDCollection/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DVDCollection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("title,category,runtime,yearOfRelease,price")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dVD);
        }

        // GET: DVDCollection/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs.FirstOrDefaultAsync(d => d.Id == id);
            if (dVD == null)
            {
                return NotFound();
            }
            return View(dVD);
        }

        // POST: DVDCollection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("title,category,runtime,yearOfRelease,price")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dvdToDelete = await _context.DVDs.FirstOrDefaultAsync(d => d.Id == id);
                    if (dvdToDelete != null)
                    {
                        _context.DVDs.Remove(dvdToDelete);
                        _context.DVDs.Add(dVD);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDExists(dVD.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dVD);
        }

        // GET: DVDCollection/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dVD == null)
            {
                return NotFound();
            }

            return View(dVD);
        }

        // POST: DVDCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DVDs == null)
            {
                return Problem("Entity set 'DVDCollecitonContext.DVDs'  is null.");
            }
            var dVD = await _context.DVDs.FirstOrDefaultAsync(d => d.Id == id);
            if (dVD != null)
            {
                _context.DVDs.Remove(dVD);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDExists(int id)
        {
          return (_context.DVDs?.Any(d => d.Id == id)).GetValueOrDefault();
        }
    }
}
