using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SynonymsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SynonymsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynonymsController : ControllerBase
    {
        private readonly SynonymsContext _context;

        public SynonymsController(SynonymsContext context)
        {
            _context = context;
        }


        // GET: api/Synonyms
        [HttpGet("{word}")]
        public async Task<ActionResult<IEnumerable<string>>> GetSynonims(string word)
        {
            /* "subset" represents synonyms string list for the given word*/
            var subset = _context.Synonims.Where(y => y.Word == word).Select(y => y.Synonym).ToList();

            /*union data subset represents data selected by transitive rule 
             * UnionSubset = from x in _context.Synonims where subset.Contains(x.Word) select x.Synonym;*/

            return await _context.Synonims.Where(y => y.Word == word).Select(y => y.Synonym).Union(from x in _context.Synonims where subset.Contains(x.Word) select x.Synonym).ToListAsync();

        }


        // POST api/Synonyms/word/synonymsString
        [HttpPost("{word}/{synonym}")]
        public async Task<ActionResult<Synonyms>> Post(string word, string synonym)
        {
            var syn = new Synonyms();
            syn.Word = word;
            syn.Synonym = synonym;
            _context.Synonims.Add(syn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post", new { syn.Word, syn.Synonym }, syn);
        }
       
       
    }
}
