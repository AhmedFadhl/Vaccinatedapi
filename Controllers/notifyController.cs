using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class notifyController : ControllerBase
    {

        dbdatacontexts _dbdatacontexts = null;

        public notifyController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }


        [HttpPost]
        public async Task<ActionResult> add_notify(notify notify)
        {
            try
            {
                _dbdatacontexts.Add(notify);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_notify(notify notify, int id)
        {
            if (notify.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(notify).State = EntityState.Modified;
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
        public async Task<ActionResult<List<notify>>> get_notify()
        {
            return await _dbdatacontexts.notifies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<notify>> get_notify(int id)
        {
            return await _dbdatacontexts.notifies.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_notify(int id)
        {
            var applicant_recording = await _dbdatacontexts.notifies.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.notifies.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }






    }
}