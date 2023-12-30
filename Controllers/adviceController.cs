using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adviceController : ControllerBase
    {



        dbdatacontexts _dbdatacontexts = null;

        public adviceController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }


        [HttpPost]
        public async Task<ActionResult> add_advice(advices advices)
        {
            try
            {
                _dbdatacontexts.Add(advices);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_advice(advices advices, int id)
        {
            if (advices.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(advices).State = EntityState.Modified;
            try
            {
                await _dbdatacontexts.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<List<advices>>> get_advices()
        {
            return await _dbdatacontexts.advices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<advices>> get_advice(int id)
        {
            return await _dbdatacontexts.advices.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_advice(int id)
        {
            var applicant_recording = await _dbdatacontexts.advices.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.advices.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }
    }
}
