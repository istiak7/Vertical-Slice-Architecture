using FluentValidation;
//using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Middleware;
using Vertical_Slice_Architecture.Shared;
using Vertical_Slice_Architecture.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger services
builder.Services.AddSwaggerGen();


//Database Registration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//MediatR Registration
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediator(typeof(Program).Assembly);
//FluentValidation Registration
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//Validation Behavior PipelineRegistration
//builder.Services.AddTransient(
//    typeof(IPipelineBehavior<,>),
//    typeof(ValidationBehavior<,>));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    });
}

// Map endpoints dynamically
var endpoints = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

foreach (var endpoint in endpoints)
{
    var instance = Activator.CreateInstance(endpoint) as IEndpoint;
    instance?.MapEndpoint(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
