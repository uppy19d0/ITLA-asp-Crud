using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITLASchool.Models;

namespace ITLASchool.Controllers
{
    public class AsignaturasEstudiantesController : Controller
    {
        private readonly MyDbContext _context;

        public AsignaturasEstudiantesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: AsignaturasEstudiantes
        public async Task<IActionResult> MenuAsignStudents()
        {
            var myDbContext = _context.AsignaturasEstudiantes.Include(a => a.Asignaturas).Include(a => a.Estudiantes);
            return View(await myDbContext.ToListAsync());
        }

        // GET: AsignaturasEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasEstudiantes = await _context.AsignaturasEstudiantes
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.AsignaturasEstudiantesID == id);
            if (asignaturasEstudiantes == null)
            {
                return NotFound();
            }

            return View(asignaturasEstudiantes);
        }

        // GET: AsignaturasEstudiantes/Create
        public IActionResult CrearAsignStudents()
        {
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre");
            ViewData["EstudiantesID"] = new SelectList(_context.Estudiantes, "EstudiantesID", "Matricula");
            return View();
        }

        // POST: AsignaturasEstudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAsignStudents([Bind("AsignaturasEstudiantesID,EstudiantesID,AsignaturasID")] AsignaturasEstudiantes asignaturasEstudiantes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignaturasEstudiantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MenuAsignStudents));
            }
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasEstudiantes.AsignaturasID);
            ViewData["EstudiantesID"] = new SelectList(_context.Estudiantes, "EstudiantesID", "Matricula", asignaturasEstudiantes.EstudiantesID);
            return View(asignaturasEstudiantes);
        }

        // GET: AsignaturasEstudiantes/Editar/id
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasEstudiantes = await _context.AsignaturasEstudiantes.FindAsync(id);
            if (asignaturasEstudiantes == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasEstudiantes.AsignaturasID);
            ViewData["EstudiantesID"] = new SelectList(_context.Estudiantes, "EstudiantesID", "Matricula", asignaturasEstudiantes.EstudiantesID);
            return View(asignaturasEstudiantes);
        }

        // POST: AsignaturasEstudiantes/Editar/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("AsignaturasEstudiantesID,EstudiantesID,AsignaturasID")] AsignaturasEstudiantes asignaturasEstudiantes)
        {
            if (id != asignaturasEstudiantes.AsignaturasEstudiantesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturasEstudiantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasEstudiantesExists(asignaturasEstudiantes.AsignaturasEstudiantesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MenuAsignStudents));
            }
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasEstudiantes.AsignaturasID);
            ViewData["EstudiantesID"] = new SelectList(_context.Estudiantes, "EstudiantesID", "Matricula", asignaturasEstudiantes.EstudiantesID);
            return View(asignaturasEstudiantes);
        }

        // GET: AsignaturasEstudiantes/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasEstudiantes = await _context.AsignaturasEstudiantes
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.AsignaturasEstudiantesID == id);
            if (asignaturasEstudiantes == null)
            {
                return NotFound();
            }

            return View(asignaturasEstudiantes);
        }

        // POST: AsignaturasEstudiantes/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignaturasEstudiantes = await _context.AsignaturasEstudiantes.FindAsync(id);
            _context.AsignaturasEstudiantes.Remove(asignaturasEstudiantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MenuAsignStudents));
        }

        private bool AsignaturasEstudiantesExists(int id)
        {
            return _context.AsignaturasEstudiantes.Any(e => e.AsignaturasEstudiantesID == id);
        }
    }
}
