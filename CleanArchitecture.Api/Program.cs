using CleanArchitecture.Application;
using CleanArchitecture.Application.Mapping;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddScoped<IEventPublisher, EventPublisher>();

builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplicationService();






//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
