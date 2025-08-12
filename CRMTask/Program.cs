using CRMTask.Business.Services.IServices;
using CRMTask.Business.Services.Serv;
using CRMTask.DataAccess.Data;
using CRMTask.DataAccess.Repositories.Interfaces;
using CRMTask.DataAccess.Repositories.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ITemplateRepository, TemplateRepo>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepo>();
builder.Services.AddScoped<IMailRepository, MailRepo>();
builder.Services.AddScoped<IUserRepository, UserRepo>();

// Register services
builder.Services.AddHttpClient<IMailchimpService, MailchimpService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();

// Add AutoMapper
// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
