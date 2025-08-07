var builder = WebApplication.CreateBuilder(args);

// Agregá esto ANTES de builder.Build()
builder.Services.AddSession(); // <= ¡IMPORTANTE!
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Agregá esto ANTES de app.MapControllerRoute(...)
app.UseSession(); // <= ¡IMPORTANTE!

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();