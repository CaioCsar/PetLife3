using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetLife.Data;
using PetLife.Data.Migrations;
using PetLife.Models;
using Animal = PetLife.Models.Animal;

namespace PetLife.Controllers
{
    public class VacinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vacina
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vacina.Include(a => a.Animal).OrderBy(n => n.nomeVacina).ToListAsync());
        }

        // GET: Vacina/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacina
                .FirstOrDefaultAsync(m => m.idVacina == id);
            _context.Animal.Where(i => vacina.idAnimal == i.idAnimal).Load();
            var animal = await _context.Animal.Include(a => a.Medicamentos).SingleOrDefaultAsync(m => m.idAnimal == id);
            if (vacina == null)
            {
                return NotFound();
            }

            return View(vacina);
        }

        // GET: Vacina/Create
        public IActionResult Create()
        {
            var animal = _context.Animal.OrderBy(i => i.nomeAnimal).ToList();
            animal.Insert(0, new Animal() { idAnimal = 0, nomeAnimal = "Selecione um Pet" });
            ViewBag.Animals = animal;
            return View();
        }

        // POST: Vacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idVacina,nomeVacina,dataAplicacaoVacina,localAplicacaoVacina,dataProximaVacina,observacaoVacina,idAnimal")] Vacina vacina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacina);
        }

        // GET: Vacina/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacina.FindAsync(id);
            if (vacina == null)
            {
                return NotFound();
            }
            ViewBag.Animals = new SelectList(_context.Animal.OrderBy(b => b.nomeAnimal), "idAnimal", "nomeAnimal", vacina.idAnimal);
            return View(vacina);
        }

        // POST: Vacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("idVacina,nomeVacina,dataAplicacaoVacina,localAplicacaoVacina,dataProximaVacina,observacaoVacina,fotoCertificadoVacina,FotoMimeType,idAnimal")] Vacina vacina, IFormFile foto)
        {
            if (id != vacina.idVacina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (foto != null)
                    {
                        var stream = new MemoryStream();
                        await foto.CopyToAsync(stream);
                        vacina.fotoCertificadoVacina = stream.ToArray();
                        vacina.FotoMimeType = foto.ContentType;

                    }

                    _context.Update(vacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacinaExists(vacina.idVacina))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vacina);
        }

        // GET: Vacina/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacina
                .FirstOrDefaultAsync(m => m.idVacina == id);
            _context.Animal.Where(i => vacina.idAnimal == i.idAnimal).Load();
            if (vacina == null)
            {
                return NotFound();
            }

            return View(vacina);
        }

        // POST: Vacina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var vacina = await _context.Vacina.FindAsync(id);
            _context.Vacina.Remove(vacina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacinaExists(long? id)
        {
            return _context.Vacina.Any(e => e.idVacina == id);
        }

        //GET FOTO PARA DETAILS
        public async Task<FileContentResult> GetFoto(long id)
        {
            Vacina vacina = await _context.Vacina.FindAsync(id);
            if (vacina != null)
            {
                return File(vacina.fotoCertificadoVacina, vacina.FotoMimeType);
            }
            return null;
        }
    }
}
