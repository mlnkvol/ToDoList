using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Налаштування сервісів
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Для сесій
builder.Services.AddSession(); // Додаємо підтримку сесій

var app = builder.Build();

// Налаштування конвеєра обробки запитів
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Використовуємо сесії

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
