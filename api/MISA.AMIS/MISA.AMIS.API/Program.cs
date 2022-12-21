using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
    option.SuppressModelStateInvalidFilter = true
);

// Dependencies injection
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();
builder.Services.AddScoped<IDepartmentBL, DepartmentBL>();

DatabaseContext.ConnectionString =  builder.Configuration.GetConnectionString("MySql");

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});
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

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();