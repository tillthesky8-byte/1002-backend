using _1002_backend.Infrastructure.Data;
using _1002_backend.Infrastructure.Middleware;
using _1002_backend.Services;
using _1002_backend.Services.Interfaces;
using _1002_backend.Repositories;
using _1002_backend.Repositories.Interfaces;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlMapper.AddTypeHandler(new DateOnlyHandler());
SqlMapper.AddTypeHandler(new NullableDateOnlyHandler());

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

builder.Services.AddScoped<IDreamEntryRepository, DreamEntryRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddScoped<IDreamEntryService, DreamEntryService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ITodoService, TodoService>();


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
