using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vaccineController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;

        public vaccineController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }



        [HttpPost]
        public async Task<ActionResult> add_vaccine(vaccine vaccine)
        {
            try
            {
                _dbdatacontexts.Add(vaccine);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_hospital_type(vaccine vaccine, int id)
        {
            if (vaccine.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(vaccine).State = EntityState.Modified;
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
        public async Task<ActionResult<List<vaccine>>> get_vaccine()
        {
            return await _dbdatacontexts.vaccine.Include(p=>p.dose).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<vaccine>> get_Hospital_type(int id)
        {
            return await _dbdatacontexts.vaccine.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_vaccine(int id)
        {
            var applicant_recording = await _dbdatacontexts.vaccine.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.vaccine.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }

    }
}
