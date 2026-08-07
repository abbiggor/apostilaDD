using Capitulo03.Models;
using Microsoft.EntityFrameworkCore;
namespace Capitulo03.Data
{
    public class IESContext : DbContext // definição da classe IESContext que herda de DbContext, que é a classe base para trabalhar com o Entity Framework Core
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options) // construtor da classe IESContext que recebe as opções de configuração do DbContext
        {
        }

        //mapeamento da classe para o modelo relacional
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
    }
}
