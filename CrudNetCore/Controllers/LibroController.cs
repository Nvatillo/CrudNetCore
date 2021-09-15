using CrudNetCore.Models;
using CrudNetCore.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudNetCore.Controllers
{
    public class LibroController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public LibroController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        //Http Get Index
        public IActionResult Index()
        {
            IEnumerable<Libro> ListLibro = _dbContext.Libro;
            
            return View(ListLibro);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Libro.Add(libro);
                _dbContext.SaveChanges();

                TempData["Mensaje"] = "El Libro se ha creado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var libro = _dbContext.Libro.Find(id);

            if(libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Libro.Update(libro);
                _dbContext.SaveChanges();

                TempData["Mensaje"] = "El Libro se ha actualizado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var libro = _dbContext.Libro.Find(id);

            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteLibro(int? id)
        {
            var libro = _dbContext.Libro.Find(id);
            if (libro == null)
            {
                return NotFound();
            }
            
                _dbContext.Libro.Remove(libro);
                _dbContext.SaveChanges();

                TempData["Mensaje"] = "El Libro se ha eliminado correctamente";
                return RedirectToAction("Index");
            
        }
    }
}
