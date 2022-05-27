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
        private readonly Generator Generator;
        private readonly ConvertManager ConvertManager;

        private readonly ILogger<GeneratorController> _logger;

        public GeneratorController(ILogger<GeneratorController> logger)
        {
            _logger = logger;
            Generator = new Generator();
            ConvertManager = new ConvertManager();
        }

        // POST api/<GeneratorController>
        [HttpPost]
        public ActionResult<ICollection<List<object>>> Generate(Dictionary<string, HashSet<string>> query, int count, string locale = "en")
        {
            try
            {
                return ConvertManager.Generate(query, count, locale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/<GeneratorController>/classes/{name}?method
        [HttpGet("classes/{name}/{method}")]
        public ActionResult<object> GenerateSingle(string name, string method, string locale = "en")
        {
            try
            {
                return ConvertManager.Generate(name, method, locale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
