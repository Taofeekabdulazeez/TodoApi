using Microsoft.EntityFrameworkCore;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabase_anon_key = builder.Configuration["Supabase:AnonKey"];

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(supabaseUrl, supabase_anon_key));
builder.Services.AddScoped<TodosService>();
// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new() { Title = "Todo API", Version = "v1" });
    }
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
        }
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();