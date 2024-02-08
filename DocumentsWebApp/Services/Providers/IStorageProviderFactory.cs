namespace DocumentsAPI.Services.Providers
{
    public interface IStorageProviderFactory
    {
        IStorageProvider GetStorageProvider();
    }
}
