using API.DtosValidator;
using DBModels;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITask, TaskRepo>();
builder.Services.AddControllers().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<TaskDtoValidator>());


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("UserCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Replace with your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS middleware
app.UseCors("UserCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
