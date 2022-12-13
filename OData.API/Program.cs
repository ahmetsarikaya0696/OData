using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OData.API.Models;
using OData.API.Seeds;
using System.Reflection;

///
/// EDM => Entity Data Model
///
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder oDataConventionModelBuilder = new();
    oDataConventionModelBuilder.EntitySet<Category>("Categories");
    oDataConventionModelBuilder.EntitySet<Product>("Products");

    return oDataConventionModelBuilder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// OData Settings
builder.Services.AddControllers()
                .AddOData(options => options.AddRouteComponents("OData", GetEdmModel()).Filter().Select().Expand().OrderBy().Count().SetMaxTop(null));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ConnectionString
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    x => x.UseSqlServer(
        ConnectionString,
        options => options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name)));

var app = builder.Build();

// Seeding Db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    DataSeed.SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/odata/swagger.json", "OData"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
