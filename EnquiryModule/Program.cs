using EnquiryModule.Infrastructure;
using EnquiryModule.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEnquiryModule, EnquiryModuleRepo>();
builder.Services.AddScoped<EnquiryModuleRepo>();
builder.Services.AddDbContext<EnquiryModuleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(@"Server=(local);database=EnquiryModule;integrated security=sspi;trustservercertificate=true")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
