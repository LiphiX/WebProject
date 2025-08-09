using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;

AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

var builder = WebApplication.CreateBuilder(args);

//Включение в проект сервисов MVC.
builder.Services.AddControllersWithViews();

//Подключение в проект сервисов для реализации сеанса.
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Authentication/Registration");
        options.LoginPath = new PathString("/Authentication/Registration");
    });
builder.Services.AddAuthorization();

//Чтение в конфигурационном файле строки подключения к базе данных.
var connection = builder.Configuration.GetConnectionString("ConnectionString01");
builder
	.Services
	.AddDbContext<PostOfficeContext>(configruation => configruation
																  .UseLazyLoadingProxies()
																  .UseSqlServer(connection));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Открытие доступа к обслуживанию файлов из корневой директории (wwwroot).
app.UseStaticFiles();

app.UseHttpsRedirection();

//Подключение поддержки маршрутизации.
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

//app.MapStaticAssets();

//Подключение системы сопоставления адресов входящих запросов с методами в контроллерах.
app.MapControllerRoute(
    name: "default",
    //Задание шаблона маршрутов.
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
