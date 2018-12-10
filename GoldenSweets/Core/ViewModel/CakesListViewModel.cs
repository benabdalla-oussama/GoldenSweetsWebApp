using GoldenSweets.Core.Models;
using GoldenSweets.Core.ViewModel;
using System.Collections.Generic;

namespace GoldenSweets.Core.ViewModel
{
    public class CakesListViewModel
    {
        public List<CakeAndRating> Cakes { get; set; }
        public string CurrentCategory { get; set; }
    }
}
