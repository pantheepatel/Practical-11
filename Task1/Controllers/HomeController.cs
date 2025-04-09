using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task1.Models;
// Task 1 Definition
//Create an MVC project for the demo of CRUD functionality. The user should save the output to a static list created in the controller. The fields to be used for forms should be Name, Date of birth, Address, etc.
//There should be each action on the controller for create, update, delete & view
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
            },
            new UserModel()
            {
                Id = Guid.NewGuid(),
                Name="Nehee",
                Email="nehee@gmail.com",
                DateOfBirth=new DateTime(2008,10,31),
                Address="Address Demo 2",
                City="Ahmedabad"
            }
        };

        // Entry point for the application, will give the list of users
        public ActionResult Index()
        {
            return View(users);
        }

        // Getting the view of creating a new user
        public ActionResult Create()
        {
            return View();
        }
        // Creating a new user
        [HttpPost]
        public ActionResult Create(UserModel newUser)
        {
            newUser.Id = Guid.NewGuid();
            users.Add(newUser);
            return RedirectToAction("Index");
        }

        // To view specific user(by id)
        public ActionResult View(Guid id)
        {
            return View(users.FirstOrDefault(x => x.Id == id));
        }

        // getting the view of editing a user by id
        public ActionResult Edit(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return View(selectedUser);
        }
        // Editing the user
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

        // Getting the view of deleting a user by id
        public ActionResult Delete(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return View(selectedUser);
        }
        // Deleting the user with Post because forms supports get/post
        [HttpPost]
        public ActionResult DeleteConfirm(Guid id) 
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            users.Remove(selectedUser);
            return RedirectToAction("Index");
        }
    }
}