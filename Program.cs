using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;

AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

var builder = WebApplication.CreateBuilder(args);

//��������� � ������ �������� MVC.
builder.Services.AddControllersWithViews();

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

//app.UseHttpsRedirection();

//����������� ��������� �������������.
app.UseRouting();

//app.UseAuthorization();

//app.MapStaticAssets();

//����������� ������� ������������� ������� �������� �������� � �������� � ������������.
app.MapControllerRoute(
    name: "default",
    //������� ������� ���������.
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
