﻿using MeetUp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    var response = new LoginViewModel();
        //    return View(response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        //{
        //    if (!ModelState.IsValid) return View(loginViewModel);

        //    var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        //    if (user != null)
        //    {
        //        //User is found, check password
        //        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
        //        if (passwordCheck)
        //        {
        //            //Password correct, sign in
        //            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        //            if (result.Succeeded)
        //            {
        //                Console.WriteLine(_signInManager.IsSignedIn(user));
        //                return RedirectToAction("Index", "Index");
        //            }
        //        }
        //        //Password is incorrect
        //        TempData["Error"] = "Wrong credentials. Please try again";
        //        return View(loginViewModel);
        //    }
        //    //User not found
        //    TempData["Error"] = "Wrong credentials. Please try again";
        //    return View(loginViewModel);
        //}

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    var response = new RegisterViewModel();
        //    return View(response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid) return View(registerViewModel);

        //    var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
        //    if (user != null)
        //    {
        //        TempData["Error"] = "This email address is already in use";
        //        return View(registerViewModel);
        //    }

        //    var newUser = new AppUser()
        //    {
        //        Email = registerViewModel.EmailAddress,
        //        UserName = registerViewModel.Username
        //    };
        //    var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        //    if (newUserResponse.Succeeded)
        //        await _userManager.AddToRoleAsync(newUser, Data.UserRoles.User);

        //    return RedirectToAction("Index", "Race");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Race");
        //}

        //[HttpGet]
        //[Route("Account/Welcome")]
        //public async Task<IActionResult> Welcome(int page = 0)
        //{
        //    if (page == 0)
        //    {
        //        return View();
        //    }
        //    return View();

        //}

    }
}