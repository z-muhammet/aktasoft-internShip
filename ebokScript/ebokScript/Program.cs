using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ebokScript.Data;
using ebokScript;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ebokScriptContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ebokScriptContext") ?? throw new InvalidOperationException("Connection string 'ebokScriptContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Baðlantý dizesini al
var connectionString = configuration.GetConnectionString("ebokScriptContext");

// DbContext'i yapýlandýr
builder.Services.AddDbContext<ebokScriptContext>(options =>
    options.UseSqlServer(connectionString));



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
