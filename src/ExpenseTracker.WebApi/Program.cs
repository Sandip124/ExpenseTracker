using ExpenseTracker.Infrastructure.Configurations;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web;
using ExpenseTracker.WebApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(c =>
    c.UseMySQL(builder.Configuration.GetConnection()));

builder.Services.AddCookiePolicyConfiguration();

builder.Services.Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });

builder.Services.AddCookieAuthenticationConfiguration(builder.Configuration.GetSecret());

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.UseExpenseTracker();

builder.Services.AddControllers();
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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();