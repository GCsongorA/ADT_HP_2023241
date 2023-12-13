using ISTSU0_ADT_2023241.Logic;
using ISTSU0_ADT_2023241.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBandLogic, BandLogic>();
builder.Services.AddScoped<IBandRepository, BandRepository>();
builder.Services.AddScoped<IGuitaristLogic, GuitaristLogic>();
builder.Services.AddScoped<IGuitaristRepository, GuitaristRepository>();
builder.Services.AddScoped<IGuitarLogic, GuitarLogic>();
builder.Services.AddScoped<IGuitarRepository, GuitarRepository>();
builder.Services.AddDbContext<GuitarDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
