using HCAWeb.Client.Services;
using HCAWeb.Client.Services.Interface;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IHCAMaster, HCAMaster>();
builder.Services.AddScoped<ITaskService, TaskService>();
await builder.Build().RunAsync();
