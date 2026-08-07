using Capitulo03.Models;
using Capitulo03.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Capitulo03.Controllers

{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        public DepartamentoController(IESContext context)
        {
            this._context = context;
        }

        // View e Action INDEX //////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        // Action CREATE ///////////////////////////////////////////////////////////////////////////////
        // Método GET 
        public IActionResult Create()
        {
            return View();
        }
        // Método POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível realizar a inserção dos dados.");
            }
            return View(departamento);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////

        //	Action EDIT ////////////////////////////////////////////////////////////////////////////
        //	Método GET 
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        // Método POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID,Nome")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))
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
            return View(departamento);
        }
        private bool DepartamentoExists(long? id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoID == id);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////

        // Action DETAILS //////////////////////////////////////////////////////////////////////////
        // Método GET 
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);

        }
        //////////////////////////////////////////////////////////////////////////////////////////////

        // Action DELETE ////////////////////////////////////////////////////////////////////////////
        // Método GET 
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // Método POST - action DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction(nameof(Index));

        }
        //////////////////////////////////////////////////////////////////////////////////////////////
    }
}
