using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wall2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using wall2.Factory;
using Microsoft.AspNetCore.Http;

namespace wall2.Controllers
{
    public class MessageController : Controller
    {
       private readonly MessageFactory messageFactory;
        private readonly UserFactory userFactory;

        public MessageController(MessageFactory message, UserFactory user) {
            messageFactory = message;
            userFactory = user;
        }

        // GET: /Home/
        [HttpGet]
        [Route("message")]
        public IActionResult Index()
        {
            ViewBag.comments = messageFactory.FindAllComments();
            ViewBag.messages = messageFactory.FindAll();
            return View("Message");

        }

        [HttpPost]
        [Route("AddMessage")]

        public IActionResult AddMessage(Message newMessage)
        { 
            if(ModelState.IsValid){
                // userFactory.Find
                newMessage.user_id = (int)HttpContext.Session.GetInt32("user_id");
                messageFactory.Add(newMessage);
                return Redirect("message");
            }
            
            // return View(user);
            return Redirect("message");
        }

         [HttpPost]
        [Route("AddComment/{id}")]

        public IActionResult AddComment(string comment, int id)
        { 
            if(ModelState.IsValid){
                HttpContext.Session.SetInt32("user_id", 35);
                int user_id= (int)HttpContext.Session.GetInt32("user_id");
                int message_id = id;
                messageFactory.AddComment(message_id, comment,user_id);
                ViewBag.messages = messageFactory.FindAll();
                return RedirectToAction("Index");
            }
            
            // return View(user);
            ViewBag.messages = messageFactory.FindAll();
            return RedirectToAction("Index");
        }

    }
}
