using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Todo.Contracts;
using Todo.Data;
using Todo.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateApp(args);
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors();

        app.MapControllers();
        app.Use(async (context, next) =>
        {
            var path = context.Request.Path;
            var method = context.Request.Method;

            Console.WriteLine($"[Request] {method} {path}");

            await next(); // continue to the next middleware
        });

        app.Run();
    }

    private static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=MyTodoDb;User Id=postgres;Password=9908349684;"));

        builder.Services.AddScoped<RetrieveAllTodos,TodoService>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        var app = builder.Build();
        return app;
    }
}