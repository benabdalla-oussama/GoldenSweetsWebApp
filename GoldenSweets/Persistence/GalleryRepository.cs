using GoldenSweets.Core;
using GoldenSweets.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Persistence
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly GoldenSweetsDbContext _context;

        public GalleryRepository(GoldenSweetsDbContext context)
        {
            _context = context;
        }

        public async Task<Gallery> GetSelectedGallery()
        {
            return null;
            //return await _context.Galleries.FindAsync(e => e.Selected == true).Include(d => d.GalleryImages).;
        }

        public async Task<List<GalleryImage>> GetSelectedImages()
        {
            Gallery gallery =  _context.Galleries.FirstOrDefault();
            return await _context.GalleryImages
                .Where(d => d.Selected)
                .ToListAsync();
        }
    }
}
