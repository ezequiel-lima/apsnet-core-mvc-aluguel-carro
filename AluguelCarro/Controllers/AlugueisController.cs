using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelCarro.Data;
using AluguelCarro.Models;

namespace AluguelCarro.Controllers
{
    public class AlugueisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlugueisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alugueis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aluguel.Include(a => a.Carro).Include(a => a.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alugueis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel
                .Include(a => a.Carro)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }

        // GET: Alugueis/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Placa");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Alugueis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarroId,UsuarioId,DataDevolucao")] Aluguel aluguel)
        {
            ModelState.Remove("Usuario");
            ModelState.Remove("Carro");
            
            if (ModelState.IsValid)
            {
                var carro = _context.Carro.FirstOrDefault(x => x.Id == aluguel.CarroId);

                if (carro.Status == true)
                    return RedirectToAction(nameof(Index));
                    
                carro.Status = !carro.Status;
                _context.Add(aluguel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Marca", aluguel.CarroId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", aluguel.UsuarioId);
            return View(aluguel);
        }

        // GET: Alugueis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Marca", aluguel.CarroId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName", aluguel.UsuarioId);
            return View(aluguel);
        }

        // POST: Alugueis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarroId,UsuarioId")] Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluguel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AluguelExists(aluguel.Id))
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
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Marca", aluguel.CarroId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", aluguel.UsuarioId);
            return View(aluguel);
        }

        // GET: Alugueis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel
                .Include(a => a.Carro)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }

        // POST: Alugueis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel != null)
            {
                _context.Aluguel.Remove(aluguel);
            }

            var carro = _context.Carro.FirstOrDefault(x => x.Id == aluguel.CarroId);
            carro.Status = false;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AluguelExists(int id)
        {
            return _context.Aluguel.Any(e => e.Id == id);
        }
    }
}
