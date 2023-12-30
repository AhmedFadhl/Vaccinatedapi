using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class hospital_typeController : ControllerBase
    {


        dbdatacontexts _dbdatacontexts = null;

        public hospital_typeController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }

        [HttpPost("type")]
        public async Task<ActionResult> add_hospital_type(hospital_type hospital)
        {
            try
            {
                _dbdatacontexts.Add(hospital);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("type/{id}")]
        public async Task<ActionResult> edit_hospital_type(hospital_type hospital, int id)
        {
            if (hospital.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(hospital).State = EntityState.Modified;
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


        [HttpGet("type")]
        public async Task<ActionResult<List<hospital_type>>> get_Hospital_type()
        {
            return await _dbdatacontexts.hospital_Types.ToListAsync();
        }

        [HttpGet("type/{id}")]
        public async Task<ActionResult<hospital_type>> get_Hospital_type(int id)
        {
            return await _dbdatacontexts.hospital_Types.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpDelete("type/{id}")]
        public async Task<ActionResult> Delete_hospital_type(int id)
        {
            var applicant_recording = await _dbdatacontexts.hospital_Types.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.hospital_Types.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }

    }
}
