using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache()
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
    .AddReact()
    .AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
    .AddChakraCore();

var app = builder.Build();

app.UseReact(config => { });
app.UseDefaultFiles();
app.UseStaticFiles();

app.Map("/", () => "index page");

app.Run();
