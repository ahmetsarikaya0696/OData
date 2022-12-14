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

    #region Actions Without Parameters
    // .../OData/Categories(1)/TotalProductPrice
    oDataConventionModelBuilder.EntityType<Category>().Action("TotalProductPrice")
                                                      .Returns<int>();

    // .../OData/Categories/TotalProductPrice2
    oDataConventionModelBuilder.EntityType<Category>().Collection
                                                      .Action("TotalProductPrice2")
                                                      .Returns<int>();
    #endregion

    #region Actions With Parameters
    // .../OData/Category/TotalProductPrice
    oDataConventionModelBuilder.EntityType<Category>().Collection
                                                      .Action("TotalProductPriceWithParameter")
                                                      .Returns<int>()
                                                      .Parameter<int>("categoryId");

    var actionTotal = oDataConventionModelBuilder.EntityType<Category>().Collection
                                                                        .Action("Total")
                                                                        .Returns<int>();

    actionTotal.Parameter<int>("a");
    actionTotal.Parameter<int>("b");
    actionTotal.Parameter<int>("c");


    oDataConventionModelBuilder.EntityType<Product>().Collection
                                                     .Action("Login")
                                                     .Returns<string>()
                                                     .Parameter<Login>("Login");
    #endregion

    #region Functions Without Parameters
    oDataConventionModelBuilder.EntityType<Category>().Collection
                                                      .Function("CategoryCount")
                                                      .Returns<int>();
    #endregion

    #region Functions With Parameter

    var multiplyFunction = oDataConventionModelBuilder.EntityType<Product>().Collection
                                                                            .Function("MultiplyFunction")
                                                                            .Returns<int>();

    multiplyFunction.Parameter<int>("a1");
    multiplyFunction.Parameter<int>("a2");
    multiplyFunction.Parameter<int>("a3");
   
    oDataConventionModelBuilder.EntityType<Product>().Function("KdvHesapla")
                                                     .Returns<double>()
                                                     .Parameter<double>("kdv");
    #endregion

    #region Unbound Functions
    // Ayn? ?ekilde Action da yaz?labilir.
    oDataConventionModelBuilder.Function("KdvHesapla")
                               .Returns<double>();
    #endregion


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
