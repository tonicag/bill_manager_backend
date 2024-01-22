using BillManager.Data;
using BillManager.Model;
using BillManager.Service.IService;
using BillManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowed_origins",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:3000",
                                              "http://localhost:3000",
                                              "http://127.0.0.1:3000/")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("BillsDB"));
});


builder.Services.AddAutoMapper(typeof(MappingProfile));

var settingsSection = builder.Configuration.GetSection("ApiSettings:JwtOptions");

builder.Services.Configure<JwtOptions>(settingsSection);

var secret = settingsSection.GetValue<string>("Secret");
var issuer = settingsSection.GetValue<string>("Issuer");
var audience = settingsSection.GetValue<string>("Audience");

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddIdentity<Company, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ValidateAudience = true
    };
});

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ICompanyAuthService, CompanyAuthService>();
builder.Services.AddScoped<IBillsService, BillsService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBillEntryService, BillEntryService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

var app = builder.Build();
app.UseCors("allowed_origins");
// Configure the HTTP request pipeline.
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
