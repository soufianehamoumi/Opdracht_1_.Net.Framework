using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studentenbeheer.Data;
using Studentenbeheer.Models;

namespace Studentenbeheer.Controllers
{
    
    public class StudentsController : Controller
    {
        private readonly StudentenbeheerContext _context;

        public StudentsController(StudentenbeheerContext context)
        {
            _context = context;
        }

        // GET: Students
        
        public async Task<IActionResult> Index(string naam, char gender,string orderBy)
        {
            var studentenbeheerContext = _context.Student.Include(s => s.Gender);
            var students = from s in _context.Student where s.Deleted > DateTime.Now select s;


            if (!string.IsNullOrEmpty(naam) )
                students = from s in students
                         where s.Voornaam.Contains(naam) || s.Achternaam.Contains(naam) && s.GenderID == gender && s.Deleted > DateTime.Now
                           select s;

            switch (orderBy)
            {
                case "Achternaam":
                    students = students.OrderBy(m => m.Achternaam);
                    break;
                case "Voornaam":
                    students = students.OrderBy(m => m.Voornaam);
                    break;
                case "Geslacht":
                    students = students.OrderBy(m => m.GenderID);
                    break;
                default:
                    students = students.OrderBy(m => m.Achternaam);
                    break;
            }
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["GenderID"] = new SelectList(_context.Gender, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Voornaam,Achternaam,Geboortedatum,GenderID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "Id", "Id", student.GenderID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "Id", "Id", student.GenderID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Voornaam,Achternaam,Geboortedatum,GenderID")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "Id", "Id", student.GenderID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            //_context.Student.Remove(student);
            student.Deleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
