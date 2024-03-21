using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.JSON_Utils;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// TODO MRS: ler a connection string
var connectionString = builder.Configuration.GetConnectionString("ProjectManager_CS");

// TODO MRS: registar o serviço da EF
builder.Services.AddDbContext<ProjectManager_v00_DbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// TODO MRS: carregar dados iniciais
using (var scope = app.Services.CreateScope())
{

    var serviceProvider = scope.ServiceProvider;

    Initializer.Initialize(serviceProvider);

}

string settingsFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\Settings.json";
if (!JsonFunctions.ExistJsonFile(settingsFile))
{
    JsonFunctions.CreateJsonFile(settingsFile);

}

Dictionary<string, string> dicToAdd1 = new Dictionary<string, string>();
dicToAdd1["ActiveJobID"] = "1";
JsonFunctions.AddToJsonFile(settingsFile, dicToAdd1);
app.Run();
