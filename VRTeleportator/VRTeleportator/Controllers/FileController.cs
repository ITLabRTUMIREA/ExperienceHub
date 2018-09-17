using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using VRTeleportator.Models;
using System;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Route("api/file")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment environment;
        private readonly AppDataBase context;

        public FileController(IHostingEnvironment environment, AppDataBase context)
        {
            this.environment = environment;
            this.context = context;
        }

        [HttpPost]
        [RequestSizeLimit(100000000000)]
        [Route("{LessonId}/upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile, Guid LessonId)
        {
            var result = context.Lessons.Find(LessonId);
            var path = Path.Combine(result.Path, Path.GetExtension(uploadedFile.Name));

            using (var fileStream = new FileStream(Path.Combine(environment.WebRootPath, path), FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            ZipFile.ExtractToDirectory(path, Path.Combine(environment.WebRootPath, $@"Lessons\{result.Name}"));

            return Ok();

            //string path = Path.Combine("Lessons", Path.GetFileName(uploadedFile.FileName));



            //FileModel file = new FileModel
            //{
            //    FileName = uploadedFile.FileName,
            //    FilePath = path
            //};

            //await context.Files.AddAsync(file);
            //await context.SaveChangesAsync();
        }

        [HttpGet]
        [Route("getinfo/{folder}")]
        public IActionResult GetNumber(string folder)
        {
            if (!Directory.Exists(Path.Combine(environment.WebRootPath, $@"Extracts/{folder}")))
            {
                return NotFound("Выбранный Вами путь не существует");
            }

            return Json(new DirectoryInfo(Path.Combine(environment.WebRootPath, $@"Extracts/{folder}"))
                .GetFiles()
                .Length
                .ToString());
        }

        //[HttpDelete]
        //[Route("delete/{folder}")]
        //public async Task<IActionResult> DeleteDirectory(string folder)
        //{
        //    return Ok();
        //}
    }
}