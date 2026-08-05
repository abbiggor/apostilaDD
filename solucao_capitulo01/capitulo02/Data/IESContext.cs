using capitulo02.Models;
using capitulo01.Models;
using Microsoft.EntityFrameworkCore;

namespace capitulo01.Models
{
    public class Instituicao
    {
        public long? InstituicaoID { get; set; }
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
    }
}

namespace capitulo02.Data
{
    public class IESContext:DbContext
    {
        // método construtor da classe IESContext
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }

    }
}
