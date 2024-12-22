using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Core.Interfaces;
using TaskManager.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); 
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins", builder =>
            builder.WithOrigins("http://localhost:7000", "http://localhost:7001", "http://localhost:3000", "http://x:8081")  
                   .AllowAnyHeader()   
                   .AllowAnyMethod()   
                   .AllowCredentials());  
});

builder.Services.AddAutoMapper(typeof(TaskMappingProfile));
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.UseCors("AllowMultipleOrigins");
app.UseSwagger();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger-api-docs";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
