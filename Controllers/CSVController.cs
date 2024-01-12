using Microsoft.AspNetCore.Mvc;
using CSV_Parser_Demo.Models;
using Microsoft.AspNetCore.Http.Features;

namespace CSV_Parser_Demo.Controllers
{
    [ApiController]
    [Route("")]
    public class CSVController : ControllerBase
    {

        private readonly ILogger<CSVController> _logger;

        public CSVController(ILogger<CSVController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CSV")]
        public string ParseCSV()
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            string? input = new StreamReader(HttpContext.Request.Body).ReadToEnd();

            _logger.LogInformation($"CSV parsing request recieved at {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}" +
                $" from {HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown"}");

            if (input == null) {
                HttpContext.Response.StatusCode = 400;
                return "Please submit your CSV in the body of your request as raw text";
            }

            CSVParser parser = new CSVParser();

            //HttpContext.Response.Body = parser.ParseToString(input);

            return parser.ParseToString(input);
        }
    }
}
