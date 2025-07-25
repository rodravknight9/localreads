using LocalReads.API.Configurations;
using LocalReads.API.Context;
using LocalReads.API.Endpoints;
using LocalReads.API.Hubs;
using LocalReads.API.Middlewares;
using LocalReads.API.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IGoogleService, GoogleService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddMapster();
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

//builder.Services.AddOpenApi();
builder.Services.AddDbContext<LocalReadsContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
    };
});
builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors(x =>
    x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapUserEndpoints();
app.MapFavoriteEndpoints();
app.MapGoogleBooksEndpoints();
app.MapNotificationsEndpoints();


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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalReadsContext>();
    db.Database.Migrate();
}

app.UseMiddleware<UserMiddleware>();
app.MapHub<NotificationHub>("/notification-hub");

app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection();
app.Run();

