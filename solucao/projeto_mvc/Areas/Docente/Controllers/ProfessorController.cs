using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using Modelo.Docente;
using projeto_mvc.Data;
using projeto_mvc.Data.DAL.Cadastros;
using projeto_mvc.Data.DAL.Docente;
using System.Text.Json.Serialization;

namespace projeto_mvc.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class ProfessorController : Controller
    {
        private readonly ProfessorDAL _context;

        public ProfessorController(ProfessorDAL context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(professor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(professor);
        }
    }


}
