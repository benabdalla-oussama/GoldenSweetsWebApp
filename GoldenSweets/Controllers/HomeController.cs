using GoldenSweets.Core;
using GoldenSweets.Core.Models;
using GoldenSweets.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoldenSweets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICakeRepository _cakeRepository; 
        private readonly IGalleryRepository _galleryRepository;

        public HomeController(ICakeRepository cakeRepository,IGalleryRepository galleryRepository)
        {
            _cakeRepository = cakeRepository;
            _galleryRepository = galleryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var images = await _galleryRepository.GetSelectedImages();
            return View(new HomeViewModel
            {
                CakeOfTheWeek = await _cakeRepository.GetCakesOfTheWeek() ,
                Images = images
            });
        }
    }
}