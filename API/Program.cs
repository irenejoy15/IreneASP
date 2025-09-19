using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
