using Smart_Home_Automation_System_MVC.Handlers;
using Smart_Home_Automation_System_MVC.Interfaces;
using Smart_Home_Automation_System_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IApplianceService, ApplianceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<TokenHandler>();

builder.Services.AddHttpClient("MyAPI", client =>
{
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
