namespace projeto_mvc.Data.DAL.Docente
{
    public class ProfessorDAL
    {
        private IESContext _context;
        public ProfessorDAL(IESContext context)
        {
            _context = context;
        }



        //
        public void Add(Modelo.Docente.Professor professor)
        {
            _context.Add(professor);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
