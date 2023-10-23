using Microsoft.AspNetCore.Mvc;
using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FoodApplication.ContextDBConfig;

namespace FoodApplication.Controllers
{
    public class RecipeController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly FoodApplicationDBContext Context;
        public RecipeController(UserManager<ApplicationUser> userManager 
            , FoodApplicationDBContext dBContext)
        {
            _UserManager = userManager;
            Context = dBContext;

        }
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult GetRecipeCard([FromBody] List<Recipe> recipes)
        {
            return PartialView("_RecipeCard", recipes);
        }
        public IActionResult Search([FromQuery] string recipe)
        {
            ViewBag.Recipe = recipe;
            return View();
        }
        public IActionResult Order([FromQuery] string id) 
        {
            ViewBag.ID = id;
            return View();
        }
		[HttpPost]
        public async Task<IActionResult> ShowOrder(OrderRecipeDetails orderRecipeDetails) 
        {
            Random rand = new Random();
            ViewBag.price=rand.Next(150,500);
			var user = await _UserManager.GetUserAsync(HttpContext.User);
			ViewBag.userid = user?.Id;
			ViewBag.Address = user?.Address;
			return PartialView("_ShowOrder", orderRecipeDetails);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Order([FromForm]Order order)
        {
			order.OrderDate = DateTime.Now;
			if (ModelState.IsValid) 
            { 
                Context.Orders.Add(order);
                Context.SaveChanges();
                return RedirectToAction("Index","Recipe");
            }
            return RedirectToAction("Order", "Recipe", new {it=order.Id});
        }
	}
}
