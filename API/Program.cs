using DAL;
using DAL.Repositories;
using Core.Services;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
//ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8,1,0))
    ));

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ITextSummaryService, TextSummaryService>();
builder.Services.AddHttpClient<ITextSummaryService, TextSummaryService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CommentHub>("/hubs/comments");

var retries = 5;
var delay = TimeSpan.FromSeconds(3);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    while (retries > 0)
    {
        try
        {
            dbContext.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Database migration failed: {ex.Message}. Retries left: {retries}");
            Thread.Sleep(delay);
        }
    }

    if (retries == 0)
    {
        throw new Exception("Database migration failed after multiple attempts.");
    }
}

    app.Run();
