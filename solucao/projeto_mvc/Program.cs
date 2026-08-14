using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using projeto_mvc.Data;
using projeto_mvc.Models.Infra;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<UsuarioDaAplicacao, IdentityRole>().AddEntityFrameworkStores<IESContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Infra/Acessar";
    options.AccessDeniedPath = "/Infra/AcessoNegado";
});


// Add services to the container.
builder.Services.AddControllersWithViews();

//// cria o contexto do banco de dados
builder.Services.AddDbContext<IESContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IESConnection")));
//// e por meio da lambda, configura o contexto para buscar a string de conexão criada no appsettings.json

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    //.WithStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

// o bloco seguinte cria um escopo de serviço que inicializa o banco de dados caso ainda não exista
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<IESContext>();
        //IESDbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um	erro ocorreu ao	popular a base de dados.");

    }
}

app.Run();