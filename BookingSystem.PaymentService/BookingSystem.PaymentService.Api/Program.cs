using Serilog;
using BookingSystem.PaymentService.Infrastructure.Extensions;
using BookingSystem.PaymentService.Api.Options;
using MessageBus;
using BookingSystem.PaymentService.Api.Middlewares.SerilogLogging;
using BookingSystem.PaymentService.Api.Middlewares;
using Quartz;
using BookingSystem.PaymentService.Api.Jobs;
using BookingSystem.PaymentService.Api.Utils;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(configuration);

builder.Services.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));
builder.Services.Configure<YooKassaSettings>(configuration.GetSection(nameof(YooKassaSettings)));

builder.Services.AddSingleton<PaymentClient>();

var kafkaHost = configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();

builder.Services.AddSingleton(serviceProvider =>
{
    return new KafkaMessageBus(kafkaHost.Host);
});

builder.Services.AddQuartz(cfg =>
{
    var paymentJobKey = new JobKey(nameof(GetRequestForPaymentJob));
    var checkPaymentJobKey = new JobKey(nameof(CheckPaymentJob));

    cfg.AddJob<GetRequestForPaymentJob>(paymentJobKey)
        .AddTrigger(t =>
            t.ForJob(paymentJobKey).WithSimpleSchedule(s => s.WithIntervalInSeconds(10).RepeatForever())
        );

    cfg.AddJob<CheckPaymentJob>(checkPaymentJobKey)
            .AddTrigger(t =>
                t.ForJob(checkPaymentJobKey).WithSimpleSchedule(s => s.WithIntervalInSeconds(30).RepeatForever())
            );
});

builder.Services.AddQuartzHostedService();

var app = builder.Build();

app.Services.DatabaseMigrate();

// Configure the HTTP request pipeline.
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
