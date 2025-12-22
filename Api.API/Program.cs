using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api.Infrastructure.Data;
using System.Text;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------
// 1. Configuração de Serviços (DI - Injeção de Dependência)
// ----------------------------------------------------------------

// MediatR (Camada de Application)
// Aqui ele busca todos os Handlers dentro do projeto Application
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Api.Application"));

// Entity Framework & PostgreSQL (Camada de Infrastructure)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<AppDbContext>(options => 
//    options.UseNpgsql(connectionString));

// Configuração de Autenticação JWT (Camada de Authentication)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "ChaveSuperSecretaDeExemplo123!"))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configurado para aceitar Token JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle Gasto Residencial API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ----------------------------------------------------------------
// 2. Configuração do Pipeline de Requisição (Middleware)
// ----------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();