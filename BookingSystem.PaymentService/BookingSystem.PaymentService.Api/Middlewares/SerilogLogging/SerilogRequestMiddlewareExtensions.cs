namespace BookingSystem.PaymentService.Api.Middlewares.SerilogLogging
{
    public static class SerilogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder builder,
            Action<RequestLoggingOptions> configureOptions = null)
        {
            var options = new RequestLoggingOptions();
            configureOptions?.Invoke(options);
            return builder.UseMiddleware<SerilogMiddleware>(options);
        }

    }

}
