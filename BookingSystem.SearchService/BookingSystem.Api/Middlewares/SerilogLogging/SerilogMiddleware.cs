using Serilog.Events;

namespace BookingSystem.Api.Middlewares.SerilogLogging
{
    public class SerilogMiddleware
    {
        private const string MessageTemplate = "HTTP {Method} to '{Path}' responded with {StatusCode}";

        private readonly RequestDelegate _next;
        private readonly RequestLoggingOptions _options;
        private readonly Serilog.ILogger _logger;

        public SerilogMiddleware(RequestDelegate next, Serilog.ILogger logger, RequestLoggingOptions options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger?.ForContext<SerilogMiddleware>() ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            try
            {
                await _next(httpContext);
                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode >= 400 ? LogEventLevel.Error : LogEventLevel.Information;

                var contextualLogger = _logger.ForContext("Request", _options.RequestProjection(httpContext.Request), destructureObjects: true);
                contextualLogger = (level == LogEventLevel.Error ? PopulateLogContext(contextualLogger, httpContext) : contextualLogger);

                contextualLogger.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode);
            }
            catch (Exception ex)
            {
                PopulateLogContext(_logger, httpContext)
                    .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500);

                throw;
            }
        }

        private static Serilog.ILogger PopulateLogContext(Serilog.ILogger logger, HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = logger
                .ForContext("RequestHeaders", request.Headers
                                                     .Where(h => h.Key != "Authorization")
                                                     .ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }

    }
}
