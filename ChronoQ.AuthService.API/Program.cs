using ChronoQ.AuthService.API.Features.Auth.RequestOtp;
using ChronoQ.AuthService.API.Features.Auth.VerifyOtp;
using ChronoQ.AuthService.Application.Services.Implementations;
using ChronoQ.AuthService.Application.Services.Interfaces;
using ChronoQ.AuthService.Infrastructure.Persistence;
using ChronoQ.AuthService.Infrastructure.Redis;
using ChronoQ.AuthService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    try
    {
        return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Redis connection failed: {ex.Message}");
        throw;
    }
});

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOtpStore, RedisOtpStore>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapRequestOtp();
app.MapVerifyOtp();

app.UseHttpsRedirection();




app.Run();

