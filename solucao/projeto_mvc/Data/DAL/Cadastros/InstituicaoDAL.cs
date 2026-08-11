using Modelo.Cadastros;
using System.Linq;
using projeto_mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace projeto_mvc.Data.DAL.Cadastros
{
    public class InstituicaoDAL                         // cria classe pública chamada InstituicaoDAL (Data Access Layer) que será responsável por acessar os dados da entidade Instituicao no banco de dados
    {
        private IESContext _context;                    // cria variável privada do tipo IESContext para acessar o banco de dados
        public InstituicaoDAL(IESContext context)       // construtor da classe que recebe o parâmetro "context" do tipo IESContext
        {
            _context = context;
        }

      
        public IQueryable<Instituicao> ObterInstituicoesClassificadasPorNome()          // cria método público que retorna um IQueryable de Instituicao
        {
            return _context.Instituicoes.OrderBy(b => b.Nome);                          // retorna todas as instituições ordenadas pelo nome
        }

        public async Task<Instituicao> ObterInstituicaoPorID(long? id)
        {
            return await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoID == id);  // retorna a instituição com o ID especificado, incluindo os departamentos relacionados
        }

        public async Task<Instituicao> GravarInstituicao(Instituicao instituicao)
        {
            if (instituicao.InstituicaoID == null)
            {
                _context.Instituicoes.Add(instituicao);
            }
            else
            {
                _context.Update(instituicao);
            }
            await _context.SaveChangesAsync();
            return instituicao;
        }

        public async Task<Instituicao> EliminarInstituicaoPorID(long id)
        {
            Instituicao instituicao = await ObterInstituicaoPorID(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        }

        internal async Task EliminarInstituicaoPorIid(long? id)
        {
            throw new NotImplementedException();
        }
    }
}

