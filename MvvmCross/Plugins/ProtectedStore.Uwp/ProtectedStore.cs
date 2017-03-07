using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Xciles.Uncommon.MvvmCross.Plugins.ProtectedStore.Uwp
{
    public class ProtectedStore : IProtectedStore
    {
        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a DataProtectionProvider object.
                DataProtectionProvider Provider = new DataProtectionProvider();


                var auths = store.GetFileNames(GetStringsPath(identifier));
                foreach (var path in auths)
                {
                    using (var stream = new BinaryReader(new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, store)))
                    {
                        var length = stream.ReadInt32();
                        var dataAsBytes = stream.ReadBytes(length);


                        // Decrypt the protected message specified on input.
                        IBuffer buffUnprotected = Provider.UnprotectAsync(dataAsBytes.AsBuffer()).GetResults();

                        // Execution of the SampleUnprotectData method resumes here
                        // after the awaited task (Provider.UnprotectAsync) completes
                        // Convert the unprotected message from an IBuffer object to a string.
                        String strClearText = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffUnprotected);

                        yield return strClearText;
                    }
                }
            }
        }

        public void Save(string stringToSave, string identifier)
        {
            var provider = new DataProtectionProvider(identifier);
            var messageBuffer = CryptographicBuffer.ConvertStringToBinary(stringToSave, BinaryStringEncoding.Utf8);
            var protectedBuffer = provider.ProtectAsync(messageBuffer).GetResults();

            var path = GetStringsPath(identifier);

            using (var storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, storageFile))
                {
                    stream.Write(BitConverter.GetBytes(protectedBuffer.Length), 0, sizeof(int));
                    stream.Write(protectedBuffer.ToArray(), 0, (int)protectedBuffer.Length);
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
    }
}
