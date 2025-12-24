using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api.Infrastructure.Data;
using System.Text;
using MediatR;
using FluentValidation;
using Api.Infrastructure.Seed;
using Api.Application.Interfaces.Repositories;
using Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------
// 1. Configuração de Serviços (DI - Injeção de Dependência)
// ----------------------------------------------------------------

// --- CONFIGURAÇÃO DE CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// MediatR (Camada de Application)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Api.Application.Handlers.Categoria.Listar.ListarCategoriasHandler).Assembly);
});

// Entity Framework & PostgreSQL (Camada de Infrastructure)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));


// --- REGISTRO DOS REPOSITÓRIOS ---
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Configuração de Autenticação JWT
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

// ATIVAÇÃO DO CORS (Deve vir antes da Autenticação e Autorização)
app.UseCors("ReactPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ----------------------------------------------------------------
// 3. Execução do Seeder (População inicial do Banco)
// ----------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await GastoDbSeeder.SeedAsync(services);
        Console.WriteLine(">>> Banco de dados populado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($">>> Erro ao popular o banco: {ex.Message}");
    }
}

app.Run();