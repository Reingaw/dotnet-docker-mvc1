using Microsoft.EntityFrameworkCore;
using mvc1.Models;

var builder = WebApplication.CreateBuilder(args);

var host = Environment.GetEnvironmentVariable("DBHOST");
string mySqlConnection = string.Empty;

if (string.IsNullOrEmpty(host)) 
{
    mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    mySqlConnection = $"Server={host};{builder.Configuration.GetConnectionString("DockerConnection")}";
}

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddTransient<IRepository, ProdutoRepository>();
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

//PopulaDb.IncluiDadosDB(app);

app.Run();
