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

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }

        [HttpPost(Name = "CSV")]
        public string ParseCSV()
        {
            try
            {
                var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                }

                string? input = new StreamReader(HttpContext.Request.Body).ReadToEnd();

                _logger.LogInformation($"CSV parsing request recieved at {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}" +
                    $" from the following ip: {HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString() ?? "unknown"}");

                if (String.IsNullOrEmpty(input))
                {
                    _logger.LogWarning("Invalid request recieved");
                    HttpContext.Response.StatusCode = 400;
                    return "Please submit your CSV in the body of your request as raw text";
                }

                CSVParser parser = new CSVParser();

                return parser.ParseToString(input);
            }
            catch(Exception e)
            {
                _logger.LogCritical($"A critical error has occurred at {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
                _logger.LogCritical(e?.InnerException?.ToString());
                HttpContext.Response.StatusCode = 500;
                return "An error has occurred with this service";
            }
        }
    }
}
