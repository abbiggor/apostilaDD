using capitulo02.Data;
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

app.UseHttpsRedirection(); // redireciona requests HTTP para HTTPS
app.UseStaticFiles(); // habilita o uso de arquivos estáticos (css, js,
app.UseRouting(); // habilita rotas para que o app possa responder as requests de acordo com as actions dos controllers

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute( // define a rota padrão 
    name: "default",
    pattern: "{controller=Departamento}/{action=Index}/{id?}")
    .WithStaticAssets();

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