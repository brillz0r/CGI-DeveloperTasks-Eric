using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookingsApi.Repositories;
using BookingsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC controller support
builder.Services.AddControllers();

builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

// Map controller endpoints
app.MapControllers();

app.Run();
