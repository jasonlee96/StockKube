using DAL.Mongo.Extensions;
using DAL.Mongo.Repositories;
using MongoDB.Driver;
using System.Security.Authentication;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:MongoDB");
var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));

builder.Services.InitRepositories();

var host = builder.Build();
host.Run();
