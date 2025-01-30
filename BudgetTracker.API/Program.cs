using BudgetTracker.Database.DbContexts;
using BudgetTracker.Database.Implementations;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.AutoMappers;
using BudgetTracker.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BudgetTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Budget Tracker API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<BudgetTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BudgetTrackerDb")));
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Add your repository implementations  
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeServices, IncomeServices>();
builder.Services.AddScoped<IBudgetLimitRepository, BudgetLimitRepository>();
builder.Services.AddScoped<IBudgetLimitService, BudgetLimitService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IDataVisualizationService, DataVisualizationService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddAutoMapper(typeof(BudgetTrackerMapper));

var jwtSection = builder.Configuration["Jwt:Secret"];
builder.Services.AddAuthentication(Options =>
{

    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Budget Trackers API v1");
        c.RoutePrefix = "swagger";
    });
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();


