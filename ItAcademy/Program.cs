using ItAcademy;
using ItAcademy.Extensions;

var builder = WebApplication.CreateBuilder(args);

var jwtTokenOptions = new JwtTokenOptions(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.RegisterJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();