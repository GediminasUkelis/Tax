using TaxRepo.Api.Infrastructure.Middleware;
using TaxRepo.Application.Services;
using TaxRepo.Application.Services.Interfaces;
using TaxRepo.Infrastructure.Persistence;
using TaxRepo.Infrastructure.Repositories;
using TaxRepo.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNpgsql<AppDbContext>(connectionString);

builder.Services.AddControllers();

builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<ICityService, CityService>();

builder.Services.AddTransient<ITaxRuleService, TaxRuleService>();
builder.Services.AddTransient<ITaxRuleRepository, TaxRuleRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();