using System.Threading.Tasks;

namespace Uncommon.MvvmCross.Services
{
    public interface IEncryptedObjectStorageService<T>
    {
        Task StoreAsync(T objectToStore, string password, byte[] salt);
        Task<T> RetrieveAsync(string password, byte[] salt);
    }
}