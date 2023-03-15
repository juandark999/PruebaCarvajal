using Api.Bll;
using Api.Context;
using Api.IBll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>();
builder.Services.AddScoped<IUsuarioBusinessLogic, UsuarioBusinessLogic>();
builder.Services.AddScoped<IProductoBusinessLogic, ProductoBusinessLogic>();
builder.Services.AddScoped<IPedidoBusinessLogic, PedidoBusinessLogic>();
builder.Services.AddScoped(typeof(IGenericContext<>), typeof(GenericContext<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
