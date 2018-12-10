using GoldenSweets.Core;
using GoldenSweets.Core.Models;
using GoldenSweets.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoldenSweets.Controllers
{
    [Route("/cakes")]
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICategoryRepository _categoryRepository;


        public CakeController(ICakeRepository cakeRepository, IRatingRepository ratingRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _ratingRepository = ratingRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{category?}")]
        public async Task<IActionResult> List(string category)
        {
            var selectedCategory = !string.IsNullOrWhiteSpace(category) ? category : null;
            var cakesListViewModel = new CakesListViewModel
            {
                Cakes = await _cakeRepository.GetCakes(selectedCategory),
                CurrentCategory = selectedCategory ?? "All Cakes"
            };
            for (int i =0; i < cakesListViewModel.Cakes.Count; i++)
            {
                string rating = await _ratingRepository.GetRatingNumberByCake(cakesListViewModel.Cakes[i].Id);
                cakesListViewModel.Cakes[i].Rating = rating;
            }
            return View(cakesListViewModel);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            CakeDetailRatingViewModel ratingModel = new CakeDetailRatingViewModel();
            var rating = await _ratingRepository.GetUserRatingByCakeAsync(id);
            ratingModel.CakeId = id;
            if(rating != null)
            ratingModel.Value = rating.Value ;
            var cake = await _cakeRepository.GetCakeById(id);
            ratingModel.cake = cake;
            return View(ratingModel);
        }

        [HttpPost("Rate")]
        public async Task<IActionResult> Details([Bind("Value,CakeId")] CakeDetailRatingViewModel model)
        {
            var existRating = await _ratingRepository.GetUserRatingByCakeAsync(model.CakeId);
            if(existRating != null)
            {
                existRating.Value = model.Value;
                existRating.Cake = await model.LoadCake(_cakeRepository);
                _ratingRepository.UpdateRating(existRating);
            }
            else
            {
                Rating rating = new Rating();
                rating.CakeId = model.CakeId;
                rating.Value = model.Value;
                rating.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _ratingRepository.AddRatingAsync(rating);
            }
            model.cake = await model.LoadCake(_cakeRepository);
            return View(model);
        }

    }
}
