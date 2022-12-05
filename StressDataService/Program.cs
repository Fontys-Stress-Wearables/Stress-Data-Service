using StressDataService.Database;
using StressDataService.Interfaces;
using StressDataService.Nats;
using StressDataService.Repositories;
using StressDataService.Services;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHrvMeasurementService, HrvMeasurementService>();
builder.Services.AddSingleton<IHrvMeasurementRepository, HrvMeasurementRepository>();

builder.Services.AddSingleton<INatsService, NatsService>();
builder.Services.AddSingleton<InfluxDbService>();
builder.Services.AddScoped<InfluxDbSeeder>(); //can be placed among other "AddScoped" - above: var app = builder.Build();   

builder.Services.AddSingleton<ProcessedDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<InfluxDbSeeder>();
    seeder.Seed();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();


