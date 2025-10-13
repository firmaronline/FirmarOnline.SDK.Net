using FirmarOnline.Clients.PSC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Registro del cliente PSC en el contenedor de dependencias
builder.Services.AddScoped<PSCClient>(serviceProvider =>
{
    // Configuración directa como en la aplicación de consola
    const string authenticationToken = "e199ba90-3c50-4715-9be8-dee52f9a87c7";
    const bool isProduction = false;
    
    var apiUrl = isProduction 
        ? PSCClient.PSCProductionEnvironmentUrl 
        : PSCClient.PSCSandboxEnvironmentUrl;
    
    return new PSCClient(apiUrl, authenticationToken);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();