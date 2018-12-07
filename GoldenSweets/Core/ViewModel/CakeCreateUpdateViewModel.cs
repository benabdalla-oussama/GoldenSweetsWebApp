using GoldenSweets.Core.Dto;
using GoldenSweets.Core.Models;
using System.Collections.Generic;

namespace GoldenSweets.Core.ViewModel
{
    public class CakeCreateUpdateViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public CakeDto CakeDto { get; set; }

        public CakeCreateUpdateViewModel()
        {
            Categories = new List<Category>();
        }
    }
}
