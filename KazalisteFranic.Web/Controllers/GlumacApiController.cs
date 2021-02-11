using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KazalisteFranic.DAL;
using KazalisteFranic.Model;
using Microsoft.AspNetCore.Mvc;

namespace KazalisteFranic.Web.Controllers
{
    [Route("api/glumac")]
    [ApiController]
    public class GlumacApiController : Controller
    {
        private KazalisteManagerDbContext _dbContext;

        public GlumacApiController(KazalisteManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Get()
        {
            var glumci = this._dbContext.Glumci
                .Select(p => new GlumacDTO()
                {
                    Id = p.Id,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Citat = p.Citat,
                    Spol = p.Spol,
                    Akademija = p.Diplomirao.Naziv
                })
                .ToList();
      
            return Ok(glumci);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var glumac = this._dbContext.Glumci
                .Where(p => p.Id == id)
                .Select(p => new GlumacDTO()
                {
                    Id = p.Id,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Citat = p.Citat,
                    Spol = p.Spol,
                    Akademija = p.Diplomirao.Naziv
                })
                .FirstOrDefault();

            if(glumac == null)
            {
                return NotFound();
            }

            return Ok(glumac);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Glumac glumac)
        {
            this._dbContext.Glumci.Add(new Glumac
            {
                Ime = glumac.Ime,
                Prezime = glumac.Prezime,
                Citat = glumac.Citat,
                Spol = glumac.Spol,
                AkademijaId = glumac.AkademijaId,
            });

            this._dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] Glumac glumac)
        {
            var ClientDb = this._dbContext.Glumci.First(p => p.Id == id);

            ClientDb.Ime = glumac.Ime;
            ClientDb.Prezime = glumac.Prezime;
            ClientDb.Citat = glumac.Citat;
            ClientDb.Spol = glumac.Spol;
            ClientDb.AkademijaId = glumac.AkademijaId;

            this._dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ClientDb = this._dbContext.Glumci.First(p => p.Id == id);
            _dbContext.Remove(ClientDb);

            this._dbContext.SaveChanges();

            return Ok();
        }
    }
}