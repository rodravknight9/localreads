using LocalReads.API.Configurations;
using LocalReads.API.Context;
using LocalReads.API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//builder.Services.AddOpenApi();
builder.Services.AddDbContext<LocalReadsContext>(options =>
    options.UseSqlite("Data Source=app.db"));


var app = builder.Build();
app.UseCors(x =>
    x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapUserEndpoints();
app.MapFavoriteEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<LocalReadsContext>();
        db.Database.Migrate(); 
    }
}

app.UseHttpsRedirection();
app.Run();

