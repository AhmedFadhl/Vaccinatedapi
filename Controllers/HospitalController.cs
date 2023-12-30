using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;

        public HospitalController( dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }


        [HttpPost]
        public async Task<ActionResult> add_hospital(hospital hospital)
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



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_hospital(hospital hospital, int id)
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



        [HttpGet]
        public async Task<ActionResult<List<hospital>>> get_Hospital()
        {
            var hospital = await _dbdatacontexts.hospitals.Include(p => p.hospital_Type).Include(p=>p.city).ToListAsync();
            return hospital;
        }




      

        [HttpGet("{id}")]
        public async Task<ActionResult<hospital>> get_Hospital(int id)
        {
            var hospital = await _dbdatacontexts.hospitals.FirstOrDefaultAsync(x => x.ID == id);
            return hospital;
             
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_hospital(int id)
        {
            var applicant_recording = await _dbdatacontexts.hospitals.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.hospitals.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }
  

    }
}
