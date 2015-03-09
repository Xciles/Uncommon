using System.Collections.Generic;
using System.Text;
using Android.Content;
using Java.Security;
using Javax.Crypto;

namespace Uncommon.MvvmCross.Plugins.ProtectedStore.Droid
{
    public class ProtectedStore : IProtectedStore
    {
        private Context _context;
        private KeyStore _keyStore;
        private KeyStore.PasswordProtection _passwordProtection;

        private static readonly object FileLock = new object();
        private static readonly char[] Password = "987OI65W4QW987HG3ZX1Y5E687U0YS6854SRT0687ERY984Q320WET9X4C3FG05E".ToCharArray();
        private const string FileName = "Uncommon.Store";

        public ProtectedStore()
        {
            _context = Android.App.Application.Context;

            _keyStore = KeyStore.GetInstance(KeyStore.DefaultType);

            _passwordProtection = new KeyStore.PasswordProtection(Password);

            try
            {
                lock (FileLock)
                {
                    using (var stream = _context.OpenFileInput(FileName))
                    {
                        _keyStore.Load(stream, Password);
                    }
                }
            }
            catch (Java.IO.FileNotFoundException)
            {
                _keyStore.Load(null, Password);
                //LoadEmptyKeyStore(Password);
            }
        }

        public IEnumerable<string> GetStringsForIdentifier(string identifier)
        {
            var returnList = new List<string>();

            var postfix = MakeAlias(identifier);

            var aliases = _keyStore.Aliases();
            while (aliases.HasMoreElements)
            {
                var alias = aliases.NextElement().ToString();
                if (alias.EndsWith(postfix))
                {
                    var entry = _keyStore.GetEntry(alias, _passwordProtection) as KeyStore.SecretKeyEntry;
                    if (entry != null)
                    {
                        var bytes = entry.SecretKey.GetEncoded();
                        var password = Encoding.UTF8.GetString(bytes);
                        returnList.Add(password);
                    }
                }
            }

            return returnList;
        }

        public void Delete(string identifier)
        {
            var alias = MakeAlias(identifier);

            _keyStore.DeleteEntry(alias);
            Save();
        }

        public void Save(string stringToSave, string identifier)
        {
            var alias = MakeAlias(identifier);

            var secretKey = new SecretString(stringToSave);
            var entry = new KeyStore.SecretKeyEntry(secretKey);
            _keyStore.SetEntry(alias, entry, _passwordProtection);

            Save();
        }

        private void Save()
        {
            lock (FileLock)
            {
                using (var stream = _context.OpenFileOutput(FileName, FileCreationMode.Private))
                {
                    _keyStore.Store(stream, Password);
                }
            }
        }

        private static string MakeAlias(string serviceId)
        {
            return "Uncommon-" + serviceId;
        }

        private class SecretString : Java.Lang.Object, ISecretKey
        {
            private readonly byte[] _bytes;

            public SecretString(string stringToSave)
            {
                _bytes = Encoding.UTF8.GetBytes(stringToSave);
            }

            public byte[] GetEncoded()
            {
                return _bytes;
            }

            public string Algorithm
            {
                get
                {
                    return "RAW";
                }
            }

            public string Format
            {
                get
                {
                    return "RAW";
                }
            }
        }

        //static IntPtr id_load_Ljava_io_InputStream_arrayC;

        ///// <summary>
        ///// Work around Bug https://bugzilla.xamarin.com/show_bug.cgi?id=6766
        ///// </summary>
        //void LoadEmptyKeyStore(char[] password)
        //{
        //    if (id_load_Ljava_io_InputStream_arrayC == IntPtr.Zero)
        //    {
        //        id_load_Ljava_io_InputStream_arrayC = JNIEnv.GetMethodID(_keyStore.Class.Handle, "load", "(Ljava/io/InputStream;[C)V");
        //    }
        //    IntPtr intPtr = IntPtr.Zero;
        //    IntPtr intPtr2 = JNIEnv.NewArray(password);
        //    JNIEnv.CallVoidMethod(_keyStore.Handle, id_load_Ljava_io_InputStream_arrayC, new JValue[]
        //        {
        //            new JValue (intPtr),
        //            new JValue (intPtr2)
        //        });
        //    JNIEnv.DeleteLocalRef(intPtr);
        //    if (password != null)
        //    {
        //        JNIEnv.CopyArray(intPtr2, password);
        //        JNIEnv.DeleteLocalRef(intPtr2);
        //    }
        //}
    }
}
