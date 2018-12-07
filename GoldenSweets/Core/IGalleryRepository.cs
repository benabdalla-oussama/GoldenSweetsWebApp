using GoldenSweets.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core
{
    public interface IGalleryRepository
    {
        Task<Gallery> GetSelectedGallery();
        Task<List<GalleryImage>> GetSelectedImages();

    }
}
