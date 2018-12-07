using GoldenSweets.Core.Models;
using System.Collections.Generic;

namespace GoldenSweets.Core.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Cake> CakeOfTheWeek { get; set; }
        public List<GalleryImage> Images { get; set; }
    }
}
