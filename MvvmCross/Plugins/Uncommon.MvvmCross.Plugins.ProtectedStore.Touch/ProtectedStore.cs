using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Security;

namespace Uncommon.MvvmCross.Plugins.ProtectedStore.Touch
{
    public class ProtectedStore : IProtectedStore
    {
        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = identifier;

            SecStatusCode result;
            var records = SecKeyChain.QueryAsRecord(query, 1000, out result);

            return records != null ? records.Select(GetStringFromRecord).ToList() : new List<string>();
        }

        public void Save(string stringToSave, string identifier)
        {
            var statusCode = SecStatusCode.Success;
            var serializedAccount = stringToSave;
            var data = NSData.FromString(serializedAccount, NSStringEncoding.UTF8);

            //
            // Remove any existing record
            //
            var existing = FindString(identifier);

            if (existing != null)
            {
                var query = new SecRecord(SecKind.GenericPassword);
                query.Service = identifier;

                statusCode = SecKeyChain.Remove(query);
                if (statusCode != SecStatusCode.Success)
                {
                    throw new Exception("Could not save account to KeyChain: " + statusCode);
                }
            }

            //
            // Add this record
            //
            var record = new SecRecord(SecKind.GenericPassword);
            record.Service = identifier;
            record.Generic = data;
            record.Accessible = SecAccessible.WhenUnlocked;

            statusCode = SecKeyChain.Add(record);

            if (statusCode != SecStatusCode.Success)
            {
                throw new Exception("Could not save account to KeyChain: " + statusCode);
            }
        }

        public void Delete(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = identifier;

            var statusCode = SecKeyChain.Remove(query);

            if (statusCode != SecStatusCode.Success)
            {
                throw new Exception("Could not delete account from KeyChain: " + statusCode);
            }
        }

        private string GetStringFromRecord(SecRecord record)
        {
            return NSString.FromData(record.Generic, NSStringEncoding.UTF8);
        }

        private string FindString(string identifier)
        {
            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = identifier;

            SecStatusCode result;
            var record = SecKeyChain.QueryAsRecord(query, out result);

            return record != null ? GetStringFromRecord(record) : null;
        }
    }
}
