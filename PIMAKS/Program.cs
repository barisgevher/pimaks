using PIMAKS.Models;
using Microsoft.EntityFrameworkCore;
using PIMAKS.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKiralamaService, KiralamaService>();
builder.Services.AddScoped<ISahisService, SahisService>();
builder.Services.AddScoped<IAnaSayfaService, AnaSayfaService>();
builder.Services.AddScoped<IFirmaService, FirmaService>();
builder.Services.AddScoped<INakliyeService, NakliyeService>();
builder.Services.AddScoped<ICariService, CariService>();
builder.Services.AddScoped<IMakineService, MakineService>();
builder.Services.AddScoped<ITedarikciService, TedarikciService>();
builder.Services.AddScoped<IIstatistikService, IstatistikService>();


builder.Services.AddDbContext<PimaksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});




var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();

app.Run();
