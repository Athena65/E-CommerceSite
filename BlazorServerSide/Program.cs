using BlazorServerSide.Data;
using BlazorServerSide.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("X-Pagination")

           
        
    );
});
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();

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
using (var scope= app.Services.CreateScope())
{
    var services= scope.ServiceProvider;
    SeedData.Initialize(services);  
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();


app.UseAuthorization();



app.MapControllers();

app.Run();
