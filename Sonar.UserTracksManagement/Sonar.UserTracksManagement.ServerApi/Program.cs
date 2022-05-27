using Microsoft.EntityFrameworkCore;
using ServerApi.Filters;
using Sonar.UserTracksManagement.Application.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilter());
});
builder.Services.AddDbContext<UserTracksManagementDatabaseContext>(opt =>
    opt.UseSqlite($"Filename={builder.Configuration["DatabaseFileName"]}"));
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