using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class cityController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;

        public cityController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }



        [HttpPost]
        public async Task<ActionResult> add_city(cities cities)
        {
            try
            {
                _dbdatacontexts.Add(cities);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_city(cities city, int id)
        {
            if (city.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(city).State = EntityState.Modified;
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


        [HttpGet ]
        public async Task<ActionResult<List<cities>>> get_cities()
        {
            return await _dbdatacontexts.cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<cities>> get_city(int id)
        {
            cities city=await _dbdatacontexts.cities.FirstOrDefaultAsync(x => x.ID == id);
            if (city!=null)
            {
                return(city);
            }
            return BadRequest("ERORR cannot get this city");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_city(int id)
        {
            var applicant_recording = await _dbdatacontexts.cities.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.cities.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }




    }
}