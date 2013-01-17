using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityLibrary.Core.Dao;
using CityLibrary.Web.Models;

namespace CityLibrary.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Member/

        public ActionResult Index()
        {
            var dao = new CityLibraryDao();

            var users = dao.GetUsers();

            //var firstUser = users.FirstOrDefault(u => u.FirstName == "Czesław");

            // nie moze byc nulla
            //var single = users.SingleOrDefault(u => u.UserID == 1);

            //if (single != null)
            //{
            //    var email = single.Email;
            //}

            //int sss = users.Where(u => u.FirstName.ToUpper().Contains("CZE")).Sum(u => u.LastName.Length);


            var members = users
                .Select(u => 
                    new UserModel
                    {
                        UserId = u.UserID,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        BirthDate = u.BirthDate,
                        Email = u.Email
                    }
            ).ToList();

            return View(members);
        }

        //
        // GET: /Member/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            var model = new UserModel()
            {
                AddDate = DateTime.Now,
                BirthDate = DateTime.Today.AddYears(-5),
                IsActive = true
            };

            return View(model);
        }

        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.BirthDate > DateTime.Now)
                {
                    ModelState.AddModelError("BirthDate", "Co ty z przyszłości? Czy w planach?.");
                    return View(user);
                }


                try
                {
                    var dao = new CityLibraryDao();

                    var u = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate,
                        Email = user.Email,
                        Phone = user.Phone,
                        AddDate = user.AddDate,
                        IsActive = user.IsActive
                    };

                    var err = dao.CreateUser(u);
                    if (err != null)
                    {
                        ModelState.AddModelError("", "Wystąpił problem podczas dodawania nowego użytkownika.");
                        return View(user);
                    }


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }


            
        }

        //
        // GET: /Member/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Member/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Member/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
