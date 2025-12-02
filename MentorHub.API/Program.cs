using FluentValidation;
using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Data;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services;
using MentorHub.API.Services.Interfaces;
using MentorHub.API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<MentorHub.API.Repositories.IUnitOfWork, MentorHub.API.Repositories.UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher<Accounts>, PasswordHasher<Accounts>>();

// Add services to the container.
// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MentorHubDbContext>(options => options.UseSqlServer(connectionString));

// Register Repository Service
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILearningGoalRepository, LearningGoalsRepository>();
builder.Services.AddScoped<IMenteeGoalRepository, MenteeGoalRepository>();
builder.Services.AddScoped<IMentorSkillRepository, MentorSkillRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();


// Register Business Logic Service
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IMentorSkillService, MentorSkillService>();
builder.Services.AddScoped<IMenteeGoalService, MenteeGoalsService>();
builder.Services.AddScoped<ILearningGoalService, LearningGoalService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddValidatorsFromAssemblyContaining<SkillRequestValidator>();

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(Program).Assembly);

var smtpServer = builder.Configuration["EmailSettings:MailServer"];
var smtpPort = builder.Configuration["EmailSettings:MailPort"];
var smtpUsername = builder.Configuration["EmailSettings:MailUsername"];
var smtpPassword = builder.Configuration["EmailSettings:MailPassword"];
var smtpForm = builder.Configuration["EmailSettings:MailForm"];
builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
    smtpServer ?? "localhost",
    Convert.ToInt16(smtpPort),
    smtpUsername ?? "mentorHub",
    smtpPassword ?? "mentorHub123",
    smtpForm ?? "mentorHub@gmail.com"
));

builder.Services.AddScoped<IHashHandler, HashHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<MentorHubDbContext>(options =>
//     options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
