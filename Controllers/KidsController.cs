using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
using Vaccinatedapi.multiModels;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;
        public KidsController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }


        [HttpPost]
        public async Task<ActionResult> add_kid(kids kids)
        {
            try
            {
                _dbdatacontexts.Add(kids);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> edit_kid(kids kids, int id)
        {
            if (kids.ID != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(kids).State = EntityState.Modified;
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
        public async Task<ActionResult<List<kids>>> get_kids()
        {
            List<kids> kids_list = new List<kids>();
            var kids = await _dbdatacontexts.kids.Include(p => p.hospital).Include(p => p.father).ToListAsync();
            foreach (var item in kids)
            {
                item.mother = await _dbdatacontexts.parents.FindAsync(item.mother_id);
                kids_list.Add(item);
            }
            return kids_list;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<kids>> get_kid(int id)
        {
            return await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == id);
        }






        [HttpGet("by/parent/{id}")]
        public async Task<ActionResult<List<kids>>> get_kid_by_id(int id)
        {
            return await _dbdatacontexts.kids.Where(x => x.father_id == id || x.mother_id == id).ToListAsync();
        }
        [HttpGet("by/vaccine/{id}")]
        public async Task<ActionResult<List<kids>>> get_kid_by_vaccine(int id)
        {
            List<kids> kids=new List<kids>();
            var kid = await _dbdatacontexts.kids.Where(x => x.father_id == id || x.mother_id == id).ToListAsync();
            var vaccine = await _dbdatacontexts.vaccine.ToListAsync();
            foreach (var item in kid)
            {
                foreach (var vaccines in vaccine)
                {
                    double s = DateTime.Now.Subtract(Convert.ToDateTime(item.pirth_date)).TotalDays;
                     int days = (int)Math.Floor(s);
                    if (days == vaccines.days_to_take - 1)
                    {
                        kids.Add(item);
                    }
                }
            }
            return kids;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_kid(int id)
        {
            var applicant_recording = await _dbdatacontexts.kids.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.kids.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost("kidparent")]
        public async Task<ActionResult> add_kidparent(parents_kids kids)
        {
            try
            {
                _dbdatacontexts.Add(kids);
                await _dbdatacontexts.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("kidparent/{id}")]
        public async Task<ActionResult> edit_kidparent(parents_kids kids, int id)
        {
            if (kids.kids_id != id)
            {
                return BadRequest();
            }
            _dbdatacontexts.Entry(kids).State = EntityState.Modified;
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




        [HttpGet("kidparent")]
        public async Task<ActionResult<List<kids_parents>>> get_kidparents()
        {
            var kids_parents = new List<kids_parents>();
            var kids_parent = new kids_parents();

            var kid_rel = await _dbdatacontexts.parents_Kids.ToListAsync();
            foreach (var item in kid_rel)
            {
                kids_parent.parents = await _dbdatacontexts.parents.FirstOrDefaultAsync(x => x.ID == item.parents_id);
                kids_parent.kids = await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == item.kids_id);
                kids_parents.Add(kids_parent);
            }
            return kids_parents;
        }
        [HttpGet("kidparent/{id}")]
        public async Task<ActionResult<kids_parents>> get_kidparent(int id)
        {
            var kids_parent = new kids_parents();

            var kid_rel = await _dbdatacontexts.parents_Kids.FirstOrDefaultAsync(x => x.kids_id == id);
            kids_parent.parents = await _dbdatacontexts.parents.FirstOrDefaultAsync(x => x.ID == kid_rel.parents_id);
            kids_parent.kids = await _dbdatacontexts.kids.FirstOrDefaultAsync(x => x.ID == kid_rel.kids_id);
            return kids_parent;
        }


        [HttpDelete("kidparent/{id}")]
        public async Task<ActionResult> Delete_kidparent(int id)
        {
            var applicant_recording = await _dbdatacontexts.parents_Kids.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.parents_Kids.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }
    }
}
