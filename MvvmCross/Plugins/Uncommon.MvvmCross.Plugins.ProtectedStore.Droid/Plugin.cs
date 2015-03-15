using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Xciles.Uncommon.MvvmCross.Plugins.ProtectedStore.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IProtectedStore>(new ProtectedStore());
        }
    }
}
