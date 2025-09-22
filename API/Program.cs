using API.Data;
using API.Mappings;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
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

// STEP 4
builder.Services.AddScoped<ISubjectRepository, SQLSubjectRepository>();

// STEP 4 AUTO MAPPER
builder.Services.AddAutoMapper(typeof(AutoMappersProfile));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// STEP 1
app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
