using WeatherApplication.Business.Abstracts;
using WeatherApplication.Business.Concrete;
using WeatherApplication.DataAccess.Abstract;
using WeatherApplication.DataAccess.Concrete;
using WeatherApplication.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddHostedService<WeatherReportBackgroundService>();
builder.Services.AddScoped<IWeatherReportService, WeatherReportServiceDal>();
builder.Services.AddScoped<IWeatherReportDal, WeatherReportDal>();
builder.Services.AddScoped<IDistrictDal,DistrictDal>();
builder.Services.AddScoped<IDistrictService, DistrictServiceDal>();
builder.Services.AddScoped<IWeatherReportXmlService, WeatherReportXmlService>();
builder.Services.AddHostedService<WeatherReportXmlBackgroundService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

