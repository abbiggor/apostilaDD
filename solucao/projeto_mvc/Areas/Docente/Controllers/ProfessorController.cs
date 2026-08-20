using Microsoft.AspNetCore.Mvc;
using Modelo.Cadastros;
using Modelo.Docente;
using projeto_mvc.Data;
using projeto_mvc.Data.DAL.Cadastros;
using projeto_mvc.Data.DAL.Discente;
using projeto_mvc.Data.DAL.Docente;

namespace projeto_mvc.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class ProfessorController : Controller
    {

        readonly IESContext _context;   
        private readonly InstituicaoDAL instituicaoDAL;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly CursoDAL cursoDAL;
        private readonly ProfessorDAL professorDAL;

        public ProfessorController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
            cursoDAL = new CursoDAL(context);
            professorDAL = new ProfessorDAL(context);
        }

        public void PrepararViewBags(List<Instituicao> instituicoes, List<Departamento> departamentos, List<Curso> cursos, List<Professor> professores)
        {
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;
            departamentos.Insert(0, new Departamento() { DepartamentoID = 0, Nome = "Selecione o departamento" });
            ViewBag.Departamentos = departamentos;
            cursos.Insert(0, new Curso() { CursoID = 0, Nome = "Selecione o curso" });
            ViewBag.Cursos = cursos;
            professores.Insert(0, new Professor() { ProfessorID = 0, Nome = "Selecione o professor" });
            ViewBag.Professores = professores;
        }
    }


}
