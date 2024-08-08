using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelCarro.Data;
using AluguelCarro.Models;
using Newtonsoft.Json;

namespace AluguelCarro.Controllers
{
    [Route("alugueis")]
    public class AlugueisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlugueisController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aluguel.Include(a => a.Carro).Include(a => a.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("detalhes/{id}")]
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

        [Route("criar")]
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Placa");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }
        
        [HttpPost("criar")]
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

        [Route("editar/{id}")]
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
            
            var carroAnterior = _context.Carro.FirstOrDefault(x => x.Id == aluguel.CarroId);

            TempData["CarroAnterior"] = JsonConvert.SerializeObject(carroAnterior);
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Placa", aluguel.CarroId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName", aluguel.UsuarioId);
            return View(aluguel);
        }
        
        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarroId,UsuarioId,DataDevolucao")] Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return NotFound();
            }
            
            ModelState.Remove("Usuario");
            ModelState.Remove("Carro");
            
            if (ModelState.IsValid)
            {
                try
                {
                    var carroAnterior = JsonConvert.DeserializeObject<Carro>(TempData["CarroAnterior"] as string ?? string.Empty);
                    var carro = _context.Carro.FirstOrDefault(x => x.Id == aluguel.CarroId);
                    
                    if (carro.Status != true)
                    {
                        _context.Attach(carroAnterior);
                        carroAnterior.Status = !carroAnterior.Status;
                        carro.Status = !carro.Status;
                        
                        _context.Update(aluguel);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AluguelExists(aluguel.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Marca", aluguel.CarroId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", aluguel.UsuarioId);
            return View(aluguel);
        }

        [Route("deletar/{id}")]
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
        
        [HttpPost("deletar/{id}"), ActionName("Delete")]
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
