using MagnificentSuperHeroes.ServerAPI.MSHBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration.
    GetValue<string>("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<MagSuperHeroContext>
    (options => options.
    UseSqlServer(dbConnectionString));

builder.Services.AddControllers();

builder.Services.AddDirectoryBrowser();

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

app.UseCors(x => x
                 .AllowCredentials()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .SetIsOriginAllowed(origin => true));

app.UseStaticFiles();

app.UseFileServer(
    new FileServerOptions
    {
        FileProvider = new 
        PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath
            ,"SuperHeroPics")),
        RequestPath = "/SuperHeroPics",
        EnableDirectoryBrowsing = true
    }
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
