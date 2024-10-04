using Microsoft.EntityFrameworkCore;// nugget entity frameworkCote.inMemory
using PRDH.DataBase;
using PRDH.services;

var builder = WebApplication.CreateBuilder(args);


// register the angular cors to allow calls from angular local host 4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200") // Replace with your Angular app URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

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

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
