using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SPA.Application.Interfaces;
using SPA.Application.Services;
using SPA.CrossCutting;
using SPA.Data.Context;
using SPA.Data.Repositories;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;
using SPA.Domain.Services;
using System.Text;

const string corsPolicyName = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc(opts =>
{
    opts.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresDbContext>(opts => opts //.UseLazyLoadingProxies()
                                                             .UseNpgsql("Server=localhost;Port=5432;Database=SPA;Username=postgres;Password=postgres")
                                                             //.UseNpgsql(EnvironmentVars.ConnectionString)
                                                             .EnableSensitiveDataLogging()
                                                             .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddSerilog(); }))
                                                );

builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>(opts =>
{
    opts.Password.RequireDigit = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireNonAlphanumeric = true;
    opts.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<PostgresDbContext>().AddDefaultTokenProviders();

builder.Services.AddOptions().AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvironmentVars.JwtSecret)),
        ValidIssuer = EnvironmentVars.JwtIssuer,
        ValidAudience = EnvironmentVars.JwtAudience,
        ClockSkew = TimeSpan.Zero
    };
});

// CorsPolicy
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// AppServices
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddScoped<IClienteAppService, ClienteAppService>();
builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
builder.Services.AddScoped<IJwtAppService, JwtAppService>();

// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
