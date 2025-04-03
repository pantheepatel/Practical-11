using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.Models;

namespace Task2.Controllers
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
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", users);
        }
        
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        
        [HttpPost]
        public ActionResult Create(UserModel newUser)
        {
            newUser.Id = Guid.NewGuid();
            users.Add(newUser);
            return PartialView("_List", users);
        }

        public ActionResult View(Guid id)
        {
            UserModel selectedUser = users.FirstOrDefault(x => x.Id == id);
            return PartialView("_View", selectedUser);
        }

        public ActionResult Edit(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return PartialView("_Edit", selectedUser);
        }
        
        [HttpPost]
        public ActionResult Edit(UserModel updatedUser)
        {
            UserModel selectedUser = users.Where(user => user.Id == updatedUser.Id).FirstOrDefault();
            if (selectedUser != null)
            {
                // Update existing user properties instead of replacing the object reference
                selectedUser.Name = updatedUser.Name;
                selectedUser.Email = updatedUser.Email;
                selectedUser.DateOfBirth = updatedUser.DateOfBirth;
                selectedUser.Address = updatedUser.Address;
                selectedUser.City = updatedUser.City;
            }
            return PartialView("_List", users);
        }

        public ActionResult Delete(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            return PartialView("_Delete", selectedUser);
        }
        
        [HttpPost]
        public ActionResult DeleteConfirm(Guid id)
        {
            UserModel selectedUser = users.Where(user => user.Id == id).FirstOrDefault();
            users.Remove(selectedUser); 
            return PartialView("_List", users);
        }
    }
}