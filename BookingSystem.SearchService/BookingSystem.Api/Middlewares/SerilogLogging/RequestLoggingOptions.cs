namespace BookingSystem.Api.Middlewares.SerilogLogging
{
    public class RequestLoggingOptions
    {
        public Func<HttpRequest, object> RequestProjection { get; set; } = req => new RequestInfo(req);
    }
}
