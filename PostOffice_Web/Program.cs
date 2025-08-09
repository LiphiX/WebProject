using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;

AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

var builder = WebApplication.CreateBuilder(args);

//��������� � ������ �������� MVC.
builder.Services.AddControllersWithViews();

//����������� � ������ �������� ��� ���������� ������.
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Authentication/Registration");
        options.LoginPath = new PathString("/Authentication/Registration");
    });
builder.Services.AddAuthorization();

//������ � ���������������� ����� ������ ����������� � ���� ������.
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

//�������� ������� � ������������ ������ �� �������� ���������� (wwwroot).
app.UseStaticFiles();

app.UseHttpsRedirection();

//����������� ��������� �������������.
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

//app.MapStaticAssets();

//����������� ������� ������������� ������� �������� �������� � �������� � ������������.
app.MapControllerRoute(
    name: "default",
    //������� ������� ���������.
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
