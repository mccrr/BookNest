using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookNest.Data;
using BookNest.DataAccess;
using BookNest.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using BookNest.Middleware;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Herhangi bir domain
              .AllowAnyHeader() // Herhangi bir header
              .AllowAnyMethod(); // Herhangi bir HTTP metodu (GET, POST, vb.)
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 40))
    )
);
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserDao>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookDao>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<ReviewDao>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ShelfService>();
builder.Services.AddScoped<ShelfDao>();
builder.Services.AddScoped<ShelfBookDao>();
builder.Services.AddScoped<ShelfBookService>();
builder.Services.AddScoped<BookUserDao>();
builder.Services.AddScoped<BookProgressService>();
builder.Services.AddScoped<ChallengeDao>();
builder.Services.AddScoped<ChallengeService>();
builder.Services.AddScoped<FriendsDao>();
builder.Services.AddScoped<FriendsService>();
builder.Services.AddScoped<AchievementDao>();
builder.Services.AddScoped<AchievementService>();
builder.Services.AddScoped<GoogleService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:AccessTokenSecret"]))
                };
            });

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<TokenValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll"); // Veya "AllowAll"

app.Run();