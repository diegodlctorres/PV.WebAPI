using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PV.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Plato API", Version = "v1" });
            });



// Configure the DbContext
builder.Services.AddDbContext<DbPlatoVoladorContext>(options =>
{
    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .Build();

    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plato API");
                options.RoutePrefix = string.Empty;
            });

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();