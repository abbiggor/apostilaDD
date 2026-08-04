using capitulo01.Models;
using Microsoft.AspNetCore.Mvc;

namespace capitulo01.Controllers
{
    public class InstituicaoController : Controller
    {
        private static IList<Instituicao> instituicoes =
            new List<Instituicao>()
            {
                new Instituicao()
                {
                    InstituicaoID = 1,
                    Nome ="UniParana",
                    Endereco = "Parana"
                },
                new Instituicao()
                {
                    InstituicaoID = 2,
                    Nome = "UniSanta",
                    Endereco = "Santa Catarina"
                },
                new Instituicao()
                {
                    InstituicaoID= 3,
                    Nome = "UniRS",
                    Endereco = "Rio Grande do Sul"
                },
                new Instituicao()
                {
                    InstituicaoID = 4,
                    Nome = "UniSP",
                    Endereco = "São Paulo"
                },
                new Instituicao()
                {
                    InstituicaoID = 5,
                    Nome = "UniMG",
                    Endereco = "Minas Gerais"
                }

            };

        public ActionResult Create()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View(instituicoes.OrderBy(i => i.Nome));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instituicao instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.InstituicaoID = instituicoes.Select(i => i.InstituicaoID).Max() + 1;
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long id) // Esta action Edit faz um método GET, que recebe o id da instituição e retorna a view com os dados da instituição para edição
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instituicao instituicao) // Método POST
        {
            //instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
            //instituicoes.Add(instituicao);
            // o código acima remove o registro selecionado e adiciona um novo com os dados atualizados
            // enquanto o código ativo abaixo atualiza o registro selecionado

            instituicoes[instituicoes.IndexOf(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First())] = instituicao;
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id) {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        public ActionResult Delete(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
            return RedirectToAction("Index");
        }
    }
}
