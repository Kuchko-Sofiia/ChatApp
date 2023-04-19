using Blazored.LocalStorage;
using ChatApp.Blazor.Data;
using ChatApp.Blazor.Helpers;
using ChatApp.Blazor.Services;
using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IWrappedHttpClient, WrappedHttpClient>();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ICustomLocalStorageService, CustomLocalStorageService>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITestPageService, TestPageService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
