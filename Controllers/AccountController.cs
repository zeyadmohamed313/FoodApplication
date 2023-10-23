using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FoodApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager
			,SignInManager<ApplicationUser>signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public async Task<IActionResult>Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel Login ,string? returnUrl)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{ Email = Login.Email };
                var ressult = await _signInManager.PasswordSignInAsync(Login.Email, Login.Password, false, false);
				if (ressult.Succeeded)
				{	if (!string.IsNullOrEmpty(returnUrl))
						LocalRedirect(returnUrl);
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(Login);
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel Register)
		{
			if(ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{
					Name = Register.Name,
					Email = Register.Email,
					Address = Register.Address,
					UserName = Register.Email
				};
                var result = await _userManager.CreateAsync(user, Register.Password);
				if (result.Succeeded)
				{
					await _signInManager.PasswordSignInAsync(user,Register.Password ,false, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach(var it in result.Errors)
					{
						ModelState.AddModelError("", it.Description);
					}
				}
            }
            return View(Register);
		}
	}
}
