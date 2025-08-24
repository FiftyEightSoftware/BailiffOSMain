using BailiffOS.Application.Abstractions;
using BailiffOS.Infrastructure.Services;
using BailiffOS.Domain;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BailiffOS API",
        Version = "v1"
    });

    // Include XML comments if available (API, Application, Domain, Infrastructure)
    var baseDir = AppContext.BaseDirectory;
    foreach (var xml in Directory.EnumerateFiles(baseDir, "*.xml", SearchOption.TopDirectoryOnly))
    {
        c.IncludeXmlComments(xml, includeControllerXmlComments: true);
    }
});

// Application services
builder.Services.AddSingleton<ITodoService, InMemoryTodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BailiffOS API v1");
        c.RoutePrefix = string.Empty; // serve UI at application root
    });
}

// Minimal endpoints using Application layer
app.MapGet("/todos", async (ITodoService service, CancellationToken ct) =>
{
    var items = await service.GetAllAsync(ct);
    return Results.Ok(items);
})
.WithName("GetTodos")
.WithOpenApi(op =>
{
    op.Summary = "List todo items";
    op.Description = "Retrieves all todo items.";
    return op;
});

app.MapPost("/todos", async (AddTodoRequest request, ITodoService service, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest("Title is required");
    }
    var created = await service.AddAsync(request.Title, ct);
    return Results.Created($"/todos/{created.Id}", created);
})
.WithName("CreateTodo")
.WithOpenApi(op =>
{
    op.Summary = "Create a todo item";
    op.Description = "Creates a new todo item with the given title.";
    return op;
});

 app.Run();

 // Request DTOs
 /// <summary>
 /// Request payload for creating a new todo item.
 /// </summary>
 /// <param name="Title">The title of the todo item to create.</param>
 public record AddTodoRequest(string Title);
