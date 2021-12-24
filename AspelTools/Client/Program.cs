using AspelTools.Client;
using AspelTools.Client.Pages.Traspasos;
using AspelTools.Client.Servicios;
using AspelTools.Client.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlBase = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services.AddHttpClient<IServicioAlmacenes, ServicioAlmacenes>(op => op.BaseAddress = urlBase);
builder.Services.AddTransient<IValidator<TraspasoPorLineaModelo>, TraspasoPorLineaValidator>();


await builder.Build().RunAsync();
