using Capitulo03.Data;
using Capitulo03.Models;
using System.Linq;
namespace Capitulo03.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureCreated();
            if (context.Instituicoes.Any())
            {
                return;
            }
            var instituicoes = new Instituicao[]
            {
                new Instituicao {Nome="UniPR", Endereco="Parana"},
                new Instituicao {Nome="UniRS", Endereco="Rio Grande do Sul"}
            };
            foreach (Instituicao i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }
            context.SaveChanges();
        }
    }
}
