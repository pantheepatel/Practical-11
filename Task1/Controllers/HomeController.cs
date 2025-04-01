using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task1.Models;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        static List<UserModel> users = new List<UserModel>()
        {
            new UserModel()
            {
                Id = Guid.NewGuid(),
                Name="Panthee",
                Email="panthee@gmail.com",
                DateOfBirth=new DateTime(2004,08,18),
                Address="Address Demo 1",
                City="Ahmedabad"
            }
        };
        public ActionResult Index()
        {
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel newUser)
        {
            newUser.Id = Guid.NewGuid();
            users.Add(newUser);
            return RedirectToAction("Index");
        }

        public ActionResult View(Guid id)
        {
            return View(users.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult Edit(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return View(selectedUser);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, UserModel updatedUser)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            if (selectedUser != null)
            {
                // Update existing user properties instead of replacing the object reference
                selectedUser.Name = updatedUser.Name;
                selectedUser.Email = updatedUser.Email;
                selectedUser.DateOfBirth = updatedUser.DateOfBirth;
                selectedUser.Address = updatedUser.Address;
                selectedUser.City = updatedUser.City;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return View(selectedUser);
        }
        [HttpPost]
        public ActionResult DeleteConfirm(Guid id) 
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            users.Remove(selectedUser);
            return RedirectToAction("Index");
        }
    }
}