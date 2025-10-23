using MinhaApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //avisamos que iremos trabalhar com controllers
builder.Services.AddDbContext<AppDbContext>();  

var app = builder.Build(); 

app.MapControllers(); //mapea os controller 



app.Run(); 