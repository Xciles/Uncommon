using System.Collections.Generic;

// Initial version based on: 
// - https://github.com/escfrya/Locator/tree/master/Xamarin.Auth
// - http://stackoverflow.com/questions/28429004/correct-way-to-store-encryption-key-for-sqlcipher-database
namespace Uncommon.MvvmCross.Plugins.ProtectedStorage
{
    public interface IProtectedStore
    {
        void CreateStore();
        IEnumerable<string> GetStringsForIdentifier(string identifier);
        void Save(string stringToSave, string identifier);
        void Delete(string identifier);
    }
}