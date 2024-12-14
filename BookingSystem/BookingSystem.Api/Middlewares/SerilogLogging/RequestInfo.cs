namespace BookingSystem.Api.Middlewares.SerilogLogging
{
    public class RequestInfo
    {
        public RequestInfo(HttpRequest request)
        {
            ContentLength = request.ContentLength;
            ContentType = request.ContentType;
            Protocol = request.Protocol;
            Host = request.Host.Value;
            IsAuthenticated = request.HttpContext.User?.Identity?.IsAuthenticated ?? false;
        }

        public long? ContentLength { get; }
        public string ContentType { get; }
        public string Protocol { get; }
        public string Host { get; }
        public bool IsAuthenticated { get; }

    }
}
