using ApiCp.Data;

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      builder =>
//                      {
//                          builder.WithOrigins("http://localhost:8081/",
//                                              "http://192.168.1.173:8081/").AllowAnyHeader().AllowAnyMethod();
//                      });
//});
// Add services to the container.
builder.Services.AddCors();
//builder.Configuration["ConnectionStrings:DefaultConnection"]
var mysqlcon = new MysqlConfiguration(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(mysqlcon);
builder.Services.AddScoped<TablaClientes>();
builder.Services.AddScoped<TablaPagos>();

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
