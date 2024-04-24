using Microsoft.EntityFrameworkCore;
using System;
using HCA.Service;
using HCA.Repository;
using WebAPI.Data;
using Serilog;
using Notofication.Service;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(Options =>
    {
        Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(o =>
    {
        o.FileProviders.Add(new PhysicalFileProvider(Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "bin\\Debug\\net8.0\\Email\\Templates")).Replace(Path.DirectorySeparatorChar, '/')));
    });
}
else
{
    builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(o =>
    {
        o.FileProviders.Add(new PhysicalFileProvider(Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "Email\\Templates")).Replace(Path.DirectorySeparatorChar, '/')));
    });
}

builder.Services.HCAService();
builder.Services.HCARepository();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowCors", policy =>
    {
        policy.WithOrigins(config["Cors"]).AllowCredentials().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.NotificationService(config);

var app = builder.Build();

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
