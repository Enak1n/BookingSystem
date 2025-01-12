using BookingSystem.Infrastructure.Extensions;
using BookingSystem.BL.Extensions;
using BookingSystem.Api.Middlewares;
using BookingSystem.Api.Middlewares.SerilogLogging;
using Serilog;
using BookingSystem.Api.Options;
using MessageBus;
using Quartz;
using BookingSystem.Api.Jobs;
using BookingSystem.SearchService.Api.Jobs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

configuration.AddEnvironmentVariables();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddBlServices();

builder.Services.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));

var kafkaHost = configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();

builder.Services.AddSingleton(serviceProvider =>
{
    return new KafkaMessageBus(kafkaHost.Host);
});


builder.Services.AddQuartz(cfg =>
{
    var sendPaymentJobKey = new JobKey(nameof(SendPaymentMessagesJob));
    var cancelPaymentJobKey = new JobKey(nameof(CancelPaymentJob));

    cfg.AddJob<SendPaymentMessagesJob>(sendPaymentJobKey)
        .AddTrigger(t =>
            t.ForJob(sendPaymentJobKey).WithSimpleSchedule(s => s.WithIntervalInSeconds(10).RepeatForever())
        );

    cfg.AddJob<CancelPaymentJob>(cancelPaymentJobKey)
        .AddTrigger(t =>
            t.ForJob(cancelPaymentJobKey).WithSimpleSchedule(s => s.WithIntervalInSeconds(10).RepeatForever())
        );
});

builder.Services.AddQuartzHostedService();


var app = builder.Build();

app.Services.DatabaseMigrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
