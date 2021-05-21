using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDesafioCustomIT.Data;
using AppDesafioCustomIT.Models;
using AppDesafioCustomIT.Models.ViewModels;
using System.Text.RegularExpressions;

namespace AppDesafioCustomIT.Controllers
{
    public class PessoasController : Controller
    {
        private readonly AppDesafioCustomITContext _context;

        public PessoasController(AppDesafioCustomITContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index(string filtraNome, string filtraCidade, string filtraUF)
        {
            var pessoas = from p in _context.Pessoa
                          select p;
            if (!string.IsNullOrEmpty(filtraNome))
            {
                pessoas = pessoas.Where(s => s.Nome.Contains(filtraNome));
            }
            if (!string.IsNullOrEmpty(filtraCidade))
            {
                pessoas = pessoas.Where(s => s.Cidade.Contains(filtraCidade));
            }
            if (!string.IsNullOrEmpty(filtraUF))
            {
                pessoas = pessoas.Where(s => s.UF.Contains(filtraUF));
            }
            return View(await pessoas.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNasc,CPF,Endereco,Bairro,UF,Cidade,Email")] Pessoa pessoa, Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                pessoa.CPF = pessoa.CPF.Replace(".", "").Replace("-", "");                
                _context.Add(pessoa);
                if (telefone.NumTelefone != null)
                {
                    telefone.NumTelefone = telefone.NumTelefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                    telefone.Pessoa = pessoa;
                    _context.Add(telefone);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            var telefone = await _context.Telefone.FirstOrDefaultAsync(t => t.PessoaId == pessoa.Id);
            PessoaViewModel viewModel = new PessoaViewModel { Pessoa = pessoa, Telefone = telefone };
            return View(viewModel);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNasc,CPF,Endereco,Bairro,UF,Cidade,Email")] Pessoa pessoa, Telefone telefone)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pessoa.CPF = pessoa.CPF.Replace(".", "").Replace("-", "");
                    if (telefone.NumTelefone != null)
                    {
                        telefone.NumTelefone = telefone.NumTelefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                    }
                    _context.Update(pessoa);
                    bool hasAny = await _context.Telefone.AnyAsync(x => x.PessoaId == pessoa.Id);
                    if (!hasAny)
                    {                        
                        telefone.Pessoa = pessoa;
                        _context.Add(telefone);
                    }
                    else
                    {
                        var fone = await _context.Telefone.FirstOrDefaultAsync(t => t.PessoaId == pessoa.Id);
                        fone.NumTelefone = telefone.NumTelefone;
                        _context.Update(fone);
                    }
                    await _context.SaveChangesAsync();                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }
}
