using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cryptov2.Filters;
using Cryptov2.DataBaseContext;
using Crypto2.Models;

namespace Cryptov2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoesController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptoesController(CryptoContext context)
        {
            _context = context;
        }

        // GET: api/Cryptoes
        //RETORNA todas as chaves cadastradas na base
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCrypto()
        {
            return await _context.Crypto.ToListAsync();
        }

        // GET: api/Cryptoes/5
        // RETORNA a chave com o id dado de parametro
        [HttpGet("{id}")]
        public async Task<ActionResult<Crypto>> GetCrypto(int id)
        {
            var crypto = await _context.Crypto.FindAsync(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return crypto;
        }

        // GET: api/Cryptoes/SearchByName?nome=Test
        // RETORNA a chave com o nome dado de parametro
        [HttpGet("SearchByName")]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCryptoNome(string nome)
        {
            var crypto = await GetByName(nome);

            if (crypto == null)
            {
                return NotFound();
            }

            return crypto.ToList<Crypto>();
        }

        // GET: api/Cryptoes/SearchBySigla?sigla=Test
        // RETORNA a chave com o nome dado de parametro
        [HttpGet("SearchBySigla")]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCryptoSigla(string sigla)
        {
            var crypto = await GetBySigla(sigla);

            if (crypto == null)
            {
                return NotFound();
            }

            return crypto.ToList<Crypto>();
        }

        // PUT: api/Cryptoes/5
        // UPTATE não pode mudar id do JSON do BODY
        [ApiKeyAuth]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrypto(int id, Crypto crypto)
        {
            if (id != crypto.id)
            {
                return BadRequest();
            }

            _context.Entry(crypto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cryptoes
        // CADASTRA chave na base
        [ApiKeyAuth]
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {
            _context.Crypto.Add(crypto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrypto", new { id = crypto.id }, crypto);
        }

        // DELETE: api/Cryptoes/5
        // DELETA chave da base
        [ApiKeyAuth]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrypto(int id)
        {
            var crypto = await _context.Crypto.FindAsync(id);
            if (crypto == null)
            {
                return NotFound();
            }

            _context.Crypto.Remove(crypto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CryptoExists(int id)
        {
            return _context.Crypto.Any(e => e.id == id);
        }

        private async Task<IEnumerable<Crypto>> GetByName(string nome)
        {
            return await _context.Crypto.Where(x => x.nome.Equals(nome)).ToListAsync<Crypto>();
        }

        private async Task<IEnumerable<Crypto>> GetBySigla(string sigla)
        {
            return await _context.Crypto.Where(x => x.sigla.Equals(sigla)).ToListAsync<Crypto>();
        }
    }
}
