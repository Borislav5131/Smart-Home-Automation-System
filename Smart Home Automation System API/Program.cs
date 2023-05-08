using ApplicationService.Interfaces;
using ApplicationService.Repositories;
using ApplicationService.Services;
using Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SHASContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IApplianceService, ApplianceService>();

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
