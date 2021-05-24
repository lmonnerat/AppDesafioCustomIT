using AppDesafioCustomIT.Data;
using AppDesafioCustomIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var telefones = await _context.Telefone.Where(t => t.PessoaId == pessoa.Id).ToListAsync();
            pessoa.Telefones = telefones;
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
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNasc,CPF,Endereco,Bairro,UF,Cidade,Email")] Pessoa pessoa)
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
                    _context.Update(pessoa);
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
