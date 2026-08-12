using projeto_mvc.Models;
using Microsoft.EntityFrameworkCore;
namespace projeto_mvc.Data
{
    public class IESContext : DbContext // definição da classe IESContext que herda de DbContext, que é a classe base para trabalhar com o Entity Framework Core
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options) // construtor da classe IESContext que recebe as opções de configuração do DbContext
        {
        }

        //mapeamento da classe para o modelo relacional
        public DbSet<Modelo.Cadastros.Instituicao> Instituicoes { get; set; }
        public DbSet<Modelo.Cadastros.Departamento> Departamentos { get; set; }
    }
}
