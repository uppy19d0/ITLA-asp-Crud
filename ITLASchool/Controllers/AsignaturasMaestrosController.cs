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
    public class AsignaturasMaestrosController : Controller
    {
        private readonly MyDbContext _context;

        public AsignaturasMaestrosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: AsignaturasMaestros
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.AsignaturasMaestros.Include(a => a.Asignaturas).Include(a => a.Profesores);
            return View(await myDbContext.ToListAsync());
        }

        // GET: AsignaturasMaestros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasMaestros = await _context.AsignaturasMaestros
                .Include(a => a.Asignaturas)
                .Include(a => a.Profesores)
                .FirstOrDefaultAsync(m => m.AsignaturasMaestrosID == id);
            if (asignaturasMaestros == null)
            {
                return NotFound();
            }

            return View(asignaturasMaestros);
        }

        // GET: AsignaturasMaestros/Create
        public IActionResult Create()
        {
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre");
            ViewData["ProfesoresID"] = new SelectList(_context.Profesores, "ProfesoresID", "Apellido");
            return View();
        }

        // POST: AsignaturasMaestros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignaturasMaestrosID,AsignaturasID,ProfesoresID")] AsignaturasMaestros asignaturasMaestros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignaturasMaestros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasMaestros.AsignaturasID);
            ViewData["ProfesoresID"] = new SelectList(_context.Profesores, "ProfesoresID", "Apellido", asignaturasMaestros.ProfesoresID);
            return View(asignaturasMaestros);
        }

        // GET: AsignaturasMaestros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasMaestros = await _context.AsignaturasMaestros.FindAsync(id);
            if (asignaturasMaestros == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasMaestros.AsignaturasID);
            ViewData["ProfesoresID"] = new SelectList(_context.Profesores, "ProfesoresID", "Apellido", asignaturasMaestros.ProfesoresID);
            return View(asignaturasMaestros);
        }

        // POST: AsignaturasMaestros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsignaturasMaestrosID,AsignaturasID,ProfesoresID")] AsignaturasMaestros asignaturasMaestros)
        {
            if (id != asignaturasMaestros.AsignaturasMaestrosID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturasMaestros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasMaestrosExists(asignaturasMaestros.AsignaturasMaestrosID))
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
            ViewData["AsignaturasID"] = new SelectList(_context.Asignaturas, "AsignaturasID", "Nombre", asignaturasMaestros.AsignaturasID);
            ViewData["ProfesoresID"] = new SelectList(_context.Profesores, "ProfesoresID", "Apellido", asignaturasMaestros.ProfesoresID);
            return View(asignaturasMaestros);
        }

        // GET: AsignaturasMaestros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturasMaestros = await _context.AsignaturasMaestros
                .Include(a => a.Asignaturas)
                .Include(a => a.Profesores)
                .FirstOrDefaultAsync(m => m.AsignaturasMaestrosID == id);
            if (asignaturasMaestros == null)
            {
                return NotFound();
            }

            return View(asignaturasMaestros);
        }

        // POST: AsignaturasMaestros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignaturasMaestros = await _context.AsignaturasMaestros.FindAsync(id);
            _context.AsignaturasMaestros.Remove(asignaturasMaestros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturasMaestrosExists(int id)
        {
            return _context.AsignaturasMaestros.Any(e => e.AsignaturasMaestrosID == id);
        }
    }
}
