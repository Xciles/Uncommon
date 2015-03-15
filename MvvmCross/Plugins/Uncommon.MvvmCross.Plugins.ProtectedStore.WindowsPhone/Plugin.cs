using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Xciles.Uncommon.MvvmCross.Plugins.ProtectedStore.WindowsPhone
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IProtectedStore>(new ProtectedStore());
        }
    }
}
