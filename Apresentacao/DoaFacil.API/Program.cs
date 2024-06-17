using DoaFacil.API.Configuration;
using DoaFacil.Estoque.Infra.Ioc;
using DoaFacil.Pedidos.Infra.Ioc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.RegisterServices();

builder.Services.RegisterServicesEstoque();

builder.Services.RegisterServicesPedidos();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


app.UseApiConfiguration(app.Environment);

app.UseSwaggerConfiguration();

app.Run();

public partial class Program { }
