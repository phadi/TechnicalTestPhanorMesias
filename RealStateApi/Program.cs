using Microsoft.EntityFrameworkCore;
using RealStateDataModel.DataModel;
using RealStateService.Interfaces;
using RealStateService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DbRealStateCompanyApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectionString"));
});

builder.Services.AddDbContext<DbRealStateCompanyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectionString"));
});

builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IPropertyApiService, PropertyApiService>();
builder.Services.AddTransient<IOwnerService, OwnerService>();

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
