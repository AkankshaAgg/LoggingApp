using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Logging.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly ILogger _logger;
        ////The standard way of capturing the category
        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public IndexModel(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("DemoCategory");
        }

        public void OnGet()
        {
            _logger.LogInformation(LoggingId.DemoCode, "This is the first logged message.");
            //Different levels of logging
            //used for heavy debugging info.
            _logger.LogTrace("This is a trace log");
            _logger.LogDebug("This is a debug log");
            //used to know flow how our application is being used
            _logger.LogInformation("This is an information log");
            //catching exception and log that as warning.
            _logger.LogWarning("This is a warning log");
            //exception may crash your application.
            _logger.LogError("This is an error log");
            //application is crashing. massive issue.
            _logger.LogCritical("This is a critical log");

            //Additional info we can put into message.
            //Advanced logging messages.
            _logger.LogError("This server went down temporarily at {Time}", DateTime.UtcNow);
            //we can store DateTime.UtcNow info in other file (say json).


            try
            {
                throw new Exception("You forgot to catch me");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a bad exception at {Time}", DateTime.UtcNow);
            }
        }
    }
}

public class LoggingId
{
    public const int DemoCode = 1001;
}
