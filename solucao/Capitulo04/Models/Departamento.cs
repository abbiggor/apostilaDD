namespace Capitulo04.Models
{
    public class Departamento
    {
        public long? DepartamentoID { get; set; }
        public string Nome { get; set; }

        // Associação com a classe Instituicao
        public long? InstituicaoID { get; set; }
        public Instituicao? Instituicao { get; set; }
    }
}