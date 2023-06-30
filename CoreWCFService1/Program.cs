using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCFService1;


//var builder = WebApplication.CreateBuilder();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.AllowSynchronousIO = true;
});

//builder.Services.AddServiceModelServices();
//builder.Services.AddServiceModelMetadata();
//builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

//var app = builder.Build();

//app.UseServiceModel(serviceBuilder =>
//{
//    serviceBuilder.AddService<Service>();
//    serviceBuilder.AddServiceEndpoint<Service, IService>(new BasicHttpBinding(), "/Service.svc");
//    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
//    serviceMetadataBehavior.HttpGetEnabled = true;
//});

//app.Run();

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

// Configure an explicit none credential type for WSHttpBinding as it defaults to Windows which requires extra configuration in ASP.NET
var myWSHttpBinding = new WSHttpBinding(SecurityMode.Transport);
myWSHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

app.UseServiceModel(builder =>
{
    builder.AddService<EchoService>((serviceOptions) => { })
    // Add a BasicHttpBinding at a specific endpoint
    .AddServiceEndpoint<EchoService, IEchoService>(new BasicHttpBinding(), "/EchoService/basichttp")
    // Add a WSHttpBinding with Transport Security for TLS
    .AddServiceEndpoint<EchoService, IEchoService>(myWSHttpBinding, "/EchoService/WSHttps");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();
