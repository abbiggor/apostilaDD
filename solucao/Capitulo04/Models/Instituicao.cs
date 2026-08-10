namespace Capitulo04.Models
{
    public class Instituicao // definição da classe Instituicao
    {
        // definição das propriedades da classe
        public long? InstituicaoID { get; set; } // long? permite que o valor seja nulo
        public string Nome { get; set; }
        public string Endereco { get; set; }

        // Associação com a classe Departamento
        public virtual ICollection<Departamento>? Departamentos // virtual permite que a propriedade seja substituída em classes derivadas, ICollection<Departamento> é uma coleção de objetos do tipo Departamento
        {
            get; set;
        }

    }
}
