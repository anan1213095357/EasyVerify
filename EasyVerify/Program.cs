using EasyVerify.Data;
using Microsoft.AspNetCore.Identity;
using EasyVerify;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new FreeSql.FreeSqlBuilder()
      .UseConnectionString(FreeSql.DataType.Sqlite, connectionString)
      .UseAutoSyncStructure(true)
      .Build());

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<SignInManager>();
builder.Services.AddSingleton<IHostedService>(p => p.GetService<SignInManager>()!);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapControllers();

app.MapFallbackToPage("/_Host");


app.AddIdentityAuthentication();

app.MapBlazorHub();



app.Run();
