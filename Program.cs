using AnnaMelnyk_TestTask.Services;
using App.Data;
using App.Services;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
         policy =>
         {
             policy.WithOrigins("http://localhost:3000/").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
         });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddScoped<ICSVReaderService, CSVReaderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//void ConfigureServices(IServiceCollection services)
//{
//    services.AddScoped<IRequestService, RequestService> ();
//}
//builder.Services.AddScoped<();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
        context.Response.Headers.Add("Access-Control-Max-Age", "86400");
        context.Response.StatusCode = 204;
        await context.Response.CompleteAsync();
    }
    else
    {
        await next();
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
