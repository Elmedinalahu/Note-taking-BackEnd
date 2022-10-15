using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notetaking.Configs;
using Notetaking.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesAppConnectionString"));
});
builder.Services.Configure<JwtConfigs>(builder.Configuration.GetSection("JwtConfigs"));
builder.Services.AddAuthentication(options =>
{
    
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        //We take the present key that we have and we store it in a variable named "key"
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfigs:Secret").Value);
        // We said that the token must be saved after a successful authorization
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {   // We check if it is a valid key, than we compare that key with our key
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // for developing purposes
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true

        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
                          policy =>
                          {
                              policy.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
