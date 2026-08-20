using Modelo.Cadastros;
using System.Linq;
using projeto_mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace projeto_mvc.Data.DAL.Cadastros
{
    public class DepartamentoDAL                         // cria classe pública chamada InstituicaoDAL (Data Access Layer) que será responsável por acessar os dados da entidade Instituicao no banco de dados
    {
        private IESContext _context;                    // cria variável privada do tipo IESContext para acessar o banco de dados
        public DepartamentoDAL(IESContext context)       // construtor da classe que recebe o parâmetro "context" do tipo IESContext
        {
            _context = context;
        }


        public IQueryable<Departamento> ObterDepartamentosClassificadosPorInstituicao()          // cria método público que retorna um IQueryable de Departamentos     
        {
            return _context.Departamentos.Include(i => i.Instituicao).OrderBy(b => b.Nome);                          // retorna todas as instituições ordenadas pelo nome
        }

        public async Task<Departamento> ObterDepartamentoPorID(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);  // retorna a instituição com o ID especificado, incluindo os departamentos relacionados
            _context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
            return departamento;
        }

        public async Task<Departamento> GravarDepartamento(Departamento departamento)
        {
            if (departamento.DepartamentoID == null)
            {
                _context.Departamentos.Add(departamento);
            }
            else
            {
                _context.Update(departamento);
            }
            await _context.SaveChangesAsync();
            return departamento;
        }

        public async Task<Departamento> EliminarDepartamentoPorID(long id)
        {
            Departamento departamento = await ObterDepartamentoPorID(id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }

        public IQueryable<Departamento> ObterDepartamentosPorInstituicao(long instituicaoID)
        {
            var departamentos = _context.Departamentos.Where(d => d.InstituicaoID == instituicaoID).OrderBy(d => d.Nome);
            return departamentos;
        }


            
    }
}

