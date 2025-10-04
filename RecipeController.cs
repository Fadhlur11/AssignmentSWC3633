using Microsoft.AspNetCore.Mvc;
using RecipeFinder.Services;
using System.Threading.Tasks;
using System.Linq; // Add this to use .FirstOrDefault()

namespace RecipeFinder.Controllers
{
    public class RecipeController : Controller
    {
        private readonly MealApiService _mealService;

        public RecipeController(MealApiService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.Error = "Please enter a meal name to search.";
                return View();
            }

            var data = await _mealService.SearchMealsAsync(searchTerm);

            if (data == null || data.meals == null)
            {
                ViewBag.Error = "No recipes found for that keyword.";
            }

            return View(data);
        }

        // NEW METHOD for View Details
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            var data = await _mealService.GetMealByIdAsync(id);

            if (data == null || data.meals == null || data.meals.Count == 0)
            {
                ViewBag.Error = "Recipe details not found.";
                return RedirectToAction("Index");
            }

            var meal = data.meals.FirstOrDefault();
            return View(meal);
        }
    }
}
