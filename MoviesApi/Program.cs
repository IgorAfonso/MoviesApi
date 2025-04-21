using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesApi.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("3.1.0", new OpenApiInfo { Title = "Meu API", Version = "3.1.0" });
//
//     // 🔐 Configuração do JWT Bearer
//     var securityScheme = new OpenApiSecurityScheme
//     {
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Description = "Insira o token JWT assim: **Bearer seu_token_aqui**"
//     };
//
//     var securityRequirement = new OpenApiSecurityRequirement
//     {
//         { securityScheme, new[] { "Bearer" } }
//     };
//
//     c.AddSecurityDefinition("Bearer", securityScheme);
//     c.AddSecurityRequirement(securityRequirement);
// });

//Add Dependency Injection
builder.Services.RegisterServices();

//Add JWT Authentication and Authorization
var key = Encoding.ASCII.GetBytes(Configuration.GetSectionValue("Jwt", "Key") ?? string.Empty);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // ← NÃO valida o issuer
            ValidateAudience = false, // ← NÃO valida o audience
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
