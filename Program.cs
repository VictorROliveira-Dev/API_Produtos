using APIProdutos.Data;
using APIProdutos.Repositories;
using APIProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Para gerenciamento de produtos.",
        Version = "v1",
        Description = "Sistema completo para gerenciamento de produtos, incluindo categorias e lojas.",
        Contact = new OpenApiContact
        {
            Name = "Victor Oliveira",
            Email = "victoroli.dev@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/victor-oli/")
        }
    });

    var xmlFile = "APIProdutos.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    doc.IncludeXmlComments(xmlPath);
});

// Configuration database
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCS"));
});

// Configuration Inject Dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductStoreRepository, ProductStoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
