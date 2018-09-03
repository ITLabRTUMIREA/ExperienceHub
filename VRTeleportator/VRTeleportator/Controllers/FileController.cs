using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using VRTeleportator.Models;

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
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            string path = Path.Combine("Extracts", Path.GetFileName(uploadedFile.FileName));

            using (var fileStream = new FileStream(Path.Combine(environment.WebRootPath, path), FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            FileModel file = new FileModel
            {
                FileName = uploadedFile.FileName,
                FilePath = path
            };

            //await context.Files.AddAsync(file);
            //await context.SaveChangesAsync();

            ZipFile.ExtractToDirectory(Path.Combine(environment.WebRootPath, file.FilePath),
                Path.Combine(environment.WebRootPath, "Extracts/kek"));
            return Ok();
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

        [HttpDelete]
        [Route("delete/{folder}")]
        public async Task<IActionResult> DeleteDirectory(string folder)
        {
            return Ok();
        }
    }
}