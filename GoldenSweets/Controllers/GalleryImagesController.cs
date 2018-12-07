using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoldenSweets.Core.Models;
using GoldenSweets.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace GoldenSweets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GalleryImagesController : Controller
    {
        private readonly GoldenSweetsDbContext _context;

        public GalleryImagesController(GoldenSweetsDbContext context)
        {
            _context = context;
        }

        // GET: GalleryImages
        public async Task<IActionResult> Index()
        {
            var GoldenSweetsDbContext = _context.GalleryImages.Include(g => g.Gallery);
            return View(await GoldenSweetsDbContext.ToListAsync());
        }

        // GET: GalleryImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImages
                .Include(g => g.Gallery)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryImage == null)
            {
                return NotFound();
            }

            return View(galleryImage);
        }

        // GET: GalleryImages/Create
        public IActionResult Create()
        {
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "Id", "Name");
            return View();
        }

        // POST: GalleryImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Label,Url,Selected,GalleryId")] GalleryImage galleryImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "Id", "Name", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImages.SingleOrDefaultAsync(m => m.Id == id);
            if (galleryImage == null)
            {
                return NotFound();
            }
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "Id", "Name", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // POST: GalleryImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Label,Url,Selected,GalleryId")] GalleryImage galleryImage)
        {
            if (id != galleryImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryImageExists(galleryImage.Id))
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
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "Id", "Name", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImages
                .Include(g => g.Gallery)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryImage == null)
            {
                return NotFound();
            }

            return View(galleryImage);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryImage = await _context.GalleryImages.SingleOrDefaultAsync(m => m.Id == id);
            _context.GalleryImages.Remove(galleryImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryImageExists(int id)
        {
            return _context.GalleryImages.Any(e => e.Id == id);
        }
    }
}
