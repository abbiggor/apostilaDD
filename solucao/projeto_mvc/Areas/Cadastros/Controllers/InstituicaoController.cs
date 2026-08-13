using Modelo.Cadastros;
using projeto_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using projeto_mvc.Data.DAL.Cadastros;




namespace projeto_mvc.Areas.Cadastros.Controllers // definição do namespace "projeto_mvc.Controllers" que agrupa as classes relacionadas aos controladores da aplicação
{
    [Area("Cadastros")]
    public class InstituicaoController : Controller // criação da classe InstituicaoController que herda de Controller
    {
        private readonly IESContext _context; // definição da variável "_context" privada do tipo IESContext para acessar o banco de dados (o "_" no início do nome da variável é uma convenção para indicar que é um campo privado)
        private readonly InstituicaoDAL instituicaoDAL; // definição da variável "_instituicaoDAL" privada do tipo InstituicaoDAL para acessar os dados da entidade Instituicao
        public InstituicaoController(IESContext context) // construtor da classe que recebe o parâmetro "context" do tipo IESContext
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context); 
        }

        private async Task<IActionResult> ObterViewInstituicaoPorID(long? id) // método privado que retorna uma Task<IActionResult> e recebe um parâmetro "id" do tipo long? e ser[a utilizado nos métodos GET das actions para obter a view de uma instituição específica, reduzindo redundância de código
        {
            if (id == null) 
            {
                return NotFound(); 
            }
            var instituicao = await instituicaoDAL.ObterInstituicaoPorID((long) id); 
            if (instituicao == null) 
            {
                return NotFound(); 
            }
            return View(instituicao);
        }   


        // Action INDEX --------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Index() // método assíncrono que retorna uma Task<IActionResult> e é responsável por exibir a lista de instituições
        {
            return View(await instituicaoDAL.ObterInstituicoesClassificadasPorNome().ToListAsync()); // retorna a view com a lista de instituições ordenadas pelo nome, a lista é obtida através do método ObterInstituicoesClassificadasPorNome() da classe InstituicaoDAL e convertida para uma lista assíncrona com ToListAsync()
        }

        // Action CREATE --------------------------------------------------------------------------------------------------------
        // Método GET - Exibe form para criação de nova instituição
        public IActionResult Create()
        {
            return View(); // como o método View() não recebe nenhum parâmetro, ele irá retornar a view "Create.cshtml"
        }
        //---------------------------------------------------------------------------------------------------------------------

        // Método POST - Recebe os dados do formulário e cria uma nova instituição
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await instituicaoDAL.GravarInstituicao(instituicao); // chama o método criado na DAL para gravar novo registro de instituição
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }
        //---------------------------------------------------------------------------------------------------------------------

        // Action EDIT --------------------------------------------------------------------------------------------------------
        // Método GET - Exibe form para edição de uma instituição existente
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterViewInstituicaoPorID(id);
        }

        // Método POST - Recebe os dados do formulário e atualiza a instituição existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await instituicaoDAL.GravarInstituicao(instituicao); // chama o método criado na DAL para gravar novo registro de instituição

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituicaoExists(id: instituicao.InstituicaoID))
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
            return View(instituicao);
        }

        private async Task<bool> InstituicaoExists(long? id)
        {
            return await instituicaoDAL.ObterInstituicaoPorID((long) id) != null;
        }
        //---------------------------------------------------------------------------------------------------------------------


        // Action DETAILS --------------------------------------------------------------------------------------------------------
        // Método GET - Exibe uma view comdetalhes de uma instituição existente
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterViewInstituicaoPorID(id);
        }
        //---------------------------------------------------------------------------------------------------------------------


        // Action DELETE --------------------------------------------------------------------------------------------------------
        // Método GET - Exibe uma view de confirmação para exclusão de uma instituição existente
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterViewInstituicaoPorID(id);
        }

        // Método POST - Recebe a confirmação de exclusão e remove a instituição existente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await instituicaoDAL.EliminarInstituicaoPorID((long) id);
          
            TempData["Message"] = "Instituição	" + instituicao.Nome.ToUpper() + "	foi	removida";
            return RedirectToAction(nameof(Index));
        }
        //---------------------------------------------------------------------------------------------------------------------











    }


}