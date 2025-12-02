using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_management_system.model;

namespace Student_management_system.Controllers // Fix: Use plural 'Controllers' namespace
{
    [Authorize]
    public class StudentController : Controller // Fix: Now refers to Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDBContext _context;
        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }
        //Index garne method with search and pagination
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var q = _context.students.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                q = q.Where(s => s.FullName.Contains(search) || s.Email.Contains(search) || s.EnrollmentNumber.Contains(search));
            }
            var model = await q.OrderBy(s => s.FullName).ToListAsync();
            return View(model);
        }
        //Details garne method for student
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            var student = await _context.students.FirstOrDefaultAsync(s => s.Id == id); // find garne
            if (student == null) return NotFound(); // if not found
            return View(student);
        }

        //Create garne method for student
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(students student)
        {
            if (!ModelState.IsValid) return View(student); // validate check garne
            _context.students.Add(student); // add garne
            await _context.SaveChangesAsync(); // save garne
            return RedirectToAction(nameof(Index)); // redirect to index
        }
        //Edit garne method for student
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.students.FindAsync(id);// find garne
            if (student == null) return NotFound();// if not found
            return View(student);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id, students student)
        {
            if (id != student.Id) return NotFound(); // if not found
            if (!ModelState.IsValid) return View(student); // validate check garne
            _context.students.Update(student); // update garne
            await _context.SaveChangesAsync(); // save garne
            return RedirectToAction(nameof(Index)); // redirect to index
        }

        //Delete garne method for student
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.students.FindAsync(id); // find garne
            if (student == null) return NotFound(); // if not found
            return View(student);
        }
        [HttpPost, ActionName("DeleteConfirmed"), ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.students.FindAsync(id); // find garne
            if (student == null) return NotFound(); // if not found
            _context.students.Remove(student); // remove garne
            await _context.SaveChangesAsync(); // save garne
            return RedirectToAction(nameof(Index)); // redirect to index}
        }
    }
}
