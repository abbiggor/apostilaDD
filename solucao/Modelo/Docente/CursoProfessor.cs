using Modelo.Cadastros;

namespace Modelo.Docente
{
    public class CursoProfessor
    {
        public long? CursoID { get; set; }
        public Curso NomeCurso { get; set; }
        public long? ProfessorID { get; set; }
        public Professor NomeProfessor { get; set; }
    }
}
