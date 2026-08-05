using Capitulo001.Models;
using Capitulo001.Data;
using Microsoft.AspNetCore.Mvc;
////
///

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Capitulo001.Controllers // definição do namespace "Capitulo001.Controllers" que agrupa as classes relacionadas aos controladores da aplicação
{
    public class InstituicaoController : Controller // criação da classe InstituicaoController que herda de Controller
    {
        private readonly IESContext _context; // definição da variável "_context" privada do tipo IESContext para acessar o banco de dados (o "_" no início do nome da variável é uma convenção para indicar que é um campo privado)
        public InstituicaoController(IESContext context) // construtor da classe que recebe o parâmetro "context" do tipo IESContext
        {
            this._context = context; // atribuição do parâmetro "context" à variável "_context" para que possa ser usado em outros métodos da classe
        }
        public async Task<IActionResult> Index() // método assíncrono que retorna uma Task<IActionResult> e é responsável por exibir a lista de instituições
        {
            return View(await _context.Instituicoes.OrderBy(i => i.Nome).ToListAsync()); // retorna a view com a lista de instituições ordenadas pelo nome
        }

        // Action CREATE ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método GET - Exibe form para criação de nova instituição
        public IActionResult Create()
        {
            return View(); // como o método View() não recebe nenhum parâmetro, ele irá retornar a view "Create.cshtml"
        }

        // Método POST - Recebe os dados do formulário e cria uma nova instituição
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instituicao);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Action EDIT //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método GET - Exibe form para edição de uma instituição existente
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        // Método POST - Recebe os dados do formulário e atualiza a instituição existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoId,Nome,Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!InstituicaoExists(instituicao.InstituicaoID))
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

        private bool InstituicaoExists(long? id)
        {
            return _context.Instituicoes.Any(i => i.InstituicaoID == id);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // Action DETAILS ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método GET - Exibe uma view comdetalhes de uma instituição existente
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // Action DELETE /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método GET - Exibe uma view de confirmação para exclusão de uma instituição existente
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        // Método POST - Recebe a confirmação de exclusão e remove a instituição existente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////











    }


}