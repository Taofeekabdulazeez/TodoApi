using Microsoft.EntityFrameworkCore;
using TodoApi.Contexts;

var builder = WebApplication.CreateBuilder(args);
var supabaseUrl = "https://csrhrzrwwtnsbdqcdjjv.supabase.co";
var supabase_anon_key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNzcmhyenJ3d3Ruc2JkcWNkamp2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDEyOTIxMjIsImV4cCI6MjA1Njg2ODEyMn0.u70XEww5gCScyY_gbCLOOJMCnDjb-MIVWsICM7jAFPo";

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(supabaseUrl, supabase_anon_key));
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
// builder.Services.AddOpenApi();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TodoContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();