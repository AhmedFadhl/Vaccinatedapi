using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
using Vaccinatedapi.Repository.Abstract;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {

        private readonly Ifileservice _fileService;
        private readonly IProductRepository _productRepo;
        dbdatacontexts _dbdatacontexts = null;
        private readonly IWebHostEnvironment _webenvirement;


        public ParentController(Ifileservice fs, IProductRepository productRepo, dbdatacontexts dbdatacontexts,IWebHostEnvironment webHostEnvironment)
        {
            this._fileService = fs;
            this._productRepo = productRepo;
            _dbdatacontexts = dbdatacontexts;
            _webenvirement=webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<parents>>> get_parents()
        {
            
           return await _dbdatacontexts.parents.ToListAsync();
         
            
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<parents>> get_parent(int id)
        {
            return await _dbdatacontexts.parents.FirstOrDefaultAsync(x => x.ID == id);
        }



        [HttpPut("{id}")]
        public async Task< IActionResult> Edit([FromForm] parents model, int id)
        {
            if (model.ID != id)
            {
                return BadRequest();
            }
            //2015-05-02T00:00:00
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.picture != null)
            {
                var fileResult = _fileService.savefile(model.picture);
                if (fileResult.Item1 == 1)
                {
                    model.image_path = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.Edit(model);
                if (productResult.Equals(true))
                {
                    status.StatusCode = 1;
                    status.Message = "Edited successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product";

                }
            }
            else if (model.picture == null)
            {
                try
                {

                _dbdatacontexts.Entry(model).State = EntityState.Modified;
                await _dbdatacontexts.SaveChangesAsync();
                    status.StatusCode = 1;
                    status.Message = "Edited successfully";
                }
                catch (Exception)
                {
                    status.StatusCode = 0;
                    status.Message = "not edited";
                }

            }
            return Ok(status);

        }




        [HttpPut("changepassword/{id}")]
        public async Task<ActionResult> edit_advice(int id, string password)
        {
            var result = _dbdatacontexts.parents.SingleOrDefault(b => b.ID == id);
            if (result != null)
            {
                result.password = password;
                _dbdatacontexts.SaveChanges();
                return Ok(result);
            }
            return NotFound("user not found");
        }
        [HttpPut("addtoken/{id}")]
        public async Task<ActionResult> addtoken(int id, string token)
        {
            var result = _dbdatacontexts.parents.SingleOrDefault(b => b.ID == id);
            if (result != null)
            {
                result.firebase_token = token;
                _dbdatacontexts.SaveChanges();
                return Ok(result);
            }
            return NotFound("user not found");
        }



//دالة تحميل الصور والملفات 
//تتطلب الدالة اسم الملف المطلوب تحميلة 
        [HttpGet]
        [Route("downloadfile")]
        public async Task<ActionResult> downloadfile(string filename)
        {
            //عمل رابط كامل للملف 
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\files", filename);
            var provider = new FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }
            //تحويل الملف الى بايت 
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            string filespath=Path.GetFullPath(filename);
            //ارسال الملف بالبايت واسم الملف 
             return File(bytes, contenttype, Path.GetFileName(filepath));
            // return Ok( Path.GetFileName(filepath));
        }
        [HttpGet]
        [Route("geturl")]
        public async Task<ActionResult> geturl(string filename)
        {
            var wwwPath = _webenvirement.WebRootPath;
            wwwPath=Url.Content(wwwPath);
            var image_paths = Path.Combine( $"uploads\\files\\{filename}");
            
            string image_url=Url.Content(image_paths);
            //string image_url=Url.Content(image_path);
            return Ok(new{image_url});
        }




        [HttpPost]
        public IActionResult Add([FromForm] parents model)
        {

            //2015-05-02T00:00:00
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.picture != null)
            {
                var fileResult = _fileService.savefile(model.picture);
                if (fileResult.Item1 == 1)
                {
                    model.image_path = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.Add(model);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding notification";

                }
            }
            return Ok(status);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete_parent(int id)
        {
            var applicant_recording = await _dbdatacontexts.parents.FindAsync(id);
            if (applicant_recording == null)
            {
                return NotFound();
            }
            _dbdatacontexts.parents.Remove(applicant_recording);
            await _dbdatacontexts.SaveChangesAsync();
            return NoContent();
        }





















    }
}
