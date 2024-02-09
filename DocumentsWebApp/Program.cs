using DocumentsAPI.Formatters;
using DocumentsAPI.Services;
using DocumentsAPI.Services.Providers;
using DocumentsAPI.UseCases;
using DocumentsAPI.UseCases.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.OutputFormatters.Add(new TextCsvOutputFormatter());
})
.AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGetAllDocuments, GetAllDocuments>();
builder.Services.AddScoped<IGetDocumentById, GetDocumentById>();
builder.Services.AddScoped<ICreateDocument, CreateDocument>();
builder.Services.AddScoped<IUpdateDocument, UpdateDocument>();
builder.Services.AddScoped<IDeleteDocument, DeleteDocument>();

builder.Services.AddScoped<InMemoryStorageProvider>();
builder.Services.AddScoped<IStorageProvider, InMemoryStorageProvider>(s => s.GetService<InMemoryStorageProvider>());
builder.Services.AddScoped<FileStorageProvider>();
builder.Services.AddScoped<IStorageProvider, FileStorageProvider>(s => s.GetService<FileStorageProvider>());
builder.Services.AddScoped<AzureStorageProvider>();
builder.Services.AddScoped<IStorageProvider, AzureStorageProvider>(s => s.GetService<AzureStorageProvider>());

builder.Services.AddScoped<IStorageProviderFactory, StorageProviderFactory>();
builder.Services.AddScoped<IDocumentService, DocumentService>(s => new DocumentService(s.GetService<IStorageProviderFactory>().GetStorageProvider()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
