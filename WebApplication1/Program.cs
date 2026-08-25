using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);
//add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers();

var key = builder.Configuration.GetValue<string>("AppSettings:Secret");
builder.Services.AddAuthentication((u =>
        {
            u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )).AddJwtBearer( u =>
    {
        u.RequireHttpsMetadata = false;
        u.SaveToken = true;
        u.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddOpenApi();
var app = builder.Build();
//configure http request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// this 3 under are used to wwwroot(bindings folders)!!!!
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
//-----------------------
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();