using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Uncommon.MvvmCross.Utils;

namespace Uncommon.MvvmCross.Services
{
    public class EncryptedObjectStorageService<T> : IEncryptedObjectStorageService<T>
    {
        private const string DataFolder = "data";
        //private const string FileName = "{0}.ucrypt";
        private const string FileName = "{0}.thisisnotthefileyouarelookingfor";

        public async Task StoreObjectsAsync(List<T> objectToStore, string password, byte[] salt)
        {
            var jsonSettings = GetJsonSerializerSettings();
            var objectAsString = JsonConvert.SerializeObject(objectToStore, jsonSettings);

            // encrypt
            var bytes = Crypto.EncryptAes(objectAsString, password, salt);
            // save
            await StorageService.StoreFileAsync(DataFolder, String.Format(FileName, typeof(T).Name), bytes).ConfigureAwait(false);
        }

        public async Task StoreObjectAsync(T objectToStore, string password, byte[] salt)
        {
            var jsonSettings = GetJsonSerializerSettings();
            var objectAsString = JsonConvert.SerializeObject(objectToStore, jsonSettings);

            // encrypt
            var bytes = Crypto.EncryptAes(objectAsString, password, salt);
            // save
            await StorageService.StoreFileAsync(DataFolder, String.Format(FileName, typeof(T).Name), bytes).ConfigureAwait(false);
        }

        public async Task<List<T>> RetrieveObjectsAsync(string password, byte[] salt)
        {
            var objectAsBytes = await StorageService.TryReadBinaryFileAsync(DataFolder, String.Format(FileName, typeof(T).Name)).ConfigureAwait(false);
            if (objectAsBytes != null)
            {
                var objectAsString = Crypto.DecryptAes(objectAsBytes, password, salt) ?? String.Empty;
                var jsonSettings = GetJsonSerializerSettings();

                var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<T>>(objectAsString, jsonSettings)).ConfigureAwait(false);
                return result;
            }
            return new List<T>();
        }

        public async Task<T> RetrieveObjectAsync(string password, byte[] salt)
        {
            var objectAsBytes = await StorageService.TryReadBinaryFileAsync(DataFolder, String.Format(FileName, typeof(T).Name)).ConfigureAwait(false);
            if (objectAsBytes != null)
            {
                var objectAsString = Crypto.DecryptAes(objectAsBytes, password, salt) ?? String.Empty;
                var jsonSettings = GetJsonSerializerSettings();

                var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(objectAsString, jsonSettings)).ConfigureAwait(false);
                return result;
            }
            return default(T);
        }

        public void DeleteFromStorage()
        {
            StorageService.DeleteFile(DataFolder, String.Format(FileName, typeof(T).Name));
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }
    }
}
