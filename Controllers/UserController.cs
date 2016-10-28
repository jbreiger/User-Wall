using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wall2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using wall2.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace wall2.Controllers
{
    public class UserController : Controller
    {
       private readonly UserFactory userFactory;

        public UserController(UserFactory user) {
            userFactory = user;
        }

        // GET: /Home/
        [HttpGet]
        [Route("register")]
        public IActionResult Index()
        {
            // return View("Index", userFactory.FindAll());
            return View("Register");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RouteAttribute("register")]
        public IActionResult Register(User newUser)
        { 
            if(ModelState.IsValid){
                 PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                userFactory.Add(newUser);
                var user = userFactory.FindbyEmail(newUser.email);
               int user_id = (int)user.id;
                HttpContext.Session.SetInt32("user_id", user_id);

                return Redirect("message");
            }
            
            // return View(user);
            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RouteAttribute("login")]
        public IActionResult Login(User newUser)
        { 
            var user = userFactory.FindbyEmail(newUser.email);

            // if(ModelState.IsValid){
                if(user != null && newUser.password != null){
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(user, user.password, newUser.password)){
                        int user_id = (int)user.id;
                        HttpContext.Session.SetInt32("user_id", user_id);
                        return Redirect("message");
                    }
                }
            // }
            TempData["wrong"] = "Incorrect login inforrmation";
            return RedirectToAction("Index");
        }
        
    }
}
