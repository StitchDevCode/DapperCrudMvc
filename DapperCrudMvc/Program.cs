using DapperCrudMvc.Conexcion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregamos una implementación de IDbConnectionFactory al contenedor de servicios como un singleton.
// Esto significa que durante el ciclo de vida de la aplicación, solo habrá una instancia de IDbConnectionFactory.
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
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

app.Run();
