namespace DocumentsAPI.Services.Providers
{
    public class StorageProviderFactory : IStorageProviderFactory
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;

        public StorageProviderFactory(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
        }

        public IStorageProvider GetStorageProvider()
        {
            var providerType = configuration.GetValue<string>("StorageProvider");
            switch (providerType)
            {
                case "Azure":
                    return (IStorageProvider)serviceProvider.GetService(typeof(AzureStorageProvider));
                case "File":
                    return (IStorageProvider)serviceProvider.GetService(typeof(FileStorageProvider));
                case "InMemory":
                    return (IStorageProvider)serviceProvider.GetService(typeof(InMemoryStorageProvider));
                default:
                    throw new ArgumentOutOfRangeException(nameof(providerType), providerType, $"ProviderType {providerType} is not supported.");
            }
        }
    }
}
