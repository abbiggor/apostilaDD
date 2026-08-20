using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using projeto_mvc.Data;
using projeto_mvc.Data.DAL.Cadastros;
using System.Linq;
using System.Threading.Tasks;

namespace projeto_mvc.Areas.Cadastros.Controllers

{
    [Area("Cadastros")]
    [Authorize]
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly InstituicaoDAL instituicaoDAL;
        public DepartamentoController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
        }



        // View e Action INDEX -------------------------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            return View(await departamentoDAL.ObterDepartamentosClassificadosPorInstituicao().ToListAsync());
        }

        //----------------------------------------------------------------------------------------------------------

        // Action CREATE --------------------------------------------------------------------------------------------
        // Método GET 
        public IActionResult Create()
        {
            var instituicoes = instituicaoDAL.ObterInstituicoesClassificadasPorNome().ToList();
            instituicoes.Insert(0, new Instituicao() // popula dropdownlist
            {
                InstituicaoID = 0,
                Nome = "Selecione a instituição"
            });
            ViewBag.Instituicoes = instituicoes;
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
                    await departamentoDAL.GravarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível realizar a inserção dos dados.");
            }
            // listagem abaixo implementada para repopular o dropdown list de instituições na view Create, caso ocorra algum erro de validação
            //var instituicoes = _context.Instituicoes.OrderBy(i => i.Nome).ToList();
            //instituicoes.Insert(0, new Instituicao()
            //{
            //    InstituicaoID = 0,
            //    Nome = "Selecione a instituição"
            //});
            //ViewBag.Instituicoes = instituicoes;

            return View(departamento);
        }
        //----------------------------------------------------------------------------------------------------------

        //	Action EDIT --------------------------------------------------------------------------------------------
        //	Método GET 
        public async Task<IActionResult> Edit(long? id)
        {
            ViewResult visaoDepartamento = (ViewResult)await ObterViewDepartamentoPorID(id);
            Departamento departamento = (Departamento)visaoDepartamento.Model;


            ViewBag.Instituicoes = new SelectList(instituicaoDAL.ObterInstituicoesClassificadasPorNome(), "InstituicaoID", "Nome", departamento.InstituicaoID);
            // listagem acima implementada para popular o dropdown list de instituições na view Edit, com a instituição do departamento selecionada
            return visaoDepartamento;
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
                    await departamentoDAL.GravarDepartamento(departamento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await DepartamentoExists(departamento.DepartamentoID))
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
            ViewBag.Instituicoes = new SelectList(instituicaoDAL.ObterInstituicoesClassificadasPorNome(), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return View(departamento);
        }
        private async Task<bool> DepartamentoExists(long? id)
        {
            return await departamentoDAL.ObterDepartamentoPorID((long)id) != null;
        }

        //----------------------------------------------------------------------------------------------------------

        // Action DETAILS ------------------------------------------------------------------------------------------
        // Método GET 
        public async Task<IActionResult> Details(long? id)
        {
            {
                return await ObterViewDepartamentoPorID(id);
            }


        }
        //----------------------------------------------------------------------------------------------------------

        // Action DELETE -------------------------------------------------------------------------------------------
        // Método GET 
        public async Task<IActionResult> Delete(long? id)
        {
            {
                return await ObterViewDepartamentoPorID(id);
            }
        }

        // Método POST - action DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await departamentoDAL.EliminarDepartamentoPorID((long)id);
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction(nameof(Index));

        }
         //----------------------------------------------------------------------------------------------------------

        // Método para buscar view por ID do departamento -----------------------------------------------------------
        private async Task<IActionResult> ObterViewDepartamentoPorID(long? id) // método privado que retorna uma Task<IActionResult> e recebe um parâmetro "id" do tipo long? e ser[a utilizado nos métodos GET das actions para obter a view de uma instituição específica, reduzindo redundância de código
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await departamentoDAL.ObterDepartamentoPorID((long)id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        //----------------------------------------------------------------------------------------------------------
    }
}
