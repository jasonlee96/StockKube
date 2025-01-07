using DAL.Mongo.Extensions;
using StockKube.Core.Extensions;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RateActivator>();

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:MongoDB");
builder.Services.InitDBServices(connectionString ?? "");
builder.Services.InitRepositories();
builder.Services.InitCoreServices();

var host = builder.Build();
host.Run();
