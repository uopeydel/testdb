using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstCodeDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFCDBContext _dbContext;
        public ValuesController(FCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetData")]
        public async Task<IActionResult> GetData()
        {
            var data = "";
            var Taxonomy = await _dbContext.Taxonomy.Where(w => w.Key != data).AsNoTracking().ToListAsync();
            var Master = await _dbContext.Master.Where(w => w.Key != data).AsNoTracking().ToListAsync();

            return Ok(new { Taxonomy, Master });
        }

        [HttpPost]
        [Route("AddTaxonomy")]
        public async Task<IActionResult> AddTaxonomy(Taxonomy taxonomy)
        {
            return Ok(taxonomy);
            await _dbContext.Taxonomy.AddAsync(taxonomy); await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route("DeleteTaxonomy")]
        public async Task<IActionResult> DeleteTaxonomy(int id)
        {
            try
            {
                //var Masters = await _dbContext.Master.Where(w => w.TaxonomyId == id).ToListAsync();
                //Masters.ForEach(f => f.TaxonomyId = 1);
                //_dbContext.Master.UpdateRange(Masters);
                //await _dbContext.SaveChangesAsync();


                var Taxonomy = await _dbContext.Taxonomy.Where(w => w.Id == id).FirstOrDefaultAsync();
                _dbContext.Taxonomy.Remove(Taxonomy);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(new { ex, ex.Message, Innter = ex?.InnerException?.Message });
            }
        }

        [HttpGet]
        [Route("DeleteMaster")]
        public async Task<IActionResult> DeleteMaster(int id)
        {
            try
            {
                var Master = await _dbContext.Master.Where(w => w.Id == id).FirstOrDefaultAsync();
                _dbContext.Master.Remove(Master);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(new { ex, ex.Message, Innter = ex?.InnerException?.Message });
            }
        }


    }
}
