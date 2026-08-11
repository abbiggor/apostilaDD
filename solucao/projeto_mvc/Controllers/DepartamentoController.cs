using projeto_mvc.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace projeto_mvc.Controllers

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
            //return View(await _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
            // retorno abaixo atualizado para carregamento forçado da propriedade de navegação Instituicao, para evitar o erro de carregamento lazy loading
            return View(await _context.Departamentos.Include(i => i.Instituicao).OrderBy(c => c.Nome).ToListAsync());
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////

        // Action CREATE ///////////////////////////////////////////////////////////////////////////////
        // Método GET 
        public IActionResult Create()
        {
            var instituicoes = _context.Instituicoes.OrderBy(i => i.Nome).ToList();
            instituicoes.Insert(0, new Instituicao()
            {
                InstituicaoID = 0,
                Nome = "Selecione a instituição"
            });
            ViewBag.Instituicoes = instituicoes;
            // listagem acima implementada para popular o dropdown list de instituições na view Create
            return View();
        }
        // Método POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, InstituicaoID")] Departamento departamento)
        {                                       // Bind acima atualizado para incluir a propriedade InstituicaoID, que é necessária para a criação de um novo departamento
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
            // listagem abaixo implementada para repopular o dropdown list de instituições na view Create, caso ocorra algum erro de validação
            var instituicoes = _context.Instituicoes.OrderBy(i => i.Nome).ToList();
            instituicoes.Insert(0, new Instituicao()
            {
                InstituicaoID = 0,
                Nome = "Selecione a instituição"
            });
            ViewBag.Instituicoes = instituicoes;

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

            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);
            // listagem acima implementada para popular o dropdown list de instituições na view Edit, com a instituição do departamento selecionada
            return View(departamento);
        }
        // Método POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID, Nome, InstituicaoID")] Departamento departamento)
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
            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);
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
            _context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
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
            _context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
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
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        //////////////////////////////////////////////////////////////////////////////////////////////
    }
}
