using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManualsController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        
        public ManualsController(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Поиск и загрузка инструкции по использованию устройства
        /// </summary>
        [HttpGet]
        [HttpHead]
        [Route("{manufacturer}")] 
        public IActionResult GetFile([FromRoute] string manufacturer)
        {
            var staticPath = Path.Combine(_env.ContentRootPath, "Static");
            
            var filePath = Directory
                .GetFiles(staticPath)
               .FirstOrDefault(f => f.Split('\\')
                    .Last()
                    .Split('.')[0] == manufacturer);

            if (string.IsNullOrEmpty(filePath))
                return StatusCode(404, $"Инструкции для производителя '{manufacturer}' не найдено на сервере. Проверьте название!");
            
            string fileType = "application/pdf";
            string fileName = $"{manufacturer}.pdf";
            
            return PhysicalFile(filePath, fileType, fileName);
        }
    }
}