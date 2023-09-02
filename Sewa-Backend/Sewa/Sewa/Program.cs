using Microsoft.AspNetCore.Mvc;
using Sewa.Filters;
using Sewa_Application;
using Sewa_Infastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
await builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);

//Add Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        var allowOrigins = builder.Configuration.GetSection("AllowOrigins").Get<string[]>();
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(allowOrigins).AllowCredentials();
    });
});

builder.Services.AddControllers(options =>
                   options.Filters.Add<ApiExceptionFilter>());

//Disabled default model validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


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

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
