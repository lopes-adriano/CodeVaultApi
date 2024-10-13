using System.Text.Json.Serialization;
using CodeVaultApi.Data;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Mappers;
using CodeVaultApi.Models;
using CodeVaultApi.Repository;
using CodeVaultApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeVault API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Please enter a valid token",
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            new List<string>()
        },
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddAutoMapper(typeof(SnippetMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(TagMappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder
    .Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultScheme =
            options.DefaultForbidScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:SigningKey"] ?? string.Empty
                )
            ),
        };
    });

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ISnippetRepository, SnippetRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

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
