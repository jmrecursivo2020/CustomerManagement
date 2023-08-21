using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
// Obtener la configuraci�n del archivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
// Obtener la cadena de conexi�n de la configuraci�n
var connectionString = configuration.GetConnectionString("DefaultConnection");

// A�adir la cadena de conexi�n al contenedor de servicios
//builder.Services.AddSingleton(connectionString);
builder.Services.AddSingleton<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientRepository>(provider => new ClientRepository(connectionString));
builder.Services.AddScoped<IClientService, ClientService>();
// Aprende m�s sobre c�mo configurar Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
