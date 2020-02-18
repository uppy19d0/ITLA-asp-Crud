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
    public class EstudiantesController : Controller
    {
        private readonly MyDbContext _context;

        public EstudiantesController(MyDbContext context)
        {
            _context = context;
        }
        // GET: Estudiantes
        public async Task<IActionResult> MenuEs(string nombre)
        {
            var filter = _context.Estudiantes.Where(s => s.Nombre.Contains(nombre));
            if (nombre != null)
            {

                return View(filter);
            }

            return View(await _context.Estudiantes.ToListAsync());


        }

        // GET: Estudiantes/Detalle/ID
        public async Task<IActionResult> DetalleEs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.EstudiantesID == id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // GET: Estudiantes/Create
        public IActionResult CrearEs()
        {
            return View();
        }

        // POST: Estudiantes/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEs([Bind("EstudiantesID,Matricula,Nombre,Apellido")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MenuEs));
            }
            return View(estudiantes);
        }

        // GET: Estudiantes/Editar/ID
        public async Task<IActionResult> EditarEs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes.FindAsync(id);
            if (estudiantes == null)
            {
                return NotFound();
            }
            return View(estudiantes);
        }

        // POST: Estudiantes/Editar/ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEs(int id, [Bind("EstudiantesID,Matricula,Nombre,Apellido")] Estudiantes estudiantes)
        {
            if (id != estudiantes.EstudiantesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiantesExists(estudiantes.EstudiantesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MenuEs));
            }
            return View(estudiantes);
        }

        // GET: Estudiantes/Borrar/ID
        public async Task<IActionResult> BorrarEs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.EstudiantesID == id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // POST: Estudiantes/Borrar/ID
        [HttpPost, ActionName("BorrarEs")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiantes = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MenuEs));
        }

        private bool EstudiantesExists(int id)
        {
            return _context.Estudiantes.Any(e => e.EstudiantesID == id);
        }
    }
}
