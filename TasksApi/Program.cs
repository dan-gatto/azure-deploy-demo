using TasksApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();  // in-memory store must live as long as the app
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// No UseHttpsRedirection: in Azure, App Service handles HTTPS in front of the container
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
