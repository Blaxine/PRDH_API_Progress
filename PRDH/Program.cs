using Microsoft.EntityFrameworkCore;// nugget entity frameworkCote.inMemory
using Microsoft.Extensions.Configuration;
using PRDH.constants;
using PRDH.DataBase;
using PRDH.mapers;
using PRDH.services;

var builder = WebApplication.CreateBuilder(args);
var angularHost = builder.Environment.IsDevelopment() ? builder.Configuration[PrdhContants.ALLOWED_ORIGIN.LOCAL_HOST] : builder.Configuration[PrdhContants.ALLOWED_ORIGIN.PRODUCTION_HOST];
// register the angular cors to allow calls from angular local host 4200
builder.Services.AddCors(options =>
{
    options.AddPolicy(PrdhContants.CORS_POLICY,
        policy => policy
            .WithOrigins(angularHost)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(CovidMinimalToCasesMapper));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddLogging();
//Register the worker service to be intanciated
builder.Services.AddScoped<WorkerService>();

// register the database with the database context to be used 
//.in memory to use UseInmemoryDatabase
builder.Services.AddDbContext<CaseDataBaseContext>(options =>
    options.UseInMemoryDatabase("CaseDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(PrdhContants.CORS_POLICY);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
