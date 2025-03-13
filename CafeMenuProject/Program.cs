using Microsoft.EntityFrameworkCore;
using CafeMenuProject.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Veritabanı bağlantısını ekleyin
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()); // Veritabanı loglarını açar);



// 🔹 MVC desteğini ekleyin
builder.Services.AddControllersWithViews();

var app = builder.Build();



// 🔹 Middleware ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Admin Dashboard Route (Özel Route)
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/Dashboard",
    defaults: new { controller = "Admin", action = "Dashboard" }
);

// 🔹 Varsayılan Route Tanımlama
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();


