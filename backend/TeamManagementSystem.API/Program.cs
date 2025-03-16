using static TeamManagementSystem.Application.DependencyInjection;
using static TeamManagementSystem.Infrastructure.DepedencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000") // Allow React frontend
            .AllowAnyMethod()  // Allow GET, POST, PUT, DELETE, etc.
            .AllowAnyHeader()  // Allow all headers
            .AllowCredentials()); // Allow cookies/auth headers if needed
});


var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty; // Swagger will be served at the root
    });
}

app.UseHttpsRedirection();
// Use CORS before other middlewares
app.UseCors("AllowReactApp");

app.UseAuthentication();

app.Run();
