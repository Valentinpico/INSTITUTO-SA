using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.AutoMappers;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Modules.Billboard.Aplication.Services;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Aplication.Services;
using reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Billboard
builder.Services.AddScoped<IBillboardService, BillboardService>();
builder.Services.AddScoped<IBillboardRepository, BillboardRepository>();

//Customer
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();



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

app.UseExceptionHandler();


app.UseAuthorization();

app.MapControllers();

app.Run();
