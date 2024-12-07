using Application;
using Infrastructure;
using Serilog;
using Microsoft.Extensions.Configuration;
using static WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// cors политики
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastracture(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddWebApi();


// public static string connectionString = "Host=localhost;Port=5432;Database=killchan;Username=postgres;Password=4z5636spxr1p8wxkb186akyr84e4e7o78";
//docker run -p 5432:5432 --name postgres -e POSTGRES_PASSWORD=4z5636spxr1p8wxkb186akyr84e4e7o78 -d postgres

var app = builder.Build();


// cors политики 2
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        "default",
        "api/{controller}/{action}/{id?}");
});
#pragma warning restore ASP0014

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();