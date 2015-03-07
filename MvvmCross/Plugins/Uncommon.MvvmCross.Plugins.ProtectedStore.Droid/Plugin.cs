using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Uncommon.MvvmCross.Plugins.ProtectedStorage;

namespace Uncommon.MvvmCross.Plugins.ProtectedStore.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IProtectedStore>(new ProtectedStore());
        }
    }
}
