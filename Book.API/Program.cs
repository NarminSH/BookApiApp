using Book.BL;
using Book.BL.DTOs.BookDtos;
using Book.BL.Profiles.BookProfiles;
using Book.BL.Services.Abstractions;
using Book.BL.Services.Implementations;
using Book.Core.Entities;
using Book.Data.DAL;
using Book.Data.Repositories.Abstractions;
using Book.Data.Repositories.Implementations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddBusinessServices();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    {
        
        opt.Password.RequiredLength = 8;
        opt.User.RequireUniqueEmail = true;
        opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        opt.Lockout.MaxFailedAccessAttempts = 3;
    }
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

//new version 
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddValidatorsFromAssembly(typeof(BookCreateDtoValidation).Assembly);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddControllers();
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
        };
    });

//old version
//builder.Services.AddControllers().AddFluentValidation(opt =>
//{
//    opt.RegisterValidatorsFromAssembly(typeof(BookCreateDtoValidation).Assembly);
//    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
