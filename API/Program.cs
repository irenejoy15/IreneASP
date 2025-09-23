using System.Text;
using API.Data;
using API.Mappings;
using API.Middleware;
using API.Models.Domain;
using API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//STEP 3
var logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.Console()
    .WriteTo.File("Logs/Irene.txt" ,rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//STEP 1
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//STEP 2 
builder.Services.AddDbContext<IreneDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IreneConnectionString"));
});

//STEP 6
builder.Services.AddDbContext<IreneAuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IreneAuthConnectionString"));
});

// STEP 4
builder.Services.AddScoped<ISubjectRepository, SQLSubjectRepository>();
builder.Services.AddScoped<ITeacherRepository, SQLTeacherRepository>();

// STEP 4 AUTO MAPPER
builder.Services.AddAutoMapper(typeof(AutoMappersProfile));

// STEP 5 SEE API CONTROLLER

// STEP 6 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
    .AddEntityFrameworkStores<IreneAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        }
    );

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // STEP 1
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseMiddleware<ExceptionHandler>();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
