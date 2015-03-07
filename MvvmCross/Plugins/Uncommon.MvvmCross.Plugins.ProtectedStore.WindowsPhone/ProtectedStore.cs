using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using Uncommon.MvvmCross.Plugins.ProtectedStorage;

namespace Uncommon.MvvmCross.Plugins.ProtectedStore.WindowsPhone
{
    public class ProtectedStore : IProtectedStore
    {
        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var auths = store.GetFileNames(GetStringsPath(identifier));
                foreach (var path in auths)
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);

                        var unprotectedData = ProtectedData.Unprotect(dataAsBytes, null);
                        yield return Encoding.UTF8.GetString(unprotectedData, 0, unprotectedData.Length);
                    }
                }
            }
        }

        public void Save(string stringToSafe, string identifier)
        {
            var dataAsBytes = Encoding.UTF8.GetBytes(stringToSafe);
            var protectedData = ProtectedData.Protect(dataAsBytes, null);

            var path = GetStringsPath(identifier);

            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, storageFile))
                {
                    stream.WriteAsync(BitConverter.GetBytes(protectedData.Length), 0, sizeof(int)).Wait();
                    stream.WriteAsync(protectedData, 0, protectedData.Length).Wait();
                }
            }
        }

        public void Delete(string identifier)
        {
            var path = GetStringsPath(identifier);
            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                storageFile.DeleteFile(path);
            }
        }

        private static string GetStringsPath(string identifier)
        {
            return String.Format("Uncommon-{0}", identifier);
        }

        public void CreateStore()
        {
            throw new NotImplementedException();
        }
    }
}
