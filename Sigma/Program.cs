using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository;
using Repository.Implementation;
using Repository.Interface;
using Service.Implementation;
using Service.Interface;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// implement services here
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    // if we want to implement different database rather than sql server 
    // we can change it here
    option.UseSqlServer(connectionString);
});

var app = builder.Build();
builder.Logging.AddDebug();


// Automatically apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.Migrate(); // Apply pending migrations
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

// This will handle the all the exception that will occured on application
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    context.Response.StatusCode = 400;
    context.Response.ContentType = "application/json";

    Exception? exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    ILogger logger = app.Services.GetRequiredService<ILogger<Program>>();
    var responseModal = new ResponseModal
    {
        Message = exception?.Message ?? string.Empty,
        ResponseType = ResponseType.Failed
    };

    string message = exception?.Message ?? string.Empty,
        stackTrace= exception?.StackTrace ?? string.Empty,
        exceptionUrl = context.Request.Path,
        exceptionType = exception?.GetType()?.ToString() ?? string.Empty;
    DateTime dateTime = DateTime.UtcNow;

    if (exception != null)
    {
        string error = $@"Date:: {dateTime}{Environment.NewLine}
                          Path:: {exceptionUrl}{Environment.NewLine}
                          Type:: {exceptionType}{Environment.NewLine}
                          Message:: {message}{Environment.NewLine}
                          Stacktrace:: {stackTrace}";
        logger.LogError("Exception", error);

        // TODO: Log exception here
        // We can log exception either on file or database
        // To implement log in file we just need to configure file here
    }

    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseModal));
}));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
