using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xciles.Uncommon.MvvmCross.Services
{
    public interface IEncryptedObjectStorageService<T>
    {
        Task StoreObjectsAsync(List<T> objectToStore, string password, byte[] salt);
        Task StoreObjectAsync(T objectToStore, string password, byte[] salt);
        Task<List<T>> RetrieveObjectsAsync(string password, byte[] salt);
        Task<T> RetrieveObjectAsync(string password, byte[] salt);
        void DeleteFromStorage();
    }
}