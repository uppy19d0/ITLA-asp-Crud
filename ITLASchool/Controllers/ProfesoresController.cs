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
    public class ProfesoresController : Controller
    {
        private readonly MyDbContext _context;

        public ProfesoresController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> MenuProf(string nombre)
        {
            if (nombre != null)
            {
                var filter = _context.Profesores.Where(s => s.Nombre.Contains(nombre));
                return View(filter);
            }
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesores/Details/ID
        public async Task<IActionResult> DetalleProf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .FirstOrDefaultAsync(m => m.ProfesoresID == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // GET: Profesores/Create
        public IActionResult CrearProf()
        {
            return View();
        }

        // POST: Profesores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProf([Bind("ProfesoresID,Nombre,Apellido")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MenuProf));
            }
            return View(profesores);
        }

        // GET: Profesores/Edit/ID
        public async Task<IActionResult> EditarProf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores.FindAsync(id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }

        // POST: Profesores/Edit/ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProf(int id, [Bind("ProfesoresID,Nombre,Apellido")] Profesores profesores)
        {
            if (id != profesores.ProfesoresID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresExists(profesores.ProfesoresID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MenuProf));
            }
            return View(profesores);
        }

        // GET: Profesores/Delete/ID
        public async Task<IActionResult> BorrarProf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .FirstOrDefaultAsync(m => m.ProfesoresID == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // POST: Profesores/Delete/ID
        [HttpPost, ActionName("BorrarProf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesores = await _context.Profesores.FindAsync(id);
            _context.Profesores.Remove(profesores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MenuProf));
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.ProfesoresID == id);
        }
    }
}
