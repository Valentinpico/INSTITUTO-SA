using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.AutoMappers;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Domain.Ports;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Modules.Billboard.Aplication.Services;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Booking.Aplication.Services;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Aplication.Services;
using reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Movie.Aplication.Services;
using reserva_butacas.Modules.Movie.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Room.Aplication.Services;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Aplication.Services;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Billboard
builder.Services.AddScoped<IBillboardService, BillboardService>();
builder.Services.AddScoped<IBillboardRepository, BillboardRepository>();
//booking
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
//Customer
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//Movie
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
//Room
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
//Seat
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();


// Example in Program.cs or Startup.cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure custom Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
//mappers
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
