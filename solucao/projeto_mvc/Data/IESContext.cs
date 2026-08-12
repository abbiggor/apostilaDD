using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using projeto_mvc.Models;
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
        public DbSet<Modelo.Cadastros.Curso> Cursos { get; set; }
        public DbSet<Modelo.Cadastros.Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoDisciplina>()
                            .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(c => c.Curso)
                            .WithMany(cd => cd.CursosDisciplinas)
                            .HasForeignKey(c => c.CursoID);
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(d => d.Disciplina)
                            .WithMany(cd => cd.CursosDisciplinas)
                            .HasForeignKey(d => d.DisciplinaID);
        }

    }
}
