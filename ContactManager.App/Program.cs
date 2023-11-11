using ContactManager.DAL;
using ContactManager.Hubs;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ContactManager.Data.Contexts;
using ContactManager.App.Extentions;
using Serilog;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration)
                       => configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
builder.Services.AddRazorPages()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<ApplicationContext>(options =>
                options
                    .UseSqlServer(builder.Configuration.GetConnectionString("ContactDb"),
                        opts => opts.CommandTimeout(600)));

builder.Services.AddSignalR();
builder.Services.AddApplicationServices();
builder.Services.AddElmahIo(o =>
{
    o.ApiKey = builder.Configuration["ElmahIo:ApiKey"];
    o.LogId = new Guid(builder.Configuration["ElmahIo:LogId"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dataContext.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseElmahIo();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ContactHub>("/contacthub");

app.Run();
