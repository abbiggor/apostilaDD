using Capitulo001.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
    name: "default",
    pattern: "{controller=Instituicao}/{action=Index}/{id?}")
    .WithStaticAssets();

// o bloco seguinte cria um escopo de serviço que inicializa o banco de dados caso ainda não exista
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<IESContext>();
        IESDbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um	erro	ocorreu	ao	popular  a   base    de  dados.");

    }
}

app.Run();
