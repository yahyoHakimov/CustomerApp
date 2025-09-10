var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
        builder.Services.AddScoped<IClientService<BaseClient>, ClientService<BaseClient>>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IRepository<BaseClient>, GenericRepository<BaseClient>>();
        builder.Services.AddScoped<IReportingService, ReportingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware pipeline
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        
        app.UseRouting();
        app.MapControllers();

        // Event handlers
        ClientEventManager.ClientChanged += (client, action) =>
        {
            Console.WriteLine($"Event: {action} - {client.Name}");
        };

        ClientEventManager.LogEvent += (message) =>
        {
            Console.WriteLine($"Log: {message}");
        };


app.UseHttpsRedirection();
app.Run();
