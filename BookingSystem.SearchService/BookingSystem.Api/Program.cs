using BookingSystem.Infrastructure.Extensions;
using BookingSystem.BL.Extensions;
using BookingSystem.Api.Middlewares;
using BookingSystem.Api.Middlewares.SerilogLogging;
using Serilog;
using BookingSystem.Api.Options;
using MessageBus;
using Quartz;
using BookingSystem.Api.Jobs;

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
    var jobKey = new JobKey(nameof(SendPaymentMessagesJob));

    cfg.AddJob<SendPaymentMessagesJob>(jobKey)
        .AddTrigger(t =>
            t.ForJob(jobKey).WithSimpleSchedule(s => s.WithIntervalInSeconds(10).RepeatForever())
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