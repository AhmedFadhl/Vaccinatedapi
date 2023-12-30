using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoseController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;

        public DoseController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }


        [HttpPost]
        public async Task<ActionResult> add_dose(dose dose)
        {
            try
            {
                _dbdatacontexts.Add(dose);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_dose(dose dose, int id)
        {
            if (dose.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(dose).State = EntityState.Modified;
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
        public async Task<ActionResult<List<dose>>> get_dose()
        {
            return await _dbdatacontexts.doses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dose>> get_dose(int id)
        {
            return await _dbdatacontexts.doses.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_dose(int id)
        {
            var applicant_recording = await _dbdatacontexts.doses.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.doses.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }



    }
}
