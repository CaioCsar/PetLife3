using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetLife.Data;
using PetLife.Models;

namespace PetLife.Controllers
{
    public class MedicamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicamento.Include(a => a.Animal).OrderBy(n => n.nomeMed).ToListAsync());
        }

        // GET: Medicamento/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamento
                .FirstOrDefaultAsync(m => m.idMed == id);
            _context.Animal.Where(i => medicamento.idAnimal == i.idAnimal).Load();
            var animal = _context.Animal.OrderBy(i => i.nomeAnimal).ToList();
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // GET: Medicamento/Create
        public IActionResult Create()
        {
            var animal = _context.Animal.OrderBy(i => i.nomeAnimal).ToList();
            animal.Insert(0, new Animal() { idAnimal = 0, nomeAnimal = "Selecione um Pet" });
            ViewBag.Animals = animal;
            return View();
        }

        // POST: Medicamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMed,nomeMed,tipoMed,dataInicioMed,dosagemMed,periodoUsoMed,intervaloUsoMed,observacoesMed,idAnimal")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicamento);
        }

        // GET: Medicamento/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamento.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            ViewBag.Animals = new SelectList(_context.Animal.OrderBy(b => b.nomeAnimal), "idAnimal", "nomeAnimal");
            return View(medicamento);
        }

        // POST: Medicamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("idMed,nomeMed,tipoMed,dataInicioMed,dosagemMed,periodoUsoMed,intervaloUsoMed,observacoesMed,idAnimal")] Medicamento medicamento)
        {
            if (id != medicamento.idMed)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.idMed))
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
            return View(medicamento);
        }

        // GET: Medicamento/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamento
                .FirstOrDefaultAsync(m => m.idMed == id);
            _context.Animal.Where(i => medicamento.idAnimal == i.idAnimal).Load();
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // POST: Medicamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var medicamento = await _context.Medicamento.FindAsync(id);
            _context.Medicamento.Remove(medicamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentoExists(long? id)
        {
            return _context.Medicamento.Any(e => e.idMed == id);
        }
    }
}
