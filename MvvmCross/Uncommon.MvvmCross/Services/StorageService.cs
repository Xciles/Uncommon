using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.File;

namespace Xciles.Uncommon.MvvmCross.Services
{
    public static class StorageService
    {
        private static readonly IMvxFileStore FileStore;
        private static readonly IMvxFileStoreAsync FileStoreAsync;

        static StorageService()
        {
            FileStore = Mvx.Resolve<IMvxFileStore>();
            FileStoreAsync = Mvx.Resolve<IMvxFileStoreAsync>();
        }

        public static async Task<string> StoreFileAsync(string folder, string fileFullName, string fileAsString)
        {
            var fullPath = FileChecks(folder, fileFullName);

            await FileStoreAsync.WriteFileAsync(fullPath, fileAsString).ConfigureAwait(false);
            return FileStore.NativePath(fullPath);
        }
        
        public static async Task<string> StoreFileAsync(string folder, string fileFullName, byte[] fileAsBytes)
        {
            var fullPath = FileChecks(folder, fileFullName);

            await FileStoreAsync.WriteFileAsync(fullPath, fileAsBytes).ConfigureAwait(false);
            return FileStore.NativePath(fullPath);
        }

        public static async Task<string> TryReadTextFileAsync(string folder, string fileFullName)
        {
            var fullPath = FileStore.PathCombine(folder, fileFullName);
            var result = await FileStoreAsync.TryReadTextFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public static async Task<byte[]> TryReadBinaryFileAsync(string folder, string fileFullName)
        {
            var fullPath = FileStore.PathCombine(folder, fileFullName);
            var result = await FileStoreAsync.TryReadBinaryFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        public static void DeleteFile(string folder, string fileFullName)
        {
            var filestore = Mvx.Resolve<IMvxFileStore>();

            try
            {
                filestore.DeleteFile(filestore.PathCombine(folder, fileFullName));
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "StorageService.DeleteFile", ex.Message + " - " + ex.StackTrace);
            }
        }

        private static string FileChecks(string folder, string fileFullName)
        {
            try
            {
                FileStore.EnsureFolderExists(folder);
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "StorageService.FileChecks", ex.Message + " - " + ex.StackTrace);
            }

            var fullPath = FileStore.PathCombine(folder, fileFullName);

            if (FileStore.Exists(fullPath))
            {
                DeleteFile(folder, fileFullName);
            }
            return fullPath;
        }
    }
}
