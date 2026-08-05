namespace Capitulo001.Models
{
    public class Instituicao // definição da classe Instituicao
    {
        // definição das propriedades da classe
        public long? InstituicaoID { get; set; } // long? permite que o valor seja nulo
        public string Nome { get; set; }
        public string Endereco { get; set; }
    }
}
