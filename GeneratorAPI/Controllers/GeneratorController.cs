using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Core;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private readonly AnyClass Generator;
        private readonly ILogger<GeneratorController> _logger;

        public GeneratorController(ILogger<GeneratorController> logger)
        {
            _logger = logger;
            Generator = new AnyClass();
        }

        // POST api/<GeneratorController>
        [HttpPost]
        public ActionResult<ICollection<List<object>>> Generate(Dictionary<string, HashSet<string>> query, int count, string language = "en")
        {
            List<List<object>> data = new List<List<object>>();

            for (int i = 0; i < count; i++)
            {
                data.Add(new List<object>());
                foreach (var className in query.Keys)
                {
                    foreach (var method in query[className])
                    {
                        data[i].Add(Generator.GenerateData(className, method, language));
                    }
                }
            }

            return data;
        }

        // GET api/<GeneratorController>/locale
        [HttpGet("locale")]
        public ActionResult<ICollection<string>> GetLocales()
        {
            return Generator.Locales;
        }

        // GET api/<GeneratorController>/classes
        [HttpGet("classes")]
        public ActionResult<ICollection<string>> GetClasses()
        {
            return Generator.GetClasses();
        }

        // GET api/<GeneratorController>/classes/{name}
        [HttpGet("classes/{name}")]
        public ActionResult<ICollection<string>> GetMethods(string name)
        {
            try
            {
                return Generator.GetMethodsByClass(name);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/<GeneratorController>/classes/{name}?method
        [HttpGet("classes/{name}/{method}")]
        public ActionResult<object> GenerateSingle(string name, string method, string locale = "en")
        {
            try
            {
                return Generator.GenerateData(name, method, locale);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
