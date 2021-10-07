using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDBEntities _db = new MoviesDBEntities();

        public ActionResult Home()
        {
            return View(_db.Movies1.ToList());
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Movies1.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }
        //GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="Id")] Movie movieToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            _db.Movies1.Add(movieToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        //GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var movieToEdit = (from m in _db.Movies1
                               where m.Id == id
                               select m).First(); 
            return View(movieToEdit);
        }
        //POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Movie movieToEdit)
        {
            var originalMovie = (from m in _db.Movies1
                                 where m.Id == movieToEdit.Id
                                 select m).First();
            if (!ModelState.IsValid)
            {
                return View(originalMovie);
            }

            _db.Entry(originalMovie).CurrentValues.SetValues(movieToEdit);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            //get the movie object with this id. Return object in view
            var movieToDelete = (from m in _db.Movies1
                                 where m.Id == id
                                 select m).First();
            return View(movieToDelete);
        }
        //POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Movie movieToDelete)
        {
            //remove from db table. return index view
            var originalMovie = _db.Movies1.Find(movieToDelete.Id);
            _db.Movies1.Remove(originalMovie);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }

}
