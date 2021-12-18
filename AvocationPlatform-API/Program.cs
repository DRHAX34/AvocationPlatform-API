using AvocationPlatform_API.Extensions;
using AvocationPlatform_DatabaseAccess.Config;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                   builder.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
            });

//Configure the services
builder.Services.ConfigureBusinessServices();

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
DataAccessConfig.Set(connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
