using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using wallet_microservice_dotnet._1.Domain.DatabaseContext;
using wallet_microservice_dotnet._1.Domain.Entities;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;
using wallet_microservice_dotnet._2.Application.Behaviours;
using wallet_microservice_dotnet._2.Application.Services;
using wallet_microservice_dotnet._2.Application.UseCases;
using wallet_microservice_dotnet._3.Infraestructure;
using wallet_microservice_dotnet._3.Infraestructure.ClientSerivces;
using wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_dotnet._4.Presentation.Middleware;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

{
    var configuration = builder.Configuration;
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // busca todas las assemblies de profile en el projecto,
    // sino seria necesario añadirlas manualmente
    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    builder.Services.AddDbContextPool<DbContextData>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
    .EnableSensitiveDataLogging()); //dot not use in production EnableSensitiveDataLogging


    //IoC
    builder.Services.AddDependency(configuration);



    var app = builder.Build();

    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
    }

    app.UseAuthorization();

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        DbContextData context = scope.ServiceProvider.GetRequiredService<DbContextData>();
        context.Database.EnsureDeletedAsync().Wait();
        context.Database.EnsureCreatedAsync().Wait();
    }


    app.Run();

}

public static class IoC
{
    public static IServiceCollection AddDependency(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<StripeHttpResponseErrorHandler>();
        // Registrar el servicio HTTP de Stripe con un manejador de errores
        services.AddHttpClient<StripeService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("StripeEndpoint"));
        }).AddHttpMessageHandler<StripeHttpResponseErrorHandler>();

        // Registrar las dependencias del repositorio genérico
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<WalletUseCase>();

        // Registrar los manejadores específicos de excepción
        services.AddSingleton<IErrorHandler, ApplicationExceptionHandler>();
        services.AddSingleton<IErrorHandler, RegexParseExceptionHandler>();
        services.AddSingleton<IErrorHandler, KeyNotFoundExceptionHandler>();

        return services;
    }
}
