using System.Text;
using Dapper.Contrib.Extensions;
using SlaveWorkColab.Api.DataAccess;
using SlaveWorkColab.Api.DataAccess.Interfaces;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Api.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TecPurisima.Ecommerce.Api.Services;
using TecPurisima.Ecommerce.Api.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton<IProjectsRepository, ProjectsRepository>();
builder.Services.AddSingleton<ITasksRepository, TasksRepository>();
builder.Services.AddSingleton<ICommentsRepository, CommentsRepository>();

builder.Services.AddSingleton<IUsersService, UsersService>();
builder.Services.AddSingleton<IProjectsService, ProjectsService>();
builder.Services.AddSingleton<ITasksService, TasksService>();
builder.Services.AddSingleton<ICommentsService, CommentsService>();

builder.Services.AddSingleton<IDbContext, DbContext>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("SlaveWorkColab.Core.Entities"))
    {
        name = name.Replace("SlaveWorkColab.Core.Entities.", "");
    }

    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};




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
