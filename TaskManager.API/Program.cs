using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Core.Interfaces;
using TaskManager.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger setup
builder.Services.AddEndpointsApiExplorer();

// Add Swagger 
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); // Swagger açýklamalarýný etkinleþtir
});

// CORS ekleme
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React uygulamanýzýn URL'si
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));

// Add DbContext
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Add Services
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// CORS middleware kullanýmý
app.UseCors("AllowReactApp");
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
