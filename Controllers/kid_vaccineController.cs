using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
using Vaccinatedapi.multiModels;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class kid_vaccineController : ControllerBase
    {
        private readonly IHubContext<SignlrHub> _hubcontext;


        dbdatacontexts _dbdatacontexts = null;

        public kid_vaccineController(dbdatacontexts dbdatacontexts, IHubContext<SignlrHub> hubContext)
        {
            _dbdatacontexts = dbdatacontexts;
            _hubcontext = hubContext;
        }












        [HttpGet("getmessage")]
        public async Task<IActionResult> getessage()
        {
            var kids_vaccines = new List<vaccine_kids>();
            var kids_vaccine = new vaccine_kids();

            var kid_rel = await _dbdatacontexts.kid_Vaccines.ToListAsync();
            foreach (var item in kid_rel)
            {
                kids_vaccine.kid_Vaccine = item;
                kids_vaccine.kids = await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == item.kids_Id);
                kids_vaccine.vaccine = await _dbdatacontexts.vaccine.FirstOrDefaultAsync(x => x.ID == item.vaccines_Id);
                kids_vaccines.Add(kids_vaccine);
            }
            _hubcontext.Clients.All.SendAsync("check your kid vaccine  ");

            return Ok("Done");
        }




        [HttpPost]
        public async Task<ActionResult> add_vaccine(kid_vaccine vaccine)
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



        [HttpPut("{kidid}/{vaccineid}")]
        public async Task<ActionResult> edit_vaccine(kid_vaccine vaccine, int kidid, int vaccineid)
        {
            if (vaccine.kids_Id != kidid && vaccineid != vaccine.vaccines_Id)
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
        public async Task<ActionResult<List<kid_vaccine>>> get_kid_vaccine()
        {
            return await _dbdatacontexts.kid_Vaccines.ToListAsync();
        }

        [HttpGet("detiles")]
        public async Task<ActionResult<List<vaccine_kids>>> get_kid_vaccine2()
        {
            var kids_vaccines = new List<vaccine_kids>();
            var kids_vaccine = new vaccine_kids();

            var kid_rel = await _dbdatacontexts.kid_Vaccines.ToListAsync();
            foreach (var item in kid_rel)
            {
                kids_vaccine.kid_Vaccine = item;
                kids_vaccine.kids = await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == item.kids_Id);
                kids_vaccine.vaccine = await _dbdatacontexts.vaccine.FirstOrDefaultAsync(x => x.ID == item.vaccines_Id);
                kids_vaccines.Add(kids_vaccine);
            }
            return kids_vaccines;
        }
        [HttpGet("detiles/{id}")]
        public async Task<ActionResult<List<vaccine_kids>>> get_kid_vac_id(int id)
        {
            var kids_vaccines = new List<vaccine_kids>();
            var kids_vaccine = new vaccine_kids();

            var kid_rel = _dbdatacontexts.kid_Vaccines.Where(x => x.kids_Id == id).ToList();
            foreach (var item in kid_rel)
            {
                kids_vaccine.kid_Vaccine = item;
                kids_vaccine.kids = await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == item.kids_Id);
                kids_vaccine.vaccine = await _dbdatacontexts.vaccine.FirstOrDefaultAsync(x => x.ID == item.vaccines_Id);
                kids_vaccines.Add(kids_vaccine);
            }
            return kids_vaccines;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<kid_vaccine>>> get_kid_vaccine(int id)
        {
            var kid_Vaccine= await _dbdatacontexts.kid_Vaccines.Where(x => x.kids_Id == id).ToListAsync();

            return kid_Vaccine;
        }

        [HttpDelete("{kidid}/{vaccineid}")]
        public async Task<ActionResult> Delete_kid_vaccine(int kidid, int vaccineid)
        {


            var applicant_recording = await _dbdatacontexts.kid_Vaccines.FirstOrDefaultAsync(x => x.kids_Id == kidid && x.vaccines_Id == vaccineid);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.kid_Vaccines.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }


    }
}
